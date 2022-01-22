using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class mainPlay : MonoBehaviour
{
    //미로파일 4개 Panel
    public GameObject Button_PlayFile;
    public GameObject Panel_files;
    public Sprite image_lock;
    public Sprite image_unLock;

    //search 변수
    public InputField searchInputField;


    //Dropdown 변수
    public Dropdown dropdown;
    private int chooseSearchType = 0; //0이면 닉네임 / 1이면 미로명

    //Toggle
    public Toggle Toggle_popular;
    public Toggle Toggle_latest;

    public void LoadfilestoPanel()
    {
        //기존의 판넬에 들어있는 파일들 모두 삭제.
        Transform[] childList = Panel_files.GetComponentsInChildren<Transform>();
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                {
                    Destroy(childList[i].gameObject);
                }
            }
        }
        // 선택한 페이지에 속한 파일 정보를 가져와야함.

        for(int i = 0; i < 4; i++) //가져온 파일의 개수가 4개인 경우, List형식으로 반환할 예정
        {
            GameObject file = Instantiate(Button_PlayFile) as GameObject;
            file.transform.parent = Panel_files.transform;
            file.transform.GetChild(0).GetComponent<Text>().text = "미로 " + i + "/ 락큼";
            if(i%2 == 0) // 이 미로가 공유파일인지 확인하는 절차 => 공유인경우
            {
                file.transform.GetChild(1).GetComponent<Image>().sprite = image_unLock;
            }
            else
            {
                file.transform.GetChild(1).GetComponent<Image>().sprite = image_lock;
            }
            file.transform.GetChild(2).GetComponent<Text>().text = "총 플레이 :  " + i;
        }

    }

    public void getFileInfo(int pageNum)
    {
        int fileCount = 0;
    }


    public void searchNull()
    {
        searchInputField.text = "";
        if (Toggle_popular.isOn)
        {
            //인기순으로 검색해서 화면에 보이기
        }
        else
        {
            //최신순으로 검색해서 화면에 보이기
        }
    }

    public void searchNotNull(string text)
    {
        int isPopular = -1;
        int isNickName = chooseSearchType;

        if (Toggle_popular.isOn)
        {
            isPopular = 0; // 인기순
        }
        else
        {
            isPopular = 1; // 최신순
        }
        // isPopular, isNickName, text 세가지 넘겨서 값 받아오기


    }


    public void search()
    {
        for(int i = 0; i < searchInputField.text.Length; i++)
        {
            if(searchInputField.text[i]==' ')
            {
                Debug.Log("띄어쓰기는 입력할 수 없습니다.");
                return;
            }
        }

        if(searchInputField.text.Length == 0)
        {
            searchNull();
        }
        else
        {
            searchNotNull(searchInputField.text);
        }

    }

    public void searchDelete()
    {
        searchNull();
    }



    public void getPagingInfo()
    {
        //1. 검색 inputField가 비어있는 경우 
            //1.1 인기순이면 인기순으로 전체 미로 정보 페이징 처리해서 가져오기
            //1.1 최신순이면 최신순으로 전체 미로 정보 페이징 처리해서 가져오기
        //2. 검색 inputField에서 검색을 진행한 경우 (닉네임 검색/미로이름 검색 나뉨)
            //2.1 인기순이면 인기순으로 검색된 미로 정보 페이징 처리해서 가져오기
            //2.2 최신순이면 최신순으로 검색된 미로 정보 페이징 처리해서 가져오기

        //인기순/최신순 토글 입력시 inputField 내용이 없으면 1, 있으면 2
        //x 버튼 입력시 1 수행


    }
    public void OnDropdownEvent(int index)
    {
        chooseSearchType = index;
        Debug.Log(chooseSearchType);
    }

    // Start is called before the first frame update
    void Start()
    {
        dropdown.onValueChanged.AddListener(OnDropdownEvent);
        LoadfilestoPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
