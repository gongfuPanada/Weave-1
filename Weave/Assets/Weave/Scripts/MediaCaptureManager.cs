using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class MediaCaptureManager : Singleton<MediaCaptureManager> {

    public GameObject mediaCapturePanel; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void AddMedia(bool addMediaMode)
    {
        if (addMediaMode)
            mediaCapturePanel.SetActive(true);
        else
            mediaCapturePanel.SetActive(false);

        //gameObject.SetActive(false);

        GameObject[] gameObjs = GameObject.FindGameObjectsWithTag("Team");
        foreach (GameObject g in gameObjs)
        {
            g.SetActive(false);
        }


    }

    public void OnSelect()
    {
        AddMedia(true);
    }

}
