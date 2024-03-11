using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityRewardedAds : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
  //  [SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "5573033";
    [SerializeField] string _iOSAdUnitId = "5562331";

    public PlayerController _playerController;


    public bool testMode = true;
    public Text t_puntos;
    private bool is_init = false;
    private bool is_load = false;
    string _adUnitId = null; // This will remain null for unsupported platforms
    private const string VIDEO_PLACEMENT = "anuncio1";

    private void Start()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif


    }
    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

    }
    public void Initialize()
    {
        if (!is_init)
        {
            if (Advertisement.isSupported)
            {
                Debug.Log(Application.platform + " supported by Advertisement");
            }
            else
            {
                Debug.Log("No está soportado el sistema UnityAds.");
            }
            Debug.Log("Inicializando... Espere, por favor.");
            Advertisement.Initialize(_adUnitId, testMode, this);
        }
        else
        {
            Debug.Log("Sistema inicializado correctamente");
        }
    }


    void OnDestroy()
    {
    }

// Call this public method when you want to get an ad ready to show.
public void LoadAd()
    {
        Debug.Log("A?");
        if (is_init)
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(VIDEO_PLACEMENT, this);
        }
        else
        {
            Debug.Log("Inicializa antes de cargar");
        }
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(VIDEO_PLACEMENT))
        {
           
            is_load = true;
            ShowAd();
        }
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Then show the ad:
        Advertisement.Show(VIDEO_PLACEMENT, this);
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(VIDEO_PLACEMENT) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            _playerController.vidas++;
            _playerController.setVidas();

            //Seteamos la muerte
            _playerController.anim.SetBool("dead", false);
            _playerController.vulnerable = true;
        }
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log("ERROR: No se puede cargar el anuncio.");
        is_load = false;
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {

        Debug.Log("Mostrando anuncio...");
    }
    public void OnUnityAdsShowClick(string adUnitId) { }



    void IUnityAdsInitializationListener.OnInitializationComplete()
    {
        Debug.Log("Init Success");
        is_init = true;


        LoadAd();
    }

    void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Init Failed: [{error}]: {message}");
        Debug.Log("ERROR: No se puede inicializar.");
        is_init = false;
    }
}
