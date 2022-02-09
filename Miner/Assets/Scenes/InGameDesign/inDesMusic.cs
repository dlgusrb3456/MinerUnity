using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inDesMusic : MonoBehaviour
{
    public Camera main;
    
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("InGameBGM"));
        if (PlayerPrefs.GetInt("InGameBGM") == 0)
        {
            main.GetComponent<AudioSource>().Play();
        }
        else
        {
            main.GetComponent<AudioSource>().Pause();
        }
    }

    public void InbgmOff()
    {
        PlayerPrefs.SetInt("InGameBGM", 1);
        main.GetComponent<AudioSource>().Pause();
    }
    public void InbgmOn()
    {
        PlayerPrefs.SetInt("InGameBGM", 0);
        main.GetComponent<AudioSource>().Play();
    }
}
