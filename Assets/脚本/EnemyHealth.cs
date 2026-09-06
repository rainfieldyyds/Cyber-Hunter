using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int hp = 30;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    Animator anim;


    public void TakeDamage(int damage)
    {

        hp -= damage;


        Debug.Log(
        "Enemy受到伤害:"
        + damage
        );


        if(hp<=0)
        {
            Die();
        }

    }



    void Die()
    {
        EnemyPatrol ai = GetComponent<EnemyPatrol>();

    if(ai != null)
    {
        ai.enabled = false;
    }
    //关闭攻击HitBox
        EnemyHitBox hitBox =
        GetComponentInChildren<EnemyHitBox>();


        if(hitBox != null)
        {

            Collider2D col =
            hitBox.GetComponent<Collider2D>();


            if(col != null)
            {
                col.enabled = false;
            }

        }


        if(anim != null)
        {
            anim.SetTrigger("Death");
        }
        Destroy(gameObject,5f);
    }

}