using System.Collections;
using UnityEngine;


public class GameOverUI : MonoBehaviour
{

    public CanvasGroup canvasGroup;


    public float fadeTime = 1.5f;
    public AudioSource audioSource;

public AudioClip gameOverSound;



    public void Show()
    {

        gameObject.SetActive(true);
         //播放GameOver音效
        if(audioSource != null && gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

        StartCoroutine(Fade());

    }



    IEnumerator Fade()
    {

        canvasGroup.alpha = 0;


        float timer = 0;


        while(timer < fadeTime)
        {

            timer += Time.unscaledDeltaTime;


            canvasGroup.alpha =
            timer / fadeTime;


            yield return null;

        }


        canvasGroup.alpha = 1;


        //渐入完成后暂停游戏
        Time.timeScale = 0;

    }

}