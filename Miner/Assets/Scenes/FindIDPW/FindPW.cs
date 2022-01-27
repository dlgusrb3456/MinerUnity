using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class findPWInfo
{
    public string address;
}

public class checkPWinfo
{
    public string userIdx;
    public string authNum;
}

public class FindPW : MonoBehaviour
{

    public InputField InputField_email;
    public InputField InputField_emailCode;

    public Text Text_email;
    public Text Text_emailCode;


    private string checkID = "Miner";
    private string userIdxs = "-1";
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

      

        for (int i = 0; i < InputField_email.text.Length; i++)
        {
            if(InputField_email.text[i]==' ')
            {
                Text_email.text = "이메일 형식이 올바르지 않습니다.";
                Text_email.color = Color.red;
                return;
            }
        }

        if(InputField_email.text == "")
        {
            Text_email.text = "이메일 형식이 올바르지 않습니다.";
            Text_email.color = Color.red;
            return;
        }

        checkID = InputField_email.text;
        Text_email.color = Color.clear;

        StartCoroutine(sendEmailCode(checkID));

        
    }

    IEnumerator sendEmailCode(string emails)
    {
        string realURL = "https://miner22.shop/miner/email/emailSend";
        findPWInfo myObject = new findPWInfo { address = emails };
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

                    Text_email.text = "인증번호를 전송했습니다.";
                    Text_email.color = Color.green;
                    string[] tmparr = words[3].Split(':');
                    string[] useridxarr = tmparr[2].Split('}');
                    userIdxs = useridxarr[0];
                }
                else if (returncode[1] == "3017")
                {
                    Text_email.text = "없는 이메일 정보입니다.";
                    Text_email.color = Color.red;
                }
                else if (returncode[1] == "2016" || returncode[1] == "500")
                {
                    Text_email.text = "이메일 형식을 확인해주세요.";
                    Text_email.color = Color.red;
                }
                else
                {
                    Text_email.text = "검색 실패.";
                    Text_email.color = Color.red;
                }

            }
        }
    }

    public void checkCode()
    {

        for (int i = 0; i < InputField_emailCode.text.Length; i++)
        {
            if (InputField_emailCode.text[i] == ' ')
            {
                Text_emailCode.text = "인증번호가 일치하지 않습니다.";
                Text_emailCode.color = Color.red;
                return;
            }
        }

        if (InputField_emailCode.text == "")
        {
            Text_emailCode.text = "인증번호가 일치하지 않습니다.";
            Text_emailCode.color = Color.red;
            return;
        }

        StartCoroutine(checkEmailCode(InputField_emailCode.text));

    }
    IEnumerator checkEmailCode(string authNum)
    {
        string realURL = "https://miner22.shop/miner/email/compareAuth";
        checkPWinfo myObject = new checkPWinfo {userIdx = userIdxs , authNum = authNum};
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
                    string[] tmparr = words[3].Split(':');
                    string[] useridxarr = tmparr[2].Split('}');
                    PlayerPrefs.SetString("modifyuserIdx", useridxarr[0]);
                    SceneManager.LoadScene("modifyPW");
                }
                else if (returncode[1] == "2021")
                {
                    Text_emailCode.text = "인증번호를 입력해주세요.";
                    Text_emailCode.color = Color.red;
                }
                else if (returncode[1] == "2022")
                {
                    Text_emailCode.text = "인증번호가 일치하지 않습니다.";
                    Text_emailCode.color = Color.red;
                }
                else
                {
                    Text_emailCode.text = "검색 실패.";
                    Text_emailCode.color = Color.red;
                }

            }
        }
    }
}
