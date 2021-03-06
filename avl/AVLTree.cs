using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    /// AVL Tree data structure
    class AVL
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

        public AVL()
        {
        }

        private void dichotomyTree(AVL tree, int[] array, int start, int end)
        {
            double middle = (end - start) / 2;
            int rootIndex = (int) Math.Floor(middle);
            tree.Add(array[start + rootIndex]);
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

        public AVL(int[] array)
        {
            Array.Sort(array);
            dichotomyTree(this, array, 0, array.Length - 1);
        }

        //only used for check in test
        public int Count()
        {
            return getSize(root);
        }

        public Node Head()
        {
            return root;
        }

        public void Add(int data)
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
        }

        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (n.data < current.data)
            {
                current.left = RecursiveInsert(current.left, n);
                current = balance_tree(current);
            }
            else if (n.data > current.data)
            {
                current.right = RecursiveInsert(current.right, n);
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

        public void Delete(int target)
        {
            root = Delete(root, target);
        }

        private Node Delete(Node current, int target)
        {
            Node parent;
            if (current == null)
            {
                return null;
            }

            //left subtree
            if (target < current.data)
            {
                current.left = Delete(current.left, target);
                if (balance_factor(current) == -2) //here
                {
                    if (balance_factor(current.right) <= 0)
                    {
                        current = RotateRR(current);
                    }
                    else
                    {
                        current = RotateRL(current);
                    }
                }
            }
            //right subtree
            else if (target > current.data)
            {
                current.right = Delete(current.right, target);
                if (balance_factor(current) == 2)
                {
                    if (balance_factor(current.left) >= 0)
                    {
                        current = RotateLL(current);
                    }
                    else
                    {
                        current = RotateLR(current);
                    }
                }
            }
            //if target is found
            else
            {
                if (current.right != null)
                {
                    //delete its inorder successor
                    parent = current.right;
                    while (parent.left != null)
                    {
                        parent = parent.left;
                    }

                    current.data = parent.data;
                    current.right = Delete(current.right, parent.data);
                    if (balance_factor(current) == 2) //rebalancing
                    {
                        if (balance_factor(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                else
                {
                    //if current.left != null
                    return current.left;
                }
            }

            return current;
        }

        public void Find(int key)
        {
            if (Find(key, root).data == key)
            {
                Console.WriteLine("{0} was found!", key);
            }
            else
            {
                Console.WriteLine("Nothing found!");
            }
        }

        private Node Find(int target, Node current)
        {
            if (target < current.data)
            {
                if (target == current.data)
                {
                    return current;
                }

                return Find(target, current.left);
            }

            if (target == current.data)
            {
                return current;
            }

            return Find(target, current.right);
        }

        public int Peek()
        {
            Node current = this.root;
            while (current.left != null)
            {
                current = current.left;
            }
            return current.data;
        }

        public int Pop()
        {
            Node current = this.root;
            while (current.left != null)
            {
                current = current.left;
            }

            int value = current.data;
            this.Delete(current.data);
            return value;
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

        //only used for check in test
        private int getSize(Node current)
        {
            if (current.left == null && current.right == null)
            {
                return 1;
            }

            int currentSize = 1;
            if (current.left != null)
            {
                currentSize += getSize(current.left);
            }

            if (current.right != null)
            {
                currentSize += getSize(current.right);
            }

            return currentSize;
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
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }

        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }

        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }

        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }
}