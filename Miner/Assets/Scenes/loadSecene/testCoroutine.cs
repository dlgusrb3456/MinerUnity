using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
public class testCoroutine : MonoBehaviour
{
    public bool isDelay = false;
    public float delayTime = 5.0f;
    public float accumTime;

    public GameObject testPanel;
    public Image circleProgress;

    private float progress = 0.0f;

    public void testbutton()
    {
        if(isDelay == false)
        {
            Debug.Log("시작");
            
            
            StartCoroutine(testDrink());
            //Thread.Sleep(5000);
            
        }
        else
        {
            Debug.Log("아직 안됨");
        }
    }

    IEnumerator testDrink()
    {
        testPanel.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        for(int i = 0; i < 10000; i++)
        {
            Debug.Log(i);
        }
        testPanel.SetActive(false);
        isDelay = false;
    }


    // Update is called once per frame
    void Update()
    {
        progress += 0.3f*Time.deltaTime;
        if (progress > 1)
        {
            progress = 0;
        }
        circleProgress.fillAmount = progress;
    }
    void Start()
    {
        testPanel.SetActive(false);
    }
}
