using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//톱니바퀴 
public GameObject Panel_settings;


//추가
public GameObject Panel_getmiroName;
public GameObject Panel_getmiroSize;

public class mainDesignMain : MonoBehaviour
{


    public void startFile()
    {
        //인게임_설계화면으로 이동
        //SceneManager.LoadScene("");
    }

    public void settingFile()
    {
        //세팅 Panel active => 톱니바퀴 누른경우
    }
    
    public void addFile()
    {
        //추가Panel active => 추가버튼 누른경우

    }

    public void moveDesign() 
    {
        //메인_설계 화면으로 이동 => 표지판에서 설계 누른경우
        SceneManager.LoadScene("mainDesign");
    }

    public void movePlay()
    {
        //메인_플레이 화면으로 이동 => 표지판에서 플레이 누른경우
        //SceneManager.LoadScene("");
    }

    public void reLoad()
    {
        //화면 reload => 새로고침 누른 경우 (될 수 있으면 스크롤만 reload)
        
    }


    // Start is called before the first frame update
    void Start()
    {
        //Panel 다 비활성화
        //파일 가져와서 스크롤_contents에 넣기

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
