using System;
using System.Linq;
using NUnit.Framework;

namespace DataStructures
{
    public class ImmutableAVLTreeTests
    {
        private static int[][] arrays =
        {
            new[] {10, 9, 8, 7, 6},
            new[]{69, 42, 110, 3, 7, 1, 39}
        };

        [Test, TestCaseSource(nameof(arrays))]
        public void ListToTree(int[] values)
        {
            ImmutableAVL tree = new ImmutableAVL(values);
            Assert.AreEqual(values.Length, tree.Count());
            Assert.IsTrue(isAVLTreeValid(tree));
        }

        [Test, TestCaseSource(nameof(arrays))]
        public void Insert(int[] values)
        {
            ImmutableAVL tree = new ImmutableAVL(values);
            tree.Add(5);
            Assert.AreEqual(values.Length + 1, tree.Count());
            Assert.IsTrue(isAVLTreeValid(tree));
        }

        [Test, TestCaseSource(nameof(arrays))]
        public void Peek(int[] values)
        {
            ImmutableAVL tree = new ImmutableAVL(values);
            int peek = tree.Peek();
            Assert.AreEqual(peek, values.Min());
            Assert.AreEqual(values.Length, tree.Count());
        }

        [Test, TestCaseSource(nameof(arrays))]
        public void Pop(int[] values)
        {
            ImmutableAVL tree = new ImmutableAVL(values);
            int pop = tree.Pop();
            Assert.AreEqual(pop, values.Min());
            Assert.AreEqual(values.Length - 1, tree.Count());
            Assert.IsTrue(isAVLTreeValid(tree));
        }


        private bool isAVLTreeValid(ImmutableAVL tree)
        {
            return recursiveCheck(tree.Head());
        }

        private bool recursiveCheck(ImmutableAVL.Node current)
        {
            if (current.left != null)
            {
                if (current.left.data > current.data)
                {
                    return false;
                }
                else
                {
                    recursiveCheck(current.left);
                }
            }
            if (current.right != null)
            {
                if (current.right.data < current.data)
                {
                    return false;
                }
                else
                {
                    recursiveCheck(current.right);
                }
            }
            return true;
        }
    }
}