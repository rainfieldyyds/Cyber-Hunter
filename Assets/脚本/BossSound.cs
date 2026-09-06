using UnityEngine;


public class BossSound : MonoBehaviour
{

    public AudioSource audioSource;


    [Header("攻击")]
    public AudioClip attackSound;


    [Header("跳跃攻击")]
    public AudioClip jumpAttackSound;


    [Header("受击")]
    public AudioClip hurtSound;


    [Header("死亡")]
    public AudioClip deathSound;



    //普通攻击和重击共用
    public void PlayAttackSound()
    {

        if(attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }

    }



    public void PlayJumpAttackSound()
    {

        if(jumpAttackSound != null)
        {
            audioSource.PlayOneShot(jumpAttackSound);
        }

    }



    public void PlayHurtSound()
    {

        if(hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

    }



    public void PlayDeathSound()
    {

        if(deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

    }

}