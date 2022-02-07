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
    //카메라
    public Camera MainCamera;

    public GameObject Player;

    public GameObject postionPrefabs;
    private GameObject positions;

    //텍스트 감소;
    public Text textCount;

    private int startCount = 10;
    private int positionCount = 10;


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
        positionCount += 3;
        textCount.text = positionCount.ToString();
    }


    public void ggClick()
    {
        SceneManager.LoadScene("mainPlay");
    }

    public void reGame()
    {
        SceneManager.LoadScene("InGamePlay");
    }

    public void goDesign()
    {
        SceneManager.LoadScene("InGameDesign");
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
