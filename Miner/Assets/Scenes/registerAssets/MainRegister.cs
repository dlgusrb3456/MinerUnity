using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainRegister : MonoBehaviour
{
    // Start is called before the first frame update

    public InputField IDfield;
    public InputField PWfield;
    public InputField PWCheckfield;
    public InputField PhoneNumfield;
    public InputField PhoneCodefield;

    public Text IDText;
    public Text PWcheckText;
    public Text PWconditionText;
    public Text PWcodeCheckText;
    public Text PhoneNumCheck;
    public Text finalRegister;
    public Text Text_alert_agree;


    public GameObject alertPanel;
    public Toggle toggle;

    private string checkID = "";
    private bool isIDcheck = false;
    private bool PWconfitioncheck = false;
    private bool PWconfirmcheck = false;
    private string checkPhoneNum = "";
    private bool isPhoneCheck = false;
    private string sendCode = "Miner";
    private bool isCodeCheck = false;
    private bool updateStop = false;

    public void IDduplicateCheck()
    {
        //api로 호출하고, 반환값에 따라.
        checkID = IDfield.text; //중복확인을 진행한 이메일 주소 저장
        bool returnAPI = false;
        if (IDfield.text=="dlgusrb3456@naver.com") //성공한 경우 => 사용 가능한 아이디 , 실패한경우 1. 이미 아이디가 있는 경우, 2. 아이디 형식이 이메일 형식이 아닌 경우
        {
            isIDcheck = true;
            IDText.text = "사용 가능한 아이디입니다";
            IDText.color = Color.green;
        }
        else
        {
            isIDcheck = false;
            IDText.text = "아이디가 이미 존재합니다";
            IDText.color = Color.red;
        }

    }

    public void SendCode()
    {
        checkPhoneNum = PhoneNumfield.text;
        bool returnAPI = false;
        if (PhoneNumfield.text!="01033614263") // 형식에 오류있는 경우
        {
            PhoneNumCheck.text = "전화번호 형식이 옳지 않습니다";
            PhoneNumCheck.color = Color.red;
        }
        else
        {
            //sendCode = api에서 사용자에게 보낸 코드 네자리 받아와서 저장하기.
            sendCode = "1234";
            isPhoneCheck = true;
            PhoneNumCheck.color = Color.clear;
        }

    }

    public void checkCode()
    {
        if(PhoneCodefield.text == sendCode)
        {
            PWcodeCheckText.text = "인증 완료!";
            PWcodeCheckText.color = Color.green;
            isCodeCheck = true;
        }
        else
        {
            PWcodeCheckText.text = "코드가 일치하지 않습니다";
            PWcodeCheckText.color = Color.red;
        }
    }

    public void register()
    {
        if(checkID == IDfield.text)
        {
            IDText.text = "사용 가능한 아이디입니다";
            IDText.color = Color.green;
            if (checkPhoneNum == PhoneNumfield.text)
            {
                PhoneNumCheck.color = Color.clear;
                if (isIDcheck && PWconfitioncheck && PWconfirmcheck && isPhoneCheck && isCodeCheck)
                {
                    //서비스 약관 확인 화면으로 이동.
                    sendCode = "miner";
                    updateStop = true;
                    alertPanel.gameObject.SetActive(true);
                    
                }
                else
                {
                    finalRegister.text = "빨간 글씨를 확인해주세요";
                    finalRegister.color = Color.red;
                }
            }
            else
            {
                PhoneNumCheck.text = "인증된 전화번호와 다릅니다";
                PhoneNumCheck.color = Color.red;
            }
        }
        else
        {
            IDText.text = "중복 확인된 아이디와 다릅니다";
            IDText.color = Color.red;
        }
    }

    public void close()
    {
        alertPanel.gameObject.SetActive(false);
    }

    public void agreeConfirm()
    {
        if (toggle.isOn)
        {
            //닉네임 입력 화면으로 이동
            //api에 id, pw, 전화번호 넘겨서 회원가입 진행
            PlayerPrefs.SetString("id", checkID); //id PlayersPrefs로 저장
            //화면 이동 후 사용자가 정한 닉네임, id 값이랑 같이 넘겨서 닉네임 저장.
            SceneManager.LoadScene("nickName");
        }
        else
        {
            //경고문구 보여주기.
            Text_alert_agree.color = Color.red;
        }
    }


    public void goBack()
    {
        //loginMain 으로 씬 연결.
        SceneManager.LoadScene("loginMain");
        PlayerPrefs.SetInt("autoLogin", 0);
    }

    void Start()
    {
        alertPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!updateStop)
        {
            if (PWfield.text != "")
            {
                if (PWfield.text.Length < 10) //조건 추가 가능, 추가하면 하단의 경고 문구도 바꾸기
                {
                    PWconditionText.text = "비밀번호는 10자리 이상입니다";
                    PWconditionText.color = Color.red;
                    PWconfitioncheck = false;
                }
                else
                {
                    PWconditionText.text = "사용 가능한 비밀번호입니다";
                    PWconditionText.color = Color.green;
                    PWconfitioncheck = true;
                }
            }
            else
            {
                PWconditionText.color = Color.clear;
            }



            if (PWCheckfield.text != "")
            {
                if (PWfield.text != PWCheckfield.text)
                {
                    PWcheckText.text = "비밀번호가 일치하지 않습니다";
                    PWcheckText.color = Color.red;
                    PWconfirmcheck = false;
                }
                else
                {
                    PWcheckText.text = "비밀번호 확인 완료";
                    PWcheckText.color = Color.green;
                    PWconfirmcheck = true;
                }
            }
            else
            {
                PWcheckText.color = Color.clear;
            }
        }
      
    }
}
