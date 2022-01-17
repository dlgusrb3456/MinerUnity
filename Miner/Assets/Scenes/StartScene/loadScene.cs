using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadScene : MonoBehaviour
{
    // Start is called before the first frame update
    bool autoLogin = false;
    void Start()
    {
        Debug.Log("asdf loaded");
        // MinerEnvironments 로 초기 디렉터리를 설정합니다.
        MinerEnvironment.initEnvironment();

        // 여기서 자동로그인이 되어 있다면 bool 변수 값에 true를 아니라면 false를 줌
    }

    // Update is called once per frame
    void Update()
    {
        // 화면 터치시 혹은 몇초 후에 autoLogin의 값을 확인하고
        if(Input.touchCount>0) // 또는 몇초 후에
            if (autoLogin) 
            {
                //메인화면_설계로 바로 이동
            }
            else
            {
                //로그인 화면으로 이동
                SceneManager.LoadScene("loginMain");
            }
    }
}
