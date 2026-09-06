using UnityEngine;


public class BossStartTrigger : MonoBehaviour
{

    public BossCameraController cameraController;

    public BossAI bossAI;
    public GameObject bossHealthBar;
    public BossMusicManager musicManager;
    public AudioSource GameBGM;
public BossGateController gateController;

    bool triggered;



    private void OnTriggerEnter2D(Collider2D other)
    {

        if(triggered)
            return;


        if(other.CompareTag("Player"))
        {

            triggered=true;


            //切换摄像机
            cameraController.StartBossCamera();
            //关闭Boss房
        if(gateController != null)
        {
            gateController.LockBossRoom();
        }


            //开启Boss AI
            if(bossAI != null)
            {
                bossAI.enabled=true;
            }
            bossHealthBar.SetActive(true);
            //开启Boss音乐
        if(musicManager != null)
        {
            musicManager.StartBossMusic();
        }
        //停止游戏BGM
        if(GameBGM != null)
        {
            GameBGM.Stop();
        }


        }

    }

}