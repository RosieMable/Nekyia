using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour {

    //The signalListener "listens" to a specific game object from which it is waiting to hear a "signal"
    //To that signal there is an event that the listener will call

    public Signal signal;
    public UnityEvent signalEvent;

    public void OnRaisedSignal()
    {
        signalEvent.Invoke();
    }

    private void OnEnable()
    {
        signal.RegisterListener(this);
    }

    //Removes item from the list when disable, so it doesn't take up memory
    private void OnDisable()
    {
        signal.DeRegisterListener(this);
    }

}
