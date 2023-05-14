using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System;
using UnityEngine.Events;
//using UnityEngine.iOS;

public class PlayFabManager : MonoBehaviour
{

    public static UnityEvent OnSignInSuccess = new UnityEvent();
    public static UnityEvent<string> OnSignInFailed = new UnityEvent<string>();
    public static PlayFabManager instance;
    //public TMP_Text coinsValueText;
    public static int coins;
    //public TMP_Text txt2;
    [SerializeField]
    private MainMenuMaster mmm;
    [SerializeField]
    private RankingScript rs;

    [SerializeField]
    private string rankText;
    [SerializeField]
    private string scoreText;
    [SerializeField]
    private int scoreInt;
    [SerializeField]
    private int rankPos;


    public int[] scores = new int[50];
    public string[] playFabids = new string[50];
    public string[] walletsFull = new string[50];
    [SerializeField]
    private int incw;

    [SerializeField]
    private int myHighScore;

    [SerializeField]
    private GameChecker gc;

    [SerializeField]
    //private bool flag;

    //public TMP_Text txt3;

    private bool alreadyLoged;

    private bool logedWithWallet;

    private int score2;

    [SerializeField]
    private int numberOfDies;
    [SerializeField]
    private GoogleMobileAdsDemoScript adsScript;
    [SerializeField]
    private bool canShowAd;

    [SerializeField]
    private int numberOfGames;

    [SerializeField]
    private bool canShowRewarded;

    [SerializeField]
    private player playerScript;

    private int prevPoints;

    private string playerplayfabid;
    private string playerSkins;
    private string[] skinsIds = new string[1];
    private string playerAddy;

    public string[] playFabids3 = new string[25];
    public string Adresses3;
    public string walletsWon;



