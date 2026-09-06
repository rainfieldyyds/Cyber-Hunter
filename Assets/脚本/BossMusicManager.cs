using UnityEngine;


public class BossMusicManager : MonoBehaviour
{

    public AudioSource suspenseBGM;

    public AudioSource bossBGM;



    //进入Boss战
    public void StartBossMusic()
    {

        //关闭悬疑音乐
        if(suspenseBGM != null)
        {
            suspenseBGM.Stop();
        }


        //播放Boss音乐
        if(bossBGM != null)
        {
            bossBGM.Play();
        }

    }



    //Boss死亡
    public void StopBossMusic()
    {

        if(bossBGM != null)
        {
            bossBGM.Stop();
        }

    }

}