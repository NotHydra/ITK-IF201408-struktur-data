namespace Tree.Structure
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

    public class Tree<TypeKey>()
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

        public bool Add(TypeKey key)
        {
            if (!((typeof(TypeKey) != typeof(int)) || (typeof(TypeKey) != typeof(char))))
            {
                throw new ArgumentException("Type must be int or char");
            }

            if (IsExist(key))
            {
                return false;
            }

            if (this.Root == null)
            {
                this.Root = new Node<TypeKey>(key);
            }
            else
            {
                this.AddRecursively(this.Root, key);
            }

            return true;
        }

        private void AddRecursively(Node<TypeKey> currentNode, TypeKey key)
        {
            if ((typeof(TypeKey) == typeof(int)) ? ((int)(object)key! < (int)(object)currentNode.Key!) : ((char)(object)key! < (char)(object)currentNode.Key!))
            {
                if (currentNode.Left == null)
                {
                    currentNode.Left = new Node<TypeKey>(key);
                }
                else
                {
                    this.AddRecursively(currentNode.Left, key);
                }
            }
            else
            {
                if (currentNode.Right == null)
                {
                    currentNode.Right = new Node<TypeKey>(key);
                }
                else
                {
                    this.AddRecursively(currentNode.Right, key);
                }
            }
        }

        public bool Remove(TypeKey key)
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException("Tree is empty");
            };

            this.Root = RemoveRecursively(this.Root, key, out bool wasRemoved);

            return wasRemoved;
        }

        private Node<TypeKey>? RemoveRecursively(Node<TypeKey>? currentNode, TypeKey key, out bool wasRemoved)
        {
            if (currentNode == null)
            {
                wasRemoved = false;
                return null;
            }

            if ((typeof(TypeKey) == typeof(int)) ? ((int)(object)key! < (int)(object)currentNode.Key!) : ((char)(object)key! < (char)(object)currentNode.Key!))
            {
                currentNode.Left = RemoveRecursively(currentNode.Left, key, out wasRemoved);
            }
            else if ((typeof(TypeKey) == typeof(int)) ? ((int)(object)key! > (int)(object)currentNode.Key!) : ((char)(object)key! > (char)(object)currentNode.Key!))
            {
                currentNode.Right = RemoveRecursively(currentNode.Right, key, out wasRemoved);
            }
            else
            {
                wasRemoved = true;

                if (currentNode.Left == null && currentNode.Right == null)
                {
                    return null;
                }
                else if (currentNode.Left == null)
                {
                    return currentNode.Right;
                }
                else if (currentNode.Right == null)
                {
                    return currentNode.Left;
                }
                else
                {
                    Node<TypeKey> smallestNode = FindMin(currentNode.Right);
                    currentNode = new Node<TypeKey>(smallestNode.Key!);
                    currentNode.Right = RemoveRecursively(currentNode.Right, smallestNode.Key!, out _);
                    currentNode.Left = currentNode.Left;
                }
            }

            return currentNode;
        }

        private Node<TypeKey> FindMin(Node<TypeKey> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
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

            List<string> textList = [$"Root(0): {this.Root.Key}"];
            ToStringRecursivelyWithNull(textList, this.Root.Left, 'L');
            ToStringRecursivelyWithNull(textList, this.Root.Right, 'R');

            return string.Join("\n", textList);
        }

        public void ToStringRecursively(List<string> textList, Node<TypeKey>? node, char type, int level = 1)
        {
            textList.Add($"{string.Concat(Enumerable.Repeat(' ', level))}{type}({level}): {node!.Key}");

            if (node.Left != null)
            {
                ToStringRecursively(textList, node.Left, 'L', level + 1);
            }

            if (node.Right != null)
            {
                ToStringRecursively(textList, node.Right, 'R', level + 1);
            }
        }

        public void ToStringRecursivelyWithNull(List<string> textList, Node<TypeKey>? node, char type, int level = 1)
        {
            if (node == null)
            {
                textList.Add($"{string.Concat(Enumerable.Repeat(' ', level))}{type}({level}): null");

                return;
            }

            textList.Add($"{string.Concat(Enumerable.Repeat(' ', level))}{type}({level}): {node.Key}");
            ToStringRecursivelyWithNull(textList, node.Left, 'L', level + 1);
            ToStringRecursivelyWithNull(textList, node.Right, 'R', level + 1);
        }
    }
}