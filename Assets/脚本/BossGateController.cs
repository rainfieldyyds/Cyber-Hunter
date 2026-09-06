using UnityEngine;


public class BossGateController : MonoBehaviour
{

    public GameObject frontGate;
    public GameObject backGate;



    //开始Boss战，关闭道路
    public void LockBossRoom()
    {

        if(frontGate != null)
            frontGate.SetActive(true);


        if(backGate != null)
            backGate.SetActive(true);

    }



    //Boss死亡，打开道路
    public void UnlockBossRoom()
    {

        if(frontGate != null)
            frontGate.SetActive(false);


        if(backGate != null)
            backGate.SetActive(false);

    }

}