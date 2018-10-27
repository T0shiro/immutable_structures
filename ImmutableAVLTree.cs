﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    class ImmutableAVL
    {
        public class Node
        {
            public int data;
            public Node left;
            public Node right;

            public Node(int data)
            {
                this.data = data;
            }

            public void PrintPretty(string indent, bool last)
            {
                Console.Write(indent);
                if (last)
                {
                    Console.Write("\\-");
                    indent += "  ";
                }
                else
                {
                    Console.Write("|-");
                    indent += "| ";
                }
                Console.WriteLine("({0}) ", this.data);
                if (this.left != null)
                {
                    this.left.PrintPretty(indent, false);
                }
                if (this.right != null)
                {
                    this.right.PrintPretty(indent, true);
                }
            }
        }

        Node root;

        private void dichotomyTree(ImmutableAVL tree, int[] array, int start, int end)
        {
            double middle = (end - start) / 2;
            int rootIndex = (int)Math.Floor(middle);
            tree.root = tree.Add(array[start + rootIndex]);
            if (end - start > 0)
            {
                if (start + rootIndex - 1 >= 0)
                {
                    dichotomyTree(tree, array, start, start + rootIndex - 1);
                }
                if (start + rootIndex + 1 >= 0 && end >= 0)
                {
                    dichotomyTree(tree, array, start + rootIndex + 1, end);
                }
            }
        }

        public ImmutableAVL(int[] array)
        {
            Array.Sort(array);
            dichotomyTree(this, array, 0, array.Length - 1);
        }

        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            this.root.PrintPretty("", true);
            Console.WriteLine();
        }

        private Node newNode(Node old)
        {
            Node result = new Node(old.data);
            result.left = old.left;
            result.right = old.right;
            return result;
        }

        private Node newNode(Node old, Node left, Node right)
        {

            Node result = new Node(old.data);
            result.left = left;
            result.right = right;
            return result;
        }

        public Node Add(int data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
            return root;
        }

        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = new Node(n.data);
                return current;
            }
            else if (n.data < current.data)
            {
                current.left = newNode(RecursiveInsert(current.left, n));
                current = balance_tree(current);
            }
            else if (n.data > current.data)
            {
                current.right = newNode(RecursiveInsert(current.right, n));
                current = balance_tree(current);
            }
            return current;
        }

        private Node balance_tree(Node current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }

        private int max(int l, int r)
        {
            return l > r ? l : r;
        }

        private int getHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }

        private int balance_factor(Node current)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }

        private Node RotateRR(Node parent)
        {
            Node pivot = parent.right;
            parent = newNode(parent, parent.left, pivot.left);
            pivot = newNode(pivot, parent, pivot.right);
            //parent.right = pivot.left; //new parent
            //pivot.left = parent;
            return pivot;
        }

        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent = newNode(parent, pivot.right, parent.right);
            pivot = newNode(pivot, pivot.left, parent);
            //parent.left = pivot.right;
            //pivot.right = parent;
            return pivot;
        }

        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent = newNode(parent, RotateRR(pivot), parent.right);
            //parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }

        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent = newNode(parent, parent.left, RotateLL(pivot));
            //parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }
}