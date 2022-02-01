using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class closePanelalert : MonoBehaviour
{
    public GameObject alertPanel;

    public void closeAlertPanel()
    {
        alertPanel.SetActive(false);
    }
}
