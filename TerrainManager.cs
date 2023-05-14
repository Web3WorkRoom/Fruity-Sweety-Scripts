using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

public class TerrainManager : MonoBehaviour
{
    [SerializeField]
    private GameObject parentTerrains;

    [SerializeField]
    private GameObject[] Terrains;

    [SerializeField]
    private GameObject chosenTerrain;
    [SerializeField]
    private GameObject PrevTerrain;

    [SerializeField]
    private Transform OriginPos;
    [SerializeField]
    private Transform NextPos;

    private int i;
    private int r;

    [SerializeField]
    private float xSpeed;
    [SerializeField]
    private float xSpeed2;
    [SerializeField]
    private float xTarget;

    [SerializeField]
    private MMF_Player camTransition;
    [SerializeField]
    private GameObject faderbg;

    public static Transform nextContainer;

    public bool startedGame;
    [SerializeField]
    private GameObject BeforeStartTerrain;

    void Awake()
    {
        xTarget = -0.13f;
        startedGame = false;
        faderbg.SetActive(true);
        camTransition.Initialization();
        camTransition.PlayFeedbacks();
        i = parentTerrains.transform.childCount;
        Terrains = new GameObject[i];

        for (int i2 = 0; i2 < i; i2++)
        {
            Terrains[i2] = parentTerrains.transform.GetChild(i2).gameObject;
            Terrains[i2].GetComponent<BuildingsColour>().ChangeIt();
            Terrains[i2].GetComponent<TerrainObstaclesManager>().InitializeObs();
        }

        chosenTerrain = BeforeStartTerrain;
        // r = Random.Range(0, i);
        // chosenTerrain = Terrains[r];
        chosenTerrain.transform.position = OriginPos.position;
        chosenTerrain.SetActive(true);
    }

    
    private void Update()
    {
        // print(chosenTerrain.transform.position.x);
        if(startedGame)
        {
            if(chosenTerrain.transform.position.x < 20)
            {
                while (true)
                {
                    r = Random.Range(0, i);
                    if (!Terrains[r].activeSelf)
                        break;
                }
                if(PrevTerrain!= null)
                {
                    PrevTerrain.SetActive(false);
                }
                PrevTerrain = chosenTerrain;
                chosenTerrain = Terrains[r];
                chosenTerrain.transform.position = nextContainer.position;
                chosenTerrain.SetActive(true);
            }
        }
    }

    private void FixedUpdate() 
    {
        if(!startedGame) return;
        chosenTerrain.transform.Translate(xSpeed,0,0);
        xSpeed = Mathf.Lerp(xSpeed, xTarget, 0.025f);
        if(PrevTerrain==null) return;
        PrevTerrain.transform.Translate(xSpeed,0,0);
        xSpeed = Mathf.Lerp(xSpeed, xTarget, 0.025f);
    }

    public void ChangeX()
    {
        xTarget = 0;
    }

}
