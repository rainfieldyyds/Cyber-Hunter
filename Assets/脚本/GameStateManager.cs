using UnityEngine;


public class GameStateManager : MonoBehaviour
{

    public static GameStateManager instance;


    public bool bossDead;



    void Awake()
    {

        instance=this;

    }


}