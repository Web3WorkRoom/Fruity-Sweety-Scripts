using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

public class GameManager : MonoBehaviour
{
    public bool showInstructions;
    [SerializeField]
    private MMF_Player Transition1;
    private bool once;


    public Material[] colours;

    public int pointsSoundsI;
    public MMFeedbacks[] SoundsTransitions = new MMFeedbacks[3];

    [SerializeField]
    private GoogleMobileAdsDemoScript GMA;
    [SerializeField]
    private AnimatedButton buttonRewarded;


    private void Awake() {
        showInstructions = false;
        once = false;
        pointsSoundsI = 0;
        // colourssL = colours.Length;
        // colourss = new Material[colourssL];
        // colourss = colours;
        GMA = FindObjectOfType<GoogleMobileAdsDemoScript>();
        if(GMA == null)
            GMA = FindObjectOfType<GoogleMobileAdsDemoScript>();
        buttonRewarded.onClick.AddListener(GMA.ShowRewardedAd);
    }


    private void Update() 
    {
        if(showInstructions && !once)
        {
            Transition1.PlayFeedbacks();
            once = true;
        }
    }

}
