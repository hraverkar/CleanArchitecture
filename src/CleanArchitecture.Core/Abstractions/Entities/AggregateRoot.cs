﻿using CleanArchitecture.Core.Abstractions.DomainEvents;

namespace CleanArchitecture.Core.Abstractions.Entities
{
    public abstract class AggregateRoot : EntityBase
    {
        public bool IsDeleted { get; set; }
        protected AggregateRoot() : this(Guid.NewGuid())
        {

        }
        protected AggregateRoot(Guid id)
        {
            Id = id;
            IsDeleted = false;
        }

        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(DomainEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
