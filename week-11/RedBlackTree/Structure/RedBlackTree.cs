namespace RedBlackTree.Structure
{
    public enum KeyDirection
    {
        Left,
        Right,
    }

    public class Node<TypeKey>(TypeKey key, bool red, Node<TypeKey>? parent)
    {
        private readonly TypeKey? _key = key;
        private bool _red = red;
        private Node<TypeKey>? _parent = parent;
        private Node<TypeKey>? _right = null;
        private Node<TypeKey>? _left = null;

        public TypeKey? Key
        {
            get { return this._key; }
        }

        public bool Red
        {
            get { return this._red; }
            set { this._red = value; }
        }

        public Node<TypeKey>? Parent
        {
            get { return this._parent; }
            set { this._parent = value; }
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

        public KeyDirection CompareKey(TypeKey key1, TypeKey key2)
        {
            if (typeof(TypeKey) == typeof(int))
            {
                return (int)(object)key1! < (int)(object)key2! ? KeyDirection.Left : KeyDirection.Right;
            }
            else
            {
                return (char)(object)key1! < (char)(object)key2! ? KeyDirection.Left : KeyDirection.Right;
            }
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

            if (this.CompareKey(key, currentNode.Key!) == KeyDirection.Left)
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

            if (this.IsExist(key))
            {
                return false;
            }

            if (this.Root == null)
            {
                this.Root = new Node<TypeKey>(key, false, null);

                return true;
            }

            Node<TypeKey> node = this.Add(this.Root, key);
            KeyDirection childDirection = this.CompareKey(node.Key!, node.Parent!.Key!);
            node = node.Parent;

            int addCase;
            do
            {
                addCase = GetAddCase(node);

                switch (addCase)
                {
                    case 1:
                        break;

                    case 2:
                        Node<TypeKey> oldParent = node.Parent!;
                        node = AddCase2(node)!;

                        if (node != null)
                        {
                            childDirection = this.CompareKey(oldParent.Key!, oldParent.Parent!.Key!);
                        }

                        break;

                    case 4:
                        node.Red = false;
                        break;

                    case 56:
                        AddCase56(node, this.CompareKey(node.Key!, node.Parent!.Key!), childDirection);
                        break;
                }
            } while (addCase == 2 && node != null);

            return true;
        }

        private Node<TypeKey> Add(Node<TypeKey> node, TypeKey key)
        {
            Node<TypeKey> newNode;
            while (true)
            {
                if (this.CompareKey(key, node.Key!) == KeyDirection.Left)
                {
                    if (node.Left == null)
                    {
                        newNode = new Node<TypeKey>(key, true, node);
                        node.Left = newNode;
                        break;
                    }
                    else
                    {
                        node = node.Left;
                    }
                }
                else
                {
                    if (node.Right == null)
                    {
                        newNode = new Node<TypeKey>(key, true, node);
                        node.Right = newNode;
                        break;
                    }
                    else
                    {
                        node = node.Right;
                    }
                }
            }

            return newNode;
        }

        private int GetAddCase(Node<TypeKey> node)
        {
            if (node.Red == false)
            {
                return 1;
            }
            else if (node.Parent == null)
            {
                return 4;
            }
            else
            {
                Node<TypeKey> grandParent = node.Parent;
                KeyDirection parentDirection = this.CompareKey(node.Key!, node.Parent.Key!);
                Node<TypeKey> uncle = parentDirection == KeyDirection.Left ? grandParent.Right! : grandParent.Left!;

                if (uncle == null || uncle.Red == false)
                {
                    return 56;
                }

                return 2;
            }
        }

        private Node<TypeKey>? AddCase2(Node<TypeKey> node)
        {
            Node<TypeKey> grandParent = node.Parent!;
            KeyDirection parentDirection = this.CompareKey(node.Key!, node.Parent!.Key!);
            Node<TypeKey> uncle = parentDirection == KeyDirection.Left ? grandParent.Right! : grandParent.Left!;

            node.Red = false;
            uncle.Red = false;
            grandParent.Red = true;

            if (node.Parent.Parent == null)
            {
                node.Parent.Red = false;
            }

            return node.Parent.Parent;
        }

        private void AddCase56(Node<TypeKey> node, KeyDirection parentDirection, KeyDirection childDirection)
        {
            if (parentDirection == KeyDirection.Left)
            {
                if (childDirection == KeyDirection.Right)
                {
                    node = RotateLeft(node);
                }

                node = RotateRight(node.Parent!);
                node.Red = false;
                node.Right!.Red = true;
            }
            else
            {
                if (childDirection == KeyDirection.Left)
                {
                    node = RotateRight(node);
                }

                node = RotateLeft(node.Parent!);
                node.Red = false;
                node.Left!.Red = true;
            }
        }

        private Node<TypeKey> RotateLeft(Node<TypeKey> node)
        {
            Node<TypeKey>? temp1 = node;
            Node<TypeKey>? temp2 = node!.Right!.Left;

            node = node.Right;
            node.Parent = temp1.Parent;
            if (node.Parent != null)
            {
                if (this.CompareKey(node.Key!, node.Parent.Key!) == KeyDirection.Left)
                {
                    node.Parent.Left = node;
                }
                else
                {
                    node.Parent.Right = node;
                }
            }

            node.Left = temp1;
            node.Left.Parent = node;

            node.Left.Right = temp2;
            if (temp2 != null)
            {
                node.Left.Right!.Parent = temp1;
            }

            if (node.Parent == null)
            {
                this.Root = node;
            }

            return node;
        }

        private Node<TypeKey> RotateRight(Node<TypeKey> node)
        {
            Node<TypeKey>? temp1 = node;
            Node<TypeKey>? temp2 = node!.Left!.Right;

            node = node.Left;
            node.Parent = temp1.Parent;
            if (node.Parent != null)
            {
                if (this.CompareKey(node.Key!, node.Parent.Key!) == KeyDirection.Left)
                {
                    node.Parent.Left = node;
                }
                else
                {
                    node.Parent.Right = node;
                }
            }

            node.Right = temp1;
            node.Right.Parent = node;

            node.Right.Left = temp2;
            if (temp2 != null)
            {
                node.Right.Left!.Parent = temp1;
            }

            if (node.Parent == null)
            {
                this.Root = node;
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