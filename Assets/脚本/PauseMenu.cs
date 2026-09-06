using UnityEngine;


public class PauseMenu : MonoBehaviour
{

    public GameObject pausePanel;
    public PlayerController player;


    bool paused;



    void Start()
    {
        pausePanel.SetActive(false);
    }



    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {

            if(paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }

    }

public UIButtonSound buttonSound;

    public void Pause()
{

    paused=true;

    pausePanel.SetActive(true);
    buttonSound.PlayClickSound();

    Time.timeScale=0;


    if(player!=null)
        player.enabled=false;

}


    public void Resume()
{

    paused=false;

    pausePanel.SetActive(false);

    Time.timeScale=1;
    buttonSound.PlayClickSound();


    if(player!=null)
        player.enabled=true;

}

}