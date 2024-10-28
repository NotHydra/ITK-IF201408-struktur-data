namespace Tree.Structure
{
    public class Node<Type>(Type key)
    {
        private readonly Type? _key = key;
        private Node<Type>? _right = null;
        private Node<Type>? _left = null;

        public Type? Key
        {
            get { return this._key; }
        }

        public Node<Type>? Right
        {
            get { return this._right; }
            set { this._right = value; }
        }

        public Node<Type>? Left
        {
            get { return this._left; }
            set { this._left = value; }
        }

        public override string ToString()
        {
            return $"Node({this.Key})";
        }
    };

    public class Tree<Type>()
    {
        private Node<Type>? _root = null;

        public Node<Type>? Root
        {
            get { return this._root; }
            set { this._root = value; }
        }

        public bool IsExist(Type key)
        {
            return IsExistRecursively(this.Root, key);
        }

        private bool IsExistRecursively(Node<Type>? currentNode, Type key)
        {
            if (currentNode == null)
            {
                return false;
            }

            if (currentNode.Key!.Equals(key))
            {
                return true;
            }

            if ((typeof(Type) == typeof(int)) ? ((int)(object)key! < (int)(object)currentNode.Key!) : ((char)(object)key! < (char)(object)currentNode.Key!))
            {
                return IsExistRecursively(currentNode.Left, key);
            }
            else
            {
                return IsExistRecursively(currentNode.Right, key);
            }
        }

        public bool Add(Type key)
        {
            if (!((typeof(Type) != typeof(int)) || (typeof(Type) != typeof(char))))
            {
                throw new ArgumentException("Type must be int or char");
            }

            if (IsExist(key))
            {
                return false;
            }

            if (this.Root == null)
            {
                this.Root = new Node<Type>(key);
            }
            else
            {
                this.AddRecursively(this.Root, key);
            }

            return true;
        }

        private void AddRecursively(Node<Type> currentNode, Type key)
        {
            if ((typeof(Type) == typeof(int)) ? ((int)(object)key! < (int)(object)currentNode.Key!) : ((char)(object)key! < (char)(object)currentNode.Key!))
            {
                if (currentNode.Left == null)
                {
                    currentNode.Left = new Node<Type>(key);
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
                    currentNode.Right = new Node<Type>(key);
                }
                else
                {
                    this.AddRecursively(currentNode.Right, key);
                }
            }
        }

        public bool Remove(Type key)
        {
            this.Root = RemoveRecursively(this.Root, key, out bool wasRemoved);

            return wasRemoved;
        }

        private Node<Type>? RemoveRecursively(Node<Type>? currentNode, Type key, out bool wasRemoved)
        {
            if (currentNode == null)
            {
                wasRemoved = false;
                return null;
            }

            if ((typeof(Type) == typeof(int)) ? ((int)(object)key! < (int)(object)currentNode.Key!) : ((char)(object)key! < (char)(object)currentNode.Key!))
            {
                currentNode.Left = RemoveRecursively(currentNode.Left, key, out wasRemoved);
            }
            else if ((typeof(Type) == typeof(int)) ? ((int)(object)key! > (int)(object)currentNode.Key!) : ((char)(object)key! > (char)(object)currentNode.Key!))
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
                    Node<Type> smallestNode = FindMin(currentNode.Right);
                    currentNode = new Node<Type>(smallestNode.Key!);
                    currentNode.Right = RemoveRecursively(currentNode.Right, smallestNode.Key!, out _);
                    currentNode.Left = currentNode.Left;
                }
            }

            return currentNode;
        }

        private Node<Type> FindMin(Node<Type> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }

        public string PreOrder()
        {
            List<Type> result = [];

            PreOrderRecursively(this.Root, result);

            return string.Join(", ", result);
        }

        private void PreOrderRecursively(Node<Type>? node, List<Type> result)
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
            List<Type> result = [];

            InOrderRecursively(this.Root, result);

            return string.Join(", ", result);
        }

        private void InOrderRecursively(Node<Type>? node, List<Type> result)
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
            List<Type> result = [];

            PostOrderRecursively(this.Root, result);

            return string.Join(", ", result);
        }

        private void PostOrderRecursively(Node<Type>? node, List<Type> result)
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
            return this.InOrder();
        }
    }
}