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


    public void ggClick()
    {
        SceneManager.LoadScene("mainPlay");
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
        Panel_preventSettings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
