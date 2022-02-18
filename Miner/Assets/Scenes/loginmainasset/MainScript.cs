using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System;


//오토로그인  PlayerPrefs.SetInt("autoLogin", 1);



[Serializable]
public class loginapiInfo
{
    public string email;
    public string password;
}



public class MainScript : MonoBehaviour
{
    [Header("Login & Register")]
    public InputField ID;
    public InputField PW;
    public Toggle toggle;
    public Text LoginExceptiontxt;

    //bgm;
    GameObject BackgroundMusic;
    AudioSource backmusic;

    public GameObject Panel_loginMainSettings;

    public void moveRegister()
    {
        SceneManager.LoadScene("register");
    }
    public void moveFindID()
    {
        SceneManager.LoadScene("FindID");
    }
    public void moveregiOut()
    {
        SceneManager.LoadScene("regiOut");
    }

    public void closePanel()
    {
        Panel_loginMainSettings.SetActive(false);
    }

    public void privateURL()
    {
        Application.OpenURL("https://miners.netlify.app/privacy");
    }

    IEnumerator loginAPI(string emails, string passwords)
    {
        string URL = "https://miner22.shop/miner/users/login";
        loginapiInfo myObject = new loginapiInfo { email = emails, password = passwords };
        string json = JsonUtility.ToJson(myObject);

        using(UnityWebRequest www = UnityWebRequest.Post(URL, json))
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bytes);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.Send();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string returns = www.downloadHandler.text;
                string[] words = returns.Split(',');
                string[] isSuccess = words[0].Split(':');
                if (isSuccess[1] == "false")
                {
                    string[] isFailed = words[2].Split('"');
                    LoginExceptiontxt.text = isFailed[3];
                    LoginExceptiontxt.color = Color.red;
                    Invoke("changeColorSuccess", 2f);
                    //Debug.Log(isFailed[3]); //틀렸을때 나오는 오류.
                }
                else
                {
                    string[] nickNames = words[5].Split('"');
                    //Debug.Log(nickNames[3]); //성공시 닉네임
                    PlayerPrefs.SetString("nickName", nickNames[3]);
                    //Debug.Log(PlayerPrefs.GetString("nickName"));
                    if (toggle.isOn)
                    {
                        PlayerPrefs.SetInt("autoLogin", 1);
                        //Debug.Log(PlayerPrefs.GetInt("autoLogin"));
                    }
                    else
                    {
                        PlayerPrefs.SetInt("autoLogin", 0);
                        //Debug.Log(PlayerPrefs.GetInt("autoLogin"));
                    }
                    SceneManager.LoadScene("mainDesign");

                }


            }
        }
    }


    public void settingOn()
    {
        Panel_loginMainSettings.SetActive(true);
    }


    public void Login()
    {
        if(ID.text == "" || PW.text == "")
        {
            LoginExceptiontxt.text = "아이디 또는 비밀번호를 입력해주세요";
            LoginExceptiontxt.color = Color.red;
            Invoke("changeColorSuccess", 2f);
        }
        else
        {
            StartCoroutine(loginAPI(ID.text,PW.text));

        }

    }

    void changeColorSuccess()
    {
        LoginExceptiontxt.color = Color.clear;
        //Debug.Log("changeColor");
    }

    void Start()
    {
        BackgroundMusic = GameObject.FindGameObjectWithTag("mainBGM");
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); //배경음악 저장해둠
        Panel_loginMainSettings.SetActive(false);
        
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
        //Debug.Log("mainBGM off");
    }

    void Update()
    {

    }
}
