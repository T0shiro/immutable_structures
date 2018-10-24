using System;
using System.Collections.Generic;

namespace DataStructures.BinaryHeap
{
    public class MinHeapImmutable
    {
        private Node root;
        private int size;

        public MinHeapImmutable(int[] values)
        {
            Heapify(values);
        }

        private void Heapify(int[] ints)
        {
            int index = ints[0];
            root = new Node(index);
            root.leftChild = new Node(ints[2 * index + 1]);
            root.rightChild = new Node(ints[2 * index + 2]);
        }

        private void PercolateDown(int index)
        {
        }

        private void PercolateUp(int index)
        {
        }

        public void Insert(Node n)
        {
            var sizeAfterInsertInBinary = Convert.ToString(size + 1, 2);
            Node currentNode = root;
            Stack<StackElement> stack = new Stack<StackElement>();
            if (sizeAfterInsertInBinary[0] == '1')
            {
                for (int i = 1; i < sizeAfterInsertInBinary.Length; i++)
                {
                    if (sizeAfterInsertInBinary[i] == '0')
                    {
                        if (currentNode.HasLeftChild())
                        {
                            currentNode = currentNode.leftChild;
                            stack.Push(new StackElement(currentNode, StackElement.Direction.LEFT));
                        }
                        else
                        {
                            currentNode.leftChild = n;
                            break;
                        }
                    }
                    else if (sizeAfterInsertInBinary[i] == '1')
                    {
                        if (currentNode.HasRightChild())
                        {
                            currentNode = currentNode.rightChild;
                            stack.Push(new StackElement(currentNode, StackElement.Direction.RIGHT));
                        }
                        else
                        {
                            currentNode.rightChild = n;
                            break;
                        }
                    }
                }
            }

            size++;
        }

        public Node Peek()
        {
            return root;
        }

        public int Pop()
        {
            return 0;
        }

        private void Swap(int index1, int index2)
        {
        }
    }

    public class Node
    {
        public Node leftChild;
        public Node rightChild;
        public int value;

        public Node(int value)
        {
            this.value = value;
        }

        public bool HasLeftChild()
        {
            return !leftChild.Equals(null);
        }

        public bool HasRightChild()
        {
            return !rightChild.Equals(null);
        }
    }

    public class StackElement
    {
        public enum Direction
        {
            LEFT, RIGHT
        }
        
        public Node node;
        public Direction direction;

        public StackElement(Node node, Direction direction)
        {
            this.node = node;
            this.direction = direction;
        }
    }
}