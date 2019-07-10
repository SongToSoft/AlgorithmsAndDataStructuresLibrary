using System;
using System.Collections.Generic;


namespace LibraryOfEverything
{
    namespace DiscreteMath
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
            private int[] m_Parents;
            private float[] m_Distance;

            public Graph(int numberVertex, EConnected connected = EConnected.DOUBLY_CONNECTED)
            {
                m_Connected = connected;
                m_Vertexes = new Vertex[numberVertex];
                m_Way = new List<int>();
                m_Queue = new Queue<int>();
                m_Parents = new int[numberVertex];
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
                    if (m_Vertexes[i].IsVisited())
                    {
                        return false;
                    }
                }
                return true;
            }

            private void SetVertexVisitedFalse()
            {
                for (int i = 0; i < m_Vertexes.Length; ++i)
                {
                    m_Vertexes[i].SetVisited(false);
                }
            }

            private bool InternalDepthFirstSearch(int start, int end)
            {
                if (!m_Vertexes[start].IsVisited())
                {
                    m_Way.Add(start);
                    m_Vertexes[start].SetVisited(true);
                    if (start == end)
                    {
                        return true;
                    }
                    for (int i = 0; i < m_Vertexes[start].GetEdges().Count; ++i)
                    {
                        if (!m_Vertexes[m_Vertexes[start].GetEdges()[i].GetValue()].IsVisited())
                        {
                            if (InternalDepthFirstSearch(m_Vertexes[start].GetEdges()[i].GetValue(), end))
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

            public List<int> DepthFirstSearch(int start, int end)
            {
                m_Way.Clear();
                InternalDepthFirstSearch(start, end);
                SetNonVisitedVertex();
                return GetWay();
            }

            public List<int> BreadthFirstSearch(int start, int end)
            {
                m_Queue.Clear();
                m_Queue.Enqueue(start);
                m_Vertexes[start].SetVisited(true);
                m_Parents[start] = -1;
                //Console.WriteLine("Start from: " + start);
                while (m_Queue.Count != 0)
                {
                    var vertex = m_Queue.Peek();
                    m_Queue.Dequeue();
                    //Console.WriteLine("Go to vertex: " + vertex);
                    for (int i = 0; i < m_Vertexes[vertex].GetEdges().Count; ++i)
                    {
                        if (!m_Vertexes[m_Vertexes[vertex].GetEdges()[i].GetValue()].IsVisited())
                        {
                            m_Vertexes[m_Vertexes[vertex].GetEdges()[i].GetValue()].SetVisited(true);
                            m_Queue.Enqueue(m_Vertexes[vertex].GetEdges()[i].GetValue());
                            m_Parents[m_Vertexes[vertex].GetEdges()[i].GetValue()] = vertex;
                            //Console.WriteLine("Add in queue vertex: " + m_Vertexes[vertex].GetEdges()[i].GetValue());
                        }
                    }
                }
                SetNonVisitedVertex();
                m_Way.Clear();
                int startParent = end;
                while (!m_Vertexes[startParent].IsVisited())
                {
                    m_Way.Add(startParent);
                    m_Vertexes[startParent].SetVisited(true);
                    startParent = m_Parents[startParent];
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
                while(CheckVertexVisited())
                {
                    int currentVertex = GetMinDistanceIndex();
                    for (int i = 0; i < m_Vertexes[GetMinDistanceIndex()].GetEdges().Count; ++i)
                    {
                        m_Distance[m_Vertexes[currentVertex].GetEdges()[i].GetValue()] = m_Vertexes[currentVertex].GetEdges()[i].GetWeight() + m_Distance[GetMinDistanceIndex()];
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
                return minIndex;
            }

            public void InitializeDistance()
            {
                for (int i = 0; i < m_Distance.Length; ++i)
                {
                    m_Distance[i] = int.MaxValue;
                }
            }
        }
    }
}
