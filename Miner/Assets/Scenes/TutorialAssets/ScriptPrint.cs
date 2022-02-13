using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScriptPrint : MonoBehaviour
{
    public Text scriptText;
    public GameObject mainPanel;
    public Image scriptImage;
    public Sprite[] pages;

    private int currentPageIndex = 0;
    private string currentPageString = TutorialScript.scripts[0];
    private int currentPageStringIndex = 0;

    private bool isCanvasClicked = false;

    private float timer = 0;

    void Start()
    {
        mainPanel.GetComponent<Image>().sprite= pages[currentPageIndex];
    }

    public void onCanvasClick() => isCanvasClicked = true;

    void Update()
    {
        timer -= Time.deltaTime;

        while (timer <= 0f)
        {
            timer += .1f;

            if (currentPageStringIndex < currentPageString.Length) 
                scriptText.text = currentPageString.Substring(0, currentPageStringIndex++);

            if (isCanvasClicked)
            {
                isCanvasClicked = false;
                if (currentPageIndex == TutorialScript.scripts.Length - 1)
                {
                    SceneManager.LoadScene("mainDesign");
                    return;
                }
                currentPageString = TutorialScript.scripts[++currentPageIndex];
                currentPageStringIndex = 0;
                mainPanel.GetComponent<Image>().sprite = pages[currentPageIndex];
            }
        }

        
    }
}
