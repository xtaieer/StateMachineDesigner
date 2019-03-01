using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineDesigner.Runtime
{
    public class StateGroup
    {
        private AdjacencyListDirectedGroup<State, Transition> group = new AdjacencyListDirectedGroup<State, Transition>();

        public State defaultState
        {
            get;
            set;
        }

        public void AddTransition(State from, State to, Transition transition)
        {
            group.AddEdge(from, to, transition);
        }

        public IEnumerable<AdjacencyListDirectedGroup<State, Transition>.AdjacencyNode> GetAdjacencyVertexIterator(State state)
        {
            return group.GetAdjacencyVertexIterator(state);
        }
    }
}
