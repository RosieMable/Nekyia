using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {


  

    [SerializeField]
    private Text txt_dialogueText;

    [SerializeField]
    private GameObject DialogueBox;

    //A queue works similarly to a list, but it is a data type of type FIFO (first in first out)
    private Queue <string> qq_sentences;



	// Use this for initialization
	void Start () {


        qq_sentences = new Queue<string>();

	}
	
    public void StartDialogue (Dialogue dialogue)
    {
        DialogueBox.SetActive(true);

        qq_sentences.Clear();

        foreach (string qq_senteces in dialogue.str_sentences)
        {
            qq_sentences.Enqueue(qq_senteces);
        }

        DisplayNextSentence();
    }



    public void DisplayNextSentence()
    {

        if (qq_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string str_sentence = qq_sentences.Dequeue();
        StopAllCoroutines(); //If the Player starts a new sentence before the previous has finished, this will stop the previous coroutine and start the new one
        StartCoroutine(TypeSentence(str_sentence));

    }
    

    IEnumerator TypeSentence (string sentence)
    {
        txt_dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            txt_dialogueText.text += letter;
            yield return null;
        }

    }

    public void EndDialogue()
    {

        DialogueBox.SetActive(false);
    }

}
