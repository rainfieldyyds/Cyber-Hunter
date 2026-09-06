using UnityEngine;


public class StartPoint : MonoBehaviour
{

    void Start()
    {

        if(!RespawnManager.instance.HasCheckpoint())
        {

            RespawnManager.instance
            .SetCheckpoint(transform.position);

        }

    }

}