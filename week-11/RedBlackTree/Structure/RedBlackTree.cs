namespace RedBlackTree.Structure
{
    public enum KeyDirection
    {
        Left,
        Middle,
        Right,
    }

    public class Node<TypeKey>(TypeKey key, bool red, Node<TypeKey>? parent)
    {
        private TypeKey? _key = key;
        private bool _red = red;
        private Node<TypeKey>? _parent = parent;
        private Node<TypeKey>? _right = null;
        private Node<TypeKey>? _left = null;

        public TypeKey? Key
        {
            get { return this._key; }
            set { this._key = value; }
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

        private Node<TypeKey> GetMin(Node<TypeKey> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }

        private Node<TypeKey> GetMax(Node<TypeKey> node)
        {
            while (node.Right != null)
            {
                node = node.Right;
            }

            return node;
        }

        private KeyDirection CompareKeyDirection(TypeKey key1, TypeKey key2)
        {
            if (typeof(TypeKey) == typeof(int))
            {
                return (int)(object)key1! < (int)(object)key2! ? KeyDirection.Left : ((int)(object)key1! == (int)(object)key2! ? KeyDirection.Middle : KeyDirection.Right);
            }
            else
            {
                return (char)(object)key1! < (char)(object)key2! ? KeyDirection.Left : ((char)(object)key1! == (char)(object)key2! ? KeyDirection.Middle : KeyDirection.Right);
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

            if (this.CompareKeyDirection(key, currentNode.Key!) == KeyDirection.Left)
            {
                return IsExistRecursively(currentNode.Left, key);
            }
            else
            {
                return IsExistRecursively(currentNode.Right, key);
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
                if (this.CompareKeyDirection(node.Key!, node.Parent.Key!) == KeyDirection.Left)
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
                if (this.CompareKeyDirection(node.Key!, node.Parent.Key!) == KeyDirection.Left)
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
                this.Root = new Node<TypeKey>(key, false, null);

                return true;
            }

            Node<TypeKey> node = this.Add(this.Root, key);
            KeyDirection childDirection = this.CompareKeyDirection(node.Key!, node.Parent!.Key!);
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
                            childDirection = this.CompareKeyDirection(oldParent.Key!, oldParent.Parent!.Key!);
                        }

                        break;

                    case 4:
                        node.Red = false;
                        break;

                    case 56:
                        AddCase56(node, this.CompareKeyDirection(node.Key!, node.Parent!.Key!), childDirection);
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
                if (this.CompareKeyDirection(key, node.Key!) == KeyDirection.Left)
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
                KeyDirection parentDirection = this.CompareKeyDirection(node.Key!, node.Parent.Key!);
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
            KeyDirection parentDirection = this.CompareKeyDirection(node.Key!, node.Parent!.Key!);
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

        public bool Remove(TypeKey key)
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

            Node<TypeKey> node = Remove(this.Root, key);
            node = RemoveSimpleCases(node)!;

            if (node == null)
            {
                return true;
            }

            RemoveLeaf(node.Parent!, CompareKeyDirection(node.Key!, node.Parent!.Key!));

            do
            {
                node = RemoveColor(node)!;
            }
            while (node != null && node.Parent != null);

            return true;
        }

        private Node<TypeKey> Remove(Node<TypeKey> node, TypeKey key)
        {
            while (true)
            {
                if (this.CompareKeyDirection(key, node.Key!) == KeyDirection.Left)
                {
                    node = node.Left!;
                }
                else if (this.CompareKeyDirection(key, node.Key!) == KeyDirection.Right)
                {
                    node = node.Right!;
                }
                else
                {
                    break;
                }
            }

            return node;
        }

        private int GetRemoveCase(Node<TypeKey> node)
        {
            KeyDirection direction = this.CompareKeyDirection(node.Key!, node.Parent!.Key!);

            Node<TypeKey> sibling = direction == KeyDirection.Left ? node.Parent!.Right! : node.Parent!.Left!;
            Node<TypeKey> closeNephew = direction == KeyDirection.Left ? sibling.Left! : sibling.Right!;
            Node<TypeKey> farNephew = direction == KeyDirection.Left ? sibling.Right! : sibling.Left!;

            if (sibling.Red == true)
            {
                return 3;
            }
            else if (farNephew != null && farNephew.Red == true)
            {
                return 6;
            }
            else if (closeNephew != null && closeNephew.Red == true)
            {
                return 5;
            }
            else if (node.Parent!.Red == true)
            {
                return 4;
            }
            else
            {
                return 1;
            }
        }

        private Node<TypeKey>? RemoveSimpleCases(Node<TypeKey> node)
        {
            if (node.Parent == null && node.Left == null && node.Right == null)
            {
                this.Root = null;

                return null;
            }

            if (node.Left != null && node.Right != null)
            {
                Node<TypeKey> successor = GetMin(node.Right!);
                node.Key = successor.Key;
                node = successor;
            }

            if (node.Red == true)
            {
                RemoveLeaf(node.Parent!, CompareKeyDirection(node.Key!, node.Parent!.Key!));

                return null;
            }
            else
            {
                return RemoveBlackNode(node);
            }
        }

        private void RemoveLeaf(Node<TypeKey> node, KeyDirection childDirection)
        {
            if (childDirection == KeyDirection.Left)
            {
                node.Left = null;
            }
            else
            {
                node.Right = null;
            }
        }

        private Node<TypeKey>? RemoveBlackNode(Node<TypeKey> node)
        {
            Node<TypeKey>? child = node.Left ?? node.Right;

            if (child == null)
            {
                return node;
            }

            child.Red = false;
            child.Parent = node.Parent;

            KeyDirection childDirection = node.Parent == null ? KeyDirection.Middle : this.CompareKeyDirection(node.Key!, node.Parent.Key!);

            Transplant(node.Parent!, child, childDirection);

            return null;
        }

        private void Transplant(Node<TypeKey> node, Node<TypeKey> child, KeyDirection childDirection)
        {
            if (node == null)
            {
                this.Root = child;
            }
            else if (child == null)
            {
                RemoveLeaf(node, childDirection);
            }
            else if (childDirection == KeyDirection.Left)
            {
                node.Left = child;
            }
            else
            {
                node.Right = child;
            }
        }

        private Node<TypeKey>? RemoveColor(Node<TypeKey> node)
        {
            int removeCase = GetRemoveCase(node);

            KeyDirection direction = this.CompareKeyDirection(node.Key!, node.Parent!.Key!);

            Node<TypeKey> sibling = direction == KeyDirection.Left ? node.Parent!.Right! : node.Parent!.Left!;
            Node<TypeKey> closeNephew = direction == KeyDirection.Left ? sibling.Left! : sibling.Right!;
            Node<TypeKey> farNephew = direction == KeyDirection.Left ? sibling.Right! : sibling.Left!;

            switch (removeCase)
            {
                case 1:
                    sibling.Red = true;

                    return node.Parent;

                case 3:
                    RemoveCase3(node, closeNephew, direction);

                    break;

                case 4:
                    RemoveCase4(sibling);

                    break;

                case 5:
                    RemoveCase5(node, sibling, direction);

                    break;

                case 6:
                    RemoveCase6(node, farNephew, direction);

                    break;
            }

            return null;
        }

        private void RemoveCase3(Node<TypeKey> node, Node<TypeKey>? closeNephew, KeyDirection childDirection)
        {
            Node<TypeKey> sibling = childDirection == KeyDirection.Left ? RotateLeft(node.Parent!) : RotateRight(node.Parent!);

            sibling.Red = false;
            if (childDirection == KeyDirection.Left)
            {
                sibling.Left!.Red = true;
            }
            else
            {
                sibling.Right!.Red = true;
            }

            sibling = closeNephew!;
            Node<TypeKey> farNephew = childDirection == KeyDirection.Left ? sibling.Right! : sibling.Left!;
            if (farNephew != null && farNephew.Red == true)
            {
                RemoveCase6(node, farNephew, childDirection);
                return;
            }

            closeNephew = childDirection == KeyDirection.Left ? sibling.Left! : sibling.Right!;
            if (closeNephew != null && closeNephew.Red == true)
            {
                RemoveCase5(node, sibling, childDirection);
                return;
            }

            RemoveCase4(sibling);
        }

        private void RemoveCase4(Node<TypeKey> sibling)
        {
            sibling.Red = true;
            sibling.Parent!.Red = false;
        }

        private void RemoveCase5(Node<TypeKey> node, Node<TypeKey> sibling, KeyDirection childDirection)
        {
            sibling = childDirection == KeyDirection.Left ? RotateRight(sibling) : RotateLeft(sibling);
            Node<TypeKey> farNephew = childDirection == KeyDirection.Left ? sibling.Right! : sibling.Left!;

            sibling.Red = false;
            farNephew.Red = true;

            RemoveCase6(node, farNephew, childDirection);
        }

        private void RemoveCase6(Node<TypeKey> node, Node<TypeKey> farNephew, KeyDirection childDirection)
        {
            Node<TypeKey> oldParent = node.Parent!;
            node = childDirection == KeyDirection.Left ? RotateLeft(oldParent) : RotateRight(oldParent);
            node.Red = oldParent.Red;
            oldParent.Red = false;
            farNephew.Red = false;
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