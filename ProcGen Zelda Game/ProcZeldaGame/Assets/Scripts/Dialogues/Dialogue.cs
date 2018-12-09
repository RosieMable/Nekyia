using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue  {

    //To make the boxes in the inspector nicer and easier to use
    [TextArea(3, 10)]
    public string[] str_sentences; 
    //Sentences that will be called in the queue
}
