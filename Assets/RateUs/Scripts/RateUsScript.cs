using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RateUsScript : MonoBehaviour 
{

	int numBeforeAsking = 0; //How many times the scene have to be loaded before the player is asked to rate the game
	public string urlToRate = ""; //Url where the player will be redirected to after clicking the Rate Now button
	public int numBetweenAsking = 0; //How many times the scene have to be loaded before the player is asked to rate the game after he click the button Ask Later.

	void Start () 
	{
		//PlayerPrefs.DeleteAll();
		this.gameObject.SetActive(false);
		getRateUsState();
	}

	public static void SetPref (string prefName, int prefValue) 
	{
		PlayerPrefs.SetInt(prefName, prefValue);
	}

	public static int GetPref (string prefName)
	{
		return PlayerPrefs.GetInt(prefName);
	}

	public static int GetPref (string name, int defaultValue)
	{
	    if(PlayerPrefs.HasKey(name)) 
		{
	        return GetPref(name);
	    }
 
	    return defaultValue;
	}

	public bool isRateUsExists
	{
		get {return PlayerPrefs.HasKey("rateUs");}
	}

	public bool isRateUsState
	{
		get{return PlayerPrefs.HasKey("isRateUsState");}
	}

	void getRateUsState ()
	{
		if(!isRateUsState)
		{
			if(!isRateUsExists)
			{
				numBeforeAsking = numBetweenAsking;
			}
			else
			{
				numBeforeAsking = GetPref("rateUs");
			}
			numBeforeAsking = numBeforeAsking - 1;
			if(numBeforeAsking <= 0)
			{
				this.gameObject.SetActive(true);
			}
			SetPref("rateUs", numBeforeAsking);
		}
	}

	public void RateUs ()
	{
		SetPref("isRateUsState", 0);
		this.gameObject.SetActive(false);
		Application.OpenURL(urlToRate);
	}

	public void AskLater ()
	{
		numBeforeAsking = numBetweenAsking;
		SetPref("rateUs", numBeforeAsking);
		this.gameObject.SetActive(false);
	}

	public void DontAsk ()
	{
		SetPref("isRateUsState", 0);
		this.gameObject.SetActive(false);
	}
}
