using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class CanvasScripts : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Panel_settings;
    public GameObject Panel_preventSettings;


    public GameObject Player;

    public GameObject postionPrefabs;
    private GameObject positions;

    //텍스트 감소;
    public Text textCount;

    private int startCount = 10;
    private int positionCount = 10;


    public void positionClicked()
    {
        if (positionCount > 0)
        {
            positionCount--;
            positions = Instantiate(postionPrefabs) as GameObject;
            //positions.transform.GetChild(0).GetComponent<Text>().text = (startCount-positionCount).ToString();
            positions.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, 0);
            textCount.text = positionCount.ToString();
        }
    }

    public void addCount()
    {
        positionCount += 3;
        textCount.text = positionCount.ToString();
    }


    public void ggClick()
    {
        SceneManager.LoadScene("mainPlay");
    }

    public void reGame()
    {
        SceneManager.LoadScene("InGamePlay");
    }

    public void clickSettings()
    {
        Timer.pauseTimer();
        Panel_preventSettings.SetActive(true);
    }

    public void closeSettings()
    {
        Timer.startTimer();
        Panel_preventSettings.SetActive(false);
    }

    void Start()
    {
        //시작할때 position count size기준으로 변경;
        Panel_preventSettings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
