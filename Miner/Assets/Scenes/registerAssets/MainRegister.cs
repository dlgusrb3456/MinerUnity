using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class emailClass
{
    public string email;
}

public class registerInfo
{
    public string email;
    public string password;
    public string phoneNum;
    public string nickName;
    public int isChecked;
}


public class nickNameClass
{
    public string nickName;
}



public class MainRegister : MonoBehaviour
{
    // Start is called before the first frame update

    public InputField IDfield;
    public InputField PWfield;
    public InputField PWCheckfield;
    public InputField PhoneNumfield;
    public InputField PhoneCodefield;
    public InputField getNickName;

    public Text IDText;
    public Text PWcheckText;
    public Text PWconditionText;
    public Text PWcodeCheckText;
    public Text PhoneNumCheck;
    public Text finalRegister;
    public Text Text_alert_agree;
    public Text Text_nickNameAlert;

    public GameObject preventPanel;
    public GameObject alertPanel;
    public Toggle toggle_user;
    public Toggle toggle_pri;

    private string checkID = "";
    private string checkNickName = "";

    private bool isIDcheck = false;
    private bool isNickNamecheck = false;
    private bool PWconfitioncheck = false;
    private bool PWconfirmcheck = false;
    private string checkPhoneNum = "";

    private bool isCodeCheck = false;

    private Regex regex = new Regex(@"^(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[\W]).{8,20}$");

    public GameObject ExitPanel;
    public GameObject testPanel;
    private float progress = 0.0f;
    public Image circleProgress;

    public void privateURL()
    {
        Application.OpenURL("https://miners.netlify.app/privacy");
    }
    public void UseprivateURL()
    {
        Application.OpenURL("https://miners.netlify.app/usePrivacy");
    }
    public void checkAll()
    {
        toggle_user.isOn = true;
        toggle_pri.isOn = true;
    }

    public void ExitYes()
    {
        Application.Quit();
    }

    public void ExitNo()
    {
        ExitPanel.SetActive(false);
    }

    public void IDduplicateCheck()
    {
        checkID = IDfield.text; //중복확인을 진행한 이메일 주소 저장
        if(checkID.Length != 0)
        {
            StartCoroutine(registEmailCheck(checkID));
        }
        else
        {
            IDText.text = "이메일을 입력해주세요";
            IDText.color = Color.red;
        }
    }

    IEnumerator registEmailCheck(string emails)
    {
        testPanel.SetActive(true);
        string realURL = "https://miner22.shop/miner/users/email";
        emailClass myObject = new emailClass { email = emails };
        string json = JsonUtility.ToJson(myObject);


        using (UnityWebRequest www = UnityWebRequest.Post(realURL, json))
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bytes);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.Send();

