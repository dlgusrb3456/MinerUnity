using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]

public class MapData : MonoBehaviour
{
    public Vector2Int mapSize;
    public int[] mapData;
    public Vector2Int playerPosition; 

    //MapData mapData
    //{
    //    "mapSize" : {
    //        "x" : 20,
    //        "y" : 20,
    //        "magnitude" : 15.2643375
    //        "sqrMagnitude" : 233
    //    }
    //}
        
    
}
