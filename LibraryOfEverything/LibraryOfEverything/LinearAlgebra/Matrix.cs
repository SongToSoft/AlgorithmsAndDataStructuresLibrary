using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOfEverything
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

            public Matrix<T> Rotate()
            {
                Matrix<T> result = new Matrix<T>(Width(), Height());
                int k = 0;
                for (int j = 0; j < Width(); ++j)
                {
                    int w = 0;
                    for (int i = Height() - 1; i >= 0; --i)
                    {
                        result.SetValue(k, w, m_values[i, j]);
                        ++w;
                    }
                    ++k;
                }
                return result;
            }
            public static Matrix<T> operator * (Matrix<T> matrix, double value)
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

            public static Matrix<T> operator / (Matrix<T> matrix, double value)
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

            public static Matrix<T> operator + (Matrix<T> matrix1, Matrix<T> matrix2)
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

            public static Matrix<T> operator - (Matrix<T> matrix1, Matrix<T> matrix2)
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

            public static Matrix<T> operator * (Matrix<T> matrix1, Matrix<T> matrix2)
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

            public static Matrix<T> operator / (Matrix<T> matrix1, Matrix<T> matrix2)
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
        }
    }
}
