namespace BasicSorting.Structure
{
    public class Node<Type>(Type value)
    {
        private readonly Type _value = value;
        private Node<Type>? _next = null;
        private Node<Type>? _previous = null;

        public Type Value
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

        private void Add(Type value)
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

        public string BubbleSort()
        {
            if (typeof(Type) != typeof(int))
            {
                throw new ArgumentException("Type must be int");
            };

            if (this.First == null || this.Last == null)
            {
                throw new InvalidOperationException("Linked List is empty");
            };

            bool swapped;
            do
            {
                swapped = false;
                for (Node<Type>? currentNode = this.First; (currentNode != null) && (currentNode.Next != null); currentNode = currentNode.Next)
                {
                    if (((int)(object)currentNode.Value!) > ((int)(object)currentNode.Next.Value!))
                    {
                        this.Swap(currentNode, currentNode.Next);

                        swapped = true;
                    };
                };
            } while (swapped);

            return this.ToString();
        }

        public string SelectionSort()
        {
            if (typeof(Type) != typeof(int))
            {
                throw new ArgumentException("Type must be int");
            };

            if (this.First == null || this.Last == null)
            {
                throw new InvalidOperationException("Linked List is empty");
            };

            for (Node<Type>? currentNode = this.First; (currentNode != null) && (currentNode.Next != null);)
            {
                Node<Type>? minimumNode = currentNode;
                for (Node<Type>? comparisonNode = currentNode.Next; comparisonNode != null; comparisonNode = comparisonNode.Next)
                {
                    if (((int)(object)comparisonNode.Value!) < ((int)(object)minimumNode.Value!))
                    {
                        minimumNode = comparisonNode;
                    };
                };

                Node<Type>? tempNode = currentNode.Next;
                if (minimumNode != currentNode)
                {
                    this.Swap(currentNode, minimumNode);
                };

                currentNode = tempNode;
            };

            return this.ToString();
        }

        public string InsertionSort()
        {
            if (typeof(Type) != typeof(int))
            {
                throw new ArgumentException("Type must be int");
            };

            if (this.First == null || this.Last == null)
            {
                throw new InvalidOperationException("Linked List is empty");
            };

            for (Node<Type>? extractedNode = this.First.Next; extractedNode != null;)
            {
                Node<Type>? nextNode = extractedNode.Next;
                for (Node<Type>? currentNode = extractedNode.Previous; currentNode != null;)
                {
                    if (((int)(object)currentNode.Value!) > ((int)(object)extractedNode.Value!))
                    {
                        Node<Type>? temp = currentNode.Previous;

                        this.Swap(currentNode, extractedNode);

                        currentNode = temp;

                        continue;
                    };

                    break;
                };

                extractedNode = nextNode;
            };

            return this.ToString();
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