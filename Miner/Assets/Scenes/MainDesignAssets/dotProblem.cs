using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class dotProblem : MonoBehaviour
{
    public GameObject Panel_setFilePrefab1;
    public GameObject Panel_setFilePrefab2;
    public GameObject Button_mapPrefab;
    public GameObject Panel_deletePrefab;

    //public Sprite imageBG;

    public Text Text_mironame;
    //public Text Panel_deletePrefab_Text;

    private GameObject sharePanel;
    private GameObject nonesharePanel;
    private GameObject predeletePrefab;
    private string selectedFileName;


    public void dotdotdotPanel()
    {
        
        selectedFileName = Text_mironame.text;
        Debug.Log(selectedFileName);
        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            if (Map.localMaps[i].name == selectedFileName)
            {
                Debug.Log(i);
                if (Map.localMaps[i].isShared)
                {
                    Debug.Log("isShared");
                    sharePanel = Instantiate(Panel_setFilePrefab1) as GameObject;
                    sharePanel.transform.parent = Button_mapPrefab.transform;
                    sharePanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(closesharePanel);
                    sharePanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(deletePre);
                    //sharePanel.transform.GetComponent<Image>().sprite = imageBG;
                    sharePanel.transform.localPosition = new Vector3(120, 0, 0);
                    sharePanel.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log("isNoneShared");
                    nonesharePanel = Instantiate(Panel_setFilePrefab2) as GameObject;
                    nonesharePanel.transform.parent = Button_mapPrefab.transform;
                    nonesharePanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(closeNonesharePanel);
                    nonesharePanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(deletePre);
                    nonesharePanel.transform.localPosition = new Vector3(120, 0, 0);
                    nonesharePanel.gameObject.SetActive(true);
                }
                break;
            }

        }
    }

    public void share()
    {

    }

    public void shareStop()
    {

    }

    public void shareModify()
    {

    }



    public void deleteFinal()
    {
        closepredeletePrefab();
        for (int i = 0; i < Map.localMaps.Count; i++)
        {
            if (Map.localMaps[i].name == selectedFileName)
            {
                Map.deleteLocalMap(Map.localMaps[i]);
                //삭제 완료 추가.
                //load 추가 => 화면을 다시 받아오기
                SceneManager.LoadScene("mainDesign");
                break;
            }

        }
    }

    public void deletePre()
    {
        predeletePrefab = Instantiate(Panel_deletePrefab) as GameObject;
        predeletePrefab.transform.parent = Button_mapPrefab.transform;
        predeletePrefab.transform.GetChild(2).GetComponent<Text>().text = selectedFileName + "을 정말 삭제하시겠습니까?";
        predeletePrefab.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(deleteFinal);
        predeletePrefab.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(closepredeletePrefab);
        predeletePrefab.transform.position = new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight / 2, 0) ;
        sharePanel.gameObject.SetActive(true);
    }


    public void closepredeletePrefab()
    {
        Destroy(predeletePrefab);
    }

    public void closeNonesharePanel()
    {
        Destroy(nonesharePanel);
    }

    public void closesharePanel()
    {
        Destroy(sharePanel);
    }


    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
