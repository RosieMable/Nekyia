using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Hero>())
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Hero>())
        {

            if (Input.GetKeyDown(KeyCode.Return))
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Hero>())
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }

    private void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
        