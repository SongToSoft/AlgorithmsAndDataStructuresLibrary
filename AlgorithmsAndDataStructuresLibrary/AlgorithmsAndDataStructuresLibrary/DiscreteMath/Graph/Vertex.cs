using System;
using System.Collections.Generic;

namespace AlgorithmsAndDataStructuresLibrary.DiscreteMath.Graph
{
    public class Vertex
    {
        private int m_Value;
        private bool m_IsVisited = false;
        private List<Edge> m_Edges;
        private int m_Parent = -2;
        private int m_ConnectedCount = 0;
        private int m_x, m_y;

        public Vertex(int value, float weight = 0, int x = 0, int y = 0)
        {
            m_Value = value;
            m_x = x;
            m_y = y;
            m_Edges = new List<Edge>();
        }

        public void AddEdge(int value, float weight = 1)
        {
            m_Edges.Add(new Edge(value, weight));
        }

        public List<Edge> GetEdges()
        {
            return m_Edges;
        }

        public bool CheckEdge(int vertex)
        {
            for (int i = 0; i < m_Edges.Count; ++i)
            {
                if (m_Edges[i].GetValue() == vertex)
                {
                    return true;
                }
            }
            return false;
        }
 
        public void RemoveEdge(int value)
        {
            for (int i = 0; i < m_Edges.Count; ++i)
            {
                var graphNode = m_Edges[i];
                if (graphNode.GetValue() == value)
                {
                    m_Edges.Remove(graphNode);
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

        public void SetParent(int parent)
        {
            m_Parent = parent;
        }

        public int GetParent()
        {
            return m_Parent;
        }

        public void SetConnectedCount(int connectedCount)
        {
            m_ConnectedCount = connectedCount;
        }

        public int GetConnectedCount()
        {
            return m_ConnectedCount;
        }

        public int GetPositionX()
        {
            return m_x;
        }

        public int GetPositionY()
        {
            return m_y;
        }

        public void SetPosition(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        public void Print()
        {
            Console.Write("Vertex " + m_Value + ": ");
            for (int i = 0; i < m_Edges.Count; ++i)
            {
                Console.Write(" (" + m_Edges[i].GetValue() + " -- " + m_Edges[i].GetWeight() + ") ");
            }
            Console.WriteLine();
        }
    }
}