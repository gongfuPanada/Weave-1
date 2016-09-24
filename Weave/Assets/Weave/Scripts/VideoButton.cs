using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class VideoButton : MonoBehaviour {

    [SerializeField]
    private GameObject videoCaptureMessage = null;


    int videoRecordingState;
    VideoManager video = null;
    MeshRenderer[] mr;
    // Use this for initialization
    void Start () {
        videoRecordingState = 0;
        mr = gameObject.transform.parent.GetComponentsInChildren<MeshRenderer>();

    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnSelect()
    {
        switch (videoRecordingState)
        {
            case 0:

                Debug.Log("Clicked once" + gameObject.name);
                foreach (MeshRenderer m in mr)
                {
                    m.enabled = false;
                }


                GameObject videoCaptureMessageObject;
                videoCaptureMessageObject = (GameObject)Instantiate(videoCaptureMessage, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 2f), Quaternion.identity);
                videoCaptureMessageObject.AddComponent<Billboard>();
                Destroy(videoCaptureMessageObject, 2f);

                GestureManager.Instance.OverrideFocusedObject = gameObject;
                videoRecordingState = 1;

                break;
            case 1:
                Debug.Log("Clicked twice" + gameObject.name);
                video = new VideoManager();
                video.TakeVideo();
                videoRecordingState = 2;
                break;
            case 2:
                Debug.Log("Clicked thrice" +gameObject.name);

                video.StopRecordingVideo();
                Destroy(video);
                videoRecordingState = 0;

                foreach (MeshRenderer m in mr)
                {
                    m.enabled = true;
                }
                GestureManager.Instance.OverrideFocusedObject = null;
                break;

        }

    }
}
