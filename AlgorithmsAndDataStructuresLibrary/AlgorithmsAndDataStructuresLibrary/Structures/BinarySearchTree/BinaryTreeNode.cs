using System;

namespace AlgorithmsAndDataStructuresLibrary.Structures.BinarySearchTree
{
    class BinaryTreeNode<TNodeType> where TNodeType : IComparable<TNodeType>
    {
        private TNodeType m_value;
        private BinaryTreeNode<TNodeType> m_left;
        private BinaryTreeNode<TNodeType> m_right;

        public void SetValue(TNodeType _value)
        {
            m_value = _value;
        }

        public TNodeType GetValue()
        {
            return m_value;
        }

        public void AddValue(TNodeType _value)
        {
            if (m_value.CompareTo(_value) > 0)
            {
                if (m_left == null)
                {
                    m_left = new BinaryTreeNode<TNodeType>();
                    m_left.m_value = _value;
                }
                else
                {
                    m_left.AddValue(_value);
                }
            }
            else
            {
                if (m_right == null)
                {
                    m_right = new BinaryTreeNode<TNodeType>();
                    m_right.m_value = _value;
                }
                else
                {
                    m_right.AddValue(_value);
                }
            }
        }

        public BinaryTreeNode<TNodeType> GetLeftNode()
        {
            return m_left;
        }

        public BinaryTreeNode<TNodeType> GetRightNode()
        {
            return m_right;
        }

        public void Print()
        {
            Console.WriteLine(m_value);
            if (m_left != null)
            {
                m_left.Print();
            }
            if (m_right != null)
            {
                m_right.Print();
            }
        }
    }
}
