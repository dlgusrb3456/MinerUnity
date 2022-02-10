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
         for (int i = 0; i < Map.localMaps.Count; i++)
        {

            if (Map.localMaps[i].name == text_mironame.text)
            {
                string mapSizes = Map.localMaps[i].mapSize;
                if(mapSizes == "18X18")
                {
                    PlayerPrefs.SetInt("loadSize", 1);
                }
                else if(mapSizes == "34X34")
                {
                    PlayerPrefs.SetInt("loadSize", 2);
                }
                else if(mapSizes == "52X52")
                {
                    PlayerPrefs.SetInt("loadSize", 3);
                }
                else
                {
                    Debug.Log("사이즈 오류");
                }

            }
           

         }
        PlayerPrefs.SetInt("loadMode", 1);
        SceneManager.LoadScene("loadingScene");

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
