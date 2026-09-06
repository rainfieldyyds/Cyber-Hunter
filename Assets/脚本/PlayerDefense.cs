using UnityEngine;

public class PlayerDefense : MonoBehaviour
{

    public Collider2D defenseCollider;

    public SpriteRenderer playerSprite;


    Vector2 originalOffset;


    bool defending;



    void Start()
    {

        if(defenseCollider != null)
        {
            defenseCollider.enabled=false;

            BoxCollider2D box =
            defenseCollider as BoxCollider2D;

            if(box != null)
            {
                originalOffset = box.offset;
            }
        }



        if(playerSprite == null)
        {
            playerSprite =
            GetComponent<SpriteRenderer>();
        }

    }



    public void EnableDefense()
    {

        defending=true;


        FlipDefense();


        if(defenseCollider != null)
        {
            defenseCollider.enabled=true;
        }

    }



    public void DisableDefense()
    {

        defending=false;


        if(defenseCollider != null)
        {
            defenseCollider.enabled=false;
        }

    }



    void FlipDefense()
    {

        BoxCollider2D box =
        defenseCollider as BoxCollider2D;


        if(box == null)
            return;



        if(playerSprite.flipX)
        {
            //朝左
            box.offset =
            new Vector2(
                -originalOffset.x,
                originalOffset.y
            );
        }
        else
        {
            //朝右
            box.offset =
            originalOffset;
        }

    }



    public bool IsDefending()
    {
        return defending;
    }

}