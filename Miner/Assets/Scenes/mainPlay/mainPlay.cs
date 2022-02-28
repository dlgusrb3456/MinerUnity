using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Linq;
public class searchNullClass
{
    public int orderType;
    public int pageNo;
}

public class searchNotNullClass
{
    public int orderType;
    public int searchType;
    public string searchContent;
    public int pageNo;
}
public class mapInfos
{
    public string mapName { get; set; }
    public string mapPassword { get; set; }
    public string editorName { get; set; }
    public string playCount { get; set; }
    
    public mapInfos(string mapName, string mapPassword,string editorName, string playCount)
    {
        this.mapName = mapName;
        this.mapPassword = mapPassword;
        this.editorName = editorName;
        this.playCount = playCount;
    }

}

public class mainPlay : MonoBehaviour
{
    //미로파일 4개 Panel
    public GameObject Button_PlayFile;
    public GameObject Panel_files;
    public Sprite image_lock;
    public Sprite image_unLock;

    //search 변수
    public InputField searchInputField;
    private string searchText = "";

    //Dropdown 변수
    public Dropdown dropdown;
    private int chooseSearchType = 0; //0이면 닉네임 / 1이면 미로명

    //Toggle
    public Toggle Toggle_popular;
    public Toggle Toggle_latest;

    public GameObject Toggle_Page;
    public GameObject Toggle_PageGroup;

    public GameObject Panel_settings;
    private GameObject Page_Toggle;
    public GameObject ExitPanel;
    //mapInfo list
    List<mapInfos> maps = new List<mapInfos>();

    public GameObject canvas;
    //bgm;
    GameObject BackgroundMusic;
    AudioSource backmusic;

    //pagingInfo

    private int totalMapNum=0;
    private int currentPage = 1;
    private int canvasChild = 12;

    public void ExitYes()
    {
        Application.Quit();
    }

    public void ExitNo()
    {
        
        int canvasChildCount = canvas.transform.childCount;
        if (canvasChildCount > canvasChild)
        {
            for (int i = canvasChild; i < canvasChildCount; i++)
            {
                canvas.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        ExitPanel.SetActive(false);
    }

    public void moveDesign()
    {
        //메인_설계 화면으로 이동 => 표지판에서 설계 누른경우
        SceneManager.LoadScene("mainDesign");
    }

    public void movePlay()
    {
        //메인_플레이 화면으로 이동 => 표지판에서 플레이 누른경우
        SceneManager.LoadScene("mainPlay");
    }




    public void LoadfilestoPanel(int listCount)
    {
        //Debug.Log("maps.Count = " + listCount);
        //기존의 판넬에 들어있는 파일들 모두 삭제.
        Transform[] childList = Panel_files.GetComponentsInChildren<Transform>();
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                {
                    Destroy(childList[i].gameObject);
                }
            }
        }
        // 선택한 페이지에 속한 파일 정보를 가져와야함.

        for(int i = 0; i < listCount; i++) //가져온 파일의 개수가 4개인 경우, List형식으로 반환할 예정
        {
            GameObject file = Instantiate(Button_PlayFile) as GameObject;
            file.transform.parent = Panel_files.transform;
            file.transform.GetChild(0).GetComponent<Text>().text = maps[i].mapName + "/"+maps[i].editorName;
            //Debug.Log(maps[i].mapPassword);
            if(maps[i].mapPassword == "0") // 이 미로가 공유파일인지 확인하는 절차 => 공유인경우
            {
                file.transform.GetChild(1).GetComponent<Image>().sprite = image_unLock;
            }
            else
            {
                file.transform.GetChild(1).GetComponent<Image>().sprite = image_lock;
            }
            file.transform.GetChild(2).GetComponent<Text>().text = "총 플레이  " + maps[i].playCount;    
        }

    }


    public void searchNull(int pageNos)
    {
        searchInputField.text = "";
        if (Toggle_popular.isOn)
        {
            searchText = "";
            PlayerPrefs.SetString("searchText", searchText);
            PlayerPrefs.SetInt("ToggleType", 0);
            StartCoroutine(searchNullAPI(0, pageNos));
            
            //인기순으로 검색해서 화면에 보이기
        }
        else
        {
            searchText = "";
            PlayerPrefs.SetInt("ToggleType", 1);
            PlayerPrefs.SetString("searchText", searchText);
            StartCoroutine(searchNullAPI(1, pageNos));
            
            //최신순으로 검색해서 화면에 보이기
        }
    }

