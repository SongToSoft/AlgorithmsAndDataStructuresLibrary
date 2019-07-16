using System;
using System.Collections.Generic;

namespace AlgorithmsAndDataStructuresLibrary.LinearAlgebra
{
    namespace LinearAlgebra
    {
        public class Matrix<T>
        {
            private T[,] m_values;
            private int m_height = 0, m_width = 0;

            public Matrix(int sizeI, int sizeJ)
            {
                m_height = sizeI;
                m_width = sizeJ;
                m_values = new T[m_height, m_width];
                for (int i = 0; i < m_height; ++i)
                {
                    for (int j = 0; j < m_width; ++j)
                    {
                        m_values[i, j] = default(T);
                    }
                }
            }

            public Matrix(int sizeI, int sizeJ, T value)
            {
                m_height = sizeI;
                m_width = sizeJ;
                m_values = new T[m_height, m_width];
                for (int i = 0; i < m_height; ++i)
                {
                    for (int j = 0; j < m_width; ++j)
                    {
                        m_values[i, j] = value;
                    }
                }
            }

            public Matrix(T[,] values)
            {
                m_height = values.GetLength(0);
                m_width = values.GetLength(1);
                m_values = new T[m_height, m_width];
                for (int i = 0; i < m_height; ++i)
                {
                    for (int j = 0; j < m_width; ++j)
                    {
                        m_values[i, j] = values[i, j];
                    }
                }
            }

            public Matrix(int sizeI, int sizeJ, List<T> values)
            {
                m_height = sizeI;
                m_width = sizeJ;
                m_values = new T[m_height, m_width];
                int count = 0;
                for (int i = 0; i < m_height; ++i)
                {
                    for (int j = 0; j < m_width; ++j)
                    {
                        m_values[i, j] = values[count];
                        ++count;
                    }
                }
            }

            public Matrix(Matrix<T> values)
            {
                m_height = values.Height();
                m_width = values.Width();
                m_values = new T[m_height, m_width];
                for (int i = 0; i < m_height; ++i)
                {
                    for (int j = 0; j < m_width; ++j)
                    {
                        m_values[i, j] = values.GetValue(i, j);
                    }
                }
            }

            public int Height()
            {
                return m_height;
            }

            public int Width()
            {
                return m_width;
            }

            public T GetValue(int i, int j)
            {
                return m_values[i, j];
            }

            public void SetValue(int i, int j, T value)
            {
                m_values[i, j] = value;
            }

