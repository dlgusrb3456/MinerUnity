using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class getmapInfoClass
{
    public string editorName;
    public string mapName;
}

public class mapMaker : MonoBehaviour
{
    public GameObject grassPrefab;
    public GameObject startPrefab;
    public GameObject endPrefab;
    public GameObject rockPrefab;
    public GameObject Players;




    private GameObject grass_obj;
    private GameObject start_obj;
    private GameObject end_obj;
    private GameObject rock_obj;
    private static string mapCompressionBase64String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
    //크기.
    int xSize = 0;
    int ySize = 0;
    int small = 22;
    int middle = 34;
    int large = 52;
    //배열;
    int[,] mapArr;




    int[,] testarr = new int[7, 7]
    {
        {4,4,4,4,4,4,4 },
        {4,0,1,1,1,1,4 },
        {4,0,1,1,1,1,4 },
        {4,0,1,1,1,1,4 },
        {4,0,0,2,1,1,4 },
        {4,1,1,1,3,1,4 },
        {4,4,4,4,4,4,4 }
    };

    int[,] designArr;

    //arr만 받아오면 그냥 쓰면 됨;

    IEnumerator getmapInfoAPI()
    {
        string URL = "https://miner22.shop/miner/playmaps/info";
        Debug.Log("miroNames: " + PlayerPrefs.GetString("mapName"));
        Debug.Log("editorNames: " + PlayerPrefs.GetString("editorName"));

        getmapInfoClass myObject = new getmapInfoClass { editorName = PlayerPrefs.GetString("editorName"), mapName = PlayerPrefs.GetString("mapName") };

        string json = JsonUtility.ToJson(myObject);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, json))
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bytes);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.Send();

            if (www.isNetworkError)
            {
                Debug.Log(www.isNetworkError);
            }
            else
            {
                string returns = www.downloadHandler.text;
                string[] words = returns.Split(',');
                //for (int i = 0; i < words.Length; i++)
                //{
                //    Debug.Log(words[i]);
                //}

                string[] returncode = words[1].Split(':');

                if (returncode[1] == "1000")
                {
                    string[] mapStrArr = words[3].Split('"');
                    string mapStr = mapStrArr[5];
                    //Debug.Log("mapStr: "+mapStr);
                    string[] mapSizeArr = words[4].Split(':');
                    string mapSize = mapSizeArr[1];
                    mapSize = mapSize[0].ToString();
                    int intMapSize = Convert.ToInt32(mapSize);
                    PlayerPrefs.SetInt("InmapSize", intMapSize);
                    //Debug.Log("mapSize: " + mapSize);
                    arrToMapPlay(mapStr, mapSize);
                }

                else
                {

                }

            }
        }
    }
    public void arrToMapPlay(string mapStr, string mapSize)
    {
        if(mapSize == "1")
        {
            xSize = small;
            ySize = small;
        }
        else if(mapSize == "2")
        {
            xSize = middle;
            ySize = middle;
        }
        else if (mapSize == "3")
        {
            xSize = large;
            ySize = large;
        }

        designArr = new int[xSize, ySize];
        designArr = decodeMapDataPlay(mapStr, xSize, ySize);


        for (int i = 0; i < designArr.GetLength(0); i++)
        {
            for (int j = 0; j < designArr.GetLength(1); j++)
            {

                Vector3 position = new Vector3(-1.0f + (float)(0.6) * i, -1.0f + (float)(-0.6) * j, 0);
                if (designArr[i, j] == 1)
                {
                    grass_obj = Instantiate(grassPrefab);
                    grass_obj.transform.position = position;

                }
                else if (designArr[i, j] == 2)
                {

                    //end_obj = Instantiate(endPrefab);
                    endPrefab.transform.position = position;
                }
                else if (designArr[i, j] == 3)
                {
                    start_obj = Instantiate(startPrefab);
                    start_obj.transform.position = position;
                    Players.transform.position = position;
                }
                else if (designArr[i, j] == 4)
                {
                    rock_obj = Instantiate(rockPrefab);
                    rock_obj.transform.position = position;
                }
            }
        }
    }






    public void arrToMap()
    {

        Map.loadLocalMaps();
        string[] mapSize = new string[2];
        string selectedFileName = PlayerPrefs.GetString("DesSelectedFile");
        Debug.Log("DesSelectedFile: " + selectedFileName);
        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            if (Map.localMaps[i].name == selectedFileName)
            {
                mapSize = Map.localMaps[i].mapSize.Split('X');
            }
        }

        xSize = Convert.ToInt32(mapSize[0]);
        ySize = Convert.ToInt32(mapSize[1]);

        if(xSize == 18)
        {
            PlayerPrefs.SetInt("InmapSize", 1);
        }
        else if(xSize == 34)
        {
            PlayerPrefs.SetInt("InmapSize", 2);
        }
        else if (xSize == 52)
        {
            PlayerPrefs.SetInt("InmapSize", 3);
        }

        designArr = new int[xSize, ySize];

        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            if (Map.localMaps[i].name == selectedFileName)
            {
                designArr = decodeMapData(Map.localMaps[i], xSize, ySize);
                break;
            }
        }
        for (int i = 0; i < designArr.GetLength(0); i++)
        {
            for (int j = 0; j < designArr.GetLength(1); j++)
            {
             
                Vector3 position = new Vector3(-1.0f + (float)(0.6) * j, -1.0f + (float)(-0.6) * i, 0);
                if (designArr[i, j] == 1)
                {
                    grass_obj = Instantiate(grassPrefab);
                    grass_obj.transform.position = position;

                }
                else if (designArr[i, j] == 2)
                {

                    //end_obj = Instantiate(endPrefab);
                    endPrefab.transform.position = position;
                }
                else if (designArr[i, j] == 3)
                {
                    start_obj = Instantiate(startPrefab);
                    start_obj.transform.position = position;
                    Players.transform.position = position;
                }
                else if (designArr[i, j] == 4)
                {
                    rock_obj = Instantiate(rockPrefab);
                    rock_obj.transform.position = position;
                }

            }
        }


    }

    public int[,] decodeMapDataPlay(string mapStr, int height, int width)
    {
        string raw = mapStr;
        int[,] rl = new int[height, width];

        for (int i = 0; i < height; i++)
            for (int j = 0; j < width / 2; j++)
            {
                int idx = i * (width / 2) + j;
                int k;

                for (k = 0; k < mapCompressionBase64String.Length; k++)
                    if (raw[idx] == mapCompressionBase64String[k]) break;

                if (k == mapCompressionBase64String.Length) return null;

                rl[i, j * 2] = k >> 3;
                rl[i, j * 2 + 1] = k & 0x07;
            }
        return rl;
    }


    public int[,] decodeMapData(Map map, int height, int width)
    {
        string raw = map.mapData;
        int[,] rl = new int[height, width];

        for (int i = 0; i < height; i++)
            for (int j = 0; j < width / 2; j++)
            {
                int idx = i * (width / 2) + j;
                int k;

                for (k = 0; k < mapCompressionBase64String.Length; k++)
                    if (raw[idx] == mapCompressionBase64String[k]) break;

                if (k == mapCompressionBase64String.Length) return null;

                rl[i, j * 2] = k >> 3;
                rl[i, j * 2 + 1] = k & 0x07;
            }
        return rl;
    }





    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("playMode"));
        if (PlayerPrefs.GetString("playMode") == "Play")
        {
            StartCoroutine(getmapInfoAPI());
        }
        else if(PlayerPrefs.GetString("playMode") == "Design")
        {
            arrToMap();
        }
        

    }
}
