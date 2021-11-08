using System;
using User.Domain.Kernel;

namespace User.Domain.OutboxAggregate
{
    public class IntegrationEvent : Entity<Guid>
    {
        private readonly string _aggregateType;
        private readonly string _aggregateId;
        private readonly string _eventType;
        private readonly string _payload;
        public string AggregateType { get { return _aggregateType; } }
        public string AggregateId { get { return _aggregateId; } }
        public string EventType { get { return _eventType; } }
        public string Payload { get { return _payload; } }
        public IntegrationEvent(string aggregateType, string aggregateId, string eventType, string payload)
        {
            _aggregateType = aggregateType;
            _aggregateId = aggregateId;
            _eventType = eventType;
            _payload = payload;
        }
    }
}
