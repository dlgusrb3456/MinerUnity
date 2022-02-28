using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public GameObject ExitPanel;


    void Start()
    {
        ExitPanel.SetActive(false);
    }

    
    void Update()
    {
        //안드로이드인 경우
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape)) // 뒤로가기 키 입력
            {
                ExitPanel.SetActive(true);

                /*if (ExitPanel.activeSelf) // 판넬 켜져있으면
                {
                    ExitPanel.SetActive(false); 
                }else {
                    ExitPanel.SetActive(true);
                }*/
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

}
