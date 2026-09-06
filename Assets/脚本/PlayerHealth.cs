using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    PlayerController playerController;
    public Image healthBarFill;
    public GameOverUI gameOverUI;

    int currentHealth;
    Animator anim;
    SpriteRenderer sr;
    bool dead;
    Rigidbody2D rb;

    PlayerDefense defense;
    public ParticleSystem hitEffect;
    public AudioSource BossBGM;
    public AudioSource SuspenseBGM;
    public AudioSource GameBGM;
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        defense=GetComponent<PlayerDefense>();
        sr = GetComponent<SpriteRenderer>();
        playerController =GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }



    public void TakeDamage(int damage)
    {
        if(dead)
        return;
         PlayerSound sound = GetComponent<PlayerSound>();

        if(defense != null && defense.IsDefending())
        {
            hitEffectPlay();
            Debug.Log("防御成功");
           
            if(sound != null)
            {
                sound.PlayDefenseSound();
            }
            return;
        }


        currentHealth -= damage;
        


if(sound != null)
{
    sound.PlayHurtSound();
}


        Debug.Log(
        "玩家受到伤害:"
        + damage
        );
        UpdateHealthBar();


        if(currentHealth <= 0)
        {
            Die();
        }

    }
    void UpdateHealthBar()
    {

        if(healthBarFill != null)
        {

            healthBarFill.fillAmount =
            (float)currentHealth / maxHealth;

        }

    }
    void hitEffectPlay()
{

    if(sr.flipX)
    {
        hitEffect.transform.localRotation =
        Quaternion.Euler(0,180,0);
    }
    else
    {
        hitEffect.transform.localRotation =
        Quaternion.identity;
    }


    hitEffect.Play();

}



    void Die()
    {
        dead=true;
        BossBGM.Stop();
        SuspenseBGM.Stop();
        GameBGM.Stop();

        if(playerController != null)
        {
        playerController.isDead=true;
        }
        rb.velocity = Vector2.zero;
        
        if(anim != null)
        {
            anim.SetTrigger("Death");
        }
        Debug.Log("玩家死亡");
        StartCoroutine(GameOver());
    }
    IEnumerator GameOver()
    {

        //这里时间根据你的死亡动画长度调整
        yield return new WaitForSecondsRealtime(2f);



        gameOverUI.Show();

    }
    
}
