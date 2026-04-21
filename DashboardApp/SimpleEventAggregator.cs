using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Contracts;

namespace DashboardApp
{
    [Export(typeof(IEventAggregator))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SimpleEventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers =
            new Dictionary<Type, List<Delegate>>();

        public void Publish<TEvent>(TEvent eventToPublish)
        {
            var eventType = typeof(TEvent);

            if (!_subscribers.ContainsKey(eventType))
                return;

            foreach (var handler in _subscribers[eventType].OfType<Action<TEvent>>())
            {
                handler(eventToPublish);
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            var eventType = typeof(TEvent);

            if (!_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType] = new List<Delegate>();
            }

            _subscribers[eventType].Add(handler);
        }
    }
}
