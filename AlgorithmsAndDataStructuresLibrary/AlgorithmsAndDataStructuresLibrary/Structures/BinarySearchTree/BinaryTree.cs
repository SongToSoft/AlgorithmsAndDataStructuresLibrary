using System;

namespace AlgorithmsAndDataStructuresLibrary.Structures.BinarySearchTree
{
    class BinaryTree<TTreeType> where TTreeType : IComparable<TTreeType>
    {
        private BinaryTreeNode<TTreeType> m_head;

        public void AddValue(TTreeType _value)
        {
            if (m_head == null)
            {
                m_head = new BinaryTreeNode<TTreeType>();
                m_head.SetValue(_value);
            }
            else
            {
                m_head.AddValue(_value);
            }
        }

        public bool Search(TTreeType _value)
        {
            if (m_head == null)
            {
                return false;
            }
            else
            {
                var node = m_head;
                while (node != null)
                {
                    if (node.GetValue().CompareTo(_value) == 0)
                    {
                        return true;
                    }
                    else
                    {
                        if (node.GetValue().CompareTo(_value) > 0)
                        {
                            if (node.GetLeftNode() != null)
                            {
                                node = node.GetLeftNode();
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (node.GetRightNode() != null)
                            {
                                node = node.GetRightNode();
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                return false;
            }
        }

        public void DeleteNode(TTreeType _value)
        {
            if (m_head != null)
            {
                BinaryTreeNode<TTreeType> newHead = new BinaryTreeNode<TTreeType>();
                newHead.SetValue(m_head.GetValue());
                if (m_head.GetLeftNode() != null)
                {
                    DeleteNode(_value, newHead, m_head.GetLeftNode());
                }
                if (m_head.GetRightNode() != null)
                {
                    DeleteNode(_value, newHead, m_head.GetRightNode());
                }
                m_head = newHead;
            }
        }

        public void DeleteNode(TTreeType _value, BinaryTreeNode<TTreeType> newHead, BinaryTreeNode<TTreeType> node)
        {
            if (node.GetValue().CompareTo(_value) != 0)
            {
                newHead.AddValue(node.GetValue());
            }
            if (node.GetLeftNode() != null)
            {
                DeleteNode(_value, newHead, node.GetLeftNode());
            }
            if (node.GetRightNode() != null)
            {
                DeleteNode(_value, newHead, node.GetRightNode());
            }
        }

        public void Print()
        {
            if (m_head != null)
            {
                m_head.Print();
            }
        }
    }
}
