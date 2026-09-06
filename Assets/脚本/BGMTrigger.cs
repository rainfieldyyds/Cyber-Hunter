using UnityEngine;


public class BGMTrigger : MonoBehaviour
{

    public BGMManager bgmManager;


    bool triggered;



    private void OnTriggerEnter2D(Collider2D other)
    {

        if(triggered)
            return;


        if(other.CompareTag("Player"))
        {

            triggered=true;


            bgmManager.PlaySuspense();

        }

    }

}