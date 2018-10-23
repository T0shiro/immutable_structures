using System;
using System.Collections.Generic;
using System.Text;

namespace CSKicksCollection.Trees
{
    class AVLTreeNode<T> where T : IComparable
    {
        private T value;
        private AVLTreeNode<T> leftChild;
        private AVLTreeNode<T> rightChild;
        private AVLTreeNode<T> parent;
        private AVLTree<T> tree;

        /// <summary>
        /// The value stored at the node
        /// </summary>
        public virtual T Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Gets or sets the left child node
        /// </summary>
        public virtual AVLTreeNode<T> LeftChild
        {
            get { return leftChild; }
            set { leftChild = value; }
        }

        /// <summary>
        /// Gets or sets the right child node
        /// </summary>
        public virtual AVLTreeNode<T> RightChild
        {
            get { return rightChild; }
            set { rightChild = value; }
        }

        /// <summary>
        /// Gets or sets the parent node
        /// </summary>
        public virtual AVLTreeNode<T> Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        /// <summary>
        /// Gets or sets the Binary Tree the node belongs to
        /// </summary>
        public virtual AVLTree<T> Tree
        {
            get { return tree; }
            set { tree = value; }
        }

        /// <summary>
        /// Gets whether the node is a leaf (has no children)
        /// </summary>
        public virtual bool IsLeaf
        {
            get { return this.ChildCount == 0; }
        }

        /// <summary>
        /// Gets whether the node is internal (has children nodes)
        /// </summary>
        public virtual bool IsInternal
        {
            get { return this.ChildCount > 0; }
        }

        /// <summary>
        /// Gets whether the node is the left child of its parent
        /// </summary>
        public virtual bool IsLeftChild
        {
            get { return this.Parent != null && this.Parent.LeftChild == this; }
        }

        /// <summary>
        /// Gets whether the node is the right child of its parent
        /// </summary>
        public virtual bool IsRightChild
        {
            get { return this.Parent != null && this.Parent.RightChild == this; }
        }

        /// <summary>
        /// Gets the number of children this node has
        /// </summary>
        public virtual int ChildCount
        {
            get
            {
                int count = 0;

                if (this.LeftChild != null)
                    count++;

                if (this.RightChild != null)
                    count++;

                return count;
            }
        }

        /// <summary>
        /// Gets whether the node has a left child node
        /// </summary>
        public virtual bool HasLeftChild
        {
            get { return (this.LeftChild != null); }
        }

        /// <summary>
        /// Gets whether the node has a right child node
        /// </summary>
        public virtual bool HasRightChild
        {
            get { return (this.RightChild != null); }
        }

        /// <summary>
        /// Create a new instance of an AVL Tree node
        /// </summary>
        public AVLTreeNode(T value)
        {
            this.value = value;
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
            Console.WriteLine("({0}) ", this.value);
            if (this.leftChild != null)
            {
                this.leftChild.PrintPretty(indent, false);
            }
            if (this.rightChild != null)
            {
                this.rightChild.PrintPretty(indent, true);
            }
        }
    }

    /// <summary>
    /// AVL Tree data structure
    /// </summary>
    class AVLTree<T> where T : IComparable
    {
        private AVLTreeNode<T> head;
        private Comparison<IComparable> comparer = CompareElements;
        private int size;

        private void dichotomyTree(AVLTree<T> tree, T[] array, int start, int end)
        {
            double middle = (end - start) / 2;
            int rootIndex = (int)Math.Floor(middle);
            Console.WriteLine(start+" "+end+" "+rootIndex);
            Console.WriteLine(start + rootIndex);
            tree.Add(array[start + rootIndex]);
            if (end-start > 0)
            {
                if (start + rootIndex - 1 > 0)
                {
                    dichotomyTree(tree, array, start, start + rootIndex - 1);
                }
                if (start + rootIndex + 1 > 0 && end > 0)
                {
                    dichotomyTree(tree, array, start + rootIndex + 1, end);
                }
            }
        }

        public AVLTree(T[] array)
        {
            Array.Sort(array);
            dichotomyTree(this, array, 0, array.Length-1);
        }

        public int Count
        {
            get { return size; }
        }

        public void PrintPretty()
        {
            this.head.PrintPretty("", true);
        }

        /// <summary>
        /// Gets or sets the root of the tree (the top-most node)
        /// </summary>
        public virtual AVLTreeNode<T> Root
        {
            get { return head; }
            set { head = value; }
        }

        /// <summary>
        /// Compares two elements to determine their positions within the tree.
        /// </summary>
        public static int CompareElements(IComparable x, IComparable y)
        {
            return x.CompareTo(y);
        }

        /// <summary>
        /// Returns the AVL Node corresponding to the given value
        /// </summary>
        public new AVLTreeNode<T> Find(T value)
        {
            AVLTreeNode<T> node = this.head; //start at head
            while (node != null)
            {
                if (node.Value.Equals(value)) //parameter value found
                    return node;
                else
                {
                    //Search left if the value is smaller than the current node
                    bool searchLeft = comparer((IComparable)value, (IComparable)node.Value) < 0;

                    if (searchLeft)
                        node = node.LeftChild; //search left
                    else
                        node = node.RightChild; //search right
                }
            }
            return null; //not found
        }

