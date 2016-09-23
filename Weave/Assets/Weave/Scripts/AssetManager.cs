using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit;
using HoloToolkit.Unity;
//using Newtonsoft.Json;

class Story
{
    //public string ExperienceName;
    public List<StoryDetail> StoryDetails;
}

class StoryDetail
{
    string assetName;
    string assetLocation;
    string assetType;

    public StoryDetail(string an, string al, string at)
    {
        assetName = an;
        assetLocation = al;
        assetType = at;
    }

    public string AssetName
    {
        get
        {
            return assetName;
        }

        set
        {
            assetName = value;
        }
    }
    public string AssetLocation
    {
        get
        {
            return assetLocation;
        }

        set
        {
            assetLocation = value;
        }
    }
    public string AssetType
    {
        get
        {
            return assetType;
        }

        set
        {
            assetType = value;
        }
    }
}

public class AssetManager : Singleton<AssetManager>{

    Dictionary<string, StoryDetail> storyDetails = new Dictionary<string, StoryDetail>();

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReadAssets()
    {

        string[] files = System.IO.Directory.GetFiles(Application.persistentDataPath);
    }

    public void AddPhoto(string photoName, string filePath)
    {
        StoryDetail storyDetail = new StoryDetail(photoName, filePath, "Photo");
        storyDetails.Add(photoName, storyDetail);
    }

    public void AddVideo(string videoName, string filePath)
    {
        StoryDetail storyDetail = new StoryDetail(videoName, filePath, "Video");

        storyDetails.Add(videoName, storyDetail);
    }


    public void Serialize()
    {
       foreach (StoryDetail story in storyDetails.Values)
       {
            //

       }
    }

    public void LoadData()
    {

    }


}
