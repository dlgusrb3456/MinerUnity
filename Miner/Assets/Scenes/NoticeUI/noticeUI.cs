using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class noticeUI : MonoBehaviour
{

    [Header("SubNotice")]
    public GameObject subbox;
    public Text subintext;


    private WaitForSeconds _UIDelay1 = new WaitForSeconds(2.0f);
    private WaitForSeconds _UIDelay2 = new WaitForSeconds(0.3f);



    // Start is called before the first frame update
    void Start()
    {
        subbox.SetActive(false);
    }


    public void SUB(string message)
    {
        subintext.text = message;
        subbox.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(SUBDelay());
    }

    IEnumerator SUBDelay()
    {
        subbox.SetActive(true);
        yield return _UIDelay1;
        yield return _UIDelay2;
        subbox.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
