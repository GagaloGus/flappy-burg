using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    public string androidAdID = "5527597";
    public string adUnityIdAndroid = "Interstitial_Android";

    public bool testMode = true;

    //public static AdManager instance;

    /*private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Advertisement.Initialize(myGameIdAndroid, testMode, this);
            myAdUnityId = adUnityIdAndroid;
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

    public void HandleAd(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Finished:
            case ShowResult.Skipped:
            case ShowResult.Failed:
                
                break;
        }
    }


    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        Advertisement.Initialize(androidAdID, testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }


    public void ShowAd()
    {
        if (Advertisement.isInitialized)
        {
            Debug.Log("Showing Ad: " + adUnityIdAndroid);
            Advertisement.Show(adUnityIdAndroid, this);

            LoadAd();
        }

    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + adUnityIdAndroid);
        Advertisement.Load(adUnityIdAndroid, this);
    }


    public void OnUnityAdsAdLoaded(string adUnitId) { }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    //Cuando se acabe o skip el anuncio se carga la escena de nuevo
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        GameManager.instance.gamePaused = false;
        GameManager.instance.ChangeScene();
    }
}