    private void Awake()
    {
        playFabids3 = walletsWon.Split(',');
        prevPoints = 0;
        logedWithWallet = false;
        alreadyLoged = false;
        //flag = false;
        instance = this;
        try
        {
            mmm = FindObjectOfType<MainMenuMaster>();
            rs = mmm.rs;
        }
        catch
        {
            print("No ranking script");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Login();
        //SignInWithDevice();
    }

    public void Login(string uniqueID)
    {
        if (logedWithWallet) return;
        if (alreadyLoged)
            PlayFabClientAPI.ForgetAllCredentials();
        playerAddy = uniqueID;
        var request = new LoginWithCustomIDRequest
        {
            CustomId = uniqueID,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

#if UNITY_ANDROID

    public void SignInWithDevice()
    {

        GetDeviceID(out string android_id, out string custom_id);
        alreadyLoged = true;

        if (!string.IsNullOrEmpty(android_id))
        {
            Debug.Log("Using Android Device ID: " + android_id);
            //txt3.text = "Using Android Device ID: " + android_id;

            PlayFabClientAPI.LoginWithAndroidDeviceID(new LoginWithAndroidDeviceIDRequest()
            {
                AndroidDeviceId = android_id,
                OS = SystemInfo.operatingSystem,
                AndroidDevice = SystemInfo.deviceModel,
                TitleId = PlayFabSettings.TitleId,
                CreateAccount = true
            }, response =>
            {
                print("Successful login with Android ID");
                //OnSignInSuccess.Invoke();
            }, error =>
            {
                print("Unsiccessful login with Android ID");
                //OnSignInFailed.Invoke(error.ErrorMessage);
            });

        }
        else if (!string.IsNullOrEmpty(custom_id))
        {
            Debug.Log("Using Custom ID");
            //txt3.text = "Using Custom Device ID: " + custom_id;


            PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
            {
                CustomId = custom_id,
                TitleId = PlayFabSettings.TitleId,
                CreateAccount = true
            }, response =>
            {
                print("Successful login with Custom ID");
                GetAppStatus();
                //OnSignInSuccess.Invoke();
            }, error =>
            {
                print("Unsiccessful login with Custom ID");
                //OnSignInFailed.Invoke(error.ErrorMessage);
            });
        }
    }
    //Android
    void GetDeviceID(out string android_id, out string custom_id)
    {
        android_id = string.Empty;
        custom_id = string.Empty;


        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass clsUnity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject objActivity = clsUnity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject objResolver = objActivity.Call<AndroidJavaObject>("getContentResolver");
            AndroidJavaClass clsSecure = new AndroidJavaClass("android.provider.Settings$Secure");
            android_id = clsSecure.CallStatic<string>("getString", objResolver, "android_id");
        }
        else
        {
            custom_id = SystemInfo.deviceUniqueIdentifier;
        }

    }
#elif UNITY_IPHONE

    public void SignInWithDevice()
    {
        //if (alreadyLoged)
        //    Application.Quit();
        GetDeviceID(out string android_id, out string ios_id, out string custom_id);
        //GetDeviceID(out string android_id, out string custom_id);
        alreadyLoged = true;
        //PlayFabClientAPI.LoginWithIOSDeviceID(new LoginWithIOSDeviceIDRequest()
        //{
        //    DeviceId = ios_id,
        //    TitleId = PlayFabSettings.TitleId,
        //    CreateAccount = true
        //}, response =>
        //{
        //    print("Successful login with IOS ID");
        //    //OnSignInSuccess.Invoke();
        //}, error =>
        //{
        //    print("Unsiccessful login with IOS ID");
        //        //OnSignInFailed.Invoke(error.ErrorMessage);
        //});

        if (!string.IsNullOrEmpty(android_id))
        {
            Debug.Log("Using Android Device ID: " + android_id);
            //txt3.text = "Using Android Device ID: " + android_id;

            PlayFabClientAPI.LoginWithAndroidDeviceID(new LoginWithAndroidDeviceIDRequest()
            {
                AndroidDeviceId = android_id,
                OS = SystemInfo.operatingSystem,
                AndroidDevice = SystemInfo.deviceModel,
                TitleId = PlayFabSettings.TitleId,
                CreateAccount = true
            }, response =>
            {
                print("Successful login with Android ID");
                //OnSignInSuccess.Invoke();
            }, error =>
            {
                print("Unsiccessful login with Android ID");
                //OnSignInFailed.Invoke(error.ErrorMessage);
            });

        }
        else if (!string.IsNullOrEmpty(ios_id))
        {
            Debug.Log("Using IOS Device ID");
            //txt3.text = "Using IOS Device ID: " + ios_id;


            PlayFabClientAPI.LoginWithIOSDeviceID(new LoginWithIOSDeviceIDRequest()
            {
                DeviceId = ios_id,
                TitleId = PlayFabSettings.TitleId,
                CreateAccount = true
            }, response =>
            {
                print("Successful login with IOS ID");
                GetAppStatus();
                //OnSignInSuccess.Invoke();
            }, error =>
            {
                print("Unsiccessful login with IOS ID");
                //OnSignInFailed.Invoke(error.ErrorMessage);
            });
        }
        else if (!string.IsNullOrEmpty(custom_id))
        {
            Debug.Log("Using Custom ID");
            //txt3.text = "Using Custom Device ID: " + custom_id;


            PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
            {
                CustomId = custom_id,
                TitleId = PlayFabSettings.TitleId,
                CreateAccount = true
            }, response =>
            {
                print("Successful login with Custom ID");
                GetAppStatus();
                //OnSignInSuccess.Invoke();
            }, error =>
            {
                print("Unsiccessful login with Custom ID");
                //OnSignInFailed.Invoke(error.ErrorMessage);
            });
        }
    }

    // IOS
    void GetDeviceID(out string android_id, out string ios_id, out string custom_id)
    {
        android_id = string.Empty;
        ios_id = string.Empty;
        custom_id = string.Empty;

        //ios_id = UnityEngine.iOS.Device.vendorIdentifier;
        //ios_id = UnityEngine.iOS.Device.vendorIdentifier;


        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass clsUnity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject objActivity = clsUnity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject objResolver = objActivity.Call<AndroidJavaObject>("getContentResolver");
            AndroidJavaClass clsSecure = new AndroidJavaClass("android.provider.Settings$Secure");
            android_id = clsSecure.CallStatic<string>("getString", objResolver, "android_id");
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            ios_id = UnityEngine.iOS.Device.vendorIdentifier;
            //ios_id = Device.vendorIdentifier.ToString();
        }
        else
        {
            custom_id = SystemInfo.deviceUniqueIdentifier;
        }

    }
