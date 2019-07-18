using System;
using System.Collections.Generic;

namespace AlgorithmsAndDataStructuresLibrary.DiscreteMath.Graph
{
    public enum EConnected
    {
        DOUBLY_CONNECTED,
        SINGLE_CONNECTED
    }

    public class Graph
    {
        private Vertex[] m_Vertexes;
        private List<int> m_Way;
        private Queue<int> m_Queue;
        private EConnected m_Connected;
        private float[] m_Distance;

        public Graph(int numberVertex, EConnected connected = EConnected.DOUBLY_CONNECTED)
        {
            m_Connected = connected;
            m_Vertexes = new Vertex[numberVertex];
            m_Way = new List<int>();
            m_Queue = new Queue<int>();
            m_Distance = new float[numberVertex];
            for (int i = 0; i < m_Vertexes.Length; ++i)
            {
                m_Vertexes[i] = new Vertex(i);
            }
        }

        private void SetNonVisitedVertex()
        {
            for (int i = 0; i < m_Vertexes.Length; ++i)
            {
                m_Vertexes[i].SetVisited(false);
            }
        }

        public void AddEdge(int start, int end, float weight = 1)
        {
            m_Vertexes[start].AddEdge(end, weight);
            if (m_Connected == EConnected.DOUBLY_CONNECTED)
            {
                m_Vertexes[end].AddEdge(start, weight);
            }
        }

        public void RemoveEdge(int start, int end)
        {
            m_Vertexes[start].RemoveEdge(end);
            if (m_Connected == EConnected.DOUBLY_CONNECTED)
            {
                m_Vertexes[end].RemoveEdge(start);
            }
        }

        public Vertex GetVertex(int index)
        {
            return m_Vertexes[index];
        }

        public void Print()
        {
            for (int i = 0; i < m_Vertexes.Length; ++i)
            {
                m_Vertexes[i].Print();
            }
        }

        private bool CheckVertexVisited()
        {
            for (int i = 0; i < m_Vertexes.Length; ++i)
            {
                if (!m_Vertexes[i].IsVisited())
                {
                    return true;
                }
            }
            return false;
        }

        private void SetVertexVisitedFalse()
        {
            for (int i = 0; i < m_Vertexes.Length; ++i)
            {
                m_Vertexes[i].SetVisited(false);
            }
        }

        private bool InternalDepthFirstSearch(int start, int end, int connectedCount)
        {
            if (!m_Vertexes[start].IsVisited())
            {
                m_Way.Add(start);
                m_Vertexes[start].SetVisited(true);
                m_Vertexes[start].SetConnectedCount(connectedCount);
                if (start == end)
                {
                    return true;
                }
                for (int i = 0; i < m_Vertexes[start].GetEdges().Count; ++i)
                {
                    if (!m_Vertexes[m_Vertexes[start].GetEdges()[i].GetValue()].IsVisited())
                    {
                        if (InternalDepthFirstSearch(m_Vertexes[start].GetEdges()[i].GetValue(), end, connectedCount))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            else
            {
                m_Way.RemoveAt(m_Way.Count - 1);
                return false;
            }
        }

        private List<int> GetWay()
        {
            return m_Way;
        }

        public List<int> DepthFirstSearch(int start, int end, int connectedCount = 0)
        {
            m_Way.Clear();
            InternalDepthFirstSearch(start, end, connectedCount);
            SetNonVisitedVertex();
            var way = GetWay();
            if (!way.Contains(end))
            {
                way.Clear();
            }
            return way;
        }

        public List<int> BreadthFirstSearch(int start, int end)
        {
            m_Queue.Clear();
            m_Queue.Enqueue(start);
            m_Vertexes[start].SetVisited(true);
            m_Vertexes[start].SetParent(-1);
            while (m_Queue.Count != 0)
            {
                var vertex = m_Queue.Peek();
                m_Queue.Dequeue();
                for (int i = 0; i < m_Vertexes[vertex].GetEdges().Count; ++i)
                {
                    if (!m_Vertexes[m_Vertexes[vertex].GetEdges()[i].GetValue()].IsVisited())
                    {
                        m_Vertexes[m_Vertexes[vertex].GetEdges()[i].GetValue()].SetVisited(true);
                        m_Queue.Enqueue(m_Vertexes[vertex].GetEdges()[i].GetValue());
                        m_Vertexes[m_Vertexes[vertex].GetEdges()[i].GetValue()].SetParent(vertex);
                    }
                }
            }
            SetNonVisitedVertex();
            m_Way.Clear();
            if (m_Vertexes[end].GetParent() == (-2))
            {
                return m_Way;
            }
            int startParent = end;
            while (!m_Vertexes[startParent].IsVisited())
            {
                m_Way.Add(startParent);
                m_Vertexes[startParent].SetVisited(true);
                startParent = m_Vertexes[startParent].GetParent();
                if (startParent == (-1))
                {
                    break;
                }
            }
            SetNonVisitedVertex();
            m_Way.Reverse();
            return m_Way;
        }

        public float[] Dijkstra(int start)
        {
            InitializeDistance();
            m_Distance[start] = 0;
            while (CheckVertexVisited())
            {
                int currentVertex = GetMinDistanceIndex();
                for (int i = 0; i < m_Vertexes[currentVertex].GetEdges().Count; ++i)
                {
                    var value = m_Vertexes[currentVertex].GetEdges()[i].GetWeight() + m_Distance[currentVertex];
                    if (m_Distance[m_Vertexes[currentVertex].GetEdges()[i].GetValue()] > value)
                    {
                        m_Distance[m_Vertexes[currentVertex].GetEdges()[i].GetValue()] = value;
                    }
                }
                m_Vertexes[currentVertex].SetVisited(true);
            }
            SetNonVisitedVertex();
            return m_Distance;
        }

        public int GetMinDistanceIndex()
        {
            float minDistance = int.MaxValue;
            int minIndex = 0;
            for (int i = 0; i < m_Distance.Length; ++i)
            {
                if ((m_Distance[i] < minDistance) && (!m_Vertexes[i].IsVisited()))
                {
                    minDistance = m_Distance[i];
                    minIndex = i;
                }
            }
            Console.WriteLine("minDistance: " + minDistance);
            Console.WriteLine("minIndex: " + minIndex);
            return minIndex;
        }

        public void InitializeDistance()
        {
            for (int i = 0; i < m_Distance.Length; ++i)
            {
                m_Distance[i] = int.MaxValue;
            }
        }

        public bool IsSetConnectedCount()
        {
            for (int i = 0; i < m_Vertexes.Length; ++i)
            {
                if (m_Vertexes[i].GetConnectedCount() == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetConnectedCount()
        {
            int connectedCount = 0;
            while (IsSetConnectedCount())
            {
                for (int i = 0; i < m_Vertexes.Length; ++i)
                {
                    if (m_Vertexes[i].GetConnectedCount() == 0)
                    {
                        ++connectedCount;
                        m_Vertexes[i].SetConnectedCount(connectedCount);
                    }
                    else
                    {
                        continue;
                    }
                    for (int j = 0; j < m_Vertexes.Length; ++j)
                    {
                        if (DepthFirstSearch(i, j, connectedCount).Count != 0)
                        {
                            m_Vertexes[j].SetConnectedCount(connectedCount);
                        }
                    }
                }
            }
            return connectedCount;
        }
    }
}
