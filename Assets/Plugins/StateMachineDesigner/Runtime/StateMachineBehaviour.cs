using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineDesigner.Runtime
{
    public class StateMachineBehaviour : MonoBehaviour
    {
        private State _currentState = null;
        private StateGroup _stateGroup;
        private StateStatus _status;

        public event System.Action onStateChanged;

        public State currentState
        {
            get
            {
                return _currentState;
            }
            private set
            {
                if(_currentState != null)
                {
                    _currentState.OnExit();
                }
                _currentState = value;

                if (_currentState != null)
                {
                    _currentState.OnEnter();
                }
                if (onStateChanged != null)
                {
                    onStateChanged();
                }
            }
        }

        public StateGroup stateGroup
        {
            set
            {
#if DEBUG
                if(currentState != null)
                {
                    Debug.LogError("状态机正在运行，不能更换状态转换图");
                    return;
                }
#endif
                _stateGroup = value;
            }
        }

        public bool isRunning
        {
            get
            {
                return currentState != null;
            }
        }

        private void Awake()
        {
            enabled = false;
        }

        private void OnEnable()
        {
            if(currentState == null)
            {
                EnterStateMachine();
            }
        }

        public void EnterStateMachine()
        {
#if DEBUG
            if (currentState != null)
            {
                Debug.LogError("状态机在运行，不能重新进入");
                return;
            }
#endif
            currentState = _stateGroup.defaultState;
#if DEBUG
            if(currentState == null)
            {
                Debug.LogError("默认状态不能未空");
                return;
            }
#endif
            enabled = true;
        }

        public void ExitStateMachine()
        {
            currentState = null;
            enabled = false;
        }

        private void Update()
        {
            StateStatus status = _status = currentState.OnUpdate(Time.deltaTime);
            foreach(AdjacencyListDirectedGroup<State, Transition>.AdjacencyNode adjacenyNode in  _stateGroup.GetAdjacencyVertexIterator(currentState))
            {
                Transition transition = adjacenyNode.edge;
                if(transition.canInterrupt)
                {
                    if(TryTransfer(transition, adjacenyNode.vertex))
                    {
                        break;
                    }
                }
                else if(status == StateStatus.Success)
                {
                    if (TryTransfer(transition, adjacenyNode.vertex))
                    {
                        break;
                    }
                }
            }
        }

        private bool TryTransfer(Transition transition, State state)
        {
            if(transition.canTransfer)
            {
                currentState = state;
                return true;
            }
            return false;
        }

        private void FixedUpdate()
        {
            currentState.OnFixedUpdate(Time.deltaTime);
        }

        private void OnAnimatorMove()
        {
            currentState.OnAnimatorMove(Time.deltaTime);
        }

        private void LateUpdate()
        {
            currentState.OnLateUpdate(Time.deltaTime);
        }
    }
}
