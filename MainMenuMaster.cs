using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;


public class MainMenuMaster : MonoBehaviour
{
    public GameObject blob;
    public bool pressed;
    public bool passed1;

    public Blob blobScript;
    [SerializeField]
    private MMF_Player camTransition;
    [SerializeField]
    private GameObject faderbg;

    public RankingScript rs;

    [SerializeField]
    private PlayFabManager pfm;

    public GameObject Web1;
    public GameObject Web2;

    private void Awake() 
    {
        pressed=false;
        passed1=false;
        rs.GetNormals();
    }

    private void Update() 
    {
        if(pressed)
        {
            playPressed2();
        }
    }

    public void PlayScecne()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        this.enabled= false;
    }

    public void playPressed()
    {
        // pressed = true;
        blobScript.started = true;
    }

    public void playPressed2()
    {
        faderbg.SetActive(true);
        camTransition.PlayFeedbacks();
        pressed = false;
    }

    public void GetLeader()
    {
        if (pfm == null)
        {
            pfm = FindObjectOfType<PlayFabManager>();
        }
        pfm.GetLeader();
    }

    public void GetMyLeader()
    {
        if (pfm == null)
        {
            pfm = FindObjectOfType<PlayFabManager>();
        }
        pfm.GetOwnScore();
    }

    public void OpenLink(string URL)
    {
        Application.OpenURL(URL);
    }

}

