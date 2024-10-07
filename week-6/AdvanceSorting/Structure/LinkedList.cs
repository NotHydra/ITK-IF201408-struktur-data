namespace AdvanceSorting.Structure
{
    public class Node<Type>(Type value)
    {
        private readonly Type? _value = value;
        private Node<Type>? _next = null;
        private Node<Type>? _previous = null;

        public Type? Value
        {
            get { return this._value; }
        }

        public Node<Type>? Next
        {
            get { return this._next; }
            set { this._next = value; }
        }

        public Node<Type>? Previous
        {
            get { return this._previous; }
            set { this._previous = value; }
        }
        
        public override string ToString()
        {
            string text = "null -> ";
            Node<Type>? currentNode = this;
            while (currentNode != null)
            {
                text += $"{currentNode.Value} -> ";
                currentNode = currentNode.Next;
            };
        
            text += "null";
        
            return text;
        }
    };

    public class LinkedList<Type>
    {
        private Node<Type>? _first = null;
        private Node<Type>? _last = null;

        public Node<Type>? First
        {
            get { return this._first; }
            set { this._first = value; }
        }

        public Node<Type>? Last
        {
            get { return this._last; }
            set { this._last = value; }
        }

        public void Show()
        {
            Console.WriteLine(this);
        }

        public void Debug()
        {
            Console.WriteLine("Debug: Start");

            int currentIndex = 0;
            for (Node<Type>? currentNode = this.First; currentNode != null; currentNode = currentNode.Next)
            {
                string previousText = (currentNode.Previous != null) ? currentNode.Previous.Value!.ToString()! : "null";
                string nextText = (currentNode.Next != null) ? currentNode.Next.Value!.ToString()! : "null";

                Console.WriteLine($"Debug {currentIndex + 1}: {previousText} <- {currentNode.Value} -> {nextText}");

                currentIndex += 1;
            };

            Console.WriteLine("Debug: End");
        }

        public void Add(Type value)
        {
            Node<Type> newNode = new(value);
            if (this.First == null || this.Last == null)
            {
                this.First = newNode;
                this.Last = newNode;

                return;
            };

            this.Last.Next = newNode;
            newNode.Previous = this.Last;
            this.Last = newNode;
        }

        public void addRange(int minimum, int maximum)
        {
            for (int i = minimum; i <= maximum; i++)
            {
                this.Add((Type)Convert.ChangeType(i, typeof(Type)));
            }
        }

        public void AddRandom(int amount, int minimum, int maximum)
        {
            if (typeof(Type) != typeof(int))
            {
                throw new ArgumentException("Type must be int");
            };

            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than 0");
            };

            if (minimum >= maximum)
            {
                throw new ArgumentException("Minimum must be less than maximum");
            };

            Random random = new();
            for (int _ = 0; _ < amount; _++)
            {
                this.Add((Type)(object)random.Next(minimum, maximum + 1));
            };
        }

        private void Swap(Node<Type> firstNode, Node<Type> secondNode)
        {
            if (this.First == null || this.Last == null)
            {
                throw new InvalidOperationException("Linked List is empty");
            };

            if (firstNode == this.First)
            {
                this.First = secondNode;
            }
            else if (secondNode == this.First)
            {
                this.First = firstNode;
            };

            if (firstNode == this.Last)
            {
                this.Last = secondNode;
            }
            else if (secondNode == this.Last)
            {
                this.Last = firstNode;
            };

            Node<Type> temp = firstNode.Next!;
            firstNode.Next = secondNode.Next;
            secondNode.Next = temp;

            if (firstNode.Next != null)
            {
                firstNode.Next.Previous = firstNode;
            };

            if (secondNode.Next != null)
            {
                secondNode.Next.Previous = secondNode;
            };

            temp = firstNode.Previous!;
            firstNode.Previous = secondNode.Previous;
            secondNode.Previous = temp;

            if (firstNode.Previous != null)
            {
                firstNode.Previous.Next = firstNode;
            };

            if (secondNode.Previous != null)
            {
                secondNode.Previous.Next = secondNode;
            };
        }
        
        public string MergeSort()
        {
            if (typeof(Type) != typeof(int))
            {
                throw new ArgumentException("Type must be int");
            };

            if (this.First == null || this.Last == null)
            {
                throw new InvalidOperationException("Linked List is empty");
            };

            this.First = this.SplitLinkedList(this.First);

            Node<Type> currentNode = this.First;
            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
            };
            
            this.Last = currentNode;
            
            return this.ToString();
        }
    
        private Node<Type> RightLinkedList(Node<Type> headNode)
        {
            Node<Type> middleNode = headNode;
            Node<Type> lastNode = headNode;
            while (lastNode.Next != null && lastNode.Next.Next != null)
            {
                middleNode = middleNode.Next!;
                lastNode = lastNode.Next.Next;
            };

            Node<Type> rightNode = middleNode.Next!;
            rightNode.Previous = null;
            middleNode.Next = null;

            return rightNode;
        }
        
        private Node<Type> SplitLinkedList(Node<Type> headNode)
        {
            if (headNode.Next == null)
            {
                return headNode;
            };
            
            Node<Type> rightNode = this.RightLinkedList(headNode);

            headNode = this.SplitLinkedList(headNode);
            rightNode = this.SplitLinkedList(rightNode);
            
            return MergeLinkedList(headNode, rightNode);
        }

        private Node<Type> MergeLinkedList(Node<Type>? leftNode, Node<Type>? rightNode)
        {
            if (leftNode == null)
            {
                return rightNode!;
            };
            
            if (rightNode == null)
            {
                return leftNode!;
            };
            
            if (((int)(object)leftNode.Value!) <= ((int)(object)rightNode.Value!))
            {
                leftNode.Next = MergeLinkedList(leftNode.Next, rightNode);
                leftNode.Next.Previous = leftNode;
                leftNode.Previous = null;
                
                return leftNode;
            };
            
            rightNode.Next = MergeLinkedList(leftNode, rightNode.Next);
            rightNode.Next.Previous = rightNode;
            rightNode.Previous = null;
            
            return rightNode;
        }
        
        public override string ToString()
        {
            string text = "null -> ";
            Node<Type>? currentNode = this.First;
            while (currentNode != null)
            {
                text += $"{currentNode.Value} -> ";
                currentNode = currentNode.Next;
            };

            text += "null";

            return text;
        }
    }
}