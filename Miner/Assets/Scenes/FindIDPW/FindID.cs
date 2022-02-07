using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class isPhoneNum
{
    public string phoneNum;
}

public class checkPhoneCode
{
    public string phoneNum;
    public string authNum;
}

public class registphoneNum
{
    public string recipientPhoneNumber;
}
public class FindID : MonoBehaviour
{

    public InputField PhoneNumfield;
    public InputField PhoneCodefield;

    public Text Text_PhoneNumField;
    public Text Text_PhonecodeField;
    public Text Text_UserId;



    public GameObject alertPanel;
    public GameObject blackPanel;


    //로딩판넬
    public GameObject testPanel;
    public Image circleProgress;
    private float progress = 0.0f;

    private string checkPhoneNum = "Miner";

    public GameObject ExitPanel;






 

    public void ExitYes()
    {
        Application.Quit();
    }

    public void ExitNo()
    {
        ExitPanel.SetActive(false);
    }


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
                if (returncode[1] == "1000")
                {
                    StartCoroutine(sendPhCode(phoneNums)); // => checkCode로 이동
                }
                else if (returncode[1] == "3015")
                {
                    testPanel.SetActive(false);
                    Text_PhoneNumField.text = "회원 정보가 존재하지 않습니다";
                    Text_PhoneNumField.color = Color.red;
                }
                else
                {
                    testPanel.SetActive(false);
                    Text_PhoneNumField.text = "검색 실패";
                    Text_PhoneNumField.color = Color.red;
                }

            }
        }
    }

    IEnumerator sendPhCode(string phoneNums)
    {
        string realURL = "https://miner22.shop/miner/sms";
        registphoneNum myObject = new registphoneNum { recipientPhoneNumber = phoneNums };
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
                    //이메일 보내기
                    Text_PhoneNumField.text = "인증번호를 전송했습니다";
                    Text_PhoneNumField.color = Color.green;

                }
                else if (returncode[1] == "2025")
                {
                    Text_PhoneNumField.text = "전화번호 형식을 확인해주세요.";
                    Text_PhoneNumField.color = Color.red;
                }
                else if (returncode[1] == "3016")
                {
                    Text_PhoneNumField.text = "메세지 전송에 실패했습니다.";
                    Text_PhoneNumField.color = Color.red;
                }
                else
                {
                    Text_PhoneNumField.text = "검색 실패";
                    Text_PhoneNumField.color = Color.red;
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
        string realURL = "https://miner22.shop/miner/users/find-email";
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
                    string[] ids = words[3].Split('"');
                    string id = ids[5];
                    Text_UserId.text = id;
                    blackPanel.gameObject.SetActive(true);
                    alertPanel.gameObject.SetActive(true);
                    //아이디 찾기 panel업로드

                }
                else if (returncode[1] == "2030")
                {
                    Text_PhonecodeField.text = "인증번호가 일치하지 않습니다";
                    Text_PhonecodeField.color = Color.red;
                }
                else
                {
                    Text_PhonecodeField.text = "인증 실패";
                    Text_PhonecodeField.color = Color.red;
                }

            }
        }
        testPanel.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        alertPanel.gameObject.SetActive(false);
        blackPanel.gameObject.SetActive(false);
        testPanel.SetActive(false);
        ExitPanel.SetActive(false);
    }

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

    }
}
