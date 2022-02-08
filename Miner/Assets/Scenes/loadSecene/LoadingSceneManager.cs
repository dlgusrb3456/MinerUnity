﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public static int nextScene;

    [SerializeField]
    Image progressBar;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(int index)
    {
        nextScene = index;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op;
        op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;

        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            if(op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if(progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if(progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }

        // 다른 코드에서 호출할 때 >LoadingSceneManager.LoadScene(1); <
        // 인수로 전달할 값은 빌드세팅에 있는 각 씬의 인덱스를 작성
    }
}
