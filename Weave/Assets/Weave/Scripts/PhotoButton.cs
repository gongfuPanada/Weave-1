using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class PhotoButton : MonoBehaviour {

    public GameObject photoCaptureMessage = null;
    bool isTakingPhoto = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   void OnSelect()
    {
        //MediaCapturePanelManager mc = this.gameObject.GetComponentInParent<MediaCapturePanelManager>();
        //mc.mediaMode = "photo";
        if (!isTakingPhoto)

        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            isTakingPhoto = true;
            GameObject photoMessageObject;
            photoMessageObject = (GameObject)Instantiate(photoCaptureMessage, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 2f), Quaternion.identity);
            photoMessageObject.AddComponent<Billboard>();
            Destroy(photoMessageObject, 2f);
            GestureManager.Instance.OverrideFocusedObject = gameObject;
        }
        else
        {
            isTakingPhoto = false;
            PhotoManager photoManager = new PhotoManager();
            photoManager.TakePhoto();
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            GestureManager.Instance.OverrideFocusedObject = null;

        }

        //

    }
}
