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
                    sharePanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(deletePre);
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

    public void cancleShare()
    {
        Destroy(checkPrivatePrefabs);
    }





    public void confirmShare()
    {
        Destroy(checkPrivatePrefabs);
        if (checkPrivatePrefabs.transform.GetChild(6).GetComponent<Transform>().transform.GetChild(0).GetComponent<Toggle>().isOn)
        {
            Debug.Log("public 공유");
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






    public void shareStop()
    {

    }

    public void shareModify()
    {

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
