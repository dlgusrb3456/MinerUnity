using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class regiOutMain : MonoBehaviour
{
    // Start is called before the first frame update
    public class registerOutAPIInfo
    {
        public string email;
        public string password;
    }

    public GameObject Panel_loading;
    //public GameObject Panel_outComplete;
    public GameObject Panel_black;
    public InputField ID;
    public InputField PW;
    public Text LoginExceptiontxt;
    public GameObject ExitPanel;
    public GameObject Panel_outResult;
    public Text outResutText;

   

    void Start()
    {
        Panel_loading.SetActive(false);
        Panel_black.SetActive(false);
        ExitPanel.SetActive(false);
    }

    void Update()
    {
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

    public void ExitYes()
    {
        Application.Quit();
    }

    public void ExitNo()
    {
        ExitPanel.SetActive(false);
    }




    IEnumerator regoOutAPI(string emails, string passwords)
    {
        Panel_loading.SetActive(true);
        string URL = "https://miner22.shop/miner/users/deleteUserInfo"; //주소는 바꿔야함
        registerOutAPIInfo myObject = new registerOutAPIInfo { email = emails, password = passwords };
        string json = JsonUtility.ToJson(myObject);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, json))
        {
            www.method = "PATCH";
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
                //for (int i = 0; i < words.Length; i++)
                //{
                //    Debug.Log(words[i]);
                //}

                string[] returncode = words[1].Split(':');
                if (returncode[1] == "1000")
                {
                    outResutText.text = "탈퇴 완료!";
                }
                else
                {
                    outResutText.text = "탈퇴 실패!";
                }

                Panel_loading.SetActive(false);
                Panel_black.SetActive(false);
                Panel_outResult.SetActive(true);

            }
        }
    }

    public void confirmresultOut()
    {
        Panel_outResult.SetActive(false);
    }

     public void outRegi()
     {
        if (ID.text == "" || PW.text == "")
        {
            LoginExceptiontxt.text = "아이디 또는 비밀번호를 입력해주세요";
            LoginExceptiontxt.color = Color.red;
        }
        else
        {
            
            Panel_black.SetActive(true);
        }

     }

    public void confirmOut()
    {
        StartCoroutine(regoOutAPI(ID.text, PW.text));
    }

    public void cancleOut()
    {
        Panel_black.SetActive(false); ;
    }

    public void goBackLogin()
    {
        SceneManager.LoadScene("loginMain");
    }



}
