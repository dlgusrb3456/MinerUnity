using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TTS : MonoBehaviour
{
    Text flashingText;

    void Start() { 
        flashingText = GetComponent<Text>(); 
        StartCoroutine(BlinkText()); 
    }


    public IEnumerator BlinkText() { 
        while (true) { 
            flashingText.text = ""; 
            yield return new WaitForSeconds(.5f); 
            flashingText.text = "Tab to Start"; 
            yield return new WaitForSeconds(.5f); 
        } 
    }

    
}
