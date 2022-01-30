using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fileClicked : MonoBehaviour
{
    // Start is called before the first frame update

    public Text text_mironame;

    public void fileOnclick()
    {
        PlayerPrefs.SetString("DesSelectedFile", text_mironame.text);
        SceneManager.LoadScene("InGameDesign");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
