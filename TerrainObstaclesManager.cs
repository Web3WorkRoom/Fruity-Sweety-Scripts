using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainObstaclesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject parentObstacles;
    [SerializeField]
    private GameObject[] Terrains;
    [SerializeField]
    private int i;
    [SerializeField]
    private int[] randomNums = new int[50];

    [SerializeField]
    private int rIncrement;

    [SerializeField]
    private GameObject[] activeTerrains;

    [SerializeField]
    private Transform[] positions;
    [SerializeField]
    private int extraI;

    public GameManager gm;

    // private void Awake() 
    // {
    //     activeTerrains = new GameObject[10];
    //     rIncrement = 0;
    //     i = parentObstacles.transform.childCount;
    //     Terrains = new GameObject[i];

    //     for (int i2 = 0; i2 < i; i2++)
    //     {
    //         Terrains[i2] = parentObstacles.transform.GetChild(i2).gameObject;
    //         Terrains[i2].GetComponent<BuildingsColour>().ChangeIt();
    //     }

    //     for (int i2 = 0; i2 < 50; i2++)
    //     {
    //         randomNums[i2] = Random.Range(0, 50);
    //     }

    // }

    public void InitializeObs()
    {
        activeTerrains = new GameObject[10];
        rIncrement = 0;
        i = parentObstacles.transform.childCount;
        Terrains = new GameObject[i];

        for (int i2 = 0; i2 < i; i2++)
        {
            Terrains[i2] = parentObstacles.transform.GetChild(i2).gameObject;
            Terrains[i2].GetComponent<ObstaclesColours>().gm = gm;
            Terrains[i2].GetComponent<ObstaclesColours>().ChangeC();
        }

        for (int i2 = 0; i2 < 50; i2++)
        {
            randomNums[i2] = Random.Range(0, i);
        }
    }


    private void OnEnable() 
    {
        for (int i2 = 0; i2 < 10; i2++)
        {
            extraI = randomNums[rIncrement];
            while(true)
            {
                if(Terrains[extraI].activeSelf)
                {
                    extraI = Random.Range(0, i);
                }
                else
                {
                    break;
                }
            }
            activeTerrains[i2] = Terrains[extraI];
            Terrains[extraI].transform.position = positions[i2].position;
            Terrains[extraI].SetActive(true);
            rIncrement++;
            if (rIncrement >= 50)
            {
                rIncrement = 0;
            }
        }
    }

    private void OnDisable() 
    {
        for (int i2 = 0; i2 < 10; i2++)
        {
            activeTerrains[i2].SetActive(false);
        }
    }
}