            public void Print()
            {
                for (int i = 0; i < Height(); ++i)
                {
                    for (int j = 0; j < Width(); ++j)
                    {
                        Console.Write(m_values[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }

            public Matrix<T> Transposition()
            {
                Matrix<T> result = new Matrix<T>(Width(), Height());
                for (int i = 0; i < Height(); ++i)
                {
                    for (int j = 0; j < Width(); ++j)
                    {
                        result.SetValue(j, i, GetValue(i, j));
                    }
                }
                return result;
            }

            public Matrix<T> ClockwiseRotate()
            {
                Matrix<T> result = new Matrix<T>(Width(), Height());
                int countI = 0;
                for (int j = 0; j < Width(); ++j)
                {
                    int countJ = 0;
                    for (int i = Height() - 1; i >= 0; --i)
                    {
                        result.SetValue(countI, countJ, m_values[i, j]);
                        ++countJ;
                    }
                    ++countI;
                }
                return result;
            }

            public Matrix<T> CounterClockwiseRotate()
            {
                Matrix<T> result = new Matrix<T>(Width(), Height());
                int countI = 0;
                for (int j = Width() - 1; j >= 0; --j)
                {
                    int countJ = 0;
                    for (int i = 0; i < Height(); ++i)
                    {
                        result.SetValue(countI, countJ, m_values[i, j]);
                        ++countJ;
                    }
                    ++countI;
                }
                return result;
            }

            public Matrix<T> GetPart(int startI, int startJ, int sizeI, int sizeJ)
            {
                Matrix<T> result = new Matrix<T>(sizeI, sizeJ);
                int countI = 0;
                for (int i = startI; i < startI + sizeI; ++i)
                {
                    if (i == Height())
                    {
                        break;
                    }
                    else
                    {
                        int countJ = 0;
                        for (int j = startJ; j < startJ + sizeJ; ++j)
                        {
                            if (j == Width())
                            {
                                break;
                            }
                            else
                            {
                                result.SetValue(countI, countJ, GetValue(i, j));
                                ++countJ;
                            }
                        }
                        ++countI;
                    }
                }
                return result;
            }

            public dynamic SumAllValues()
            {
                dynamic result = default(T);
                for (int i = 0; i < Height(); ++i)
                {
                    for (int j = 0; j < Width(); ++j)
                    {
                        var value = GetValue(i, j) as dynamic;
                        result += value;
                    }
                }
                return result;
            }

            public List<T> ToList()
            {
                List<T> result = new List<T>();
                for (int i = 0; i < Height(); ++i)
                {
                    for (int j = 0; j < Width(); ++j)
                    {
                        result.Add(GetValue(i, j));
                    }
                }
                return result;
            }

            public T GetMaxValue()
            {
                T result = GetValue(0, 0);
                dynamic resultValue = result as dynamic;
                for (int i = 0; i < Height(); ++i)
                {
                    for (int j = 0; j < Width(); ++j)
                    {
                        dynamic value = GetValue(i, j) as dynamic;
                        if (value > resultValue)
                        {
                            result = GetValue(i, j);
                        }
                    }
                }
                return result;
            }

            public T GetMaxValueInRow(int index)
            {
                T result = GetValue(index, 0);
                dynamic resultValue = result as dynamic;
                for (int i = 0; i < Width(); ++i)
                {
                    dynamic value = GetValue(index, i) as dynamic;
                    if (result < value)
                    {
                        result = value;
                    }
                }
                return result;
            }

            public T GetMinValueInRow(int index)
            {
                T result = GetValue(index, 0);
                dynamic resultValue = result as dynamic;
                for (int i = 0; i < Width(); ++i)
                {
                    dynamic value = GetValue(index, i) as dynamic;
                    if (result > value)
                    {
                        result = value;
                    }
                }
                return result;
            }


            public T GetMinValue()
            {
                T result = GetValue(0, 0);
                dynamic resultValue = result as dynamic;
                for (int i = 0; i < Height(); ++i)
                {
                    for (int j = 0; j < Width(); ++j)
                    {
                        dynamic value = GetValue(i, j) as dynamic;
                        if (value < resultValue)
                        {
                            result = GetValue(i, j);
                        }
                    }
                }
                return result;
            }

            public T GetMaxValueInColumn(int index)
            {
                T result = GetValue(0, index);
                dynamic resultValue = result as dynamic;
                for (int i = 0; i < Height(); ++i)
                {
                    dynamic value = GetValue(i, index) as dynamic;
                    if (result < value)
                    {
                        result = value;
                    }
                }
                return result;
            }

            public T GetMinValueInColumn(int index)
            {
                T result = GetValue(0, index);
                dynamic resultValue = result as dynamic;
                for (int i = 0; i < Height(); ++i)
                {
                    dynamic value = GetValue(i, index) as dynamic;
                    if (result > value)
                    {
                        result = value;
                    }
                }
                return result;
            }

            public Matrix<T> ToTriangle()
            {
                Matrix<T> result = this;
                for (int i = 0; i < result.Height() - 1; ++i)
                {
                    dynamic coreValue = result.GetValue(i, i) as dynamic;
                    if (coreValue == default(T))
                    {
                        bool isSwap = false;
                        for (int j = i + 1; j < result.Height(); ++j)
                        {
                            if (result.GetValue(j, i) as dynamic != default(T))
                            {
                                SwapRow(i, j);
                                isSwap = true;
                                break;
                            }
                        }
                        if (!isSwap)
                        {
                            continue;
                        }
                    }
                    for (int j = i + 1; j < result.Height(); ++j)
                    {
                        dynamic jValue = result.GetValue(j, i) as dynamic;
                        if (jValue == default(T))
                        {
                            continue;
                        }
                        result.MulRow(jValue, i);
                        result.MulRow(coreValue, j);
                        result = result.LineDifference(j, i);
                        result.DivideRow(jValue, i);
                    }
                }
                return result;
            }

            public Matrix<T> LineDifference(int indexFirst, int indexSecond)
            {
                Matrix<T> result = this;
                for (int i = 0; i < result.Width(); ++i)
                {
                    dynamic firstValue = result.GetValue(indexFirst, i) as dynamic;
                    dynamic secondValue = result.GetValue(indexSecond, i) as dynamic;
                    result.SetValue(indexFirst, i, firstValue - secondValue);
                }
                return result;
            }

            public void DivideRow(T value, int index)
            {
                for (int i = 0; i < Width(); ++i)
                {
                    dynamic iValue = m_values[index, i] as dynamic;
                    m_values[index, i] = iValue / value;
                }
            }

            public void DivideColumn(T value, int index)
            {
                for (int i = 0; i < Height(); ++i)
                {
                    dynamic iValue = m_values[i, index] as dynamic;
                    m_values[i, index] = iValue / value;
                }
            }
            public void MulRow(T value, int index)
            {
                for (int i = 0; i < Width(); ++i)
                {
                    dynamic iValue = m_values[index, i] as dynamic;
                    m_values[index, i] = iValue * value;
                }
            }

            public void MulColumn(T value, int index)
            {
                for (int i = 0; i < Height(); ++i)
                {
                    dynamic iValue = m_values[i, index] as dynamic;
                    m_values[i, index] = iValue * value;
                }
            }

            public Matrix<T> SwapRow(int indexFirst, int indexSecond)
            {
                Matrix<T> result = this;
                for (int i = 0; i < result.Width(); ++i)
                {
                    dynamic tmpValue = result.GetValue(indexFirst, i);
                    result.SetValue(indexFirst, i, result.GetValue(indexSecond, i));
                    result.SetValue(indexSecond, i, tmpValue);
                }
                return result;
            }

            public Matrix<T> SwapColumn(int indexFirst, int indexSecond)
            {
                Matrix<T> result = this;
                for (int i = 0; i < result.Height(); ++i)
                {
                    dynamic tmpValue = result.GetValue(i, indexFirst);
                    result.SetValue(i, indexFirst, result.GetValue(i, indexSecond));
                    result.SetValue(i, indexSecond, tmpValue);
                }
                return result;
            }

            public T Determinant()
            {
                Matrix<T> resulMatrix = this.ToTriangle();
                dynamic result = resulMatrix.GetValue(0, 0) as dynamic;
                for (int i = 1; i < resulMatrix.Height(); ++i)
                {
                    dynamic value = resulMatrix.GetValue(i, i) as dynamic;
                    result *= value;
                }
                return result;
            }

            public static Matrix<T> operator *(Matrix<T> matrix, double value)
            {
                Matrix<T> result = new Matrix<T>(matrix.Height(), matrix.Width());
                for (int i = 0; i < result.Height(); ++i)
                {
                    for (int j = 0; j < result.Width(); ++j)
                    {
                        var matrixValue = matrix.GetValue(i, j) as dynamic;
                        result.SetValue(i, j, matrixValue * value);
                    }
                }
                return result;
            }

            public static Matrix<T> operator /(Matrix<T> matrix, double value)
            {
                Matrix<T> result = new Matrix<T>(matrix.Height(), matrix.Width());
                for (int i = 0; i < result.Height(); ++i)
                {
                    for (int j = 0; j < result.Width(); ++j)
                    {
                        var matrixValue = matrix.GetValue(i, j) as dynamic;
                        result.SetValue(i, j, matrixValue / value);
                    }
                }
                return result;
            }

            public static Matrix<T> operator +(Matrix<T> matrix1, Matrix<T> matrix2)
            {
                if ((matrix1.Height() == matrix2.Height()) && (matrix1.Width() == matrix2.Width()))
                {
                    Matrix<T> result = new Matrix<T>(matrix1.Height(), matrix1.Width());
                    for (int i = 0; i < matrix1.Height(); ++i)
                    {
                        for (int j = 0; j < matrix2.Height(); ++j)
                        {
                            var matrix1Value = matrix1.GetValue(i, j) as dynamic;
                            var matrix2Value = matrix2.GetValue(i, j) as dynamic;
                            result.SetValue(i, j, matrix1Value + matrix2Value);
                        }
                    }
                    return result;
                }
                else
                {
                    throw new Exception();
                }
            }

            public static Matrix<T> operator -(Matrix<T> matrix1, Matrix<T> matrix2)
            {
                if ((matrix1.Height() == matrix2.Height()) && (matrix1.Width() == matrix2.Width()))
                {
                    Matrix<T> result = new Matrix<T>(matrix1.Height(), matrix1.Width());
                    for (int i = 0; i < matrix1.Height(); ++i)
                    {
                        for (int j = 0; j < matrix2.Height(); ++j)
                        {
                            var matrix1Value = matrix1.GetValue(i, j) as dynamic;
                            var matrix2Value = matrix2.GetValue(i, j) as dynamic;
                            result.SetValue(i, j, matrix1Value - matrix2Value);
                        }
                    }
                    return result;
                }
                else
                {
                    throw new Exception();
                }
            }

            public static Matrix<T> operator *(Matrix<T> matrix1, Matrix<T> matrix2)
            {
                Matrix<T> result = new Matrix<T>(matrix1.Height(), matrix2.Width());
                for (int i = 0; i < matrix1.Height(); i++)
                {
                    for (int j = 0; j < matrix2.Width(); j++)
                    {
                        for (int k = 0; k < matrix2.Height(); k++)
                        {
                            var matrix1Value = matrix1.GetValue(i, k) as dynamic;
                            var matrix2Value = matrix2.GetValue(k, j) as dynamic;
                            result.SetValue(i, j, result.GetValue(i, j) + matrix1Value * matrix2Value);
                        }
                    }
                }
                return result;
            }

            public static Matrix<T> operator /(Matrix<T> matrix1, Matrix<T> matrix2)
            {
                Matrix<T> result = new Matrix<T>(matrix1.Height(), matrix2.Width());
                for (int i = 0; i < matrix1.Height(); i++)
                {
                    for (int j = 0; j < matrix2.Width(); j++)
                    {
                        for (int k = 0; k < matrix2.Height(); k++)
                        {
                            var matrix1Value = matrix1.GetValue(i, k) as dynamic;
                            var matrix2Value = matrix2.GetValue(k, j) as dynamic;
                            result.SetValue(i, j, result.GetValue(i, j) + matrix1Value / matrix2Value);
                        }
                    }
                }
                return result;
            }

            public static Matrix<T> operator !(Matrix<T> matrix)
            {
                Matrix<T> result = new Matrix<T>(matrix);
                result *= -1;
                return result;

            }
        }
    }
}
