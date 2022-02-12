using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Android;

public class chooseMapInit : MonoBehaviour
{

    void Start()
    {
        Map.loadLocalMaps();
        Map.deleteLocalMap(Map.localMaps[0]);
    }

    // called when the game is terminated
    void OnDisable()
    {
        //Debug.Log("OnDisable");
    }

    // Debug.Log("경로: " + Application.persistentDataPath);

}
