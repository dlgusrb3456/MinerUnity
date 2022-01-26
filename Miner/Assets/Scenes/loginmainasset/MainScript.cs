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
using System.Net.Http;
using System;
using System.Threading.Tasks;

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


    void Awake()
    {
  
    }

    public void moveRegister()
    {
        SceneManager.LoadScene("register");
    }
    public void moveFindID()
    {
        SceneManager.LoadScene("FindID");
    }


    void Error(string errorCode)
    {
        switch(errorCode)
        {
            case "duplicate":
                print("중복된 사용자 아이디입니다.");
                break;
            case "badauthorized":
                print("잘못된 사용자 아이디 혹은 비밀번호입니다.");
                break;
            default:
                break;
        }

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
                    Debug.Log(isFailed[3]); //틀렸을때 나오는 오류.
                }
                else
                {
                    string[] nickNames = words[5].Split('"');
                    Debug.Log(nickNames[3]); //성공시 닉네임
                    PlayerPrefs.SetString("nickName", nickNames[3]);
                    Debug.Log(PlayerPrefs.GetString("nickName"));
                    if (toggle.isOn)
                    {
                        PlayerPrefs.SetInt("autoLogin", 1);
                        Debug.Log(PlayerPrefs.GetInt("autoLogin"));
                    }
                    else
                    {
                        PlayerPrefs.SetInt("autoLogin", 0);
                        Debug.Log(PlayerPrefs.GetInt("autoLogin"));
                    }
                    SceneManager.LoadScene("mainDesign");

                }


            }
        }
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



            //if (ID.text == "dlgusrb3456@naver.com" && PW.text == "?sr06468sr") //임의로 지정해준 아이디, 비밀번호 => api 불러와서 성공하면 됨.
            //{
            //    //_notice.SUB("로그인 성공");
            //    Debug.Log("login_Success");
            //    if (toggle.isOn)
            //    {
            //        PlayerPrefs.SetInt("autoLogin", 1);
            //        Debug.Log(PlayerPrefs.GetInt("autoLogin"));
            //    }
            //    else
            //    {
            //        PlayerPrefs.SetInt("autoLogin", 0);
            //        Debug.Log(PlayerPrefs.GetInt("autoLogin"));
            //    }
            //    SceneManager.LoadScene("mainDesign");
            //}
            //else
            //{

            //    // _notice.SUB("로그인 실패");
            //    Debug.Log("login_failed");
            //    LoginExceptiontxt.text = "아이디 또는 비밀번호가 일치하지 않습니다";
            //    LoginExceptiontxt.color = Color.red;
            //    Invoke("changeColorSuccess", 2f);


            //}
            //api 호출하고 결과값 받기
            //로그인에 성공하면 => 자동로그인 값 반영 => 화면 이동

            //실퍄하면 "잘못된 사용자 아이디 혹은 비밀번호입니다." 알리기.
        }

    }

    void changeColorSuccess()
    {
        LoginExceptiontxt.color = Color.clear;
        Debug.Log("changeColor");
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
