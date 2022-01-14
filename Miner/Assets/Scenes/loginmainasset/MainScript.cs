using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            if (ID.text == "dlgusrb3456@naver.com" && PW.text == "?sr06468sr") //임의로 지정해준 아이디, 비밀번호 => api 불러와서 성공하면 됨.
            {
                //_notice.SUB("로그인 성공");
                Debug.Log("login_Success");
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
            }
            else
            {

                // _notice.SUB("로그인 실패");
                Debug.Log("login_failed");
                LoginExceptiontxt.text = "아이디 또는 비밀번호가 일치하지 않습니다";
                LoginExceptiontxt.color = Color.red;
                Invoke("changeColorSuccess", 2f);


            }
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
