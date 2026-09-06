using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    public AudioSource audioSource;

    public AudioClip attackSound;
    public AudioClip hurtSound;
    public AudioClip defenseSound;

    public void PlayAttackSound()
    {

        if(attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }

    }
    public void PlayHurtSound()
    {

        if(hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

    }
     public void PlayDefenseSound()
    {
        if(defenseSound != null)
        {
            audioSource.PlayOneShot(defenseSound);
        }
    }
}