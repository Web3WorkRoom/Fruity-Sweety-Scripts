using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameChecker : MonoBehaviour
{

    [SerializeField]
    private player ps;

    private int prevS;
    private bool val;

    public GameObject madeHighS;
    public GameObject other1;
    public GameObject other2;

    public TMP_Text textUpdate;

    private void Awake()
    {
        val = true;
        prevS = 0;
    }

    public void UpScore(int score)
    {
        if (score == 0) return;
        if (score - prevS == 1)
            prevS = score;
        else
        {
            // Cheating
            print("is cheating");
            val = false;
        }
    }

    public bool isValid()
    {
        return val;
    }

    public void SawRewarded(int score)
    {
        if (ps.ShowMyPoints() != score) return;
        if (score <= 0) return;
        prevS = score - 1;
    }

    public void MadeHighScore()
    {
        //Enable confetti and text
        other1.SetActive(false);
        other2.SetActive(false);
        madeHighS.SetActive(true);
    }
}
