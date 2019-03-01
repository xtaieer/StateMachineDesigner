using System.Collections.Generic;

namespace StateMachineDesigner.Runtime
{
    public class AdjacencyListDirectedGroup<V, E>
    {
        public class AdjacencyNode
        {
            internal GroupNode to
            {
                get;
                set;
            }

            public E edge
            {
                get;
                private set;
            }

            public V vertex
            {
                get
                {
                    return to.vertex;
                }
            }

            internal AdjacencyNode next
            {
                get;
                private set;
            }

            internal AdjacencyNode(GroupNode to, E edge, AdjacencyNode prev) : this(to, edge)
            {
                prev.next = this;
            }

            internal AdjacencyNode(GroupNode to, E edge)
            {
                this.to = to;
                this.edge = edge;
                next = null;
            }
        }

        internal class GroupNode
        {
            public V vertex
            {
                get;
                private set;
            }

            public AdjacencyNode head
            {
                get;
                set;
            }

            public AdjacencyNode tail
            {
                get;
                set;
            }

            public GroupNode(V vertex)
            {
                this.vertex = vertex;
                head = null;
            }
        }

        private Dictionary<V, GroupNode> _groupNodes = new Dictionary<V, GroupNode>();

        public void AddVertex(V vertex)
        {
            if(!_groupNodes.ContainsKey(vertex))
            {
                _groupNodes.Add(vertex, new GroupNode(vertex));
            }
        }

        private GroupNode TryGetElseAdd(V vertex)
        {
            AddVertex(vertex);
            return _groupNodes[vertex];
        }

        public void AddEdge(V from, V to, E edge)
        {
            GroupNode f = TryGetElseAdd(from);
            GroupNode t = TryGetElseAdd(to);
            if(f.head == null)
            {
                f.head = f.tail = new AdjacencyNode(t, edge);
            }
            else
            {
                f.tail = new AdjacencyNode(t, edge, f.tail);
            }
        }

        public IEnumerable<AdjacencyNode> GetAdjacencyVertexIterator(V vertex)
        {
            if(!_groupNodes.ContainsKey(vertex))
            {
                yield break;
            }
            AdjacencyNode ad = _groupNodes[vertex].head;
            while(ad != null)
            {
                yield return ad;
                ad = ad.next;
            }
        }
    }
}
