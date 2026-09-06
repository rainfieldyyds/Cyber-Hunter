using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{

    public CanvasGroup fadePanel;

    public float fadeTime = 1f;

    public string sceneName;


    [Header("标题音乐")]
    public AudioSource titleBGM;

    public float musicFadeTime = 1f;



    public void StartGame()
    {
        Debug.Log("点击开始");

        StartCoroutine(FadeOut());

    }



    IEnumerator FadeOut()
    {

        float timer = 0;


        float startVolume = 0;


        if(titleBGM != null)
        {
            startVolume = titleBGM.volume;
        }



        while(timer < fadeTime)
        {

            timer += Time.deltaTime;


            float progress =
            timer / fadeTime;



            //黑屏渐入
            if(fadePanel != null)
            {
                fadePanel.alpha = progress;
            }



            //音乐渐弱
            if(titleBGM != null)
            {
                titleBGM.volume =
                Mathf.Lerp(
                    startVolume,
                    0,
                    progress
                );
            }


            yield return null;

        }



        //完全停止标题音乐
        if(titleBGM != null)
        {
            titleBGM.Stop();
        }



        SceneManager.LoadScene(sceneName);

    }

}