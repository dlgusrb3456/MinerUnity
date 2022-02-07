using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class makeMap : MonoBehaviour
{

    //사용 이미지.
    public Sprite image_grass;
    public Sprite image_start;
    public Sprite image_end;
    public Sprite image_block;



    //프리팹들
    public GameObject obstacle_prefab;  //장애물 프리팹.
    public GameObject Panel_maps; //움직여야할 판넬.
    public GameObject Panel_saveComplete;

    //로딩화면
    public GameObject testPanel;
    private float progress = 0.0f;
    public Image circleProgress;

    public GameObject Panel_settings;
    public GameObject Panel_prevent;
    public GameObject Panel_outCheck;
    public GameObject Panel_validationCheck;


    private GameObject obstacle;
    string selectedFileName = "";
    private float zoomSpeed = 0.01f;

    private static string mapCompressionBase64String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
    //UI 토글들.
    public Toggle toggle_design;
    public Toggle toggle_start;
    public Toggle toggle_end;

    //크기.
    int xSize = 0;
    int ySize = 0;

    //배열;
    int[,] mapArr;
    //public Text Text_status; // => 프리팹의 자식으로 찾을듯.


    // 0: 길
    // 1: 장애물
    // 2: 도착
    // 3: 출발
    // 4: 테두리
    //44444
    //41114
    //41114
    //41114
    //44444

    //작은: 20 20 => 22 22 => 꼭지점 좌표 고정
    //중간: 50 50 => 52 52 => 꼭지점 좌표 고정
    //큰:   100 100 => 102 102 => 꼭지점 좌표 고정

    //줌인: 2.5
    //줌 아웃: 작은: 0.7 / 중간: 0.5 / 큰: 0.3;

    public void toggleChanged()
    {
        toggle_design.onValueChanged.AddListener(delegate {
            ValueChanged();
        });

        toggle_start.onValueChanged.AddListener(delegate {
            ValueChanged();
        });
        toggle_end.onValueChanged.AddListener(delegate {
            ValueChanged();
        });

    }

    void ValueChanged()
    {
        //Debug.Log("its done");
        if (toggle_design.isOn)
        {
            PlayerPrefs.SetInt("Toggle", 1);
        }
        else if (toggle_start.isOn)
        {
            PlayerPrefs.SetInt("Toggle", 2);
        }
        else if (toggle_end.isOn)
        {
            PlayerPrefs.SetInt("Toggle", 3);
        }
        else
        {
            PlayerPrefs.SetInt("Toggle", 0);
        }

    }

    void startValueChanged(Toggle change)
    {
        if (PlayerPrefs.GetInt("Toggle") == 2)
        {
            PlayerPrefs.SetInt("Toggle", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Toggle", 2);
        }
    }

    void endValueChanged(Toggle change)
    {
        if (PlayerPrefs.GetInt("Toggle") == 3)
        {
            PlayerPrefs.SetInt("Toggle", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Toggle", 3);
        }
    }


    public void arrToMap()
    {
      
        Map.loadLocalMaps();
        string[] mapSize = new string[2];
        selectedFileName = PlayerPrefs.GetString("DesSelectedFile");
        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            if (Map.localMaps[i].name == selectedFileName)
            {
                mapSize = Map.localMaps[i].mapSize.Split('X');
            }
        }

        xSize = Convert.ToInt32(mapSize[0]);
        ySize = Convert.ToInt32(mapSize[1]);
        Panel_maps.GetComponent<GridLayoutGroup>().constraintCount = xSize;
        mapArr = new int[xSize, ySize];

        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            if (Map.localMaps[i].name == selectedFileName)
            {
                mapArr = decodeMapData(Map.localMaps[i], xSize, ySize);
                break;
            }
        }

        
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                obstacle = Instantiate(obstacle_prefab);
                obstacle.transform.parent = Panel_maps.transform;
                obstacle.transform.GetChild(0).GetComponent<Text>().text = mapArr[i, j].ToString();
                obstacle.transform.GetChild(1).GetComponent<Text>().text = i.ToString() + "," + j.ToString();
                obstacle.transform.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                //obstacle.transform.GetComponent<RectTransform>().localScale.y = 1;
                if (mapArr[i, j] == 0)
                {
                    obstacle.GetComponent<Image>().color = Color.clear;
                }
                else if (mapArr[i, j] == 1)
                {
                    obstacle.GetComponent<Image>().sprite = image_grass;
                    obstacle.GetComponent<Image>().color = new Color32(225, 225, 225, 225);
                }
                else if (mapArr[i, j] == 2)
                {
                    obstacle.GetComponent<Image>().sprite = image_end;
                    obstacle.GetComponent<Image>().color = new Color32(225, 225, 225, 225);
                }
                else if (mapArr[i, j] == 3)
                {
                    obstacle.GetComponent<Image>().sprite = image_start;
                    obstacle.GetComponent<Image>().color = new Color32(225, 225, 225, 225);
                }
                else if (mapArr[i, j] == 4)
                {
                    obstacle.GetComponent<Image>().sprite = image_block;
                    obstacle.GetComponent<Image>().color = new Color32(225, 225, 225, 225);
                }
            }
        }

    }

    public void clickPlay()
    {
        if (checkValidation())
        {
            mapToArrSave();
            PlayerPrefs.SetString("playMode", "Design");
            SceneManager.LoadScene("InGamePlay");
        }
        else
        {
            Panel_validationCheck.SetActive(true);
            Debug.Log("validation error");
        }
    }

    public bool checkValidation()
    {
        // 2: 도착
        // 3: 출발
        Transform child = null;
        int childCount = Panel_maps.transform.childCount;
        int startCount = 0;
        int endCount = 0;
        for (int i = 0; i < childCount; i++)
        {
            child = Panel_maps.transform.GetChild(i);
            int getStatus = Convert.ToInt32(child.transform.GetChild(0).GetComponent<Text>().text);
            if(getStatus == 2)
            {
                endCount++;
            }
            else if(getStatus == 3)
            {
                startCount++;
            }
        }
        if(startCount==1 && endCount == 1)
        {
            return true;
        }
        return false;
    }

    public void mapToArrSave()
    {
        testPanel.SetActive(true);
        Transform child = null;
        int childCount = Panel_maps.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            child = Panel_maps.transform.GetChild(i);
            int getStatus = Convert.ToInt32(child.transform.GetChild(0).GetComponent<Text>().text);
            //Debug.Log(getStatus);
            //Debug.Log(child.transform.GetChild(1).GetComponent<Text>().text);
            string[] temp = child.transform.GetChild(1).GetComponent<Text>().text.Split(',');

            int xpos = Convert.ToInt32(temp[0]);
            int ypos = Convert.ToInt32(temp[1]);
            mapArr[xpos, ypos] = getStatus;
        }

        string mapInfo = encodeMapData(mapArr);
        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            if (Map.localMaps[i].name == selectedFileName)
            {
                Map.localMaps[i].mapData = mapInfo;
                Map.saveToJson(Map.localMaps[i]);
                //SceneManager.LoadScene("InGameDesign");
            }
        }
        testPanel.SetActive(false);
        Panel_saveComplete.SetActive(true);
    }

    public int[,] decodeMapData(Map map, int height, int width)
    {

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

        return rl;
    }
    public string encodeMapData(int[,] mapDataArray)
    {

        string raw = "";
        int height = mapDataArray.GetLength(0);
        int width = mapDataArray.GetLength(1);
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width / 2; j++)
            {
                int sum = 0;
                sum += mapDataArray[i, j * 2] << 3;
                sum += mapDataArray[i, j * 2 + 1];
                raw += mapCompressionBase64String[sum];
            }

        return raw;
    }

    public void clickSettings()
    {
        Panel_settings.SetActive(true);
        Panel_prevent.SetActive(true);
    }

    public void closeSettings()
    {
        Panel_settings.SetActive(false);
        Panel_prevent.SetActive(false);
    }

    public void closeSave()
    {
        Panel_saveComplete.SetActive(false);
    }

    public void outPage()
    {
        //SceneManager.LoadScene("mainDesign");
        Panel_outCheck.SetActive(true);
    }

    public void closeOutCheck()
    {
        Panel_outCheck.SetActive(false);
    }

    public void noSave()
    {
        SceneManager.LoadScene("mainDesign");
    }

    public void yesSave()
    {
        mapToArrSave();
        SceneManager.LoadScene("mainDesign");
    }
    public void confirmValidation()
    {
        Panel_validationCheck.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
      
        toggleChanged();
        arrToMap();
        PlayerPrefs.SetInt("Toggle", 0);
        //obstacle_prefab.SetActive(true);
        Panel_settings.SetActive(false);
        Panel_prevent.SetActive(false);
        Panel_outCheck.SetActive(false);
        Panel_validationCheck.SetActive(false);
        Panel_saveComplete.SetActive(false);
        testPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        //줌인 줌아웃
        if(Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0); //첫번째 손가락 터치를 저장
            Touch touchOne = Input.GetTouch(1); //두번째 손가락 터치를 저장

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition; //deltaPosition는 이동방향 추적할 때 사용
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; //magnitude는 두 점간의 거리 비교(벡터)
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            float moveSpeed = -deltaMagnitudeDiff * zoomSpeed;
            if(Panel_maps.transform.localScale.x + moveSpeed > 0.15)
            {
                Panel_maps.transform.localScale += new Vector3(moveSpeed, moveSpeed, 0);
            }
           
        }
        //로딩화면 돌아가기
        if (testPanel.activeSelf == true)
        {
            progress += 0.3f * Time.deltaTime;
            if (progress > 1)
            {
                progress = 0;
            }
            circleProgress.fillAmount = progress;
        }

        //뒤로가기
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape)) // 뒤로가기 키 입력
            {
                if (Panel_settings.activeSelf) // 판넬 켜져있으면
                {
                    Panel_settings.SetActive(false);
                    Panel_prevent.SetActive(false);
                }
                else
                {
                    Panel_settings.SetActive(true);
                    Panel_prevent.SetActive(true);
                }
            }
        }
    }
}