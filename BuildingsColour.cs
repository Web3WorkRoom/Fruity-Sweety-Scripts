using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsColour : MonoBehaviour
{
    [SerializeField]
    private Material[] colours;
    [SerializeField]
    private Material[] colours2;
    [SerializeField]
    private GameObject[] walls;
    [SerializeField]
    private Transform wallsParent;

    [SerializeField]
    private Transform nextContainer;

    private int r;

    public bool isFirst;

    private void Awake() 
    {
        if (isFirst)
            ChangeIt();
    }

    public void ChangeIt()
    {
        int cLength = colours.Length;
        int cLength2 = colours2.Length;
        int wLength = wallsParent.childCount - 3;
        walls = new GameObject[wLength];
        for (int i = 0; i < wLength; i++)
        {
            walls[i] = wallsParent.GetChild(i).gameObject;
            if(i < 2)
            {
                r = Random.Range(0, cLength2);
                walls[i].GetComponent<MeshRenderer>().material = colours2[r];
            }
            else
            {
                r = Random.Range(0, cLength);
                walls[i].GetComponent<MeshRenderer>().material = colours[r];
            }
        }
    }

    private void OnEnable() 
    {
        TerrainManager.nextContainer = nextContainer;
    }
}
