using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class regextest : MonoBehaviour
{
    public InputField input;
    public Text texts;
    private Regex regex;

    // Start is called before the first frame update
    void Start()
    {
        regex = new Regex(@"^(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[\W]).{8,20}$");
    }

    // Update is called once per frame
    void Update()
    {
        if(input.text != "")
        {
            if (regex.IsMatch(input.text))
            {
                texts.text = "성공";
            }
            else
            {
                texts.text = "실패";
            }
        }
    }
}
