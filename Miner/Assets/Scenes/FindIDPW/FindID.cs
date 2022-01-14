using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FindID : MonoBehaviour
{

    public InputField PhoneNumfield;
    public InputField PhoneCodefield;

    public Text Text_PhoneNumField;
    public Text Text_PhonecodeField;
    public Text Text_UserId;



    public GameObject alertPanel;

    private string checkPhoneNum = "Miner";
    private string code = "1234";


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
        //인풋필드의 전화번호 전송 , 
        //1. 정상적으로 성공한 경우 , 전송한 코드 받아와서 저장하기
        //2. 해당 이메일이 없는 경우 => 가입 정보가 없습니다.
        //3. 이메일 형식을 지키지 않은경우 => 이메일 형식이 올바르지 않습니다.

        if (PhoneNumfield.text == "01033614263")
        {
            checkPhoneNum = PhoneNumfield.text;
            code = "1234";
            Text_PhoneNumField.color = Color.clear;
        }
        else if (PhoneNumfield.text == "0103361426")
        {
            Text_PhoneNumField.text = "회원 정보가 존재하지 않습니다";
            Text_PhoneNumField.color = Color.red;
        }
        else
        {
            Text_PhoneNumField.text = "번호 형식이 올바르지 않습니다.";
            Text_PhoneNumField.color = Color.red;
        }


    }

    public void checkCode()
    {
        if (PhoneCodefield.text == code)
        {
            Text_PhonecodeField.color = Color.clear;
            string getEmail = "dlgu*******@naver.com"; //api 통신을 통해 받아온 email.
            //SceneManager.LoadScene("modifyPW"); => 
            Text_UserId.text = getEmail;
            alertPanel.gameObject.SetActive(true);
        }
        else
        {
            Text_PhonecodeField.text = "인증번호가 일치하지 않습니다";
            Text_PhonecodeField.color = Color.red;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        alertPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
