using UnityEngine;


public class PlayerSpawn : MonoBehaviour
{


    void Start()
    {

        transform.position =
        RespawnManager.instance.GetRespawnPosition();

    }

}