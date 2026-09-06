using UnityEngine;


public class ExitGame : MonoBehaviour
{

    public void QuitGame()
    {

        Time.timeScale=1;


#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying=false;

#else

        Application.Quit();

#endif

    }

}