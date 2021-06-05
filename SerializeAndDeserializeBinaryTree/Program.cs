using System;
using System.Collections.Generic;
using System.Linq;

namespace SerializeAndDeserializeBinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new TreeNode(1);
            root.Left = new TreeNode(2);
            root.Left.Left = new TreeNode(21);
            root.Left.Right = new TreeNode(22);
            root.Right = new TreeNode(3);
            root.Right.Left = null;
            root.Right.Right = new TreeNode(31);
            var data = "1,2,21,#,#,22,#,#,3,#,31,#,#";
            var serilaizer = new TreeSerilizer();
            var output = TreeSerilizer.Serialize(root);
            Console.WriteLine(output);
            Console.WriteLine(TreeSerilizer.Serialize(root) == data);
            Console.WriteLine(TreeSerilizer.Serialize(TreeSerilizer.Deserialize(data)) == data);
        }
    }

    public class TreeNode {
        public int Val { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(int val) {
            this.Val = val;
        }
    }

    public class TreeSerilizer {
        private static string delimiter = ",";
        private static string terminater = "#";
        public static string Serialize(TreeNode root) {
            var data = serializeHelper(root);
            return data;
        }

        private static string serializeHelper(TreeNode root) {
            if (root == null) {
                return terminater;
            }

            var left = serializeHelper(root.Left);
            var right = serializeHelper(root.Right);
            return string.Format("{0},{1},{2}", root.Val, left, right); // error
        }

        public static TreeNode Deserialize(string data) {
            var input = data.Split(delimiter).ToList(); // error
            return deserializeHelper(input);
        }

        private static TreeNode deserializeHelper(List<string> data) {
            if (data == null || data.Count == 0) { // error
                return null;
            }

            if (data[0] == terminater) {
                data.RemoveAt(0); // error
                return null;
            }

            var root = new TreeNode(Convert.ToInt32(data[0])); // error
            data.RemoveAt(0);
            root.Left = deserializeHelper(data);
            root.Right = deserializeHelper(data);
            return root;
        }
    }
}
