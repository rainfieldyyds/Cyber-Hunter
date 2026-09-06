using System.Collections;
using UnityEngine;


public class BGMManager : MonoBehaviour
{

    public AudioSource normalBGM;
    public AudioSource suspenseBGM;


    public float fadeTime = 3f;


    bool changed;



    public void PlaySuspense()
    {

        if(changed)
            return;


        changed=true;


        StartCoroutine(ChangeMusic());

    }



    IEnumerator ChangeMusic()
    {

        float timer = 0;


        float normalStart =
        normalBGM.volume;


        suspenseBGM.Play();


        while(timer < fadeTime)
        {

            timer += Time.deltaTime;


            float t =
            timer / fadeTime;


            normalBGM.volume =
            Mathf.Lerp(
                normalStart,
                0,
                t
            );


            suspenseBGM.volume =
            Mathf.Lerp(
                0,
                1,
                t
            );


            yield return null;

        }


        normalBGM.Stop();

        normalBGM.volume=normalStart;

    }
    public void PlayNormalBGM()
    {

        if(normalBGM != null)
        {
            normalBGM.Play();
        }

    }


    public void StopNormalBGM()
    {

        if(normalBGM != null)
        {
            normalBGM.Stop();
        }

    }

}