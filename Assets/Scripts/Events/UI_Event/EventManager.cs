using System.Collections.Generic;

public static class EventManager
{
    private static Dictionary<string, List<IEventListener>> listeners = new Dictionary<string, List<IEventListener>>();

    public static void Subscribe(string eventType, IEventListener listener)
    {
        if (!listeners.ContainsKey(eventType))
        {
            listeners[eventType] = new List<IEventListener>();
        }
        listeners[eventType].Add(listener);
    }

    public static void Unsubscribe(string eventType, IEventListener listener)
    {
        if (listeners.ContainsKey(eventType))
        {
            listeners[eventType].Remove(listener);
        }
    }

    public static void Notify(string eventType, Event eventData)
    {
        if (listeners.ContainsKey(eventType))
        {
            foreach (var listener in listeners[eventType])
            {
                listener.OnEventReceived(eventData);
            }
        }
    }
}
