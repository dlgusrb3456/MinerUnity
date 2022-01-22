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
    private GameObject fileInfo;
    public Text Text_puPri;


    



    public void fileOnClick()
    {
        //api 호출을 통해 다음의 정보를 가져와야함.
        //1. 미로명 2. private, public 상태, 3. 크기정보 , 4. 평균 플레이 타임, 5. 플레이한 사람들의 정보 (닉네임과 플레이타임) => 빠른 순서대로 정렬해야함.
        string miroName = "미로 1";
        string isPrivate = "private";
        string size = "작음";
        string playTime = "1m 35s";
        List<userInfo> users = new List<userInfo>();
        users.Add(new userInfo ("라큼",25));
        users.Add(new userInfo("라캄", 15));
        users.Add(new userInfo("라콤", 5));
        users.Add(new userInfo("라쿰", 85));

        users = users.OrderBy(x => x.playTime).ToList();
        foreach(var userInfo in users)
        {
            Debug.Log(userInfo.playTime);
        }

        fileInfo = Instantiate(Panel_fileInfo) as GameObject;
        fileInfo.transform.parent = Button_PlayFile.transform.parent;
        fileInfo.transform.position = new Vector3(0,0,0);
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
