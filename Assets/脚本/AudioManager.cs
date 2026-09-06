using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{

    public AudioMixer audioMixer;


    public void SetVolume(float value)
    {

        //value范围 0~1

        float volume;


        if(value <= 0.001f)
        {
            volume = -80f;
        }
        else
        {
            volume = Mathf.Log10(value) * 20;
        }


        audioMixer.SetFloat(
            "MasterVolume",
            volume
        );

    }

}