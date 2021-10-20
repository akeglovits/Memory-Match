using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using AppodealAppTracking.Unity.Api;
using AppodealAppTracking.Unity.Common;
using ConsentManager.Api;
using ConsentManager.Common;
using Facebook.Unity;

public class HomeLoading : MonoBehaviour, IConsentFormListener, IConsentInfoUpdateListener, IAppodealAppTrackingTransparencyListener{
    // Start is called before the first frame update


    private string appkey;

    private Consent appodealConsent;

    public LoadingCircle circle;
    void Start()
    {

        PlayerPrefs.SetInt("numbers", 1);
        PlayerPrefs.SetInt("letters", 1);
        PlayerPrefs.SetInt("shapes", 1);
        PlayerPrefs.SetInt("basics", 1);

        StartCoroutine(circle.circleSpin());

        #if UNITY_IOS
            appkey = "506060146d5eda62b69f558bb036a19ed76c354f985672b5";
        #elif UNITY_ANDROID
            appkey = "4ec73ccc4a769679d14c85bf742ee462db8b8bf1e506ad23";
        #else
            appkey = "unrecognized_platform";
        #endif

        if (!FB.IsInitialized) {
            FB.Init(initCallback, onHideUnity);
        } else {
            // Already initialized
            FB.ActivateApp();
        }

        ConsentManager.Api.ConsentManager consentManager = ConsentManager.Api.ConsentManager.getInstance();

        consentManager.requestConsentInfoUpdate(appkey, this);

        // Get current ShouldShow status
        Consent.ShouldShow consentShouldShow = consentManager.shouldShowConsentDialog();

        if (consentShouldShow == Consent.ShouldShow.TRUE){

            StartCoroutine(showConsentForm());

        }else{

            appodealConsent = consentManager.getConsent();

            AppodealAppTrackingTransparency.RequestTrackingAuthorization(this);

            Appodeal.initialize(appkey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, consentManager.getConsent());
            StartCoroutine(loadHome());
        

        }
    }


    private void initCallback (){
        if (FB.IsInitialized) {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        } else {
            Debug.Log("Something went wrong to Initialize the Facebook SDK");
        }
    }


    void OnApplicationPause (bool pauseStatus){
    // Check the pauseStatus to see if we are in the foreground
    // or background
    if (!pauseStatus) {
        //app resume
        if (FB.IsInitialized) {
            FB.ActivateApp();
        } else {
            //Handle FB.Init
            FB.Init( () => {
            FB.ActivateApp();
        });
        }
    }
    }

    private void onHideUnity(bool isGameShown){
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
        Time.timeScale = 1;
        }
    }


    public IEnumerator showConsentForm(){

    ConsentForm consentForm = new ConsentForm.Builder().withListener(this).build();
    consentForm?.load();

    while(!consentForm.isLoaded()){
        yield return new WaitForSeconds(.01f);
    }

        consentForm.showAsDialog();
    }

    #region ConsentInfoUpdateListener

public void onConsentInfoUpdated(Consent consent) { 
    print("onConsentInfoUpdated");
    appodealConsent = consent;
    }

public void onFailedToUpdateConsentInfo(ConsentManagerException error) { print($"onFailedToUpdateConsentInfo Reason: {error.getReason()}");}

#endregion

    #region ConsentFormListener

public void onConsentFormLoaded() { print("ConsentFormListener - onConsentFormLoaded");}

public void onConsentFormError(ConsentManagerException exception) { print($"ConsentFormListener - onConsentFormError, reason - {exception.getReason()}");}

public void onConsentFormOpened() { print("ConsentFormListener - onConsentFormOpened");}

public void onConsentFormClosed(Consent consent) { 

    appodealConsent = consent;

        AppodealAppTrackingTransparency.RequestTrackingAuthorization(this);

        Appodeal.initialize(appkey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, consent);
        StartCoroutine(loadHome());
        
        
    }

#endregion


public void AppodealAppTrackingTransparencyListenerNotDetermined(){ 
    Appodeal.initialize(appkey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, appodealConsent);
    StartCoroutine(loadHome());

    }
public void AppodealAppTrackingTransparencyListenerRestricted(){ 
   Appodeal.initialize(appkey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, appodealConsent);
   StartCoroutine(loadHome());

    }
public void AppodealAppTrackingTransparencyListenerDenied() { 
    Appodeal.initialize(appkey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, appodealConsent);
    StartCoroutine(loadHome());

}
public void AppodealAppTrackingTransparencyListenerAuthorized() { 
    Appodeal.initialize(appkey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, appodealConsent);
    StartCoroutine(loadHome());

}

 private IEnumerator loadHome(){


        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Home");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
           yield return null;
        }
    }

}
