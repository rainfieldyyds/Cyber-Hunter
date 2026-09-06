using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{

    [Header("血量")]
    public int maxHealth = 500;
    public GameObject bossHealthBar;

    private int currentHealth;
    public BossMusicManager musicManager;


    [Header("受击")]
    public float hurtCooldown = 0.2f;
    public Image healthBarFill;

    private bool canTakeDamage = true;
    bool dead = false;
    BossSound sound;
    Animator anim;
public BossGateController gateController;

    void Start()
    {

        currentHealth = maxHealth;

        anim = GetComponent<Animator>();
        sound=GetComponent<BossSound>();

    }



    public void TakeDamage(int damage)
    {

        if(!canTakeDamage|| dead)
            return;


        currentHealth -= damage;
        if(sound != null)
    {
        sound.PlayHurtSound();
    }


        Debug.Log(
            "Boss受到伤害:" 
            + damage +
            " 当前血量:" 
            + currentHealth
        );
        UpdateHealthBar();



        if(currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Invoke(nameof(ResetDamage), hurtCooldown);
        }

    }



    void ResetDamage()
    {
        canTakeDamage = true;
    }


    public BossCameraController cameraController;
    void Die()
{

    if(dead)
        return;


    dead = true;
    GameStateManager.instance.bossDead = true;
     //打开Boss房
    if(gateController != null)
    {
        gateController.UnlockBossRoom();
    }


    //停止Boss音乐
    if(musicManager != null)
    {
        musicManager.StopBossMusic();
    }

    if(sound != null)
{
    sound.PlayDeathSound();
}


    Debug.Log("Boss死亡");
    bossHealthBar.SetActive(false);


    BossAI ai = GetComponent<BossAI>();

    if(ai != null)
    {
        ai.enabled = false;
    }



    //关闭攻击HitBox
    Transform hit =
    transform.Find("AttackHitBox");


    if(hit != null)
    {
        Collider2D attack =
        hit.GetComponent<Collider2D>();

        if(attack != null)
            attack.enabled=false;
    }


    HitBox hb = GetComponentInChildren<HitBox>();

    if(hb != null)
    {
        hb.DisableHitBox();
    }

    if(anim != null)
    {
        anim.ResetTrigger("Attack");
        anim.ResetTrigger("HeavyAttack");
        anim.ResetTrigger("JumpAttack");

        anim.SetTrigger("Death");
    }
    cameraController.EndBossCamera();


}
void UpdateHealthBar()
{

    if(healthBarFill != null)
    {

        healthBarFill.fillAmount =
        (float)currentHealth / maxHealth;

    }

}



    public int GetCurrentHealth()
    {
        return currentHealth;
    }


}