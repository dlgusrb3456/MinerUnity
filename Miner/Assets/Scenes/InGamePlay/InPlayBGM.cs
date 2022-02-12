using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InPlayBGM : MonoBehaviour
{
    public Camera main;

    void Start()
    {
        //Debug.Log(PlayerPrefs.GetInt("InPlayBGM"));
        if (PlayerPrefs.GetInt("InPlayBGM") == 0)
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
        PlayerPrefs.SetInt("InPlayBGM", 1);
        main.GetComponent<AudioSource>().Pause();
    }
    public void InbgmOn()
    {
        PlayerPrefs.SetInt("InPlayBGM", 0);
        main.GetComponent<AudioSource>().Play();
    }
}
