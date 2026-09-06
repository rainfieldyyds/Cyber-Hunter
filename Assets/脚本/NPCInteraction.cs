using UnityEngine;

public class NPCInteraction : MonoBehaviour
{

    public GameObject talkHint;

    public GameObject dialogueBox;
    public DialogueSystem dialogueSystem;
    public PlayerController playerController;
    public HintUI hintUI;

    bool playerInRange;

    void Start()
    {
        
        dialogueBox.SetActive(false);
    }



    void Update()
    {

        if(playerInRange)
        {

            if(Input.GetKeyDown(KeyCode.F))
            {

                

                    playerController.canControl=false;

                    OpenDialogue();
                

            }

        }

    }



    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Player"))
        {

            playerInRange=true;

            hintUI.Show();
        }

    }



    void OnTriggerExit2D(Collider2D other)
    {

        if(other.CompareTag("Player"))
        {

            playerInRange=false;

            

            dialogueBox.SetActive(false);

        }

    }



    void OpenDialogue()
{

    hintUI.Hide();

    dialogueBox.SetActive(true);

    dialogueSystem.StartDialogue();

}

}