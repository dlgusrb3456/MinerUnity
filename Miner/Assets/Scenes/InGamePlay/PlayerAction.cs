using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
public class sendClearInfoClass
{
    public string mapName;
    public string editorName;
    public string playerName;
    public string playTime;
}


public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private VirtualJoystick virtualJoystick;
    public float moveSpeed = 5;
    SpriteRenderer spriteRenderer;

    private Rigidbody2D rigid2D;

    //종료 이벤트;
    public Text TimerText;
    public GameObject Panel_preventEnds;
    public GameObject Panel_preventEndsDesign;
    public Text rank;
    //충돌 이벤트;
    public GameObject SpriteEnd;

    public void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    public string TimerTextChange()
    {
        string[] timerTextArr = TimerText.text.Split(':');
        int min = Convert.ToInt32(timerTextArr[0]);
        int hour = min / 60;
        int sec = Convert.ToInt32(timerTextArr[1]);
        min = min % 60;
        string changeTime = hour.ToString() + ":" + min.ToString() + ":" + sec.ToString();
        Debug.Log(changeTime);
        return changeTime;
    }


    IEnumerator sendClearInfoAPI()
    {
        string URL = "https://miner22.shop/miner/playmaps/savePlayInfo";
        Debug.Log("miroNames: " + PlayerPrefs.GetString("mapName"));
        Debug.Log("editorNames: " + PlayerPrefs.GetString("editorName"));
        string times = TimerTextChange();
        sendClearInfoClass myObject = new sendClearInfoClass {mapName = PlayerPrefs.GetString("mapName"), editorName = PlayerPrefs.GetString("editorName"), playerName = PlayerPrefs.GetString("nickName"),playTime = times };

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

                if (returncode[1] == "1000")
                {
                    string[] myRankarr = words[4].Split(':');
                    string[] myRankarr2 = myRankarr[myRankarr.Length - 1].Split('}');
                    string myRank = myRankarr2[0];
                    rank.text = "나의 등수: " + myRank+"등!!";
                }

                else
                {
                    rank.text = "클리어 정보가 입력되지 않았습니다. \n 인터넷을 연결해주세요.";
                }

            }
        }
    }
    private void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        Panel_preventEnds.SetActive(false);
    }

    private void Update()
    {
        float x = virtualJoystick.Horizontal();
        float y = virtualJoystick.Vertical();

        if(x != 0 || y != 0)
        {
            //transform.position += new Vector3(x, y, 0) * moveSpeed * Time.deltaTime; 스리꺼
            rigid2D.velocity = new Vector3(x, y, 0) * moveSpeed;
        }

        //방향전환
        if (x < 0)
            spriteRenderer.flipX = false;
        if (x > 0)
            spriteRenderer.flipX = true;

    }

  
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == SpriteEnd) // 도착지점에 충돌하면
        {
            // 게임 정지, 종료 > 게임 종료(플레이 시간 등 정보 들어간)판넬 뜨게
            
            Timer.pauseTimer();
            TimerTextChange();
            
            if (PlayerPrefs.GetString("playMode") == "Play")
            {
                Panel_preventEnds.SetActive(true);
                StartCoroutine(sendClearInfoAPI());
            }
            else if (PlayerPrefs.GetString("playMode") == "Design")
            {
                Panel_preventEndsDesign.SetActive(true);
            }
        }
    }

}
