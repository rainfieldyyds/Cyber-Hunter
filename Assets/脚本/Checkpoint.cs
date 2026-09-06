using UnityEngine;


public class Checkpoint : MonoBehaviour
{

    public Transform respawnPoint;


    bool activated;



    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Player") && !activated)
        {

            activated=true;


            RespawnManager.instance
            .SetCheckpoint(respawnPoint.position);


            Debug.Log("Boss房checkpoint激活");

        }

    }

}