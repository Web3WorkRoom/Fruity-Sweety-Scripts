using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using System;

public class GoogleMobileAdsDemoScript : MonoBehaviour
{

    //ANDROID
    //private string _adUnitId = "ca-app-pub-9091945858565383/3247978231";
    //IPHONE
    //private string _adUnitId = "ca-app-pub-9091945858565383/3247978231
    //
#if UNITY_ANDROID
    private string _adUnitIdIntersitial = "ca-app-pub-9091945858565383/3247978231";
  private string _adUnitIdRewarded = "ca-app-pub-9091945858565383/6301282874";
#elif UNITY_IPHONE
    private string _adUnitIdIntersitial = "ca-app-pub-9091945858565383/7999214218";
    private string _adUnitIdRewarded = "ca-app-pub-9091945858565383/8597980015";
#else
    private string _adUnitIdIntersitial = "unused";
  private string _adUnitIdRewarded = "unused";
#endif


    private InterstitialAd interstitialAd;
    //private InterstitialAd interstitialAd2;

    private RewardedAd rewardedAd;

    [SerializeField]
    private PlayFabManager PFM;
    //private RewardedAd RAd;

    [SerializeField]
    private bool AdShown;

    public GameObject AndroidPic;
    public GameObject IOSPic;
    public GameObject MixedPic;


    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
            //LoadInterstitialAd();
            print("Initialized");
        });

        if (_adUnitIdIntersitial == "ca-app-pub-9091945858565383/3247978231"
            && _adUnitIdRewarded == "ca-app-pub-9091945858565383/6301282874")
        {
            print("Android ADS");
            AndroidPic.SetActive(true);
        }
        else if (_adUnitIdIntersitial == "ca-app-pub-9091945858565383/7999214218"
            && _adUnitIdRewarded == "ca-app-pub-9091945858565383/8597980015")
        {
            print("IOS ADS");
            IOSPic.SetActive(true);
        }
        else
        {
            print("Is Mixed ADS??");
            MixedPic.SetActive(true);
        }

    }

    public void DisableBoxes()
    {
        MixedPic.SetActive(false);
        IOSPic.SetActive(false);
        AndroidPic.SetActive(false);
    }

    public void prepAdd()
    {
        //MobileAds.Initialize(initstatus => { });

        LoadInterstitialAd();
    }

    public void ShowAdd()
    {
        print("Show Ad");
        try
        {
            if (interstitialAd.CanShowAd())
                interstitialAd.Show();
            else
                print("Can't show Ad");
        }
        catch
        {
            print("Failed showing Ad");
        }
    }

    /// <summary>
    /// Loads the interstitial ad.
    /// </summary>
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder()
                .AddKeyword("unity-admob-sample")
                .Build();

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitIdIntersitial, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
            });

    }

    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder()
            .Build();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitIdRewarded, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
            });
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                print("Is rewarded!");
                AdShown = true;
                PFM.RewardedAdFinished();
                PFM.EnableCongratsAd();
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }

    public bool WasRewardedShown()
    {
        return AdShown;
    }

    public void DisRewardedShow()
    {
        AdShown = false;
    }

}
