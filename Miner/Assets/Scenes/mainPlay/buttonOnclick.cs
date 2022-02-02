using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class buttonOnclick : MonoBehaviour
{
    public Text pageNo;
    public void clickPageButton()
    {
        //Debug.Log(pageNo.text);
        PlayerPrefs.SetInt("currentPage", Convert.ToInt32(pageNo.text));
    }
}
