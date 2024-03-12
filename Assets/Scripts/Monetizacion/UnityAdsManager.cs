using System;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnityAdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private string GAME_ID = "5573033"; //replace with your gameID from dashboard. note: will be different for each platform.
    private const string VIDEO_PLACEMENT = "anuncio2";
    public bool testMode = true;
    private bool is_init = false;
    private bool is_load = false;


    //utility wrappers for debuglog
    public delegate void DebugEvent(string msg);
    public static event DebugEvent OnDebugLog;
    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        if (!is_init)
        {
            if (Advertisement.isSupported)
            {

                DebugLog(Application.platform + " supported by Advertisement");
            }
            else
            {
                Debug.Log("No está soportado el sistema UnityAds.");
            }
            Debug.Log("Inicializando... Espere, por favor.");
            Advertisement.Initialize(GAME_ID, testMode, this);
        }
        else{
            Debug.Log("Sistema inicializado correctamente");
        }
    }

    public void LoadNonRewardedAd()
    {
        if (is_init) {
            Advertisement.Load(VIDEO_PLACEMENT, this);
        } else {
            Debug.Log("Inicializa antes de cargar");
        }
    }

    public void ShowNonRewardedAd()
    {
        if (is_load) {
            Advertisement.Show(VIDEO_PLACEMENT, this);
        }
        else {
            Debug.Log("Carga antes de mostrar.");
        }
    }

    #region Interface Implementations
    public void OnInitializationComplete()
    {
        DebugLog("Init Success");
        is_init = true;

        LoadNonRewardedAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        DebugLog($"Init Failed: [{error}]: {message}");
        is_init = false;
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        DebugLog($"Load Success: {placementId}");
        is_load = true;

        ShowNonRewardedAd();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        DebugLog($"Load Failed: [{error}:{placementId}] {message}");
        is_load = false;
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        DebugLog($"OnUnityAdsShowFailure: [{error}]: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        DebugLog($"OnUnityAdsShowStart: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        DebugLog($"OnUnityAdsShowClick: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
       
        is_load = false;
        DebugLog($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");
    }
    #endregion

     //wrapper around debug.log to allow broadcasting log strings to the UI
    void DebugLog(string msg)
    {
        OnDebugLog?.Invoke(msg);
        Debug.Log(msg);
    }
}