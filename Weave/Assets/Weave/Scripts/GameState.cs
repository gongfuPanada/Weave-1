using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class GameState : Singleton<GameState> {

    Narrative narrative = new Narrative();
    string narrativeFileName = "narrativ.xml";
    void Awake()
    {
        string filePath = Application.persistentDataPath + @"\" + narrativeFileName;
        Debug.Log(filePath);
        narrative = Narrative.Load(filePath);

        //Station item = new Station();
        //Artefact art = new Artefact();
        //art.Name = "ART";

        //item.Name = "station";
        //item.Artefacts.Add(art);
        //narrative.StationCollection.Add(item);
    }
    // Use this for initialization
    void Start () {
        Debug.Log("Started");

    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnApplicationQuit()
    {
        narrative.Save(Application.persistentDataPath + @"\" + narrativeFileName);
    }
}
