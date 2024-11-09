namespace RedBlackTree.Structure
{
    public class Node<TypeKey>(TypeKey key)
    {
        private readonly TypeKey? _key = key;
        private Node<TypeKey>? _right = null;
        private Node<TypeKey>? _left = null;

        public TypeKey? Key
        {
            get { return this._key; }
        }

        public Node<TypeKey>? Right
        {
            get { return this._right; }
            set { this._right = value; }
        }

        public Node<TypeKey>? Left
        {
            get { return this._left; }
            set { this._left = value; }
        }

        public override string ToString()
        {
            return $"Node({this.Key})";
        }
    };

    public class RedBlackTree<TypeKey>()
    {
        private Node<TypeKey>? _root = null;

        public Node<TypeKey>? Root
        {
            get { return this._root; }
            set { this._root = value; }
        }

        public void Show()
        {
            Console.WriteLine(this);
        }

        public bool IsExist(TypeKey key)
        {
            return IsExistRecursively(this.Root, key);
        }

        private bool IsExistRecursively(Node<TypeKey>? currentNode, TypeKey key)
        {
            if (currentNode == null)
            {
                return false;
            }

            if (currentNode.Key!.Equals(key))
            {
                return true;
            }

            if ((typeof(TypeKey) == typeof(int)) ? ((int)(object)key! < (int)(object)currentNode.Key!) : ((char)(object)key! < (char)(object)currentNode.Key!))
            {
                return IsExistRecursively(currentNode.Left, key);
            }
            else
            {
                return IsExistRecursively(currentNode.Right, key);
            }
        }

        public string PreOrder()
        {
            List<TypeKey> result = [];

            PreOrderRecursively(this.Root, result);

            return string.Join(", ", result);
        }

        private void PreOrderRecursively(Node<TypeKey>? node, List<TypeKey> result)
        {
            if (node != null)
            {
                result.Add(node.Key!);
                PreOrderRecursively(node.Left, result);
                PreOrderRecursively(node.Right, result);
            }
        }

        public string InOrder()
        {
            List<TypeKey> result = [];

            InOrderRecursively(this.Root, result);

            return string.Join(", ", result);
        }

        private void InOrderRecursively(Node<TypeKey>? node, List<TypeKey> result)
        {
            if (node != null)
            {
                InOrderRecursively(node.Left, result);
                result.Add(node.Key!);
                InOrderRecursively(node.Right, result);
            }
        }

        public string PostOrder()
        {
            List<TypeKey> result = [];

            PostOrderRecursively(this.Root, result);

            return string.Join(", ", result);
        }

        private void PostOrderRecursively(Node<TypeKey>? node, List<TypeKey> result)
        {
            if (node != null)
            {
                PostOrderRecursively(node.Left, result);
                PostOrderRecursively(node.Right, result);
                result.Add(node.Key!);
            }
        }

        public override string ToString()
        {
            if (this.Root == null)
            {
                return "Root(0): null";
            }

            List<string> textList = [$"Root(0): {this.Root.Key}\n"];
            ToStringRecursivelyWithNull(textList, this.Root.Left, 'L');
            ToStringRecursivelyWithNull(textList, this.Root.Right, 'R');

            return string.Join("", textList);
        }

        // private void ToStringRecursively(List<string> textList, Node<TypeKey>? node, char type, int level = 1)
        // {
        //     textList.Add($"{string.Concat(Enumerable.Repeat(' ', level))}{type}({level}): {node!.Key}");

        //     if (node.Left != null)
        //     {
        //         ToStringRecursively(textList, node.Left, 'L', level + 1);
        //     }

        //     if (node.Right != null)
        //     {
        //         ToStringRecursively(textList, node.Right, 'R', level + 1);
        //     }
        // }

        private void ToStringRecursivelyWithNull(List<string> textList, Node<TypeKey>? node, char type, int level = 1)
        {
            if (node == null)
            {
                textList.Add($"{string.Concat(Enumerable.Repeat(' ', level))}{type}({level}): null\n");

                if (type == 'R')
                {
                    textList.Add("\n");
                }

                return;
            }

            textList.Add($"{string.Concat(Enumerable.Repeat(' ', level))}{type}({level}): {node.Key}\n");
            ToStringRecursivelyWithNull(textList, node.Left, 'L', level + 1);
            ToStringRecursivelyWithNull(textList, node.Right, 'R', level + 1);
        }
    }
}