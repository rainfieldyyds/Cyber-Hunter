using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartButton : MonoBehaviour
{


    public GameObject gameOverUI;



    public void Restart()
    {

        Time.timeScale=1;


        SceneManager.LoadScene(
        SceneManager.GetActiveScene().name
        );

    }

}