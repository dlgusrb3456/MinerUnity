using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
public class shareClass
{
    public string mapName;
    public string mapInfo;
    public int mapSize;
    public int mapPassword;
    public string nickName;
    public int playCount;

}

public class shareStopClass
{
    public string nickName;
    public string mapName;
}

public class modifyMapClass
{
    public string mapInfo;
    public string nickName;
    public string mapName;
}


public class dotProblem : MonoBehaviour
{
    public GameObject Panel_setFilePrefab1;
    public GameObject Panel_setFilePrefab2;
    public GameObject Button_mapPrefab;
    public GameObject Panel_deletePrefab;
    public GameObject Panel_checkPrivatePrefabs;
    public GameObject Panel_getPrivate;
    public GameObject Panel_alertshareStatus;
    public GameObject Panel_loading;
    //public Sprite imageBG;

    public Text Text_mironame;
    //public Text Panel_deletePrefab_Text;

    private GameObject sharePanel;
    private GameObject nonesharePanel;
    private GameObject predeletePrefab;
    private GameObject checkPrivatePrefabs;
    private GameObject getPrivate;
    private GameObject alertShareStatus;
    private GameObject loading;


    private string selectedFileName="";
    private string mapInfos = "";
    private string mapSizes = "";
    private string smallMap = "22X22";
    private string middleMap = "52X52";
    private string largeMap = "102X102";
    private string mapPasswords = "";


    private static string mapCompressionBase64String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
    //checkValidation
    private bool isOK = false;
    private int[,] mapArr;
    private int[,] mapArrCheck;

