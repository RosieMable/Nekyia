using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver {

    public float initialValue;

    [HideInInspector]
    public float RuntimeValue; //Caching the initial value, so everytime the game is restarted, the value is reset


    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    //Need this method to implement the interface ISerialization
    public void OnBeforeSerialize()
    {
    }
}
