using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Http;
using System;
using System.Threading.Tasks;



[Serializable]
public class datas
{
    public string email;
    public string password;
}

public class usingAPIEx : MonoBehaviour
{
    // Start is called before the first frame update



    private string URL = "https://miner22.shop/miner/users/login";
    private string exURL = "/miner/users/login";


    public void button()
    {
        StartCoroutine(exAPi());
    }


    IEnumerator exAPi()
    {
        string realURL = URL + exURL;
        datas myObject = new datas { email = "kahyun0817@naver.com", password = "!miner2233"};
        string json = JsonUtility.ToJson(myObject);


        using (UnityWebRequest www = UnityWebRequest.Post(URL, json))
        {
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
                Debug.Log(www.downloadHandler.text);
            }
        }






        //using (UnityWebRequest www = UnityWebRequest.Get(URL,json))
        //{
        //    yield return request.SendWebRequest();
        //    if (request.isNetworkError || request.isHttpError)
        //    {
        //        Debug.Log(request.error);
        //    }
        //    else
        //    {
        //        if (request.isDone)
        //        {
        //            string jsonresult = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
        //            Debug.Log(jsonresult);
        //        }
        //    }
        //}


    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
