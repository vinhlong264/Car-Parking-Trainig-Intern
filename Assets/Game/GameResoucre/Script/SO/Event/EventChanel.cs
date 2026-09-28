using System.Collections.Generic;
using UnityEngine;

public abstract class EventChanel<T> : ScriptableObject
{
    private readonly Dictionary<EventType, System.Action<T>> eventListeners = new Dictionary<EventType, System.Action<T>>();

    public void subscribeObserver(EventType key, System.Action<T> o)
    {
        if (eventListeners.ContainsKey(key)) return;

        eventListeners.Add(key, o);

    }

    public void unsubscribeObserver(EventType key, System.Action<T> o)
    {
        if (!eventListeners.ContainsKey(key)) return;

        eventListeners.Remove(key);
    }

    public void Invoke(EventType key , T value)
    {
        eventListeners[key]?.Invoke(value);
    }

}

public enum EventType
{
    TRANSITION,
    LEVEL
}
