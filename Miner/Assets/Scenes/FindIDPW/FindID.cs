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
        checkPhoneNum = PhoneNumfield.text;
        StartCoroutine(isPhone(checkPhoneNum));
        //인풋필드의 전화번호 전송 , 
        //1. 정상적으로 성공한 경우 , 전송한 코드 받아와서 저장하기
        //2. 해당 이메일이 없는 경우 => 가입 정보가 없습니다.
        //3. 이메일 형식을 지키지 않은경우 => 이메일 형식이 올바르지 않습니다.

        //if (PhoneNumfield.text == "01033614263")
        //{
        //    checkPhoneNum = PhoneNumfield.text;
        //    code = "1234";
        //    Text_PhoneNumField.color = Color.clear;
        //}
        //else if (PhoneNumfield.text == "0103361426")
        //{
        //    Text_PhoneNumField.text = "회원 정보가 존재하지 않습니다";
        //    Text_PhoneNumField.color = Color.red;
        //}
        //else
        //{
        //    Text_PhoneNumField.text = "번호 형식이 올바르지 않습니다.";
        //    Text_PhoneNumField.color = Color.red;
        //}


    }

    IEnumerator isPhone(string phoneNums)
    {
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
                for (int i = 0; i < words.Length; i++)
                {
                    Debug.Log(words[i]);
                }

                string[] returncode = words[1].Split(':');
                if (returncode[1] == "1000")
                {
                    //이메일 보내기
                    

                    //인증번호 체크 api 사용.
                    StartCoroutine(sendPhCode(phoneNums)); // => checkCode로 이동

                    //string[] tmparr = words[3].Split(':');
                    //string[] useridxarr = tmparr[2].Split('}');
                    //userIdxs = useridxarr[0];
                }
                else if (returncode[1] == "3015")
                {
                    Text_PhoneNumField.text = "회원 정보가 존재하지 않습니다";
                    Text_PhoneNumField.color = Color.red;
                }
                else
                {
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
                for (int i = 0; i < words.Length; i++)
                {
                    Debug.Log(words[i]);
                }

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
    }




    public void checkCode()
    {


        StartCoroutine(checkPhoneCode(checkPhoneNum));
        //if (PhoneCodefield.text == code)
        //{
        //    code = "miner";
        //    Text_PhonecodeField.color = Color.clear;
        //    string getEmail = "dlgu*******@naver.com"; //api 통신을 통해 받아온 email.
        //    //SceneManager.LoadScene("modifyPW"); => 
        //    Text_UserId.text = getEmail;
        //    blackPanel.gameObject.SetActive(true);
        //    alertPanel.gameObject.SetActive(true);
        //}
        //else
        //{
        //    Text_PhonecodeField.text = "인증번호가 일치하지 않습니다";
        //    Text_PhonecodeField.color = Color.red;
        //}
    }

    IEnumerator checkPhoneCode(string phoneNums)
    {
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
                for (int i = 0; i < words.Length; i++)
                {
                    Debug.Log(words[i]);
                }

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
    }


    // Start is called before the first frame update
    void Start()
    {
        alertPanel.gameObject.SetActive(false);
        blackPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
