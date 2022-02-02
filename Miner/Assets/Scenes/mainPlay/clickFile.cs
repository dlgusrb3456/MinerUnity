using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

class userInfo
{
    public string nickName { get; set; }
    public int playTime { get; set; }

    public userInfo(string nickName, int playTime)
    {
        this.nickName = nickName;
        this.playTime = playTime;
        
    }

    public override string ToString()
    {
        return $"--nickName : {nickName}, \t playTime={playTime}--";
    }
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

    private string miroName = "";
    private string isPrivate = "";
    private string size = "";
    private string playTime = "";
    private string pw = "";

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
        }
    }

    public void clickComplete_getPW()
    {
        string str = getPWPanel.transform.GetChild(2).GetComponent<InputField>().text;
        if(str == pw)
        {
            //플래이화면으로 이동.
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

    public void fileOnClick()
    {
        //api 호출을 통해 다음의 정보를 가져와야함.
        //1. 미로명 2. private, public 상태, 3. 크기정보 , 4. 평균 플레이 타임, 5. 플레이한 사람들의 정보 (닉네임과 플레이타임) => 빠른 순서대로 정렬해야함.
        miroName = "미로 1";
        isPrivate = "private";
        size = "작음";
        playTime = "1m 35s";
        pw = "2222";

        List<userInfo> users = new List<userInfo>();
        users.Add(new userInfo ("라큼",25));
        users.Add(new userInfo("라캄", 15));
        users.Add(new userInfo("라콤", 5));
        users.Add(new userInfo("라쿰", 85));

        users = users.OrderBy(x => x.playTime).ToList();
        foreach(var userInfo in users)
        {
            Debug.Log(userInfo.ToString());
        }
        
        fileInfo = Instantiate(Panel_fileInfo) as GameObject;
        fileInfo.transform.parent = Button_PlayFile.transform.parent.parent;
        fileInfo.transform.GetChild(0).GetComponent<Text>().text = miroName;
        fileInfo.transform.GetChild(1).GetComponent<Text>().text = isPrivate;
        fileInfo.transform.GetChild(2).GetComponent<Text>().text = size;
        fileInfo.transform.GetChild(3).GetComponent<Text>().text = playTime;
        fileInfo.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(clickClose_info);
        fileInfo.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(clickPlay);
        Transform contentPanel = fileInfo.transform.GetChild(6).transform.GetChild(0).transform.GetChild(0);

        foreach (var userInfo in users)
        {
            Text_file= Instantiate(Text_UserInfo) as GameObject;
            Text_file.transform.parent = contentPanel.transform;
            Text_file.transform.GetComponent<Text>().text = userInfo.ToString();
        }

        fileInfo.transform.position = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0);
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
