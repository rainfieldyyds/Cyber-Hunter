using UnityEngine;
using Cinemachine;


public class BossCameraController : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCamera;


    public Transform player;

    public Transform bossCameraPoint;



    //进入Boss战
    public void StartBossCamera()
    {

        virtualCamera.Follow = bossCameraPoint;

    }



    //恢复玩家视角
    public void EndBossCamera()
    {

        virtualCamera.Follow = player;

    }

}