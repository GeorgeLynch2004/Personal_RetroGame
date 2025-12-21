using System;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    // listens to game event
    public GameEvent Event;
    
    // response for when game event is raised
    public UnityEvent response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }
}
