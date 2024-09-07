namespace LinkedListDoubly
{
    class Node<Type>(Type value)
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

    class LinkedList<Type>
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

        public int Length()
        {
            int count = 0;
            Node<Type>? currentNode = this.First;
            while (currentNode != null)
            {
                currentNode = currentNode.Next;
                count++;
            };

            return count;
        }

        public void Show()
        {
            Console.WriteLine(this);
        }

        public void Debug()
        {
            Console.WriteLine("Debug: Start");

            Node<Type>? currentNode = this.First;
            while (currentNode != null)
            {
                string previousText = (currentNode.Previous != null) ? currentNode.Previous.Value!.ToString()! : "null";
                string nextText = (currentNode.Next != null) ? currentNode.Next.Value!.ToString()! : "null";

                Console.WriteLine($"Debug: {previousText} <- {currentNode.Value} -> {nextText}");

                currentNode = currentNode.Next;
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

        public Type Pop()
        {
            if (this.First == null || this.Last == null)
            {
                throw new InvalidOperationException("Linked List is empty");
            };

            Type popValue = this.Last.Value;
            if (this.First == this.Last)
            {
                this.First = null;
                this.Last = null;

                return popValue;
            };

            this.Last = this.Last.Previous;
            if (this.Last != null)
            {
                this.Last.Next = null;
            };

            return popValue;
        }

        public Type Get(int index)
        {
            if (this.First == null || this.Last == null)
            {
                throw new InvalidCastException("Linked List is empty");
            };

            if (index < 0 || (this.Length() <= index))
            {
                throw new IndexOutOfRangeException("Index out of bound");
            };

            int currentIndex;
            Node<Type> currentNode;
            if (index < (this.Length() / 2))
            {
                currentNode = this.First;
                currentIndex = 0;
                while (currentIndex < index)
                {
                    currentNode = currentNode.Next!;
                    currentIndex++;
                };

                return currentNode.Value;
            };

            currentNode = this.Last;
            currentIndex = this.Length() - 1;
            while (currentIndex > index)
            {
                currentNode = currentNode.Previous!;
                currentIndex--;
            };

            return currentNode.Value;
        }

        public void Insert(Type value, int index)
        {
            if ((index < 0) || (this.Length() < index))
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of bound");
            };

            Node<Type> newNode = new(value);
            if (index == 0)
            {
                if (this.First == null || this.Last == null)
                {
                    this.First = newNode;
                    this.Last = newNode;

                    return;
                };

                newNode.Next = this.First;
                this.First.Previous = newNode;
                this.First = newNode;

                return;
            };

            if (index == this.Length())
            {
                this.Last!.Next = newNode;
                newNode.Previous = this.Last;
                this.Last = newNode;

                return;
            };

            int currentIndex;
            Node<Type> currentNode;
            if (index < (this.Length() / 2))
            {
                currentIndex = 0;
                currentNode = this.First!;
                while (currentIndex < index)
                {
                    currentNode = currentNode.Next!;
                    currentIndex++;
                };
            }
            else
            {
                currentIndex = this.Length() - 1;
                currentNode = this.Last!;
                while (currentIndex > index)
                {
                    currentNode = currentNode.Previous!;
                    currentIndex--;
                };
            };

            newNode.Next = currentNode;
            newNode.Previous = currentNode.Previous;
            currentNode.Previous!.Next = newNode;
            currentNode.Previous = newNode;
        }

        public void Swap(int firstIndex, int secondIndex)
        {
            if (this.First == null || this.Last == null)
            {
                throw new InvalidOperationException("Linked List is empty");
            };

            if ((firstIndex < 0) || (this.Length() <= firstIndex))
            {
                throw new ArgumentOutOfRangeException(nameof(firstIndex), "First index out of bound");
            };

            if ((secondIndex < 0) || (this.Length() <= secondIndex))
            {
                throw new ArgumentOutOfRangeException(nameof(secondIndex), "Second index out of bound");
            };

            if (firstIndex == secondIndex)
            {
                return;
            };

            int currentFirstIndex;
            Node<Type> currentFirstNode;
            if (firstIndex < (this.Length() / 2))
            {
                currentFirstNode = this.First;
                currentFirstIndex = 0;
                while (currentFirstIndex < firstIndex)
                {
                    currentFirstNode = currentFirstNode.Next!;
                    currentFirstIndex++;
                };
            }
            else
            {
                currentFirstNode = this.Last;
                currentFirstIndex = this.Length() - 1;
                while (currentFirstIndex > firstIndex)
                {
                    currentFirstNode = currentFirstNode.Previous!;
                    currentFirstIndex--;
                };
            };

            int currentSecondIndex;
            Node<Type> currentSecondNode;
            if (secondIndex < (this.Length() / 2))
            {
                currentSecondNode = this.First;
                currentSecondIndex = 0;
                while (currentSecondIndex < secondIndex)
                {
                    currentSecondNode = currentSecondNode.Next!;
                    currentSecondIndex++;
                };
            }
            else
            {
                currentSecondNode = this.Last;
                currentSecondIndex = this.Length() - 1;
                while (currentSecondIndex > secondIndex)
                {
                    currentSecondNode = currentSecondNode.Previous!;
                    currentSecondIndex--;
                };
            };

            if (currentFirstNode == this.First)
            {
                this.First = currentSecondNode;
            }
            else if (currentSecondNode == this.First)
            {
                this.First = currentFirstNode;
            };

            if (currentFirstNode == this.Last)
            {
                this.Last = currentSecondNode;
            }
            else if (currentSecondNode == this.Last)
            {
                this.Last = currentFirstNode;
            };

            Node<Type> temp = currentFirstNode.Next!;
            currentFirstNode.Next = currentSecondNode.Next;
            currentSecondNode.Next = temp;

            if (currentFirstNode.Next != null)
            {
                currentFirstNode.Next.Previous = currentFirstNode;
            };

            if (currentSecondNode.Next != null)
            {
                currentSecondNode.Next.Previous = currentSecondNode;
            };

            temp = currentFirstNode.Previous!;
            currentFirstNode.Previous = currentSecondNode.Previous;
            currentSecondNode.Previous = temp;

            if (currentFirstNode.Previous != null)
            {
                currentFirstNode.Previous.Next = currentFirstNode;
            };

            if (currentSecondNode.Previous != null)
            {
                currentSecondNode.Previous.Next = currentSecondNode;
            };
        }

        public void Remove(int index)
        {
            if (this.First == null || this.Last == null)
            {
                throw new InvalidOperationException("Linked List is empty");
            };

            if ((index < 0) || (this.Length() <= index))
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of bound");
            };

            if (index == 0)
            {
                if (this.First == this.Last)
                {
                    this.First = null;
                    this.Last = null;

                    return;
                };

                this.First = this.First.Next;
                this.First!.Previous = null;

                return;
            };

            if (index == (this.Length() - 1))
            {
                this.Last = this.Last.Previous;
                this.Last!.Next = null;

                return;
            };

            int currentIndex;
            Node<Type> currentNode;
            if (index < (this.Length() / 2))
            {
                currentIndex = 0;
                currentNode = this.First!;
                while (currentIndex < index)
                {
                    currentNode = currentNode.Next!;
                    currentIndex++;
                };
            }
            else
            {
                currentIndex = this.Length() - 1;
                currentNode = this.Last!;
                while (currentIndex > index)
                {
                    currentNode = currentNode.Previous!;
                    currentIndex--;
                };
            };

            currentNode.Previous!.Next = currentNode.Next;
            currentNode.Next!.Previous = currentNode.Previous;
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

    class Program
    {
        public static void Main(string[] args)
        {
            LinkedList<int> linkedList1 = new();
            linkedList1.Add(7);
            linkedList1.Add(8);
            linkedList1.Add(9);

            Console.Write("Show: ");
            linkedList1.Show();
            Console.WriteLine();

            linkedList1.Debug();
            Console.WriteLine();

            Console.WriteLine($"Length: {linkedList1.Length()}");
            Console.WriteLine();

            Console.WriteLine($"First: {linkedList1.First?.Value}");
            Console.WriteLine($"Last: {linkedList1.Last?.Value}");
            Console.WriteLine();

            Console.WriteLine($"Get 0: {linkedList1.Get(0)}");
            Console.WriteLine($"Get 1: {linkedList1.Get(1)}");
            Console.WriteLine($"Get 2: {linkedList1.Get(2)}");
            Console.WriteLine();

            Console.Write($"Pop: {linkedList1.Pop()} => ");
            linkedList1.Show();
            Console.WriteLine();

            Console.Write("Insert: ");
            linkedList1.Insert(3, 2);
            linkedList1.Insert(2, 1);
            linkedList1.Insert(1, 0);
            linkedList1.Show();
            Console.WriteLine();

            Console.Write("Remove: ");
            linkedList1.Remove(4);
            linkedList1.Remove(3);
            linkedList1.Remove(2);
            linkedList1.Show();
            Console.WriteLine();

            Console.Write("Swap: ");
            linkedList1.Swap(0, 1);
            linkedList1.Show();
            Console.WriteLine();

            Console.Write("ToString: ");
            Console.WriteLine(linkedList1);
            Console.WriteLine();

            linkedList1.Debug();
            Console.WriteLine();
        }
    }
}