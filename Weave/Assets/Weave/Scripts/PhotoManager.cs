using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;
using UnityEngine.VR.WSA.WebCam;
using System.Linq;

public class PhotoManager : MonoBehaviour {

    PhotoCapture photoCaptureObject = null;
    string filename;
    string filePath;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakePhoto()
    {
        PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);

    }

    void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        photoCaptureObject = captureObject;

        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

        CameraParameters c = new CameraParameters();
        c.hologramOpacity = 0.0f;
        c.cameraResolutionWidth = cameraResolution.width;
        c.cameraResolutionHeight = cameraResolution.height;
        c.pixelFormat = CapturePixelFormat.BGRA32;

        captureObject.StartPhotoModeAsync(c, false, OnPhotoModeStarted);
    }

    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            //   audioSource.Play();
            filename = string.Format(@"CapturedImage{0}_n.jpg", Time.time);
            filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);
            Debug.Log(filePath);
            photoCaptureObject.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);
            AssetManager.Instance.AddPhoto(filename, filePath);
        }
        else
        {
            Debug.LogError("Unable to start photo mode!");
        }
    }

    void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        if (result.success)
        {
            Debug.Log("Saved Photo to disk!");
            photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);

            GameObject photoFrame = GameObject.CreatePrimitive(PrimitiveType.Quad);

            photoFrame.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3f;
            Texture2D texture = new Texture2D(2, 2);
            Renderer renderer = photoFrame.GetComponent<Renderer>();
            byte[] imageData;
            imageData = System.IO.File.ReadAllBytes(filePath);
            texture.LoadImage(imageData);
            renderer.material.mainTexture = texture;

            Billboard b = photoFrame.AddComponent<Billboard>();
            //b.PivotAxis = PivotAxis.Y;

            //TapToPlace t = photoFrame.AddComponent<TapToPlace>();

      

        }
        else
        {
            Debug.Log("Failed to save Photo to disk");
        }
    }
    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }

}
