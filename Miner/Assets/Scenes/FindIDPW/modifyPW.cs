using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class modifyPWInfo
{
    public string userIdx;
    public string password;
}

public class modifyPW : MonoBehaviour
{
    // Start is called before the first frame update

    public InputField InputField_PW;
    public InputField InputField_PWconfirm;

    public Text Text_PW;
    public Text Text_PWconfirm;
    public Text Text_Buttonmodify;

    public GameObject alertPanel;
    public GameObject blackPanel;

    public GameObject ExitPanel;
    public GameObject testPanel;
    private float progress = 0.0f;
    public Image circleProgress;

    private bool PWconfitioncheck = false;
    private bool PWconfirmcheck = false;
    private Regex regex = new Regex(@"^(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[\W]).{8,20}$");

    public void ExitYes()
    {
        Application.Quit();
    }

    public void ExitNo()
    {
        ExitPanel.SetActive(false);
    }



    public void goBackLogin()
    {
        SceneManager.LoadScene("loginMain");
    }

    public void FindIDButton()
    {
        SceneManager.LoadScene("FindID");
    }

    public void FindPWButton()
    {
        SceneManager.LoadScene("FindPW");

    }





    public void modifyPWButton()
    {
        if (PWconfitioncheck && PWconfirmcheck)
        {
            Text_Buttonmodify.color = Color.clear;
            string modifyPW = InputField_PWconfirm.text;
            StartCoroutine(modifyPWs(modifyPW));
            //Debug.Log("success");
        }
        else
        {
            Text_Buttonmodify.color = Color.red;
        }
    }
    IEnumerator modifyPWs(string passwords)
    {
        testPanel.SetActive(true);
        string realURL = "https://miner22.shop/miner/users/modifyPw";
        string userIdxs = PlayerPrefs.GetString("modifyuserIdx");
        modifyPWInfo myObject = new modifyPWInfo { userIdx = userIdxs, password = passwords };
        string json = JsonUtility.ToJson(myObject);

        using (UnityWebRequest www = UnityWebRequest.Put(realURL, json))
        {
            www.method = "PATCH";
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
                    blackPanel.gameObject.SetActive(true);
                    alertPanel.gameObject.SetActive(true);
                }
               
                else
                {
                    Text_Buttonmodify.text = "비밀번호 변경 실패.";
                    Text_Buttonmodify.color = Color.red;
                }

            }
        }
        testPanel.SetActive(false);
    }

    void Start()
    {
        alertPanel.gameObject.SetActive(false);
        blackPanel.gameObject.SetActive(false);
        ExitPanel.SetActive(false);
        testPanel.SetActive(false);
        //Debug.Log(PlayerPrefs.GetString("modifyuserIdx"));
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

        //안드로이드인 경우
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



        if (InputField_PW.text != "")  
        {
            for(int i = 0; i < InputField_PW.text.Length; i++)
            {
                if(InputField_PW.text[i] == ' ')
                {
                    Text_PW.text = "비밀번호에 띄어쓰기는 포함할 수 없습니다.";
                    Text_PW.color = Color.red;
                    PWconfitioncheck = false;
                }
            }
           
                if (!regex.IsMatch(InputField_PW.text)) //조건 추가 가능, 추가하면 하단의 경고 문구도 바꾸기
                {
                    Text_PW.text = "숫자/특수문자/영어 포함, 한글 미포함, 8~20자 입니다.";
                    Text_PW.color = Color.red;
                    PWconfitioncheck = false;
                }
                else
                {
                    Text_PW.text = "사용 가능한 비밀번호입니다";
                    Text_PW.color = Color.green;
                    PWconfitioncheck = true;
                }
        }
        else
        {
            Text_PW.color = Color.clear;
        }



            if (InputField_PWconfirm.text != "")
            {
                if (InputField_PWconfirm.text != InputField_PW.text)
                {
                Text_PWconfirm.text = "비밀번호가 일치하지 않습니다";
                Text_PWconfirm.color = Color.red;
                    PWconfirmcheck = false;
                }
                else
                {
                Text_PWconfirm.text = "비밀번호 확인 완료";
                Text_PWconfirm.color = Color.green;
                    PWconfirmcheck = true;
                }
            }
            else
            {
            Text_PWconfirm.color = Color.clear;
            }
        
    }
}
