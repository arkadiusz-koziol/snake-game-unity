using System;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.Events
{
    /// <summary>
    /// Centralized event bus using the Observer pattern for decoupled communication.
    /// Manages event subscriptions and publishing with type safety.
    /// </summary>
    public class EventBus
    {
        private readonly Dictionary<Type, List<Delegate>> subscribers;
        
        public EventBus()
        {
            subscribers = new Dictionary<Type, List<Delegate>>();
        }
        
        public void Subscribe<T>(Action<T> handler) where T : GameEvent
        {
            Type eventType = typeof(T);
            
            if (!subscribers.ContainsKey(eventType))
            {
                subscribers[eventType] = new List<Delegate>();
            }
            
            subscribers[eventType].Add(handler);
        }
        
        public void Unsubscribe<T>(Action<T> handler) where T : GameEvent
        {
            Type eventType = typeof(T);
            
            if (subscribers.ContainsKey(eventType))
            {
                subscribers[eventType].Remove(handler);
            }
        }
        
        public void Publish<T>(T gameEvent) where T : GameEvent
        {
            Type eventType = typeof(T);
            
            if (subscribers.ContainsKey(eventType))
            {
                foreach (Delegate handler in subscribers[eventType])
                {
                    try
                    {
                        ((Action<T>)handler)(gameEvent);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error publishing event {eventType.Name}: {e.Message}");
                    }
                }
            }
        }
        
        public void Clear()
        {
            subscribers.Clear();
        }
        
        public int GetSubscriberCount<T>() where T : GameEvent
        {
            Type eventType = typeof(T);
            return subscribers.ContainsKey(eventType) ? subscribers[eventType].Count : 0;
        }
    }
}
