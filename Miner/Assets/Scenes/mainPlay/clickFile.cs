using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class userInfo
{
    public string nickName { get; set; }
    public int playTime { get; set; }
    public int rank = 0;
    public userInfo(string nickName, int playTime)
    {
        this.nickName = nickName;
        this.playTime = playTime;
        
    }

    public override string ToString()
    {
        string ranking = rank.ToString();
        string pltime = (playTime / 60).ToString() + "m " + (playTime % 60).ToString() + "s";
        return ranking+$"등 {nickName}/ 플레이 시간 : "+pltime;
    }
}

public class getPlayInfoClass
{
    public string mapName;
    public string editorName;
}

public class clickFile : MonoBehaviour
{

    public GameObject Panel_fileInfo;
    public GameObject Button_PlayFile;
    public GameObject Text_UserInfo;
    public GameObject Panel_getPW;


    private GameObject fileInfo;
    private GameObject Text_file;
    private GameObject getPWPanel;

    //private InputField getPWInputField;
    public Text Text_puPri;
    public Text Text_names;

    //private string editorNames = PlayerPrefs.GetString("");
    private string miroName = "";
    private string editorName = "";
    private string isPrivate = "";
    private string size = "";
    private string playTime = "";
    private string pw = "";

    //playerCount
    private int mapCount = 0;



    public void clickPlay()
    {
        if(isPrivate == "private")
        {
            getPWPanel = Instantiate(Panel_getPW) as GameObject;
            getPWPanel.transform.parent = fileInfo.transform.parent;


            getPWPanel.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(clickComplete_getPW);
            getPWPanel.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(clickClose_getPW);

            getPWPanel.transform.position = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0);
        }
        else
        {
            //플레이화면으로 이동.
            Debug.Log("go to IngamePlay");
            PlayerPrefs.SetString("editorName", editorName);
            PlayerPrefs.SetString("mapName", miroName);
            SceneManager.LoadScene("InGamePlay");
        }
    }

    public void clickComplete_getPW()
    {
        string str = getPWPanel.transform.GetChild(2).GetComponent<InputField>().text;
        if(str == pw)
        {
            //플래이화면으로 이동.
            PlayerPrefs.SetString("editorName", editorName);
            PlayerPrefs.SetString("mapName", miroName);
            SceneManager.LoadScene("InGamePlay");
            Debug.Log("complete");
        }
        else if(str.Length != 4)
        {
            getPWPanel.transform.GetChild(3).GetComponent<Text>().text = "비밀번호는 4자리 숫자입니다.";
            getPWPanel.transform.GetChild(3).GetComponent<Text>().color = Color.red;
        }
        else if(str != pw)
        {
            getPWPanel.transform.GetChild(3).GetComponent<Text>().text = "비밀번호를 확인해주세요";
            getPWPanel.transform.GetChild(3).GetComponent<Text>().color = Color.red;
        }
    }

    public void clickClose_info()
    {
        Destroy(fileInfo);
    }

    public void clickClose_getPW()
    {
        Destroy(getPWPanel);
    }

    IEnumerator getInfosAPI(string mapNames, string editorNames)
    {
        string URL = "https://miner22.shop/miner/playmaps/loadPlayInfo";
        //Debug.Log("miroNames: "+ mapNames);
        //Debug.Log("editorNames: "+ editorNames);

        getPlayInfoClass myObject = new getPlayInfoClass { mapName = mapNames , editorName = editorNames };

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
                    mapCount = ((words.Length) / 3) - 2;
                    makeLists(mapCount, words, miroName);
                }

                else
                {
                  
                }

            }
        }
    }

    public void makeLists(int mapCounts,string[] words,string miroNames)
    {
        fileInfo = Instantiate(Panel_fileInfo) as GameObject;
        fileInfo.transform.parent = Button_PlayFile.transform.parent.parent;
        fileInfo.transform.GetChild(0).GetComponent<Text>().text = miroNames;

        string[] tmpPasswordArr = words[3].Split(':');
        string[] tmpSizeArr = words[4].Split(':');
        string[] tmpAvgArr = words[5].Split(':');
        string tmpPassword = tmpPasswordArr[tmpPasswordArr.Length - 1];
        string tmpSize = tmpSizeArr[tmpSizeArr.Length - 1];
        int avgTime = Convert.ToInt32(tmpAvgArr[tmpAvgArr.Length - 1]);
        int avgMinute = avgTime / 60;
        int avgSecond = avgTime % 60;
        string tmpAVG = "평균: " + avgMinute.ToString() + "m " + avgSecond.ToString() + "s";
        if (tmpPassword.Length > 2)
        {
            isPrivate = "private";
            pw = tmpPassword;
            Debug.Log("pw: " + pw);
            fileInfo.transform.GetChild(1).GetComponent<Text>().text = "Private";
        }
        if(tmpSize == "0")
        {
            fileInfo.transform.GetChild(2).GetComponent<Text>().text = "/작음";
        }
        else if(tmpSize == "1")
        {
            fileInfo.transform.GetChild(2).GetComponent<Text>().text = "/중간";
        }
        else if(tmpSize == "2")
        {
            fileInfo.transform.GetChild(2).GetComponent<Text>().text = "/큼";
        }

        fileInfo.transform.GetChild(3).GetComponent<Text>().text = tmpAVG;
        List<userInfo> tmpusers = new List<userInfo>();
        for(int i = 0; i < mapCounts; i++)
        {
            string[] info = words[7 + (i * 3)].Split('"');
            string playerNames = info[info.Length - 2];
            //Debug.Log("playerName = " + playerNames);
            info = words[8 + (i * 3)].Split('"');
            string playTimes = info[info.Length - 2];
            //Debug.Log("playTimes = " + playTimes);
            string[] timeToInt = playTimes.Split(':');
            int playSec = Convert.ToInt32(timeToInt[0]) * 3600 + Convert.ToInt32(timeToInt[1]) * 60 + Convert.ToInt32(timeToInt[2]);
            tmpusers.Add(new userInfo(playerNames,playSec));
        }
        tmpusers = tmpusers.OrderBy(x => x.playTime).ToList();
        fileInfo.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(clickClose_info);
        fileInfo.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(clickPlay);
        Transform contentPanel = fileInfo.transform.GetChild(6).transform.GetChild(0).transform.GetChild(0);
        int ranks = 0;
        foreach (var userInfo in tmpusers)
        {
            ranks++;
            Text_file = Instantiate(Text_UserInfo) as GameObject;
            Text_file.transform.parent = contentPanel.transform;
            userInfo.rank = ranks;
            Text_file.transform.GetComponent<Text>().text = userInfo.ToString();
        }
        fileInfo.transform.position = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0);
    }


    public void fileOnClick()
    {
        //api 호출을 통해 다음의 정보를 가져와야함.
        //1. 미로명 2. private, public 상태, 3. 크기정보 , 4. 평균 플레이 타임, 5. 플레이한 사람들의 정보 (닉네임과 플레이타임) => 빠른 순서대로 정렬해야함.
        string[] tmpInfos = Text_names.text.Split('/');
        miroName = tmpInfos[0];
        editorName = tmpInfos[1];
        StartCoroutine(getInfosAPI(miroName, editorName));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
