using UnityEngine;
using System.Collections;

public class HintUI : MonoBehaviour
{

    public CanvasGroup canvasGroup;

    public float fadeSpeed = 3f;


    Coroutine fadeCoroutine;



    void Start()
    {
        canvasGroup.alpha = 0;
    }



    public void Show()
    {

        if(fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);


        fadeCoroutine =
        StartCoroutine(Fade(1));
        

    }



    public void Hide()
    {

        if(fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);


        fadeCoroutine =
        StartCoroutine(Fade(0));

    }



    IEnumerator Fade(float target)
    {

        while(Mathf.Abs(canvasGroup.alpha-target)>0.01f)
        {

            canvasGroup.alpha =
            Mathf.MoveTowards(
                canvasGroup.alpha,
                target,
                fadeSpeed * Time.deltaTime
            );


            yield return null;

        }


        canvasGroup.alpha=target;

    }

}