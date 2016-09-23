using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class FileButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnSelect()
    {
        AssetManager assetManager = new AssetManager();
        assetManager.ReadAssets();

    }
}
