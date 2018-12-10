using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script for the signal, it can be created as a scriptable object from the IDE
[CreateAssetMenu]
public class Signal : ScriptableObject {

    public List<SignalListener> Listeners = new List<SignalListener>();

    public void Raise()
    {
        //For loop that goes backwards, so if the list gets modified, it doesn't give a range error
        for (int i = Listeners.Count - 1; i >= 0; i--)
        {
            //Whatever it needs to be done on signal raised, it will be done now
            Listeners[i].OnRaisedSignal();
        }
    }

    #region Remove and Add stuff from the listener list
    public void RegisterListener(SignalListener listener)
    {
        Listeners.Add(listener);
    }

    public void DeRegisterListener(SignalListener listener)
    {
        Listeners.Remove(listener);
    }

    #endregion
}
