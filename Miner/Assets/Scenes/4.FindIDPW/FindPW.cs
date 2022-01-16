using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FindPW : MonoBehaviour
{

    public InputField InputField_email;
    public InputField InputField_emailCode;

    public Text Text_email;
    public Text Text_emailCode;


    private string checkID = "Miner";
    private string code = "Miner";
    // Start is called before the first frame update

    public void FindIDButton()
    {
        SceneManager.LoadScene("FindID");
    }

    public void FindPWButton()
    {
        SceneManager.LoadScene("FindPW");

    }

    public void goBackLogin()
    {
        SceneManager.LoadScene("loginMain");
    }


    public void sendCode()
    {
        //인풋필드의 이메일 전송 , 
        //1. 정상적으로 성공한 경우 , 전송한 코드 받아와서 저장하기
        //2. 해당 이메일이 없는 경우 => 가입 정보가 없습니다.
        //3. 이메일 형식을 지키지 않은경우 => 이메일 형식이 올바르지 않습니다.

        if(InputField_email.text == "dlgusrb3456@naver.com")
        {
            checkID = InputField_email.text;
            code = "1234";
            Text_email.color = Color.clear;
        }
        else if(InputField_email.text == "dlgusrn@naver.co")
        {
            Text_email.text = "회원 정보가 존재하지 않습니다";
            Text_email.color = Color.red;
        }
        else
        {
            Text_email.text = "이메일 형식이 올바르지 않습니다.";
            Text_email.color = Color.red;
        }

        
    }

    public void checkCode()
    {
        if(InputField_emailCode.text == code)
        {
            //비밀번호 재설정 화면으로 넘어가기
            Text_emailCode.color = Color.clear;
            PlayerPrefs.SetString("modifyID", checkID);
            SceneManager.LoadScene("modifyPW");
        }
        else
        {
            Text_emailCode.text = "인증번호가 일치하지 않습니다";
            Text_emailCode.color = Color.red;
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
