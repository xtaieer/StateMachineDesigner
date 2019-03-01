using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineDesigner.Runtime
{
    public abstract class Condition : ICondition
    {
        private class TrueCondition : Condition
        {
            public override bool Test()
            {
                return true;
            }
        }

        public class FalseCondition : Condition
        {
            public override bool Test()
            {
                return false;
            }
        }

        private abstract class BinaryOperation : Condition
        {
            protected ICondition _lhs;
            protected ICondition _rhs;
            public BinaryOperation(ICondition lhs, ICondition rhs)
            {
                _lhs = lhs;
                _rhs = rhs;
            }
        }

        private class OrOperate : BinaryOperation
        {
            public OrOperate(ICondition lhs, ICondition rhs) : base(lhs, rhs) { }

            public override bool Test()
            {
                return _lhs.Test() || _rhs.Test();
            }
        }

        private class AndOperation : BinaryOperation
        {
            public AndOperation(ICondition lhs, ICondition rhs) : base(lhs, rhs) { }

            public override bool Test()
            {
                return _lhs.Test() && _rhs.Test();
            }
        }

        private class NotOperation : Condition
        {
            private ICondition _operand;
            public NotOperation(ICondition operand)
            {
                _operand = operand;
            }

            public override bool Test()
            {
                return !_operand.Test();
            }
        }

        public static readonly Condition TRUE = new TrueCondition();
        public static readonly Condition FALSE = new FalseCondition();

        public abstract bool Test();

        public Condition Or(ICondition conditional)
        {
            return new OrOperate(this, conditional);
        }

        public Condition And(ICondition conditional)
        {
            return new AndOperation(this, conditional);
        }

        public Condition Not()
        {
            return new NotOperation(this);
        }

        public static Condition operator &(Condition lhs, ICondition rhs)
        {
            return lhs.And(rhs);
        }

        public static Condition operator |(Condition lhs, ICondition rhs)
        {
            return lhs.Or(rhs);
        }

        public static Condition operator !(Condition operand)
        {
            return operand.Not();
        }
    }
}