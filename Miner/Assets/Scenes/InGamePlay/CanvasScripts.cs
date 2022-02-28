using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;




public class CanvasScripts : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Panel_settings;
    public GameObject Panel_preventSettings;
    public GameObject Panel_preventEndsDesign;
    public GameObject Panel_preventEnds;
    //카메라
    public Camera MainCamera;

    public GameObject Player;
    public GameObject startPosition;

    //캔버스
    public GameObject canvas;

    public GameObject postionPrefabs;
    private GameObject positions;

    //텍스트 감소;
    public Text textCount;

    private int startCount = 10;
    private int positionCount = 10;
    private int addCounts = 5;
   public void setStartPositionCount()
   {
        int size = PlayerPrefs.GetInt("InmapSize");
        if(size == 1)
        {
            startCount = 3;
            positionCount = 3;
            addCounts = 3;
        }
        else if(size == 2)
        {
            startCount = 7;
            positionCount = 7;
            addCounts = 7;
        }
        else if(size == 3)
        {
            startCount = 15;
            positionCount = 15;
            addCounts = 15;
        }
        else
        {
            Debug.Log("error");
        }
        textCount.text = startCount.ToString();
   }

    public void closeEndDesign()
    {
        Timer.startTimer();
        Panel_preventEndsDesign.SetActive(false);
    }
    public void positionClicked()
    {
        if (positionCount > 0)
        {
            positionCount--;
            positions = Instantiate(postionPrefabs) as GameObject;
            //positions.transform.GetChild(0).GetComponent<Text>().text = (startCount-positionCount).ToString();
            positions.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, 0);
            textCount.text = positionCount.ToString();
        }
    }

    public void addCount()
    {
        positionCount += addCounts;
        textCount.text = positionCount.ToString();
    }


    public void ggClick()
    {
        //SceneManager.LoadScene("mainPlay");

        PlayerPrefs.SetInt("loadMode", 6);
        SceneManager.LoadScene("loadingScene");
    }

    public void reGame()
    {
        Timer.resetTimer();
        Player.transform.position = startPosition.transform.position;
        setStartPositionCount();
        PlayerAction.success = 0;
        Panel_preventSettings.SetActive(false);
        Panel_preventEndsDesign.SetActive(false);
        Panel_preventEnds.SetActive(false);
        GameObject [] obj = GameObject.FindGameObjectsWithTag("Bread");
        for(int i = 0; i < obj.Length; i++)
        {
            Destroy(obj[i]);
        }
        //Destroy(canvas.FindWithTag("Bread"));
        Timer.startTimer();
    }

    public void goDesign()
    {
        PlayerPrefs.SetInt("loadMode", 3);
        SceneManager.LoadScene("loadingScene");
        //SceneManager.LoadScene("InGameDesign");
    }

    public void clickSettings()
    {
        Timer.pauseTimer();
        if (PlayerPrefs.GetString("playMode") == "Play")
        {
            Panel_preventSettings.SetActive(true);
        }
        else if(PlayerPrefs.GetString("playMode") == "Design")
        {
            Panel_preventEndsDesign.SetActive(true);
        }
            
    }

    public void closeSettings()
    {
        Timer.startTimer();
        if (PlayerPrefs.GetString("playMode") == "Play")
        {
            Panel_preventSettings.SetActive(false);
        }
        else if (PlayerPrefs.GetString("playMode") == "Design")
        {
            Panel_preventEndsDesign.SetActive(false);
        }
    }

    void Start()
    {
        //시작할때 position count size기준으로 변경;
        MainCamera.orthographicSize = 3.0f;
        setStartPositionCount();
        Panel_preventSettings.SetActive(false);
        Panel_preventEndsDesign.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape)) // 뒤로가기 키 입력
            {

                if (PlayerPrefs.GetString("playMode") == "Play")
                {
                    if (Panel_preventSettings.activeSelf) // 판넬 켜져있으면
                    {
                        Panel_preventSettings.SetActive(false);
                        Panel_preventSettings.SetActive(false);
                    }
                    else
                    {
                        Panel_preventSettings.SetActive(true);
                        Panel_preventSettings.SetActive(true);
                    }
                }
                else if (PlayerPrefs.GetString("playMode") == "Design")
                {
                    if (Panel_preventEndsDesign.activeSelf) // 판넬 켜져있으면
                    {
                        Panel_preventEndsDesign.SetActive(false);
                        Panel_preventEndsDesign.SetActive(false);
                    }
                    else
                    {
                        Panel_preventEndsDesign.SetActive(true);
                        Panel_preventEndsDesign.SetActive(true);
                    }
                }
                
            }
        }
    }
}