            if (www.isNetworkError)
            {
                Debug.Log(www.isNetworkError);
            }
            else
            {
                string returns = www.downloadHandler.text;
                string[] words = returns.Split(',');
                //for(int i = 0; i < words.Length; i++)
                //{
                //    Debug.Log(words[i]);
                //}
                string[] returncode = words[1].Split(':');
                if(returncode[1] == "1001")
                {
                    isIDcheck = true;
                    IDText.text = "사용 가능한 아이디입니다";
                    IDText.color = Color.green;
                }
                else if(returncode[1] == "2015")
                {
                    isIDcheck = false;
                    IDText.text = "이메일을 입력해주세요";
                    IDText.color = Color.red;
                }
                else if(returncode[1] == "2016")
                {
                    isIDcheck = false;
                    IDText.text = "이메일 형식을 확인해주세요.";
                    IDText.color = Color.red;
                }
                else if(returncode[1] == "2017")
                {
                    isIDcheck = false;
                    IDText.text = "중복된 이메일입니다.";
                    IDText.color = Color.red;
                }
            }
        }
        testPanel.SetActive(false);
    }

    public void nickNameDuplicateCheck()
    {
        checkNickName = getNickName.text;
        StartCoroutine(registNickNameCheck(checkNickName));
    }

    IEnumerator registNickNameCheck(string nickNames)
    {
        testPanel.SetActive(true);
        string realURL = "https://miner22.shop/miner/users/name";
        nickNameClass myObject = new nickNameClass { nickName = nickNames };
        string json = JsonUtility.ToJson(myObject);


        using (UnityWebRequest www = UnityWebRequest.Post(realURL, json))
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bytes);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.Send();

            if (www.isNetworkError)
            {
                Debug.Log(www.isNetworkError);
            }
            else
            {
                string returns = www.downloadHandler.text;
                string[] words = returns.Split(',');
                //for (int i = 0; i < words.Length; i++)
                //{
                //    Debug.Log(words[i]);
                //}

                string[] returncode = words[1].Split(':');
                if (returncode[1] == "1002")
                {
                    isNickNamecheck = true;
                    Text_nickNameAlert.text = "사용 가능한 닉네임입니다";
                    Text_nickNameAlert.color = Color.green;
                }
                else if (returncode[1] == "2019")
                {
                    isNickNamecheck = false;
                    Text_nickNameAlert.text = "6자 미만으로 설정해주세요";
                    Text_nickNameAlert.color = Color.red;
                }
                else if (returncode[1] == "2020")
                {
                    isNickNamecheck = false;
                    Text_nickNameAlert.text = "이미 존재하는 닉네임입니다.";
                    Text_nickNameAlert.color = Color.red;
                }

            }
        }
        testPanel.SetActive(false); ;
    }

    public void SendCode()
    {
        checkPhoneNum = PhoneNumfield.text;
        StartCoroutine(isPhone(checkPhoneNum));

    }
    IEnumerator isPhone(string phoneNums)
    {
        testPanel.SetActive(true);
        string realURL = "https://miner22.shop/miner/users/phoneNum";
        isPhoneNum myObject = new isPhoneNum { phoneNum = phoneNums };
        string json = JsonUtility.ToJson(myObject);


        using (UnityWebRequest www = UnityWebRequest.Post(realURL, json))
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bytes);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.Send();

            if (www.isNetworkError)
            {
                Debug.Log(www.isNetworkError);
            }
            else
            {
                string returns = www.downloadHandler.text;
                string[] words = returns.Split(',');
                //for (int i = 0; i < words.Length; i++)
                //{
                //    Debug.Log(words[i]);
                //}

                string[] returncode = words[1].Split(':');
                if (returncode[1] == "1000") //회원이 있는 경우.
                {

                    PhoneNumCheck.text = "이미 등록된 전화번호입니다.";
                    PhoneNumCheck.color = Color.red;

                }
                else if (returncode[1] == "3015") //처음 등록되는 전화번호.
                {
                    StartCoroutine(sendPhCode(phoneNums)); 
                }
                else
                {
                    PhoneNumCheck.text = "검색 실패.";
                    PhoneNumCheck.color = Color.red;
                }

            }
        }
        testPanel.SetActive(false);
    }



    IEnumerator sendPhCode(string phoneNums)
    {
        testPanel.SetActive(true);
        string realURL = "https://miner22.shop/miner/sms";
        registphoneNum myObject = new registphoneNum { phoneNum = phoneNums };
        string json = JsonUtility.ToJson(myObject);


        using (UnityWebRequest www = UnityWebRequest.Post(realURL, json))
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bytes);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.Send();

            if (www.isNetworkError)
            {
                Debug.Log(www.isNetworkError);
            }
            else
            {
                string returns = www.downloadHandler.text;
                string[] words = returns.Split(',');
                //for (int i = 0; i < words.Length; i++)
                //{
                //    Debug.Log(words[i]);
                //}

                string[] returncode = words[1].Split(':');
                if (returncode[1] == "1000")
                {
                    PhoneNumCheck.text = "인증번호를 전송했습니다.";
                    PhoneNumCheck.color = Color.green;

                }
                else if (returncode[1] == "2025")
                {
                    PhoneNumCheck.text = "전화번호 형식을 확인해주세요.";
                    PhoneNumCheck.color = Color.red;
                }
                else if (returncode[1] == "3016")
                {
                    PhoneNumCheck.text = "메세지 전송에 실패했습니다.";
                    PhoneNumCheck.color = Color.red;
                }
                else
                {
                    PhoneNumCheck.text = "검색 실패";
                    PhoneNumCheck.color = Color.red;
                }

            }
        }
        testPanel.SetActive(false);
    }


    public void checkCode()
    {
        StartCoroutine(checkPhoneCode(checkPhoneNum));
    }

    IEnumerator checkPhoneCode(string phoneNums)
    {
        testPanel.SetActive(true);
        string realURL = "https://miner22.shop/miner/users/signup/auth";
        checkPhoneCode myObject = new checkPhoneCode { phoneNum = phoneNums, authNum = PhoneCodefield.text };
        string json = JsonUtility.ToJson(myObject);


        using (UnityWebRequest www = UnityWebRequest.Post(realURL, json))
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bytes);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.Send();

            if (www.isNetworkError)
            {
                Debug.Log(www.isNetworkError);
            }
            else
            {
                string returns = www.downloadHandler.text;
                string[] words = returns.Split(',');
                //for (int i = 0; i < words.Length; i++)
                //{
                //    Debug.Log(words[i]);
                //}

                string[] returncode = words[1].Split(':');
                if (returncode[1] == "1000")
                {

                    PWcodeCheckText.text = "인증 완료!";
                    PWcodeCheckText.color = Color.green;
                    isCodeCheck = true;

                }
                else if (returncode[1] == "2030")
                {
                    PWcodeCheckText.text = "인증번호가 일치하지 않습니다.";
                    PWcodeCheckText.color = Color.red;

                }
                else
                {
                    PWcodeCheckText.text = "검색 실패.";
                    PWcodeCheckText.color = Color.red;
                }

            }
        }
        testPanel.SetActive(false);
    }

    public void register()
    {
        if(checkID == IDfield.text && IDfield.text.Length != 0)
        {
            IDText.text = "사용 가능한 아이디입니다";
            IDText.color = Color.green;
            if (checkPhoneNum == PhoneNumfield.text)
            {

                if(checkNickName == getNickName.text)
                {
                    PhoneNumCheck.color = Color.clear;
                    if (isIDcheck && PWconfitioncheck && PWconfirmcheck && isCodeCheck && isNickNamecheck)
                    {
                        //서비스 약관 확인 화면으로 이동.

                        preventPanel.gameObject.SetActive(true);
                        alertPanel.gameObject.SetActive(true);

                    }
                    else
                    {
                        finalRegister.text = "빨간 글씨를 확인해주세요.";
                        finalRegister.color = Color.red;
                    }
                }
                else
                {
                    Text_nickNameAlert.text = "인증된 닉네임과 다릅니다.";
                    Text_nickNameAlert.color = Color.red;
                }
                
            }
            else
            {
                PhoneNumCheck.text = "인증된 전화번호와 다릅니다.";
                PhoneNumCheck.color = Color.red;
            }
        }
        else
        {
            IDText.text = "중복 확인된 아이디와 다릅니다.";
            IDText.color = Color.red;
        }
    }

    public void close()
    {
        preventPanel.gameObject.SetActive(false);
        alertPanel.gameObject.SetActive(false);
    }

    public void agreeConfirm()
    {
        if (toggle_user.isOn && toggle_pri.isOn)
        {
            StartCoroutine(registApi(checkID, PWCheckfield.text,checkPhoneNum,checkNickName,1));
        }
        else
        {
            //경고문구 보여주기.
            Text_alert_agree.text = "약관에 동의해야 회원가입이 진행됩니다";
            Text_alert_agree.color = Color.red;
        }
    }
    IEnumerator registApi(string emails, string passwords,string phoneNums, string nickNames,int isChecks)
    {
        testPanel.SetActive(true);
        string realURL = "https://miner22.shop/miner/users/signup";
        registerInfo myObject = new registerInfo {email= emails , password=passwords , phoneNum = phoneNums, nickName = nickNames, isChecked = isChecks};
        string json = JsonUtility.ToJson(myObject);


        using (UnityWebRequest www = UnityWebRequest.Post(realURL, json))
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bytes);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.Send();

            if (www.isNetworkError)
            {
                Debug.Log(www.isNetworkError);
            }
            else
            {
                string returns = www.downloadHandler.text;
                string[] words = returns.Split(',');
                //for (int i = 0; i < words.Length; i++)
                //{
                //    Debug.Log(words[i]);
                //}

                string[] returncode = words[1].Split(':');
                if (returncode[1] == "1000")
                {
                    PlayerPrefs.SetString("nickName", checkID);
                    PlayerPrefs.SetString("userIdx", checkID);
                    SceneManager.LoadScene("Tutorial");

                }
                else
                {
                    Text_alert_agree.text = "회원가입에 실패했습니다.";
                    Text_alert_agree.color = Color.red;
                }

            }
        }
        testPanel.SetActive(false); ;
    }
    public void goBack()
    {
        //loginMain 으로 씬 연결.
        SceneManager.LoadScene("loginMain");
        PlayerPrefs.SetInt("autoLogin", 0);
    }

    void Start()
    {
        preventPanel.gameObject.SetActive(false);
        alertPanel.gameObject.SetActive(false);
        testPanel.SetActive(false);
        ExitPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (testPanel.activeSelf == true)
        {
            progress += 0.3f * Time.deltaTime;
            if (progress > 1)
            {
                progress = 0;
            }
            circleProgress.fillAmount = progress;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape)) // 뒤로가기 키 입력
            {
                if (ExitPanel.activeSelf) // 판넬 켜져있으면
                {
                    ExitPanel.SetActive(false);
                }
                else
                {
                    ExitPanel.SetActive(true);
                }
            }
        }


        if (PWfield.text != "")
            {
            
                if (!regex.IsMatch(PWfield.text)) //조건 추가 가능, 추가하면 하단의 경고 문구도 바꾸기
                {
                    PWconditionText.text = "숫자/특수문자/영어 포함, 한글 미포함, 8~20자 입니다.";
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
