using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    [SerializeField]
    private GameObject DialogueBox;

    [SerializeField]
    private Text dialogueText;

    [SerializeField]
    private string dialogue;

    [SerializeField]
    private bool isDialogActive;

    public GameObject Player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        Player = FindObjectOfType<Hero>().gameObject;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            DialogueBox.SetActive(true);
            StartCoroutine("WriteText");
            isDialogActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            DialogueBox.SetActive(false);
            isDialogActive = false;
        }
    }

    private IEnumerator WriteText()
    {
        dialogueText.text = "";

        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.10f);
        }
    }
}
