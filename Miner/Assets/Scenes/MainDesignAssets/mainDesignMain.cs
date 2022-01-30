using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainDesignMain : MonoBehaviour
{

    ////톱니바퀴 
    public GameObject Panel_settings;
    public Toggle toggle_bgmOn;
    public Toggle toggle_bgnOff;

    ////추가
    public GameObject Panel_getmiroName;
    public InputField InputFieldmiroName;
    public Text Text_nameAlert;
    public GameObject Panel_getmiroSize;
    public Toggle toggle_small;
    public Toggle toggle_middle;
    public Toggle toggle_large;
    private string newMiroName = "";


    //스크롤 content 패널
    public GameObject contentPanel;

    //파일 버튼 프리팹
    public GameObject Button_mapPrefab;
    public Sprite image_lock;
    public Sprite image_unLock;

    //점점점 패널
    public GameObject Panel_setFileShare;
    public GameObject Panel_setFileNotShare;
    private string selectedFileName = "";


    //점점점=> 삭제 확인 패널
    public GameObject Panel_deleteReal;
    public Text deleteCheckText;

    private string smallSize = "kkkkkkkkkkkhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMhJJJJJJJJJMkkkkkkkkkkk";
    private string middleSize = "kkkkkkkkkkkkkkkkkkkkkkkkkkhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJMkkkkkkkkkkkkkkkkkkkkkkkkkk";
    private string largeSize = "kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMhJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJJMkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk";


    public void Button_settings()
    {
        Panel_settings.transform.position = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0);
        Panel_settings.gameObject.SetActive(true);
        
    }

    private void Toggle_BGMOn(bool value)
    {
        if (value)
        {
            PlayerPrefs.SetInt("mainBGM", 1);
            Debug.Log("mainBGM on");
        }
        else
        {
            PlayerPrefs.SetInt("mainBGM", 0);
            Debug.Log("mainBGM off");
        }
    }

    private void Toggle_BGMOff()
    {

    }

    public void logOut()
    {
        SceneManager.LoadScene("loginMain");
        PlayerPrefs.SetInt("autoLogin", 0);
    }
    public void loadFile()
    {
        Transform[] childList = contentPanel.GetComponentsInChildren<Transform>();
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

        Map.reloadLocalMaps();
        Debug.Log(Map.localMaps.Count);
        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            GameObject file = Instantiate(Button_mapPrefab) as GameObject;
            file.transform.parent = contentPanel.transform;
            Debug.Log(file.transform.GetChild(0).name);
            file.transform.GetChild(0).GetComponent<Text>().text = Map.localMaps[i].name;
            file.transform.GetChild(1).GetComponent<Text>().text = Map.localMaps[i].lastClearDate;
            //file.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(dotdotdot);

            if (i % 2 == 0) //나중에 삭제해야함
            {
                Map.localMaps[i].isShared = true;
            }


            if (Map.localMaps[i].isShared)
            {
                file.transform.GetChild(3).GetComponent<Text>().text = "공유 O";
                if (Map.localMaps[i].isPrivate)
                {
                    file.transform.GetChild(4).GetComponent<Image>().sprite = image_lock;
                }
                else
                {
                    file.transform.GetChild(4).GetComponent<Image>().sprite = image_unLock;
                }

            }
            else
            {
                file.transform.GetChild(3).GetComponent<Text>().text = "공유 X";
                file.transform.GetChild(4).GetComponent<Image>().color = Color.clear;
            }

        }
    }
    public void dotdotdot()
    {
        GameObject prefabs = transform.parent.gameObject;
        selectedFileName = prefabs.transform.GetChild(0).GetComponent<Text>().text;
        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            if (Map.localMaps[i].name == selectedFileName)
            {
                if (Map.localMaps[i].isShared)
                {
                    Panel_setFileShare.gameObject.SetActive(true);
                }
                else
                {
                    Panel_setFileNotShare.gameObject.SetActive(true);
                }
                break;
            }

        }
    }

    

    public void startFile()
    {
        //인게임_설계화면으로 이동
        //SceneManager.LoadScene("");
    }


    public void addFile()
    {
        //추가Panel active => 추가버튼 누른경우
        Panel_getmiroName.transform.position = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0);
        Panel_getmiroName.gameObject.SetActive(true);
    }

    public void addFileGetMiroNameConfirm()
    {
        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            if (Map.localMaps[i].name == InputFieldmiroName.text)
            {
                Text_nameAlert.text = "중복되는 미로명입니다";
                Text_nameAlert.color = Color.red;
                return;
            }

        }

        if (InputFieldmiroName.text == "")
        {
            Text_nameAlert.text = "미로명은 필수입니다";
            Text_nameAlert.color = Color.red;
            return;
        }
        if (InputFieldmiroName.text.Length > 10)
        {
            Text_nameAlert.text = "미로명은 10자리이하로 입력해야 합니다";
            Text_nameAlert.color = Color.red;
            return;
        }

        for (int i = 0; i < InputFieldmiroName.text.Length; i++)
        {
            if (InputFieldmiroName.text[i] == ' ')
            {
                Text_nameAlert.text = "미로명에 띄어쓰기는 사용할 수 없습니다";
                Text_nameAlert.color = Color.red;
                return;
            }
        }
        newMiroName = InputFieldmiroName.text;
        InputFieldmiroName.text = "";
        Panel_getmiroName.gameObject.SetActive(false);
        Panel_getmiroSize.transform.position = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0);
        Panel_getmiroSize.gameObject.SetActive(true);
    }

    public void toggleSizeChooseConfirm()
    {
        if (toggle_small.isOn)
        {
            Map.saveToJson(Map.createNew(newMiroName, false, "22X22",smallSize));
        }
        else if (toggle_middle.isOn)
        {
            Map.saveToJson(Map.createNew(newMiroName, false, "52X52",middleSize));
        }
        else if (toggle_large.isOn)
        {
            Map.saveToJson(Map.createNew(newMiroName, false, "102X102",largeSize));
        }

        newMiroName = "";
        Panel_getmiroSize.gameObject.SetActive(false);
        loadFile();
    }

    public void buttonCancle()
    {
        Panel_getmiroName.gameObject.SetActive(false);
        Panel_getmiroSize.gameObject.SetActive(false);
        Panel_settings.gameObject.SetActive(false);
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

    public void reLoad()
    {
        //화면 reload => 새로고침 누른 경우 (될 수 있으면 스크롤만 reload)
        loadFile();
    }


    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("start");
        MinerEnvironment.initEnvironment();
        toggle_bgmOn.onValueChanged.AddListener((value) =>
        {
            Toggle_BGMOn(value);
        });

        loadFile();


    }

    // Update is called once per frame
    void Update()
    {

    }
}