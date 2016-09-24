using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;
using UnityEngine.VR.WSA.WebCam;
using System.Linq;
using RenderHeads.Media.AVProVideo;

public class VideoManager : MonoBehaviour {

    [SerializeField]
    GameObject videoMediaPlayer;
    VideoCapture m_VideoCapture = null;
    string filename = null;
    string filepath = null;

    // Use this for initialization
    void Start () {
    }

    public void TakeVideo()
    {
        VideoCapture.CreateAsync(false, OnVideoCaptureCreated);

    }
    void OnVideoCaptureCreated(VideoCapture videoCapture)
    {
        if (videoCapture != null)
        {
            m_VideoCapture = videoCapture;

            Resolution cameraResolution = VideoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
            float cameraFramerate = VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending((fps) => fps).First();

            CameraParameters cameraParameters = new CameraParameters();
            cameraParameters.hologramOpacity = 0.0f;
            cameraParameters.frameRate = cameraFramerate;
            cameraParameters.cameraResolutionWidth = cameraResolution.width;
            cameraParameters.cameraResolutionHeight = cameraResolution.height;
            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

            m_VideoCapture.StartVideoModeAsync(cameraParameters,
                                                VideoCapture.AudioState.MicAudio,
                                                OnStartedVideoCaptureMode);
        }
        else
        {
            Debug.LogError("Failed to create VideoCapture Instance!");
        }
    }

    void OnStartedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        if (result.success)
        {
            filename = string.Format("Video_{0}.mp4", Time.time);
            filepath = System.IO.Path.Combine(Application.persistentDataPath, filename);

            m_VideoCapture.StartRecordingAsync(filepath, OnStartedRecordingVideo);
        }
    }

    void OnStartedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Started Recording Video!");
        // We will stop the video from recording via other input such as a timer or a tap, etc.
    }

    // The user has indicated to stop recording
    public void StopRecordingVideo()
    {
        m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);


    }
    // Update is called once per frame
    void Update () {
	
	}

    void OnStoppedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        Debug.Log("Stopped Recording Video!");
        m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);


        GameObject videoFrame = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Billboard b = videoFrame.AddComponent<Billboard>();

        videoFrame.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3f;
        videoFrame.transform.localScale = new Vector3(.5f, 0.3f, 1f);
        GameObject g = GameObject.FindGameObjectWithTag("MediaPlayer");

        MediaPlayer mp = g.GetComponent<MediaPlayer>();
        mp.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToPeristentDataFolder, filename);
        ApplyToMaterial am = g.GetComponent<ApplyToMaterial>();
        Renderer renderer = videoFrame.GetComponent<Renderer>();
        renderer.material = am._material;
        mp.Play();
    }

    void OnStoppedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        m_VideoCapture.Dispose();
        m_VideoCapture = null;
    }

}
