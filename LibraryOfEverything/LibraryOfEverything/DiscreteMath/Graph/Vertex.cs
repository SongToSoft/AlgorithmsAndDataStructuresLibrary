using System;
using System.Collections.Generic;
namespace LibraryOfEverything
{
    namespace DiscreteMath
    {
        public class Vertex
        {
            private int m_Value;
            private bool m_IsVisited = false;
            private List<Edge> edges;

            public Vertex(int value, float weight = 0)
            {
                m_Value = value;
                edges = new List<Edge>();
            }

            public void AddEdge(int value, float weight = 1)
            {
                edges.Add(new Edge(value, weight));
            }
            
            public List<Edge> GetEdges()
            {
                return edges;
            }

            public void RemoveEdge(int value)
            {
                for (int i = 0; i < edges.Count; ++i)
                {
                    var graphNode = edges[i];
                    if (graphNode.GetValue() == value)
                    {
                        edges.Remove(graphNode);
                        break;
                    }
                }
            }

            public int GetValue()
            {
                return m_Value;
            }

            public void SetValue(int value)
            {
                m_Value = value;
            }

            public bool IsVisited()
            {
                return m_IsVisited;
            }

            public void SetVisited(bool visited)
            {
                m_IsVisited = visited;
            }

            public void Print()
            {
                Console.Write("Vertex " + m_Value + ": ");
                for (int i = 0; i < edges.Count; ++i)
                {
                    Console.Write( " (" + edges[i].GetValue() + " -- " + edges[i].GetWeight() + ") ");
                }
                Console.WriteLine();
            }
        }
    }
}