#else
    public void SignInWithDevice()
    {

        GetDeviceID(out string android_id, out string custom_id);
        alreadyLoged = true;

        if (!string.IsNullOrEmpty(android_id))
        {
            Debug.Log("Using Android Device ID: " + android_id);
            //txt3.text = "Using Android Device ID: " + android_id;

            PlayFabClientAPI.LoginWithAndroidDeviceID(new LoginWithAndroidDeviceIDRequest()
            {
                AndroidDeviceId = android_id,
                OS = SystemInfo.operatingSystem,
                AndroidDevice = SystemInfo.deviceModel,
                TitleId = PlayFabSettings.TitleId,
                CreateAccount = true
            }, response =>
            {
                print("Successful login with Android ID");
                //OnSignInSuccess.Invoke();
            }, error =>
            {
                print("Unsiccessful login with Android ID");
                //OnSignInFailed.Invoke(error.ErrorMessage);
            });

        }
        else if (!string.IsNullOrEmpty(custom_id))
        {
            Debug.Log("Using Custom ID");
            //txt3.text = "Using Custom Device ID: " + custom_id;


            PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
            {
                CustomId = custom_id,
                TitleId = PlayFabSettings.TitleId,
                CreateAccount = true
            }, response =>
            {
                print("Successful login with Custom ID");
                GetAppStatus();
                //OnSignInSuccess.Invoke();
            }, error =>
            {
                print("Unsiccessful login with Custom ID");
                //OnSignInFailed.Invoke(error.ErrorMessage);
            });
        }
    }

    //Android
    void GetDeviceID(out string android_id, out string custom_id)
    {
        android_id = string.Empty;
        //ios_id = string.Empty;
        custom_id = string.Empty;

        //ios_id = UnityEngine.iOS.Device.vendorIdentifier;
        //ios_id = UnityEngine.iOS.Device.vendorIdentifier;


        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass clsUnity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject objActivity = clsUnity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject objResolver = objActivity.Call<AndroidJavaObject>("getContentResolver");
            AndroidJavaClass clsSecure = new AndroidJavaClass("android.provider.Settings$Secure");
            android_id = clsSecure.CallStatic<string>("getString", objResolver, "android_id");
        }
        //else if (Application.platform == RuntimePlatform.IPhonePlayer)
        //{
        //    ios_id = UnityEngine.iOS.Device.vendorIdentifier;
        //}
        else
        {
            custom_id = SystemInfo.deviceUniqueIdentifier;
        }

    }
