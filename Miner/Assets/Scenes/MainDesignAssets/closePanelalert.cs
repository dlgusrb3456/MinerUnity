using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class closePanelalert : MonoBehaviour
{
    public GameObject alertPanel;

    public void closeAlertPanel()
    {
        alertPanel.SetActive(false);
        SceneManager.LoadScene("mainDesign");
    }
}
