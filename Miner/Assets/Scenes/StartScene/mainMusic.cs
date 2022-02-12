using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMusic : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Awake()
    {
        //Debug.Log(PlayerPrefs.GetInt("mainBGM"));
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("mainBGM") == 0)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Pause();
        }
    }
    
}
