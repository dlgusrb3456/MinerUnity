using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class loadingScripts : MonoBehaviour
{
    public Image circleProgress;
    private float progress = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        progress += 0.3f * Time.deltaTime;
        if (progress > 1)
        {
            progress = 0;
        }
        circleProgress.fillAmount = progress;
    }
}
