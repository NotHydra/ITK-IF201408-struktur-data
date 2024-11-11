namespace BinarySearchTree.Structure
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

    public class BinarySearchTree<TypeKey>()
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

        public void AddUniqueRandomChar(int count)
        {
            Random random = new();
            for (int i = 0; i < count; i++)
            {
                char randomChar = (char)random.Next('A', 'Z' + 1);
                while (this.IsExist((TypeKey)(object)randomChar))
                {
                    randomChar = (char)random.Next('A', 'Z' + 1);
                }

                this.Add((TypeKey)(object)randomChar);
            }
        }

        public bool Add(TypeKey key)
        {
            if (!((typeof(TypeKey) != typeof(int)) || (typeof(TypeKey) != typeof(char))))
            {
                throw new ArgumentException("Type must be int or char");
            }

            if (this.IsExist(key))
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

        public bool RemoveFromLeft(TypeKey key)
        {
            return Remove(key, true);
        }

        public bool RemoveFromRight(TypeKey key)
        {
            return Remove(key, false);
        }

        private bool Remove(TypeKey key, bool isLeft)
        {
            if (!((typeof(TypeKey) != typeof(int)) || (typeof(TypeKey) != typeof(char))))
            {
                throw new ArgumentException("Type must be int or char");
            }

            if (this.Root == null)
            {
                throw new InvalidOperationException("Tree is empty");
            };

            if (!this.IsExist(key))
            {
                return false;
            }

            this.RemoveRecursively(this.Root, this.Root, key, isLeft);

            return true;
        }

        private void RemoveRecursively(Node<TypeKey> parentNode, Node<TypeKey> currentNode, TypeKey key, bool isLeft)
        {
            if ((typeof(TypeKey) == typeof(int)) ? ((int)(object)key! < (int)(object)currentNode.Key!) : ((char)(object)key! < (char)(object)currentNode.Key!))
            {
                RemoveRecursively(parentNode, currentNode.Left!, key, isLeft);

                return;
            }
            else if ((typeof(TypeKey) == typeof(int)) ? ((int)(object)key! > (int)(object)currentNode.Key!) : ((char)(object)key! > (char)(object)currentNode.Key!))
            {
                RemoveRecursively(parentNode, currentNode.Right!, key, isLeft);

                return;
            }
            else
            {
                Node<TypeKey>? replacementNode;
                if (currentNode.Left == null && currentNode.Right == null)
                {
                    replacementNode = null;
                }
                else if (currentNode.Left == null)
                {
                    replacementNode = currentNode.Right;
                }
                else if (currentNode.Right == null)
                {
                    replacementNode = currentNode.Left;
                }
                else
                {

                    if (isLeft)
                    {
                        Node<TypeKey> rightMostNode = currentNode.Left!;
                        Node<TypeKey> rightMostNodeParent = currentNode;
                        while (rightMostNode.Right != null)
                        {
                            rightMostNodeParent = rightMostNode;
                            rightMostNode = rightMostNode.Right;
                        }

                        if (rightMostNodeParent != currentNode)
                        {
                            rightMostNodeParent.Right = rightMostNode.Left;
                            rightMostNode.Left = currentNode.Left;
                        }

                        rightMostNode.Right = currentNode.Right;
                        replacementNode = rightMostNode;
                    }
                    else
                    {
                        Node<TypeKey> leftMostNode = currentNode.Right!;
                        Node<TypeKey> leftMostNodeParent = currentNode;
                        while (leftMostNode.Left != null)
                        {
                            leftMostNodeParent = leftMostNode;
                            leftMostNode = leftMostNode.Left;
                        }

                        if (leftMostNodeParent != currentNode)
                        {
                            leftMostNodeParent.Left = leftMostNode.Right;
                            leftMostNode.Right = currentNode.Right;
                        }

                        leftMostNode.Left = currentNode.Left;
                        replacementNode = leftMostNode;
                    }
                }

                if (this.Root == currentNode)
                {
                    this.Root = replacementNode;
                }
                else if (parentNode.Left == currentNode)
                {
                    parentNode.Left = replacementNode;
                }
                else
                {
                    parentNode.Right = replacementNode;
                }
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