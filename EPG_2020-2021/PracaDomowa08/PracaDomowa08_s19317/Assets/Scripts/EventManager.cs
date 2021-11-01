using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public static class EventManager
{

    private static readonly Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();

    public static void Subscribe(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;

        if (eventDictionary.TryGetValue(eventName,out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            eventDictionary.Add(eventName, thisEvent);
        }
    }
    public static void UnSubscribe(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;

        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Publish(string eventName)
    {
        UnityEvent thisEvent;

        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