    IEnumerator searchNullAPI(int orderTypes,int pageNos)
    {
        //testPanel.SetActive(true);
        string realURL = "https://miner22.shop/miner/playmaps";
        searchNullClass myObject = new searchNullClass { orderType = orderTypes, pageNo = pageNos };
        string json = JsonUtility.ToJson(myObject);


        using (UnityWebRequest www = UnityWebRequest.Post(realURL, json))
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
                //Debug.Log(returns);
                string[] words = returns.Split(',');
                //for (int i = 0; i < words.Length; i++)
                //{
                //    Debug.Log(words[i]);
                //}

                //Debug.Log(words[words.Length - 3]);
               
                string[] returncode = words[1].Split(':');
                if (returncode[1] == "1000")
                {
                    string[] mapCountsarr = words[words.Length - 3].Split(':');
                    totalMapNum = Convert.ToInt32(mapCountsarr[1]);
                    makePageInfo();
                    maps.Clear();
                    if (words.Length == 12)
                    {
                        //Debug.Log("없음");
                    } 
                    else if(words.Length == 20) {
                        maps = getInfotoAPI(1,words);
                    }
                    else if(words.Length == 29)
                    {
                        maps = getInfotoAPI(2, words);
                    }
                    else if(words.Length == 38)
                    {
                        maps = getInfotoAPI(3, words);
                    }
                    else if(words.Length == 47)
                    {
                        maps = getInfotoAPI(4, words);
                    }
                    LoadfilestoPanel(maps.Count);

                }
                else 
                {
                    //실패.
                }
            }
        }
    }

    public List<mapInfos> getInfotoAPI(int num,string[] words)
    {
        List<mapInfos> tmpList = new List<mapInfos>();
        string mapName = "";
        string mapPassword = "";
        string editor = "";
        string playCount = "";
        for (int i = 0; i < num; i++)
        {
            string[] info = words[3 + (i * 9)].Split('"');
            mapName = info[info.Length - 2];
            //Debug.Log("mapName = " + mapName);
            info = words[6 + (i * 9)].Split('"');
            mapPassword = info[info.Length - 1];
            string[] passwords = mapPassword.Split(':');
            mapPassword = passwords[passwords.Length - 1];
            //Debug.Log("mapPassword = " + mapPassword);
            info = words[7 + (i * 9)].Split('"');
            editor = info[info.Length - 2];
            //Debug.Log("editor = " + editor);
            info = words[8 + (i * 9)].Split('"');
            playCount = info[info.Length - 1];
            //Debug.Log("playCount = " + playCount);
            tmpList.Add(new mapInfos(mapName, mapPassword, editor, playCount));
        }
        return tmpList;
    }

    public void searchNotNull(string text)
    {
        int isPopular = -1;
        int isNickName = chooseSearchType;

        if (Toggle_popular.isOn)
        {
            isPopular = 0; // 인기순
        }
        else
        {
            isPopular = 1; // 최신순
        }
        // isPopular, isNickName, text ,currentPage세가지 넘겨서 값 받아오기
        StartCoroutine(searchNotNullAPI(isPopular, isNickName,text,1));
    }

    IEnumerator searchNotNullAPI(int orderTypes, int searchTypes,string searchContents, int pageNos)
    {
        //testPanel.SetActive(true);
        string realURL = "https://miner22.shop/miner/playmaps/search";
        searchNotNullClass myObject = new searchNotNullClass { orderType = orderTypes, searchType = searchTypes,searchContent = searchContents,pageNo = pageNos };
        string json = JsonUtility.ToJson(myObject);


        using (UnityWebRequest www = UnityWebRequest.Post(realURL, json))
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
                //Debug.Log(returns);
                string[] words = returns.Split(',');
                //for (int i = 0; i < words.Length; i++)
                //{
                //    Debug.Log(words[i]);
                //}

                //Debug.Log(words[words.Length - 3]);
                
                string[] returncode = words[1].Split(':');
                if (returncode[1] == "1000")
                {
                    string[] mapCountsarr = words[words.Length - 3].Split(':');
                    totalMapNum = Convert.ToInt32(mapCountsarr[1]);
                    makePageInfo();
                    maps.Clear();
                    if (words.Length == 12)
                    {
                        //Debug.Log("없음");
                    }
                    else if (words.Length == 20)
                    {
                        maps = getInfotoAPI(1, words);
                    }
                    else if (words.Length == 29)
                    {
                        maps = getInfotoAPI(2, words);
                    }
                    else if (words.Length == 38)
                    {
                        maps = getInfotoAPI(3, words);
                    }
                    else if (words.Length == 47)
                    {
                        maps = getInfotoAPI(4, words);
                    }
                    LoadfilestoPanel(maps.Count);

                }
                else
                {
                    //실패.
                }
            }
        }
    }

    public void searchConfirm()
    {
        currentPage = 1;
        for (int i = 0; i < searchInputField.text.Length; i++)
        {
            if (searchInputField.text[i] == ' ')
            {
                //Debug.Log("띄어쓰기는 입력할 수 없습니다.");
                return;
            }
        }

        if (searchInputField.text.Length == 0)
        {
            searchNull(currentPage);
            searchText = "";
        }
        else
        {
            searchText = searchInputField.text;

            searchNotNull(searchText);
        }
    }


    public void search()
    {
        for(int i = 0; i < searchInputField.text.Length; i++)
        {
            if(searchInputField.text[i]==' ')
            {
                //Debug.Log("띄어쓰기는 입력할 수 없습니다.");
                return;
            }
        }

        if(searchInputField.text.Length == 0)
        {
            searchNull(currentPage);
            searchText = "";
        }
        else
        {
            searchText = searchInputField.text;

            searchNotNull(searchText);
        }

    }

    public void searchDelete()
    {
        currentPage = 1;
        searchText = "";
        searchNull(currentPage);
    }

    public void goTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void makePageInfo()
    {
        //지우고 다시 생성.
        Transform[] childList = Toggle_PageGroup.GetComponentsInChildren<Transform>();
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                {
                    Destroy(childList[i].gameObject);
                }
            }
        }
        int totalPages = totalMapNum / 4;
        int lastMapFile = totalMapNum % 4;
        if(lastMapFile > 0)
        {
            totalPages += 1;
        }

        for(int i = 0; i < totalPages; i++)
        {
            Page_Toggle = Instantiate(Toggle_Page) as GameObject;
            Page_Toggle.transform.GetChild(0).GetComponent<Text>().text = (i+1).ToString();
            Page_Toggle.transform.parent = Toggle_PageGroup.transform;
            Page_Toggle.transform.GetComponent<Button>().onClick.AddListener(testClick);
        }

    }

    public void testClick()
    {
        //기존에 있는 애들을 다 지워야함. Destroy;
        //Debug.Log(PlayerPrefs.GetInt("currentPage"));
        currentPage = PlayerPrefs.GetInt("currentPage");
        search();
    }



    public void OnDropdownEvent(int index)
    {
        chooseSearchType = index;
        //Debug.Log(chooseSearchType);
        PlayerPrefs.SetInt("dropBox", index);
    }

    public void buttonSettingsOn()
    {
        Panel_settings.SetActive(true);
    }

    public void PanelSettingsOff()
    {
        Panel_settings.SetActive(false);
    }

    public void logOut()
    {
        SceneManager.LoadScene("loginMain");
        PlayerPrefs.SetInt("autoLogin", 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        ExitPanel.SetActive(false);
        BackgroundMusic = GameObject.FindGameObjectWithTag("mainBGM");
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); //배경음악 저장해둠
        dropdown.onValueChanged.AddListener(OnDropdownEvent);
        Panel_settings.SetActive(false);
        Toggle_popular.onValueChanged.AddListener(delegate { search(); });
        //LoadfilestoPanel();
        searchNull(1);
    }
    public void bgmON()
    {
        backmusic.Play();
        PlayerPrefs.SetInt("mainBGM", 0);
        //Debug.Log("mainBGM on");
    }

    public void bgmOff()
    {
        backmusic.Pause();
        PlayerPrefs.SetInt("mainBGM", 1);
    }

    void Update()
    {
        //안드로이드인 경우
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape)) // 뒤로가기 키 입력
            {
                //if (ExitPanel.activeSelf) // 판넬 켜져있으면
                //{
                //    ExitPanel.SetActive(false);
                //}
                //else
                //{
                    ExitPanel.SetActive(true);
                    int canvasChildCount = canvas.transform.childCount;
                    if (canvasChildCount > canvasChild)
                    {
                        for(int i = canvasChild; i < canvasChildCount; i++)
                        {
                            canvas.transform.GetChild(i).gameObject.SetActive(false);
                        }
                    }
                //}
            }
        }
    }

}
