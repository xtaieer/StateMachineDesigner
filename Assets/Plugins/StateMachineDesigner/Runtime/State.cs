using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineDesigner.Runtime
{
    public enum StateStatus
    {
        Running,
        Success
    }

    public abstract class State
    {
        public virtual void OnEnter() { }

        public virtual void OnExit() { }

        public virtual StateStatus OnUpdate(float deltaTime)
        {
            return StateStatus.Success;
        }

        public virtual void OnFixedUpdate(float deltaTime) { }

        public virtual void OnAnimatorMove(float deltaTime) { }

        public virtual void OnLateUpdate(float deltaTime) { }
    }
}
