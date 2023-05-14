using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesColours : MonoBehaviour
{

    [SerializeField]
    private GameObject[] childs;
    public GameManager gm;

    public void ChangeC()
    {
        int i = Random.Range(0, gm.colours.Length);
        childs[0].GetComponent<MeshRenderer>().material = gm.colours[i];
        if (childs.Length == 2)
        {
            int i2 = Random.Range(0, gm.colours.Length);
            childs[1].GetComponent<MeshRenderer>().material = gm.colours[i2];
        }
        else if (childs.Length == 3)
        {
            int i2 = Random.Range(0, gm.colours.Length);
            childs[2].GetComponent<MeshRenderer>().material = gm.colours[i2];
        }
        else if (childs.Length == 4)
        {
            int i2 = Random.Range(0, gm.colours.Length);
            childs[3].GetComponent<MeshRenderer>().material = gm.colours[i2];
        }
        else if (childs.Length == 5)
        {
            int i2 = Random.Range(0, gm.colours.Length);
            childs[4].GetComponent<MeshRenderer>().material = gm.colours[i2];
        }
    }
}
