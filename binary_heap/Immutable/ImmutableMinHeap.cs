using System;
using System.Collections.Generic;

namespace DataStructures.BinaryHeap
{
    public class ImmutableMinHeap
    {
        private Node root;
        private int size;

        public ImmutableMinHeap(int[] values)
        {
            Heapify(values);
        }

        private void Heapify(int[] ints)
        {
            int index = ints[0];
            root = new Node(index);
        }

        private void PercolateDown(int index)
        {
        }

        private void PercolateUp(int index, Node left, Node right)
        {
            
        }

        public void Insert(int n)
        {
            Insert(new Node(n));
        }

        private void Insert(Node n)
        {
            
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