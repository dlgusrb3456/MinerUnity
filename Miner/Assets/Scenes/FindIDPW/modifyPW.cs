using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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

    private bool PWconfitioncheck = false;
    private bool PWconfirmcheck = false;

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
            string modifyID = PlayerPrefs.GetString("modifyID");
            string modifyPW = InputField_PWconfirm.text;

            //api로 해당 ID의 PW를 modifyPW로 변경.
            blackPanel.gameObject.SetActive(true);
            alertPanel.gameObject.SetActive(true);
        }
        else
        {
            Text_Buttonmodify.color = Color.red;
        }
    }
    void Start()
    {
        alertPanel.gameObject.SetActive(false);
        blackPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
     
        if (InputField_PW.text != "")  
        {
                if (InputField_PW.text.Length < 10) //조건 추가 가능, 추가하면 하단의 경고 문구도 바꾸기
                {
                Text_PW.text = "비밀번호는 10자리 이상입니다";
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
