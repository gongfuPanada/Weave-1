using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class Manager :  Singleton<Manager>{

    //Video video = null;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void TakePhoto() {
        //CaptureMedia captureMedia = new CaptureMedia();
        //captureMedia.Photo();
        //audioSource.Play();
    }

    public void TakeVideo()
    {
        //audioSource.Play();
        //video = new Video();
        //video.TakeVideo();
    }

    public void StopVideo()
    {
        //video.StopRecordingVideo();
    }
}
