using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainNickName : MonoBehaviour
{
    [Header("Login & Register")]
    public InputField InputField_nickName;
    public Text Text_duplicateCheck;



    public string ID;



    // Start is called before the first frame update

    public void confirm()
    {
        //중복확인!! => 중복 없으면
        //사용자 지정 닉네임과 ID 넘기기 => api
        if (InputField_nickName.text == "두돼지")
        {
            //메인_설계로 이동
        }
        else if(InputField_nickName.text == "")
        {
            //랜덤 닉네임 줌
        }
        else
        {
            Text_duplicateCheck.color = Color.red;
        }
        
        //이후 메인화면_설계로 이동
        Debug.Log(ID);
        Debug.Log(InputField_nickName.text);

    }
    void Start()
    {
        ID = PlayerPrefs.GetString("id");
        Text_duplicateCheck.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
