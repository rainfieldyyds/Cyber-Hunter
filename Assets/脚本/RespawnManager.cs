using UnityEngine;


public class RespawnManager : MonoBehaviour
{

    public static RespawnManager instance;


    public Vector3 respawnPosition;


    bool hasCheckpoint=false;



    void Awake()
    {

        if(instance == null)
        {
            instance=this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }



    public void SetCheckpoint(Vector3 pos)
    {

        respawnPosition=pos;

        hasCheckpoint=true;


        Debug.Log(
        "更新复活点:"
        +pos
        );

    }



    public bool HasCheckpoint()
    {
        return hasCheckpoint;
    }



    public Vector3 GetRespawnPosition()
    {
        return respawnPosition;
    }

}