        /// <summary>
        /// Insert a value in the tree and rebalance the tree if necessary.
        /// </summary>
        public new void Add(T value)
        {
            AVLTreeNode<T> node = new AVLTreeNode<T>(value);

            this.AddNode(node); //add normally

            //Balance every node going up, starting with the parent
            AVLTreeNode<T> parentNode = node.Parent;

            while (parentNode != null)
            {
                int balance = this.getBalance(parentNode);
                if (Math.Abs(balance) == 2) //-2 or 2 is unbalanced
                {
                    //Rebalance tree
                    this.balanceAt(parentNode, balance);
                }

                parentNode = parentNode.Parent; //keep going up
            }
        }

        /// <summary>
        /// Adds a node to the tree
        /// </summary>
        public virtual void AddNode(AVLTreeNode<T> node)
        {
            if (this.head == null) //first element being added
            {
                this.head = node; //set node as root of the tree
                node.Tree = this;
                size++;
            }
            else
            {
                if (node.Parent == null)
                    node.Parent = head; //start at head

                //Node is inserted on the left side if it is smaller or equal to the parent
                bool insertLeftSide = comparer((IComparable)node.Value, (IComparable)node.Parent.Value) <= 0;

                if (insertLeftSide) //insert on the left
                {
                    if (node.Parent.LeftChild == null)
                    {
                        node.Parent.LeftChild = node; //insert in left
                        size++;
                        node.Tree = this; //assign node to this binary tree
                    }
                    else
                    {
                        node.Parent = node.Parent.LeftChild; //scan down to left child
                        this.AddNode(node); //recursive call
                    }
                }
                else //insert on the right
                {
                    if (node.Parent.RightChild == null)
                    {
                        node.Parent.RightChild = node; //insert in right
                        size++;
                        node.Tree = this; //assign node to this binary tree
                    }
                    else
                    {
                        node.Parent = node.Parent.RightChild;
                        this.AddNode(node);
                    }
                }
            }
        }

        /// <summary>
        /// Removes a given value from the tree and rebalances the tree if necessary.
        /// </summary>
        public bool Remove(T value)
        {
            AVLTreeNode<T> valueNode = this.Find(value);
            return this.RemoveNode(valueNode);
        }

        /// <summary>
        /// Removes a node from the tree and returns whether the removal was successful.
        /// </summary>>
        public bool RemoveNode(AVLTreeNode<T> removeNode)
        {
            if (removeNode == null || removeNode.Tree != this)
                return false; //value doesn't exist or not of this tree

            //Note whether the node to be removed is the root of the tree
            bool wasHead = (removeNode == head);

            if (this.Count == 1)
            {
                this.head = null; //only element was the root
                removeNode.Tree = null;

                size--; //decrease total element count
            }
            else if (removeNode.IsLeaf) //Case 1: No Children
            {
                //Remove node from its parent
                if (removeNode.IsLeftChild)
                    removeNode.Parent.LeftChild = null;
                else
                    removeNode.Parent.RightChild = null;

                removeNode.Tree = null;
                removeNode.Parent = null;

                size--; //decrease total element count
            }
            else if (removeNode.ChildCount == 1) //Case 2: One Child
            {
                if (removeNode.HasLeftChild)
                {
                    //Put left child node in place of the node to be removed
                    removeNode.LeftChild.Parent = removeNode.Parent; //update parent

                    if (wasHead)
                        this.Root = removeNode.LeftChild; //update root reference if needed

                    if (removeNode.IsLeftChild) //update the parent's child reference
                        removeNode.Parent.LeftChild = removeNode.LeftChild;
                    else
                        removeNode.Parent.RightChild = removeNode.LeftChild;
                }
                else //Has right child
                {
                    //Put left node in place of the node to be removed
                    removeNode.RightChild.Parent = removeNode.Parent; //update parent

                    if (wasHead)
                        this.Root = removeNode.RightChild; //update root reference if needed

                    if (removeNode.IsLeftChild) //update the parent's child reference
                        removeNode.Parent.LeftChild = removeNode.RightChild;
                    else
                        removeNode.Parent.RightChild = removeNode.RightChild;
                }

                removeNode.Tree = null;
                removeNode.Parent = null;
                removeNode.LeftChild = null;
                removeNode.RightChild = null;

                size--; //decrease total element count
            }
            else //Case 3: Two Children
            {
                //Find inorder predecessor (right-most node in left subtree)
                AVLTreeNode<T> successorNode = removeNode.LeftChild;
                while (successorNode.RightChild != null)
                {
                    successorNode = successorNode.RightChild;
                }

                removeNode.Value = successorNode.Value; //replace value

                this.Remove(successorNode); //recursively remove the inorder predecessor
            }


            return true;
        }

