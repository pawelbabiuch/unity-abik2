using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{

    private readonly List<GameEventListener> eventListeners = new List<GameEventListener>();

	public void raise()
    {
        for (int i = eventListeners.Count -1;  i >= 0; i--)
        {
            eventListeners[i].onEventRaise();
        }
    }

    public void registerListener(GameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void unregisterListener(GameEventListener listener)
    {
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }
}
