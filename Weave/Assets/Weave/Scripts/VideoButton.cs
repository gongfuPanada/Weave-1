using UnityEngine;
using System.Collections;

public class VideoButton : MonoBehaviour {

    bool recording;
    VideoManager video = null;

    // Use this for initialization
    void Start () {
        recording = false;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnSelect()
    {

        if (!recording)
        {
            video = new VideoManager();
            video.TakeVideo(AssetManager.Instance);
            recording = true;
        }
        else
        {
            video.StopRecordingVideo();
            recording = false;
            Destroy(video);

        }
    }
}
