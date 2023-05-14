using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingScript : MonoBehaviour
{

    public RankingItemScript first;
    public RankingItemScript second;
    public RankingItemScript third;
    public RankingItemScript normal;
    public RankingItemScript myScore;
    public RankingItemScript[] scp;
    public Transform content;
    public Text timeLeft;

    public RankingItemScript[] NormalScripts;
    public GameObject parentOfNormals;


    public void GetNormals()
    {
        int maxNum = parentOfNormals.transform.childCount;
        int rankNum = 4;
        NormalScripts = new RankingItemScript[maxNum];
        for (int i = 0; i < maxNum; i++)
        {
            NormalScripts[i] = parentOfNormals.transform.GetChild(i).GetComponent<RankingItemScript>();
            NormalScripts[i].rankNumber.text = "#"+rankNum.ToString();
            rankNum++;
        }
    }
}
