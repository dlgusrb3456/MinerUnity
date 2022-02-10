using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public static int nextScene;

    [SerializeField]
    Image progressBar;
    bool IsDone = false;
    float timer = 0.0f;
    AsyncOperation op;
    float loadTime = 2.0f;
    GameObject BackgroundMusic;
    AudioSource backmusic;
    int loadMode = 0;
    //loadMode 1 : 메인설계 => 인게임 설계

    private void Start()
    {

        BackgroundMusic = GameObject.FindGameObjectWithTag("mainBGM");
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); //배경음악 저장해둠

        progressBar.fillAmount = 0;
        int sizeMode = PlayerPrefs.GetInt("loadSize");
        Debug.Log("sizeMode = " + sizeMode);
        if(sizeMode == 1)
        {
            loadTime = 2.0f;
        }
        else if(sizeMode == 2)
        {
            loadTime = 5.0f;
        }
        else if(sizeMode == 3)
        {
            loadTime = 12.0f;
        }

        loadMode = PlayerPrefs.GetInt("loadMode");
        StartCoroutine(LoadScene(loadMode));
    }

    //public static void LoadScene(int index)
    //{
    //    //nextScene = index;
    //    SceneManager.LoadScene("LoadingScene");
    //}

    void Update()
    {
        timer += (1.0f / loadTime)*Time.deltaTime;
        progressBar.fillAmount = timer; 

        if(timer >= loadTime)
        {
            op.allowSceneActivation = true;
        }
        else
        {
            if(loadMode == 4 || loadMode == 6)
            {
                if (PlayerPrefs.GetInt("mainBGM") == 0)
                {
                    backmusic.Play();
                }
                else
                {
                    backmusic.Pause();
                }
            }
        }

    }

    IEnumerator LoadScene(int mode)
    {

        if(mode == 1)
        {
            backmusic.Pause();
            op = SceneManager.LoadSceneAsync("InGameDesign");
        }
        else if(mode == 2 || mode == 5)
        {
            op = SceneManager.LoadSceneAsync("InGamePlay");
        }
        else if (mode == 3)
        {
            op = SceneManager.LoadSceneAsync("InGameDesign");
        }
        else if(mode == 4)
        {
            
            op = SceneManager.LoadSceneAsync("mainDesign");
        }
        else if(mode == 6)
        {
            
            op = SceneManager.LoadSceneAsync("mainPlay");
        }
        else if(mode == 7)
        {
            backmusic.Pause();
            op = SceneManager.LoadSceneAsync("InGamePlay");
        }
        else
        {
            op = SceneManager.LoadSceneAsync("mainDesign");
        }
        yield return null;
         // ""에 nextScene 있던거
        op.allowSceneActivation = false;

        if (IsDone == false)
        {
            IsDone = true;

            while(op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;

                yield return true;
            }
        }
        

        // 다른 코드에서 호출할 때 >LoadingSceneManager.LoadScene(1); <
        // 인수로 전달할 값은 빌드세팅에 있는 각 씬의 인덱스를 작성
    }
}
