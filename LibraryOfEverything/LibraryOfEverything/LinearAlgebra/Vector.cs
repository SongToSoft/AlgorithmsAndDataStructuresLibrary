using System;
using System.Collections.Generic;

namespace LibraryOfEverything
{
    namespace LinearAlgebra
    {
        public class Vector<T>
        {
            private List<T> m_values;

            public Vector()
            {
                m_values = new List<T>();
            }

            public Vector(int size)
            {
                m_values = new List<T>();
                for (int i = 0; i < size; ++i)
                {
                    m_values.Add(default(T));
                }
            }

            public Vector(T[] values)
            {
                m_values = new List<T>();
                for (int i = 0; i < values.Length; ++i)
                {
                    m_values.Add(values[i]);
                }
            }

            public Vector(List<T> values)
            {
                m_values = new List<T>();
                for (int i = 0; i < values.Count; ++i)
                {
                    m_values.Add(values[i]);
                }
            }

            public void Print()
            {
                for (int i = 0; i < m_values.Count; ++i)
                {
                    Console.Write(m_values[i] + " ");
                }
            }

            public int Count()
            {
                return m_values.Count;
            }

            public void SetValue(int index, T value)
            {
                m_values[index] = value;
            }

            public T GetValue(int index)
            {
                return m_values[index];
            }

            public double Length()
            {
                double result = 0;
                for (int i = 0; i < m_values.Count; ++i)
                {
                    var vecValue = m_values[i] as dynamic;
                    result += (vecValue * vecValue);
                }
                return Math.Sqrt(result);
            }

            public void AddValue(T value)
            {
                m_values.Add(value);
            }

            public T GetMaxValue()
            {
                T result = GetValue(0);
                dynamic resultValue = result as dynamic;
                for (int i = 1; i < Count(); ++i)
                {
                    dynamic value = GetValue(i) as dynamic;
                    if (value > resultValue)
                    {
                        result = GetValue(i);
                    }
                }
                return result;
            }

            public T GetMinValue()
            {
                T result = GetValue(0);
                dynamic resultValue = result as dynamic;
                for (int i = 1; i < Count(); ++i)
                {
                    dynamic value = GetValue(i) as dynamic;
                    if (value < resultValue)
                    {
                        result = GetValue(i);
                    }
                }
                return result;
            }

            public Vector<T> Reverse()
            {
                Vector<T> result = new Vector<T>();
                for (int i = Count() - 1; i >= 0; --i)
                {
                    result.AddValue(m_values[i]);
                }
                return result;
            }

            public static Vector<T> operator + (Vector<T> vec1, Vector<T> vec2)
            {
                Vector<T> result = new Vector<T>();
                for (int i = 0; i < vec1.Count(); ++i)
                {
                    var vec1Value = vec1.GetValue(i) as dynamic;
                    var vec2Value = vec2.GetValue(i) as dynamic;
                    result.AddValue(vec1Value + vec2Value);
                }
                return result;
            }

            public static Vector<T> operator - (Vector<T> vec1, Vector<T> vec2)
            {
                Vector<T> result = new Vector<T>();
                for (int i = 0; i < vec1.Count(); ++i)
                {
                    var vec1Value = vec1.GetValue(i) as dynamic;
                    var vec2Value = vec2.GetValue(i) as dynamic;
                    result.AddValue(vec1Value - vec2Value);
                }
                return result;
            }

            public static Vector<T> operator * (Vector<T> vec, double value)
            {
                Vector<T> result = new Vector<T>();
                for (int i = 0; i < vec.Count(); ++i)
                {
                    var vecValue = vec.GetValue(i) as dynamic;
                    result.AddValue(vecValue * value);
                }
                return result;
            }

            public static Vector<T> operator / (Vector<T> vec, double value)
            {
                Vector<T> result = new Vector<T>();
                for (int i = 0; i < vec.Count(); ++i)
                {
                    var vecValue = vec.GetValue(i) as dynamic;
                    result.AddValue(vecValue / value);
                }
                return result;
            }

            public static double operator * (Vector<T> vec1, Vector<T> vec2)
            {
                double result = 0;
                for (int i = 0; i < vec1.Count(); ++i)
                {
                    var vec1Value = vec1.GetValue(i) as dynamic;
                    var vec2Value = vec2.GetValue(i) as dynamic;
                    result += (vec1Value * vec2Value);
                }
                return result;
            }

            public static bool operator == (Vector<T> vec1, Vector<T> vec2)
            {
                if (vec1.Count()  == vec2.Count())
                {
                    for (int i = 0; i < vec1.Count(); ++i)
                    {
                        var vec1Value = vec1.GetValue(i) as dynamic;
                        var vec2Value = vec2.GetValue(i) as dynamic;
                        if (vec1Value != vec2Value)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }

            public static bool operator != (Vector<T> vec1, Vector<T> vec2)
            {
                if (vec1.Count() == vec2.Count())
                {
                    for (int i = 0; i < vec1.Count(); ++i)
                    {
                        var vec1Value = vec1.GetValue(i) as dynamic;
                        var vec2Value = vec2.GetValue(i) as dynamic;
                        if (vec1Value != vec2Value)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                return true;
            }

            public static Vector<T> operator !(Vector<T> vec)
            {
                Vector<T> result = new Vector<T>();
                for (int i = 0; i < vec.Count(); ++i)
                {
                    var value = vec.GetValue(i) as dynamic;
                    result.AddValue(-1 * value);
                }
                return result;
            }

            public static Vector<T> operator ++(Vector<T> vec)
            {
                Vector<T> result = new Vector<T>();
                for (int i = 0; i < vec.Count(); ++i)
                {
                    var value = vec.GetValue(i) as dynamic;
                    result.AddValue(++value);
                }
                return result;
            }

            public static Vector<T> operator --(Vector<T> vec)
            {
                Vector<T> result = new Vector<T>();
                for (int i = 0; i < vec.Count(); ++i)
                {
                    var value = vec.GetValue(i) as dynamic;
                    result.AddValue(--value);
                }
                return result;
            }
        }
    }
}
