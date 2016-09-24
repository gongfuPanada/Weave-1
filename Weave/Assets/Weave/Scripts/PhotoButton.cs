using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class PhotoButton : MonoBehaviour {

    public GameObject photoCaptureMessage = null;
    bool isTakingPhoto = false;
    MeshRenderer[] mr;

    // Use this for initialization
    void Start () {
        mr = gameObject.transform.parent.GetComponentsInChildren<MeshRenderer>();
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

            foreach (MeshRenderer m in mr)
            {
                m.enabled = false;
            }

            isTakingPhoto = true;
            GameObject photoMessageObject;
            photoMessageObject = (GameObject)Instantiate(photoCaptureMessage, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 2f), Quaternion.identity);
            photoMessageObject.AddComponent<Billboard>();
            Destroy(photoMessageObject, 2f);
            GestureManager.Instance.OverrideFocusedObject = gameObject;

            //Change cursor
        }
        else
        {
            isTakingPhoto = false;
            PhotoManager photoManager = new PhotoManager();
            photoManager.TakePhoto();
            foreach (MeshRenderer m in mr)
            {
                m.enabled = true;
            }
            GestureManager.Instance.OverrideFocusedObject = null;

        }

        //

    }
}
