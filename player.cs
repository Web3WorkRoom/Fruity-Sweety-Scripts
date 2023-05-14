using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using TMPro;

public class player : MonoBehaviour
{
    [SerializeField]
    private float ySpeed;
    [SerializeField]
    private float yTarget;
    private bool touching;

    private bool jumpcalled;
    [SerializeField]
    private float power;
    private bool startgame;

    [SerializeField]
    private GameObject smoke;
    [SerializeField]
    private GameObject sound1;
    [SerializeField]
    private GameObject smoke2;
    [SerializeField]
    private GameObject smoke3;
    [SerializeField]
    private GameObject smokePos;

    [SerializeField]
    private GameObject[] smokes = new GameObject[3];
    [SerializeField]
    private GameObject[] sounds = new GameObject[3];

    private int smokeI;

    [SerializeField]
    private GameObject smokeDown;
    [SerializeField]
    private Transform smokeDownPos;
    [SerializeField]
    private MMF_Player camShakeScript;
    [SerializeField]
    private GameObject sound2;

    [SerializeField]
    private bool ounce;
    // [SerializeField]
    // private GameObject TMPContainer;

    [SerializeField]
    private MMF_Player Transition1;

    [SerializeField]
    private Transform objToLook;
    [SerializeField]
    private float speed;

    // private int damping = 2;
    private Transform target;

    [SerializeField]
    private Quaternion Rots;
    [SerializeField]
    private float QuatZ;

    [SerializeField]
    private Transform ModelPlayer;

    public GameManager gm;

    [SerializeField]
    private GameObject speedParticles;
    [SerializeField]
    private TerrainManager tm;

    [SerializeField]
    private MMF_Player TextScorePopTransition;
    [SerializeField]
    private MMF_Player ImageScorePopTransition;
    [SerializeField]
    private MMFeedbacks OpenMouthTransition;
    [SerializeField]
    private MMFeedbacks DieOpenMouthTransition;
    [SerializeField]
    private MMF_Player ScoreCounting;

    [SerializeField]
    private Collider lastCol;

    [SerializeField]
    private TMP_Text pointsText;

    private int pointsScore;

    [SerializeField]
    private bool died;
    public bool canStart;

    private bool diedOnce;
    //[SerializeField]
    public GameObject ScorePanel;
    [SerializeField]
    private GameObject PointsPanel;
    
    [SerializeField]
    MMF_Player scaleFeedback;

    [SerializeField]
    private Popup ppScript;

    [SerializeField]
    private PlayFabManager pfm;

    [SerializeField]
    private GameChecker gc;

    [SerializeField]
    private GameObject ConnectYourWallet;
    [SerializeField]
    private GameObject ShowHighScoreObj;
    [SerializeField]
    private TMP_Text ShowHighScoreText;

    public GameObject HighScoreForNotConnected;

    public GameObject ShowRewarded;

    public GameObject Info2;
    public GameObject WinPop;

    [SerializeField]
    private float attempts;

    [SerializeField]
    private TMP_Text attemptsText;

    private void Awake()
    {
        attempts = PlayerPrefs.GetFloat("attempts", 1f);
        attemptsText.text = "Attempt " + attempts;
        HighScoreForNotConnected.SetActive(false);
        PointsPanel.SetActive(true);
        int playerLayer = LayerMask.NameToLayer("Player");
        gameObject.layer = playerLayer;
        diedOnce = false;
        canStart = false;
        died = false;
        yTarget = -0.34f;
        pointsScore = 0;
        ounce=false;
        smokeI = 0;
        touching=false;
        jumpcalled=false;
        startgame=false;
        speedParticles.SetActive(false);

    }

