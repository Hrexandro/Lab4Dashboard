using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Contracts
{
    public interface IEventAggregator
    {
        void Publish<TEvent>(TEvent eventToPublish);
        void Subscribe<TEvent>(Action<TEvent> handler);
    }
}