#endif

    //Android
    //void GetDeviceID(out string android_id, out string custom_id)
    //{
    //    android_id = string.Empty;
    //    //ios_id = string.Empty;
    //    custom_id = string.Empty;

    //    //ios_id = UnityEngine.iOS.Device.vendorIdentifier;
    //    //ios_id = UnityEngine.iOS.Device.vendorIdentifier;


    //    if (Application.platform == RuntimePlatform.Android)
    //    {
    //        AndroidJavaClass clsUnity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    //        AndroidJavaObject objActivity = clsUnity.GetStatic<AndroidJavaObject>("currentActivity");
    //        AndroidJavaObject objResolver = objActivity.Call<AndroidJavaObject>("getContentResolver");
    //        AndroidJavaClass clsSecure = new AndroidJavaClass("android.provider.Settings$Secure");
    //        android_id = clsSecure.CallStatic<string>("getString", objResolver, "android_id");
    //    }
    //    //else if (Application.platform == RuntimePlatform.IPhonePlayer)
    //    //{
    //    //    ios_id = UnityEngine.iOS.Device.vendorIdentifier;
    //    //}
    //    else
    //    {
    //        custom_id = SystemInfo.deviceUniqueIdentifier;
    //    }

    //}

    void OnSuccess(LoginResult result)
    {
        logedWithWallet = true;
        playerplayfabid = result.PlayFabId;
        print("Loged in");
        //txt3.text = "";
        SaveWallet();
        GetSkins();
        //GetOwnScore();
        //GetLeader();
        //GetVirtualCurrencies();
    }

    void OnError(PlayFabError error)
    {
        print(error.GenerateErrorReport());
    }

    void OnErrorScore(PlayFabError error)
    {
        print(error.GenerateErrorReport());
        gc.textUpdate.text = "Couldn't Update Score!";
        gc.textUpdate.color = Color.red;

    }

    public void SaveWallet()
    {
        string walletAddy = PlayerPrefs.GetString("Account");
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Address", walletAddy }
            },
            Permission = UserDataPermission.Public
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);

    }


    public void Pa()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"MultiAccount", "True" }
            },
            Permission = UserDataPermission.Public
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend3, OnError3);

    }

    void OnError3(PlayFabError error)
    {
        print(error.GenerateErrorReport());
        Application.Quit();
    }

    private void OnDataSend3(UpdateUserDataResult result)
    {
        print("Succesful user data send 2!");
        WalletLogin wls = FindObjectOfType<WalletLogin>();

        if (wls == null)
        {
            PlayerPrefs.SetString("Account", "");
            Application.Quit();
        }

        wls.connected.SetActive(false);
        wls.notconnected.SetActive(true);
        PlayerPrefs.SetString("Account", "");
        wls.disObjOut();

        string addy = PlayerPrefs.GetString("Account", "");
        if (addy == "")
            Application.Quit();
        else
        {
            PlayerPrefs.SetString("Account", "");
            Application.Quit();

        }
    }

    private void SaveCheatingVar()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Cheating", "True" }
            },
            Permission = UserDataPermission.Public
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend2, OnError);

    }

    private void OnDataSend2(UpdateUserDataResult result)
    {
        print("Succesful user data send 2!");
    }

    void OnDataSend(UpdateUserDataResult result)
    {
        print("Succesful user data send!");
        GetOwnScore();
    }

    public void SendLeaderboard(int score, GameChecker gc1)
    {
        if (PlayerPrefs.GetString("Account") == "") return;
        //print("PP: " + PlayerPrefs.GetString("Account").Substring(0, 2));
        if (PlayerPrefs.GetString("Account").Substring(0, 2) != "0x") return;
        if (score <= myHighScore)
        {
            //something

            return;
        }
        gc = gc1;
        gc.MadeHighScore();
        if (!gc.isValid())
        {
            gc.textUpdate.text = "Cheating Detected!";
            SaveCheatingVar();
            return;
        }
        gc.textUpdate.text = "Uploading Score...";
        score2 = score;
        //myHighScore = score;
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "FruityLeader",
                    Value = score

                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLs, OnErrorScore);
    }

    private void OnLs(UpdatePlayerStatisticsResult result)
    {
        print("Seccessfull");
        myHighScore = score2;
        PlayerPrefs.SetString("HS", myHighScore.ToString());
        gc.textUpdate.text = "Score Uploaded!";
        //try
        //{
        //    player pscript = FindObjectOfType<player>();
        //    // show success
        //}
        //catch
        //{
        //    //error
        //}
    }

    public void GetLeader()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "FruityLeader",
            StartPosition = 0,
            MaxResultsCount = 50
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLGet, OnError);
    }

    void OnLGet(GetLeaderboardResult result)
    {
        print("Time left: " + result.NextReset);
        System.TimeSpan? theTime = result.NextReset - DateTime.UtcNow;
        //string tl = (result.NextReset - DateTime.UtcNow).ToString();
        string tl = theTime.Value.Days.ToString() + " D " + theTime.Value.Hours.ToString() + " H " + theTime.Value.Minutes.ToString() + " M";
        print("this: " + (result.NextReset - DateTime.UtcNow));
        if (rs == null)
        {
            mmm = FindObjectOfType<MainMenuMaster>();
            rs = mmm.rs;
        }
        rs.timeLeft.text = tl;
        foreach (var item in result.Leaderboard)
        {
            if (item.Position < 3)
            {
                print(item.Position + " " + item.PlayFabId + " " + item.StatValue);
                rs.scp[item.Position].scoreText.text = item.StatValue.ToString();
                playFabids[item.Position] = item.PlayFabId;
            }
            else
            {
                rs.NormalScripts[item.Position - 3].scoreText.text = item.StatValue.ToString();
                playFabids[item.Position] = item.PlayFabId;

            }
        }
        StartGetUserData();
    }

    public void GetOwnScore()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "FruityLeader",
            MaxResultsCount = 1
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, MyL, OnError);
    }

    void MyL(GetLeaderboardAroundPlayerResult result)
    {

        int checkingI = PlayerPrefs.GetInt("PA", 0);
        if (checkingI == 1)
        {
            SendPA();
        }

        if (rs == null)
        {
            mmm = FindObjectOfType<MainMenuMaster>();
            rs = mmm.rs;
        }
        foreach (var item in result.Leaderboard)
        {
            //print(item.Position + " " + item.PlayFabId + " " + item.StatValue);
            myHighScore = item.StatValue;
            PlayerPrefs.SetString("HS", myHighScore.ToString());
            rs.myScore.scoreText.text = myHighScore.ToString();
            if (myHighScore == 0)
                rs.myScore.rankNumber.text = "";
            else
                rs.myScore.rankNumber.text = "#" + (item.Position + 1).ToString();
        }
    }

    private void SendPA()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"MultiAccount", "True" }
            },
            Permission = UserDataPermission.Public
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend2, OnError);

    }

    void StartGetUserData()
    {
        incw = 0;

        StartCoroutine(CallPlayFabApi2());
        //for (int i = 0; i < playFabids.Length; i++)
        //{
        //    if (playFabids[i] != "")
        //    {
        //        flag = false;
        //        StartCoroutine(CallPlayFabApi(playFabids[i]));
        //        //GetUserData(playFabids[i]);
        //    }
        //}
        print("finished");
        //incw = 0;
        //walletsFull = new string[50];
    }

    void GetUserData(string myPlayFabId)
    {
        //print("rank pos: " + rankPos);
        //print("PlayFabId: " + myPlayFabId);
        //print("incw: " + incw);

        var request = new GetUserDataRequest()
        {
            PlayFabId = myPlayFabId,
            Keys = null
        };
        PlayFabClientAPI.GetUserData(request, gotWall, OnError);
        //StartCoroutine(CallPlayFabApi(myPlayFabId));
    }

    IEnumerator CallPlayFabApi2()
    {
        //for (int i = 0; i < playFabids.Length; i++)
        //{
        //    if (playFabids[i] != "")
        //    {
        //        yield return new WaitForSeconds(0.2f);
        //        //flag = false;
        //        //StartCoroutine(CallPlayFabApi(playFabids[i]));
        //        GetUserData(playFabids[i]);
        //    }
        //}

        // Get Wallets won
        for (int i = 0; i < playFabids3.Length; i++)
        {
            if (playFabids3[i] != "")
            {
                yield return new WaitForSeconds(0.25f);
                //flag = false;
                //StartCoroutine(CallPlayFabApi(playFabids[i]));
                GetUserData(playFabids3[i]);
            }
        }
    }

    void gotWall(GetUserDataResult result)
    {
        if (result.Data == null || !result.Data.ContainsKey("Address"))
        {
            string acc = "Error!";
            //print("Wallet: " + acc);
            walletsFull[incw] = acc.Substring(0, 4) + "...." + acc.Substring(acc.Length - 4);
        }
        //Get Wallets Won
        else
        {
            Adresses3 += result.Data["Address"].Value + ",";
        }
        //else
        //{
        //    string acc = result.Data["Address"].Value;
        //    //print("Wallet: " + acc);
        //    walletsFull[incw] = acc.Substring(0, 4) + "...." + acc.Substring(acc.Length - 4);
        //}
        //if (incw < 3)
        //{
        //    rs.scp[incw].walletText.text = walletsFull[incw];
        //    rs.scp[incw].rankNumber.text = "#" + (incw + 1).ToString();
        //}
        //if (incw >= 3)
        //{
        //    //GameObject NewScore = Instantiate(rs.scp[incw].gameObject, rs.content);
        //    //NewScore.SetActive(true);
        //    print("Entered!");
        //    rs.NormalScripts[incw - 3].walletText.text = walletsFull[incw];
        //}
        //incw++;

        //flag = true;
    }

    public void prepAdd()
    {
        //numberOfDies++;
        //if(numberOfDies % 5 == 0)
        //    adsScript.prepAdd();
        //print("Enetered Here: " + numberOfDies);
        //print("Entered PrepAdd");
        if (canShowRewarded)
        {
            //This get called on the Start() of Player Script
            if (playerScript == null)
                playerScript = FindObjectOfType<player>();
            playerScript.ShowRewarded.SetActive(true);
        }
        if (canShowAd)
        {
            adsScript.ShowAdd();
            numberOfDies = 0;
            canShowAd = false;
        }
    }

    public void prepAdd2()
    {
        adsScript.DisableBoxes();
        numberOfDies++;
        if (numberOfDies % 6 == 0)
        {
            adsScript.prepAdd();
            canShowAd = true;
        }
        numberOfGames++;
        if (numberOfGames % 10 == 0 && !canShowRewarded)
        {
            canShowAd = false;
            canShowRewarded = true;
            adsScript.LoadRewardedAd();
            //numberOfGames = 0;

        }
    }

    public void RewardedAdFinished()
    {
        numberOfGames = 0;
        canShowRewarded = false;
        if (playerScript == null)
            playerScript = FindObjectOfType<player>();
        playerScript.ShowRewarded.SetActive(false);
        prevPoints = playerScript.ShowMyPoints();
    }

    public int AddPrevPoints()
    {
        if (!adsScript.WasRewardedShown())
            return 0;

        adsScript.DisRewardedShow();
        return prevPoints;
    }

    private void GetAppStatus()
    {
        try
        {
            mmm.Web2.SetActive(false);
        }
        catch
        {
            print("Error at PlayFabManager Line 828");
        }
        // TODO: This gets data from PlayFab
        //string myPlayFabId = "C5341C4390825109";

        //var request = new GetUserDataRequest()
        //{
        //    PlayFabId = myPlayFabId,
        //    Keys = null
        //};
        //PlayFabClientAPI.GetUserData(request, gotWall2, OnError);
    }

    void gotWall2(GetUserDataResult result)
    {
        if (result.Data == null || !result.Data.ContainsKey("V1"))
        {
            print("No version found!");
            //IOS
            if (mmm == null) return;
            mmm.Web1.SetActive(true);
            mmm.Web2.SetActive(false);
            //Android
            //mmm.Web1.SetActive(false);
            //mmm.Web2.SetActive(true);
        }
        else
        {
            print("Version found!");
            if (mmm == null) return;
            mmm.Web1.SetActive(false);
            mmm.Web2.SetActive(true);
        }
    }


    private void GetSkins()
    {
        var request = new GetUserDataRequest()
        {
            PlayFabId = playerplayfabid,
            Keys = null
        };
        PlayFabClientAPI.GetUserData(request, gotSkinVar, OnError);
    }

    private void gotSkinVar(GetUserDataResult result)
    {
        if (result.Data == null || !result.Data.ContainsKey("skin"))
        {
            print("No skins found!");
        }
        else
        {
            print("Skins found!");
            playerSkins = result.Data["skin"].Value;
            skinsIds = playerSkins.Split(',');
            //ERC20.BalanceOf
        }
    }

    public void EnableCongratsAd()
    {
        playerScript.WinPop.SetActive(false);
        playerScript.Info2.SetActive(true);
    }
}