    private void Start()
    {
        if (pfm == null)
            pfm = FindObjectOfType<PlayFabManager>();

        pfm.prepAdd2();
        pointsScore = pfm.AddPrevPoints();
        gc.SawRewarded(pointsScore);
        gc.UpScore(pointsScore);
        pointsText.text = pointsScore.ToString();

    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0) && !died && canStart) 
        { 
            if(!startgame)
            {
                //speedParticles.SetActive(true);
                tm.startedGame = true;
            }
            if(!ounce)
            {
                Transition1.PlayFeedbacks();
                ounce = true;
            }
            jumpcalled=true;
            startgame=true;
        }
    }

    void FixedUpdate()
    {
        ModelPlayer.rotation = Quaternion.Slerp(ModelPlayer.rotation, Rots, Time.deltaTime * speed); 
        if (!startgame) return;

        if(!touching)
        {
        transform.Translate(0,ySpeed,0);
        ySpeed = Mathf.Lerp(ySpeed, yTarget, 0.025f);
        if(Rots.z > -0.234f)
        {
            Rots.z += -0.004f;
        }

        }

        if (jumpcalled) 
        { 
            if (Rots.z < 0.079f)
            {
                Rots.z = 0.079f;
            }
            else if (Rots.z >= 0.079f && Rots.z < 0.234f)
            {
                Rots.z += 0.079f;
            }
            smokes[smokeI].SetActive(false);
            sounds[smokeI].SetActive(false);
            smokes[smokeI].SetActive(true);
            sounds[smokeI].SetActive(true);
            ySpeed = power;
            transform.Translate(0,ySpeed,0);
            jumpcalled=false;
            smokeI++;
            if(smokeI >= 3)
            {
                smokeI=0;
            }
        }

    }


    void OnCollisionEnter(Collision collision)
    {
        // print(collision.transform.tag);
        
        if (collision.transform.tag == "floor")
        {
            touching = true;
            smokeDown.SetActive(false);
            smokeDown.transform.position = smokeDownPos.position;
            smokeDown.SetActive(true);
            if(died && !diedOnce) 
            {
                camShakeScript.PlayFeedbacks();
                sound2.SetActive(false);
                sound2.SetActive(true);
                diedOnce = true;
            }
        }
        if(died) return;

        if(pfm == null)
            pfm = FindObjectOfType<PlayFabManager>();

        pfm.prepAdd();

        int hs = int.Parse(PlayerPrefs.GetString("HS", "0"));
        if (pointsScore > hs)
        {
            PlayerPrefs.SetString("HS", pointsScore.ToString());
        }

        if (PlayerPrefs.GetString("Account") == "")
        {
            //ConnectYourWallet.SetActive(true);
            //ShowHighScoreObj.SetActive(false);
            // Others Off

            ConnectYourWallet.SetActive(false);
            ShowHighScoreObj.SetActive(true);

            if(pointsScore > hs)
            {
                HighScoreForNotConnected.SetActive(true);
                hs = pointsScore;
            }
            ShowHighScoreText.text = "High Score: " + hs;
        }
        else
        {
            //Show high score
            ConnectYourWallet.SetActive(false);
            ShowHighScoreObj.SetActive(true);
            ShowHighScoreText.text = "High Score: " + PlayerPrefs.GetString("HS");
        }

        try
        {
            attempts++;
            PlayerPrefs.SetFloat("attempts", attempts);
        }
        catch
        {
            print("Max attempts reached!");
        }
     
        ScoreCounting.GetFeedbackOfType<MMF_TMPCountTo>("TMP_Count_To").CountTo = pointsScore;
        ScoreCounting.PlayFeedbacks();
        DieOpenMouthTransition.PlayFeedbacks();
        int playerLayer = LayerMask.NameToLayer("Player2");
        gameObject.layer = playerLayer;
        ScorePanel.SetActive(true);
        //ppScript.Open();
        PointsPanel.SetActive(false);
        camShakeScript.PlayFeedbacks();
        sound2.SetActive(false);
        sound2.SetActive(true);
        PostScore();
        yTarget = -0.1f;
        tm.ChangeX();
        speedParticles.SetActive(false);
        died = true;
        
    }

    void OnCollisionExit(Collision collision)
    {
        touching = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (died) return;
        if (other.tag!="score") return;
        if (lastCol != null)
        {
            if (lastCol == other)
            {
                print("Came here!");
                return;
            }
        }
        lastCol = other;
        pointsScore += +1;
        gc.UpScore(pointsScore);
        if (pointsScore % 10 == 0)
            OpenMouthTransition.PlayFeedbacks();
        TextScorePopTransition.PlayFeedbacks();
        ImageScorePopTransition.PlayFeedbacks();
        gm.SoundsTransitions[gm.pointsSoundsI].PlayFeedbacks();
        pointsText.text = pointsScore.ToString();
        gm.pointsSoundsI++;
        if(gm.pointsSoundsI >= 3)
        {
            gm.pointsSoundsI = 0;
        }
    }


    private void PostScore()
    {
        // uploadin score

        if (pfm == null)
        {
            pfm = FindObjectOfType<PlayFabManager>();
        }
        try
        {
            pfm.SendLeaderboard(pointsScore, gc);
        }
        catch
        {
            // score error
            print("Error");
        }
    }

    public int ShowMyPoints()
    {
        return pointsScore;
    }

}
