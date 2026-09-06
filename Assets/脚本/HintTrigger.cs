using UnityEngine;

public class HintTrigger : MonoBehaviour
{

    public HintUI hintUI;



    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Player"))
        {
            hintUI.Show();
        }

    }



    void OnTriggerExit2D(Collider2D other)
    {

        if(other.CompareTag("Player"))
        {
            hintUI.Hide();
        }

    }

}