using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainNickName : MonoBehaviour
{
    [Header("Login & Register")]
    public InputField InputField_nickName;
    public string ID;
    // Start is called before the first frame update

    public void confirm()
    {
        //사용자 지정 닉네임과 ID 넘기기 => api
        //이후 메인화면_설계로 이동
        Debug.Log(ID);
        Debug.Log(InputField_nickName.text);

    }
    void Start()
    {
        ID = PlayerPrefs.GetString("id");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
