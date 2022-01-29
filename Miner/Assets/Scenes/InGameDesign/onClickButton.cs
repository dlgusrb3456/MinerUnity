using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class onClickButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Text Text_status;

    public GameObject obstacles;

    public Sprite image_grass;
    public Sprite image_start;
    public Sprite image_end;
    public Sprite image_block;

    private GameObject myTarget;

    public void onClickButtons()
    {
        int test_statuss = PlayerPrefs.GetInt("Toggle");
        string test_status = test_statuss.ToString();
        Debug.Log(test_status);
        //Debug.Log("clicked");
        //Debug.Log("test_status " + test_status);
        if (test_status == "1") //설계하기 버튼이 눌린 상태
        {
            //Debug.Log("design");
            if (Text_status.text == "0" || Text_status.text == "2" || Text_status.text == "3")
            {
                //Debug.Log("design => 1");
                Text_status.text = "1";
                //버튼 이미지 장애물로
                obstacles.GetComponent<Image>().sprite = image_grass;
                obstacles.GetComponent<Image>().color = new Color32(225, 225, 225, 225);
            }
            else if (Text_status.text == "1")
            {
                //Debug.Log("design => 0");
                Text_status.text = "0";
                //setactive false로
                obstacles.GetComponent<Image>().color = Color.clear;

            }

        }
        else if (test_status == "2") // 출발 버튼이 눌린 상태
        {
            //clone 된 자식 객체 프리팹을 반복문으로 검사
            //Text_status를 검사해서 1인 아이가 있다면 그 아이를 0으로 변경
            //없다면 다음으로

            //위의 검사 진행 후 
            // Debug.Log("start");
            if (Text_status.text == "0" || Text_status.text == "1" || Text_status.text == "2")
            {
                // Debug.Log("start=>3");
                Text_status.text = "3";
                //버튼 이미지 출발로
                obstacles.GetComponent<Image>().sprite = image_start;
                obstacles.GetComponent<Image>().color = new Color32(225, 225, 225, 225);
            }
            else if (Text_status.text == "3")
            {
                //Debug.Log("start=>0");
                Text_status.text = "0";
                //setactive false로
                obstacles.GetComponent<Image>().color = Color.clear;
            }
        }
        else if (test_status == "3") //도착 버튼이 눌린 상태.
        {
            //clone 된 자식 객체 프리팹을 반복문으로 검사
            //Text_status를 검사해서 2인 아이가 있다면 그 아이를 0으로 변경
            //없다면 다음으로

            //위의 검사 진행 후 

            if (Text_status.text == "0" || Text_status.text == "1" || Text_status.text == "3")
            {
                Text_status.text = "2";
                //버튼 이미지 도착으로
                obstacles.GetComponent<Image>().sprite = image_end;
                obstacles.GetComponent<Image>().color = new Color32(225, 225, 225, 225);
            }
            else if (Text_status.text == "2")
            {
                Text_status.text = "0";
                //setactive false로
                obstacles.GetComponent<Image>().color = Color.clear;
            }
        }
        else    //아무것도 클릭되어있지 않은 상태.
        {
            //정사각형 => 좌측 하단 꼭지점 좌표, 우측 상단 꼭지점 좌표

            //드래그 => 화면 터치 개수가 1개일 경우에
            //줌인 줌아웃 => 화면 터치 개수가 2개일 경우에 

            //모바일: 터치 touchCount == 1?
            //PC: 클릭 => 


            // Debug.Log("else?");
            //화면 움직이게 하기
        }
    }
    void Start()
    {
        //myTarget = GameObject.FindGameObjectWithTag("design");
        //if(myTarget != null)
        //{
        //    Debug.Log("asdf");
        //}
        //else
        //{
        //    Debug.Log("gfdaf");
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}