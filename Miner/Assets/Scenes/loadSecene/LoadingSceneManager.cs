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
    
        

    private void Start()
    {
        progressBar.fillAmount = 0;
        StartCoroutine(LoadScene());
        
    }

    public static void LoadScene(int index)
    {
        //nextScene = index;
        SceneManager.LoadScene("LoadingScene");
    }

    void Update()
    {
        timer += Time.deltaTime;
        progressBar.fillAmount = timer; 

        if(timer >= 10)
        {
            op.allowSceneActivation = true;
        }

    }

    IEnumerator LoadScene()
    {
        yield return null;
        op = SceneManager.LoadSceneAsync("InGameDesign"); // ""에 nextScene 있던거
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
