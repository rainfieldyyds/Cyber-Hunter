using System.Collections;
using TMPro;
using UnityEngine;


public class DialogueSystem : MonoBehaviour
{

    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    [TextArea(2,5)]
    public string[] dialogues;


    public float typeSpeed = 0.05f;
    public PlayerController playerController;


    int index;

    bool typing;

    bool skip;
    bool dialogueActive;
    public AudioSource audioSource;

public AudioClip dialogueSound;
public BGMManager bgmManager;



    void Update()
{

    if(!dialogueActive)
        return;


    if(Input.GetMouseButtonDown(0))
    {

        if(typing)
        {
            skip=true;
        }
        else
        {
            NextDialogue();
        }

    }

}



   public void StartDialogue()
{

    StopAllCoroutines();


    dialogueActive=true;


    index=0;

    typing=false;

    skip=false;


    dialogueText.text="";


    StartCoroutine(TypeText());

}



    IEnumerator TypeText()
{

    typing=true;

    skip=false;
    //每句话开始播放一次
    if(audioSource != null && dialogueSound != null)
    {
        audioSource.PlayOneShot(dialogueSound);
    }


    dialogueText.text="";


    foreach(char c in dialogues[index])
    {

        if(skip)
        {
            dialogueText.text=dialogues[index];
            break;
        }


        dialogueText.text += c;


        yield return new WaitForSeconds(typeSpeed);

    }


    typing=false;

}



    void NextDialogue()
    {

        index++;


        if(index >= dialogues.Length)
        {
            EndDialogue();
            return;
        }


        StartCoroutine(TypeText());

    }



    void EndDialogue()
{

    dialogueActive=false;


    StopAllCoroutines();


    dialogueBox.SetActive(false);


    playerController.canControl=true;
    //恢复探索BGM
    if(GameStateManager.instance.bossDead)
{

    if(bgmManager != null)
    {
        bgmManager.PlayNormalBGM();
    }

}


}
}