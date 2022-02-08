﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptPrint : MonoBehaviour
{
    public Text scriptText; 

    private int currentPageIndex = 0;
    private string currentPageString = TutorialScript.scripts[0];
    private int currentPageStringIndex = 0;

    private float timer = 0;

    void Update()
    {
        timer -= Time.deltaTime;

        while (timer <= 0f)
        {
            timer += .1f;

            if (currentPageStringIndex < currentPageString.Length) 
                scriptText.text = currentPageString.Substring(0, currentPageStringIndex++);

            if (Input.touchCount > 0)
            {
                currentPageString = TutorialScript.scripts[++currentPageIndex];
                currentPageStringIndex = 0;
            }
            else
            {
                // 마지막 장에서 터치했을때 여기로 옵니다. 여기에 설계/플레이 화면으로 넘어가는 코드를 넣어주세요
            }
        }

        
    }
}