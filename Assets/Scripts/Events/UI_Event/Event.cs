public class Event
{
    public string EventType { get; private set; }
    public object EventData { get; private set; }

    public Event(string eventType, object eventData)
    {
        EventType = eventType;
        EventData = eventData;
    }
}