    public void dotdotdotPanel()
    {
        
        selectedFileName = Text_mironame.text;
        Debug.Log(selectedFileName);
        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            if (Map.localMaps[i].name == selectedFileName)
            {
                mapInfos = Map.localMaps[i].mapData;
                mapSizes = Map.localMaps[i].mapSize;
                //Debug.Log(i);
                if (Map.localMaps[i].isShared)
                {
                    Debug.Log("isShared");
                    sharePanel = Instantiate(Panel_setFilePrefab1) as GameObject;
                    sharePanel.transform.parent = Button_mapPrefab.transform;
                    sharePanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(closesharePanel);
                    sharePanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(deletePreShared);
                    sharePanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(shareStop);
                    sharePanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(shareModify);
                    //sharePanel.transform.GetComponent<Image>().sprite = imageBG;
                    sharePanel.transform.localPosition = new Vector3(120,45, 0);
                    sharePanel.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log("isNoneShared");
                    nonesharePanel = Instantiate(Panel_setFilePrefab2) as GameObject;
                    nonesharePanel.transform.parent = Button_mapPrefab.transform;
                    nonesharePanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(closeNonesharePanel);
                    nonesharePanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(deletePre);
                    nonesharePanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(share);
                    nonesharePanel.transform.localPosition = new Vector3(120, 45, 0);
                    nonesharePanel.gameObject.SetActive(true);
                }
                break;
            }

        }
    }

    public void share()
    {
        checkPrivatePrefabs = Instantiate(Panel_checkPrivatePrefabs) as GameObject;
        checkPrivatePrefabs.transform.parent = Button_mapPrefab.transform.parent.parent.parent;
        checkPrivatePrefabs.transform.GetChild(0).GetComponent<Text>().text = "미로명 : " + selectedFileName;
        checkPrivatePrefabs.transform.GetChild(1).GetComponent<Text>().text = "제작자 : " + PlayerPrefs.GetString("nickName"); // => 사용자 닉네임 저장해둔걸로 사용. 
        checkPrivatePrefabs.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(confirmShare); //완료
        checkPrivatePrefabs.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(cancleShare); //취소
        checkPrivatePrefabs.transform.position = new Vector3(Camera.main.pixelWidth / 2, (Camera.main.pixelHeight / 2), 0);
    }

    public void shareStop()
    {
        StartCoroutine(shareStopAPI());
    }
    IEnumerator shareStopAPI()
    {
        loading = Instantiate(Panel_loading) as GameObject;
        loading.transform.parent = Button_mapPrefab.transform.parent.parent.parent.parent;
        string URL = "https://miner22.shop/miner/playmaps/stop";
        Debug.Log(PlayerPrefs.GetString("nickName"));
        shareStopClass myObject = new shareStopClass{nickName = PlayerPrefs.GetString("nickName"),mapName = selectedFileName};

        string json = JsonUtility.ToJson(myObject);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, json))
        {
            www.method = "PATCH";
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
                for (int i = 0; i < words.Length; i++)
                {
                    Debug.Log(words[i]);
                }

                string[] returncode = words[1].Split(':');
                alertShareStatus = Instantiate(Panel_alertshareStatus) as GameObject;
                alertShareStatus.transform.parent = Button_mapPrefab.transform.parent.parent.parent.parent;

                if (returncode[1] == "1000")
                {
                    //미로파일 상태 변화 => isShared 바꾸고 다시 load.
                    for (int i = 0; i < Map.localMaps.Count; i++)
                    {
                        if (Map.localMaps[i].name == selectedFileName)
                        {
                            Map.localMaps[i].isShared = false;
                            Map.localMaps[i].password = null;
                            Map.localMaps[i].isPrivate = false;
                            Map.saveToJson(Map.localMaps[i]);
                            break;
                        }
                    }
                    //공유 중지 성공 판넬.
                    alertShareStatus.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "공유 중지 완료!";
                }

                else
                {
                    //위의 공유 완료 판넬 공유 실패로.
                    alertShareStatus.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "공유 중지 실패!";
                    alertShareStatus.transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text = "";
                }

            }
        }
        Destroy(loading);
    }
    public void shareModify()
    {
        StartCoroutine(shareModifyAPI());
    }

    IEnumerator shareModifyAPI()
    {
        loading = Instantiate(Panel_loading) as GameObject;
        loading.transform.parent = Button_mapPrefab.transform.parent.parent.parent.parent;
        string URL = "https://miner22.shop/miner/playmaps/modify";

        string mapInfos = "";
        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            if (Map.localMaps[i].name == selectedFileName)
            {
                mapInfos = Map.localMaps[i].mapData;
                break;
            }
        }

        modifyMapClass myObject = new modifyMapClass { mapInfo = mapInfos, nickName = PlayerPrefs.GetString("nickName"), mapName = selectedFileName };

        string json = JsonUtility.ToJson(myObject);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, json))
        {
            www.method = "PATCH";
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
                for (int i = 0; i < words.Length; i++)
                {
                    Debug.Log(words[i]);
                }

                string[] returncode = words[1].Split(':');
                alertShareStatus = Instantiate(Panel_alertshareStatus) as GameObject;
                alertShareStatus.transform.parent = Button_mapPrefab.transform.parent.parent.parent.parent;

                if (returncode[1] == "1000")
                {
                    
                    //공유 수정 성공 판넬.
                    alertShareStatus.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "공유 수정 완료!";
                }

                else
                {
                    //위의 공유 완료 판넬 공유 실패로.
                    alertShareStatus.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "공유 수정 실패!";
                    alertShareStatus.transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text = "";
                }

            }
        }
        Destroy(loading);
    }

    public void cancleShare()
    {
        Destroy(checkPrivatePrefabs);
    }

    public int[,] decodeMapData(Map map, int height, int width)
    {
        Debug.Log("문자 배열화 시작");
        string raw = map.mapData;

        //int height = mapSizeStringToArray(map.mapSize)[0];
        //int width = mapSizeStringToArray(map.mapSize)[1];
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


        //d.mapData = rl;
        Debug.Log("문자 배열화 종료");
        return rl;
    }

    public bool validationCheck(int [,] mapArr)
    {
        // 0: 길
        // 1: 장애물
        // 2: 도착
        // 3: 출발
        // 4: 테두리
        int startCount = 0;
        int endCount = 0;
        int startPosX = 0;
        int startPosY = 0; 
        for (int i = 0; i < mapArr.GetLength(0); i++)
        {
            for (int j = 0; j < mapArr.GetLength(1); j++)
            {
                if (mapArr[i, j] == 2)
                {
                    endCount++;
                }
                else if(mapArr[i, j] == 3)
                {
                    startCount++;
                    startPosX = i;
                    startPosY = j;
                }

            }
        }

        if (endCount == 1 && startCount == 1)
        {
            Debug.Log("count OK");
            startToEnd(mapArr, startPosX, startPosY);
            if (isOK)
            {
                isOK = false;
                return true;
            }
        }
        Debug.Log("endCount: "+endCount);
        Debug.Log("startCount: " + startCount);
        return false;
    }

    public void startToEnd(int [,] mapArr,int i,int j)
    {
        int sizeX = mapArr.GetLength(0);
        int sizeY = mapArr.GetLength(1);
        if (j - 1 >= 0) //좌측
        {
            if (mapArr[i,j - 1] == 2)
            {
                isOK = true;
                return;
            }
            else if(mapArr[i, j - 1] == 0 && mapArrCheck[i,j-1]==0)
            {
                mapArrCheck[i, j - 1] = 1;
                startToEnd(mapArr, i, j - 1);
            }
        }

        if (i - 1 >= 0) //상단
        {
            if (mapArr[i-1, j] == 2)
            {
                isOK = true;
                return;
            }
            else if (mapArr[i-1, j] == 0 && mapArrCheck[i-1, j] == 0)
            {
                mapArrCheck[i-1, j] = 1;
                startToEnd(mapArr, i-1, j);
            }
        }

        if (j + 1 < sizeY)//우측
        {
            if (mapArr[i, j +1] == 2)
            {
                isOK = true;
                return;
            }
            else if (mapArr[i, j + 1] == 0 && mapArrCheck[i, j+1] == 0)
            {
                mapArrCheck[i, j+1] = 1;
                startToEnd(mapArr, i, j+1);
            }
        }

        if (i + 1 < sizeX)//하단
        {
            if (mapArr[i+1, j] == 2)
            {
                isOK = true;
                return;
            }
            else if (mapArr[i + 1, j] == 0 && mapArrCheck[i+1, j] == 0)
            {
                mapArrCheck[i + 1, j] = 1;
                startToEnd(mapArr, i+1, j);
            }
        }
    }

    public void confirmShare()
    {
        
        string[] mapSize = new string[2];
        
        int xSize = 0;
        int ySize = 0;
        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            if (Map.localMaps[i].name == selectedFileName)
            {
                mapSize = Map.localMaps[i].mapSize.Split('X');
                xSize = Convert.ToInt32(mapSize[0]);
                ySize = Convert.ToInt32(mapSize[1]);
                mapArr = new int[xSize, ySize];
                mapArrCheck = new int[xSize, ySize];
                mapArr = decodeMapData(Map.localMaps[i], xSize, ySize);
                break;
            }
        }
        if (validationCheck(mapArr))
        {
            Destroy(checkPrivatePrefabs);
            if (checkPrivatePrefabs.transform.GetChild(6).GetComponent<Transform>().transform.GetChild(0).GetComponent<Toggle>().isOn)
            {
                Debug.Log("public 공유");
                mapPasswords = "0";
                StartCoroutine(shareAPI());
                //api로 공유 진행
            }
            else
            {
                Debug.Log("private 공유");
                getPrivate = Instantiate(Panel_getPrivate) as GameObject;
                getPrivate.transform.parent = Button_mapPrefab.transform.parent.parent.parent.parent;
                getPrivate.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(cancleGetPW);
                getPrivate.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(confirmGetPW);
            }
        }
        else
        {
            checkPrivatePrefabs.transform.GetChild(7).GetComponent<Text>().text = "미로 형식이 유효하지 않습니다. \n*미로의 시작점과 도착점은 한개씩입니다.\n*미로의 시작점과 도착점은 길로 이어져야 합니다.";
        }
        
    }

    public void confirmGetPW()
    {
        mapPasswords = getPrivate.transform.GetChild(2).GetComponent<InputField>().text;
        if(mapPasswords.Length != 4)
        {
            getPrivate.transform.GetChild(5).GetComponent<Text>().color = Color.red;
        }
        else
        {
            Destroy(getPrivate);
            StartCoroutine(shareAPI());
            
        }
    }

    public void cancleGetPW()
    {
        Destroy(getPrivate);
    }
    IEnumerator shareAPI()
    {
        loading = Instantiate(Panel_loading) as GameObject;
        loading.transform.parent = Button_mapPrefab.transform.parent.parent.parent.parent;
        string URL = "https://miner22.shop/miner/playmaps/share";
        
        if(mapSizes == smallMap)
        {
            mapSizes = "1";
        }
        else if(mapSizes == middleMap)
        {
            mapSizes = "2";
        }
        else
        {
            mapSizes = "3";
        }
        Debug.Log("mapSizes : " + mapSizes);
        Debug.Log(PlayerPrefs.GetString("nickName"));
        shareClass myObject = new shareClass { mapName = selectedFileName, mapInfo = mapInfos, mapSize = Convert.ToInt32(mapSizes),mapPassword = Convert.ToInt32(mapPasswords),nickName = PlayerPrefs.GetString("nickName"),playCount =0 };

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
                for (int i = 0; i < words.Length; i++)
                {
                    Debug.Log(words[i]);
                }

                string[] returncode = words[1].Split(':');
                alertShareStatus = Instantiate(Panel_alertshareStatus) as GameObject;
                alertShareStatus.transform.parent = Button_mapPrefab.transform.parent.parent.parent.parent;
                
                if (returncode[1] == "1000")
                {

                    
                    
                    //미로파일 상태 변화 => isShared 바꾸고 다시 load.
                    for (int i = 0; i < Map.localMaps.Count; i++)
                    {
                        if (Map.localMaps[i].name == selectedFileName)
                        {
                            Map.localMaps[i].isShared = true;
                            Map.localMaps[i].password = mapPasswords;
                            if(mapPasswords == "0")
                            {
                                Map.localMaps[i].isPrivate = false;
                            }
                            Map.localMaps[i].isPrivate = true;
                            Map.saveToJson(Map.localMaps[i]);
                            break;
                        }
                    }
                    //공유 완료 판넬.
                    alertShareStatus.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "공유 완료!";
                }

                else
                {
                    //위의 공유 완료 판넬 공유 실패로.
                    alertShareStatus.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "공유 실패!" ;
                    alertShareStatus.transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text = "*최대 3개만 공유할 수 있습니다\n*이미 공유된 파일을 중지하고 공유해주세요";
                }

            }
        }
        Destroy(loading);
    }

    public void deleteFinal()
    {
        closepredeletePrefab();
        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            if (Map.localMaps[i].name == selectedFileName)
            {
                Map.deleteLocalMap(Map.localMaps[i]);
                //삭제 완료 추가.
                //load 추가 => 화면을 다시 받아오기
                SceneManager.LoadScene("mainDesign");
                break;
            }
        }

    }

    public void deletePre()
    {
        predeletePrefab = Instantiate(Panel_deletePrefab) as GameObject;
        predeletePrefab.transform.parent = Button_mapPrefab.transform.parent.parent.parent;
        predeletePrefab.transform.GetChild(0).GetComponent<Text>().text = selectedFileName + "을 정말 삭제하시겠습니까?";
        predeletePrefab.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(deleteFinal);
        predeletePrefab.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(closepredeletePrefab);
        predeletePrefab.transform.position = new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight / 2, 0) ;
        //sharePanel.gameObject.SetActive(true);
    }

    public void deletePreShared()
    {
        predeletePrefab = Instantiate(Panel_deletePrefab) as GameObject;
        predeletePrefab.transform.parent = Button_mapPrefab.transform.parent.parent.parent;
        predeletePrefab.transform.GetChild(0).GetComponent<Text>().text = selectedFileName + "을 정말 삭제하시겠습니까? \n 공유파일도 삭제됩니다.";
        predeletePrefab.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(deleteFinalShared);
        predeletePrefab.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(closepredeletePrefab);
        predeletePrefab.transform.position = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0);
        //sharePanel.gameObject.SetActive(true);
    }
    public void deleteFinalShared()
    {
        closepredeletePrefab();
        shareStop();
        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            if (Map.localMaps[i].name == selectedFileName)
            {
                Map.deleteLocalMap(Map.localMaps[i]);
                //삭제 완료 추가.
                //load 추가 => 화면을 다시 받아오기
                SceneManager.LoadScene("mainDesign");
                break;
            }
        }

    }
    public void closepredeletePrefab()
    {
        Destroy(predeletePrefab);
    }

    public void closeNonesharePanel()
    {
        Destroy(nonesharePanel);
    }

    public void closesharePanel()
    {
        Destroy(sharePanel);
    }


    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
