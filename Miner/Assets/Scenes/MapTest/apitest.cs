using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;



public class datas
{
    public string email;
    public string password;
}

public class apitest : MonoBehaviour
{
    private string URL = "https://miner22.shop/miner/users/login";
    private string exURL = "/miner/users/login";


    public void button()
    {
        StartCoroutine(exAPi());
    }


    IEnumerator exAPi()
    {
        string realURL = URL + exURL;
        datas myObject = new datas { email = "dlgusrb", password = "123" };
        string json = JsonUtility.ToJson(myObject);


        using (UnityWebRequest www = UnityWebRequest.Post(URL, json))
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
                string[] isSuccess = words[0].Split(':');
                if (isSuccess[1] == "false")
                {
                    string[] isFailed = words[2].Split('"');
                    Debug.Log(isFailed[3]); //틀렸을때 나오는 오류.
                }
                else
                {
                    for(int i = 0; i < words.Length; i++)
                    {
                        Debug.Log(words[i]);
                    }
                    //string[] nickNames = words[5].Split('"');
                    //Debug.Log(nickNames[3]); //성공시 닉네임
                }
                //Debug.Log(returns);
            }
        }
    }
}