        /// <summary>
        /// Removes a given node from the tree and rebalances the tree if necessary.
        /// </summary>
        public bool Remove(AVLTreeNode<T> valueNode)
        {
            //Save reference to the parent node to be removed
            AVLTreeNode<T> parentNode = valueNode.Parent;

            //Remove the node as usual
            bool removed = RemoveNode(valueNode);

            if (!removed)
                return false; //removing failed, no need to rebalance
            else
            {
                //Balance going up the tree
                while (parentNode != null)
                {
                    int balance = this.getBalance(parentNode);

                    if (Math.Abs(balance) == 1) //1, -1
                        break; //height hasn't changed, can stop
                    else if (Math.Abs(balance) == 2) //2, -2
                    {
                        //Rebalance tree
                        this.balanceAt(parentNode, balance);
                    }

                    parentNode = parentNode.Parent;
                }

                return true;
            }
        }

        /// <summary>
        /// Balances an AVL Tree node
        /// </summary>
        protected virtual void balanceAt(AVLTreeNode<T> node, int balance)
        {
            if (balance == 2) //right outweighs
            {
                int rightBalance = getBalance(node.RightChild);

                if (rightBalance == 1 || rightBalance == 0)
                {
                    //Left rotation needed
                    rotateLeft(node);
                }
                else if (rightBalance == -1)
                {
                    //Right rotation needed
                    rotateRight(node.RightChild);

                    //Left rotation needed
                    rotateLeft(node);
                }
            }
            else if (balance == -2) //left outweighs
            {
                int leftBalance = getBalance(node.LeftChild);
                if (leftBalance == 1)
                {
                    //Left rotation needed
                    rotateLeft(node.LeftChild);

                    //Right rotation needed
                    rotateRight(node);
                }
                else if (leftBalance == -1 || leftBalance == 0)
                {
                    //Right rotation needed
                    rotateRight(node);
                }
            }
        }

        /// <summary>
        /// Returns the height of the entire tree
        /// </summary>
        public virtual int GetHeight()
        {
            return this.GetHeight(this.Root);
        }

        /// <summary>
        /// Returns the height of the subtree rooted at the parameter value
        /// </summary>
        public int GetHeight(T value)
        {
            //Find the value's node in tree
            AVLTreeNode<T> valueNode = this.Find(value);
            if (value != null)
                return this.GetHeight(valueNode);
            else
                return 0;
        }

        /// <summary>
        /// Returns the height of the subtree rooted at the parameter node
        /// </summary>
        public virtual int GetHeight(AVLTreeNode<T> startNode)
        {
            if (startNode == null)
                return 0;
            else
                return 1 + Math.Max(GetHeight(startNode.LeftChild), GetHeight(startNode.RightChild));
        }


        /// <summary>
        /// Determines the balance of a given node
        /// </summary>
        protected virtual int getBalance(AVLTreeNode<T> root)
        {
            //Balance = right child's height - left child's height
            return this.GetHeight(root.RightChild) - this.GetHeight(root.LeftChild);
        }

        /// <summary>
        /// Rotates a node to the left within an AVL Tree
        /// </summary>
        protected virtual void rotateLeft(AVLTreeNode<T> root)
        {
            if (root == null)
                return;

            AVLTreeNode<T> pivot = root.RightChild;

            if (pivot == null)
                return;
            else
            {
                AVLTreeNode<T> rootParent = root.Parent; //original parent of root node
                bool isLeftChild = (rootParent != null) && rootParent.LeftChild == root; //whether the root was the parent's left node
                bool makeTreeRoot = root.Tree.Root == root; //whether the root was the root of the entire tree

                //Rotate
                root.RightChild = pivot.LeftChild;
                pivot.LeftChild = root;

                //Update parents
                root.Parent = pivot;
                pivot.Parent = rootParent;

                if (root.RightChild != null)
                    root.RightChild.Parent = root;

                //Update the entire tree's Root if necessary
                if (makeTreeRoot)
                    pivot.Tree.Root = pivot;

                //Update the original parent's child node
                if (isLeftChild)
                    rootParent.LeftChild = pivot;
                else
                    if (rootParent != null)
                    rootParent.RightChild = pivot;
            }
        }

        /// <summary>
        /// Rotates a node to the right within an AVL Tree
        /// </summary>
        protected virtual void rotateRight(AVLTreeNode<T> root)
        {
            if (root == null)
                return;

            AVLTreeNode<T> pivot = root.LeftChild;

            if (pivot == null)
                return;
            else
            {
                AVLTreeNode<T> rootParent = root.Parent; //original parent of root node
                bool isLeftChild = (rootParent != null) && rootParent.LeftChild == root; //whether the root was the parent's left node
                bool makeTreeRoot = root.Tree.Root == root; //whether the root was the root of the entire tree

                //Rotate
                root.LeftChild = pivot.RightChild;
                pivot.RightChild = root;

                //Update parents
                root.Parent = pivot;
                pivot.Parent = rootParent;

                if (root.LeftChild != null)
                    root.LeftChild.Parent = root;

                //Update the entire tree's Root if necessary
                if (makeTreeRoot)
                    pivot.Tree.Root = pivot;

                //Update the original parent's child node
                if (isLeftChild)
                    rootParent.LeftChild = pivot;
                else
                    if (rootParent != null)
                    rootParent.RightChild = pivot;
            }
        }
    }
}