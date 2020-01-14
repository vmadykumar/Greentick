using DotNetCircuitBreaker;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditMgmt.CommonLayer.CircuitBreakerHelpers
{
    public abstract class CircuitBreakerState
    {
        protected readonly CircuitBreaker circuitBreaker;

        protected CircuitBreakerState(CircuitBreaker circuitBreaker)
        {
            this.circuitBreaker = circuitBreaker;
        }

        public virtual CircuitBreaker ProtectedCodeIsAboutToBeCalled()
        {
            return this.circuitBreaker;
        }
        public virtual void ProtectedCodeHasBeenCalled() { }
        public virtual void ActUponException(Exception e) { circuitBreaker.IncreaseFailureCount(); }

        public virtual CircuitBreakerState Update()
        {
            return this;
        }
    }
}
