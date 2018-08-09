using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour {

    [Tooltip("Event to register with.")]
    public GameEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent response;

    private void OnEnable()
    {
        Event.registerListener(this);
    }

    private void OnDisable()
    {
        Event.unregisterListener(this);
    }

    public void onEventRaise()
    {
        response.Invoke();
    }
}
