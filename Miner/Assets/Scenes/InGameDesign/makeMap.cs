using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class makeMap : MonoBehaviour
{

    //사용 이미지.
    public Sprite image_grass;
    public Sprite image_start;
    public Sprite image_end;
    public Sprite image_block;


    //프리팹들
    public GameObject obstacle_prefab;

    //UI 토글들.
    public Toggle toggle_design;
    public Toggle toggle_start;
    public Toggle toggle_end;


    public Text Text_status; // => 프리팹의 자식으로 찾을듯.





    public void onClickButton()
    {
        Debug.Log("clicked");
        if (toggle_design.isOn) //설계하기 버튼이 눌린 상태
        {
            Debug.Log("design");
            if (Text_status.text == "0" || Text_status.text == "2" || Text_status.text == "3")
            {
                Debug.Log("design => 1");
                Text_status.text = "1";
                //버튼 이미지 장애물로
                obstacle_prefab.GetComponent<Image>().sprite = image_grass;
                obstacle_prefab.GetComponent<Image>().color = new Color32(225, 225, 225, 225);
            }
            else if(Text_status.text == "1")
            {
                Debug.Log("design => 0");
                Text_status.text = "0";
                //setactive false로
                obstacle_prefab.GetComponent<Image>().color = Color.clear;

            }
            
        }
        else if(toggle_start.isOn) // 출발 버튼이 눌린 상태
        {
            //clone 된 자식 객체 프리팹을 반복문으로 검사
            //Text_status를 검사해서 1인 아이가 있다면 그 아이를 0으로 변경
            //없다면 다음으로

            //위의 검사 진행 후 
            Debug.Log("start");
            if (Text_status.text == "0" || Text_status.text == "1" || Text_status.text == "2")
            {
                Debug.Log("start=>3");
                Text_status.text = "3";
                //버튼 이미지 출발로
                obstacle_prefab.GetComponent<Image>().sprite = image_start;
                obstacle_prefab.GetComponent<Image>().color = new Color32(225, 225, 225, 225);
            }
            else if (Text_status.text == "3")
            {
                Debug.Log("start=>0");
                Text_status.text = "0";
                //setactive false로
                obstacle_prefab.GetComponent<Image>().color = Color.clear;
            }
        }
        else if(toggle_end.isOn) //도착 버튼이 눌린 상태.
        {
            //clone 된 자식 객체 프리팹을 반복문으로 검사
            //Text_status를 검사해서 2인 아이가 있다면 그 아이를 0으로 변경
            //없다면 다음으로

            //위의 검사 진행 후 

            if (Text_status.text == "0" || Text_status.text == "1" || Text_status.text == "3")
            {
                Text_status.text = "2";
                //버튼 이미지 도착으로
                obstacle_prefab.GetComponent<Image>().sprite = image_end;
                obstacle_prefab.GetComponent<Image>().color = new Color32(225, 225, 225, 225);
            }
            else if (Text_status.text == "2")
            {
                Text_status.text = "0";
                //setactive false로
                obstacle_prefab.GetComponent<Image>().color = Color.clear;
            }
        }
        else    //아무것도 클릭되어있지 않은 상태.
        {
            Debug.Log("else?");
            //화면 움직이게 하기
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        obstacle_prefab.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
