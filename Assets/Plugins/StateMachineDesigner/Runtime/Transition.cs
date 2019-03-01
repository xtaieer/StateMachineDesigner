using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineDesigner.Runtime
{
    public class Transition
    {
        private class TrueCondition : ICondition
        {
            bool ICondition.Test()
            {
                return true;
            }
        }

        private ICondition _condition;

        private static ICondition TRUE = new TrueCondition();

        public bool canInterrupt
        {
            get;
            private set;
        }

        public bool canTransfer
        {
            get
            {
                return _condition.Test();
            }
        }

        public Transition() : this(TRUE, false)
        {
        }

        public Transition(ICondition condition) : this(condition, false)
        {
        }

        public Transition(ICondition condition, bool canInterrupt)
        {
            _condition = condition;
            this.canInterrupt = canInterrupt;
        }
    }
}
