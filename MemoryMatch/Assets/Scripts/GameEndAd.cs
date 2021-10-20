using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class GameEndAd : MonoBehaviour, IInterstitialAdListener
{
    // Start is called before the first frame update
    void Start()
    {
        Appodeal.setInterstitialCallbacks(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Interstitial callback handlers 
public void onInterstitialLoaded(bool isPrecache) { print("Interstitial loaded"); } // Called when interstitial was loaded (precache flag shows if the loaded ad is precache) 
public void onInterstitialFailedToLoad() { print("Interstitial failed"); } // Called when interstitial failed to load 
public void onInterstitialShowFailed() { print ("Interstitial show failed"); } // Called when interstitial was loaded, but cannot be shown (internal network errors, placement settings, or incorrect creative) 
public void onInterstitialShown() { print("Interstitial opened"); } // Called when interstitial is shown 
public void onInterstitialClosed() { print("Interstitial closed"); } // Called when interstitial is closed 
public void onInterstitialClicked() { print("Interstitial clicked"); } // Called when interstitial is clicked 
public void onInterstitialExpired() { print("Interstitial expired"); } // Called when interstitial is expired and can not be shown 

#endregion


public void showGameEndAd(){

    if(SceneManager.GetActiveScene().name == "SinglePlay"){
        if(PlayerPrefs.GetInt("BoardSize", 16) == 16){
            if(PlayerPrefs.GetInt("ShowAd", 0) == 1){
                if(Appodeal.isLoaded(Appodeal.INTERSTITIAL)){
                    Appodeal.show(Appodeal.INTERSTITIAL);
                    PlayerPrefs.SetInt("ShowAd",0);
                }
            }else{
                PlayerPrefs.SetInt("ShowAd", 1);
            }
        }else{
            if(Appodeal.isLoaded(Appodeal.INTERSTITIAL)){
                Appodeal.show(Appodeal.INTERSTITIAL);
            }
        }
    }else{
        if(Appodeal.isLoaded(Appodeal.INTERSTITIAL)){
            Appodeal.show(Appodeal.INTERSTITIAL);
        }
    }
}
}
