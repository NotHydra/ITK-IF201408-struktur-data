namespace LinkedListSingly
{
    class Node<Type>(Type value)
    {
        private readonly Type _value = value;
        private Node<Type>? _next = null;

        public Type Value
        {
            get { return this._value; }
        }

        public Node<Type>? Next
        {
            get { return this._next; }
            set { this._next = value; }
        }
    };

    class LinkedList<Type>
    {
        private Node<Type>? _first = null;

        public Node<Type>? First
        {
            get { return this._first; }
            set { this._first = value; }
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
                string? nextText = (currentNode.Next != null && currentNode.Next.Value != null) ? currentNode.Next.Value.ToString() : "null";

                Console.WriteLine($"Debug: {currentNode.Value} -> {nextText}");

                currentNode = currentNode.Next;
            };

            Console.WriteLine("Debug: End");
        }

        public void Add(Type value)
        {
            Node<Type> newNode = new(value);
            if (this.First == null)
            {
                this.First = newNode;

                return;
            };

            Node<Type> currentNode = this.First;
            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
            };

            currentNode.Next = newNode;
        }

        public Type Pop()
        {
            if (this.First == null)
            {
                throw new InvalidOperationException("Linked List is empty");
            };

            if (this.First.Next == null)
            {
                Type value = this.First.Value;
                this.First = null;

                return value;
            };

            Node<Type> currentNode = this.First;
            while ((currentNode.Next != null) && (currentNode.Next.Next != null))
            {
                currentNode = currentNode.Next;
            };

            if (currentNode.Next == null)
            {
                throw new InvalidOperationException("Invalid Node");
            };

            Type popValue = currentNode.Next.Value;
            currentNode.Next = null;

            return popValue;
        }

        public Type Get(int index)
        {
            if (this.First == null)
            {
                throw new InvalidOperationException("Linked List is empty");
            };

            if ((index < 0) || (this.Length() <= index))
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of bound");
            };

            int currentIndex = 0;
            Node<Type> currentNode = this.First;
            while ((currentNode.Next != null) && (currentIndex < index))
            {
                currentNode = currentNode.Next;
                currentIndex++;
            };

            return currentNode.Value;
        }

        public void Insert(Type value, int index)
        {
            if ((index < 0) || (this.Length() <= index))
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of bound");
            };

            Node<Type> newNode = new(value);
            if ((this.First == null) || (index == 0))
            {
                newNode.Next = this.First;
                this.First = newNode;

                return;
            };

            int currentIndex = 0;
            Node<Type> currentNode = this.First;
            while ((currentNode.Next != null) && (currentIndex < (index - 1)))
            {
                currentNode = currentNode.Next;
                currentIndex++;
            };

            newNode.Next = currentNode.Next;
            currentNode.Next = newNode;
        }

        public void Swap(int firstIndex, int secondIndex)
        {
            if (firstIndex == secondIndex)
            {
                return;
            };

            if (this.First == null)
            {
                throw new InvalidOperationException("Linked List is empty");
            };

            if ((firstIndex < 0) || (this.Length() <= firstIndex))
            {
                throw new ArgumentOutOfRangeException(nameof(firstIndex), "Index out of bound");
            };

            if ((secondIndex < 0) || (this.Length() <= secondIndex))
            {
                throw new ArgumentOutOfRangeException(nameof(secondIndex), "Index out of bound");
            };

            int currentFirstIndex = 0;
            Node<Type>? previousFirstNode = null;
            Node<Type>? currentFirstNode = this.First;
            while ((currentFirstNode != null) && (currentFirstIndex < firstIndex))
            {
                previousFirstNode = currentFirstNode;
                currentFirstNode = currentFirstNode.Next;
                currentFirstIndex++;
            };

            int currentSecondIndex = 0;
            Node<Type>? previousSecondNode = null;
            Node<Type>? currentSecondNode = this.First;
            while ((currentSecondNode != null) && (currentSecondIndex < secondIndex))
            {
                previousSecondNode = currentSecondNode;
                currentSecondNode = currentSecondNode.Next;
                currentSecondIndex++;
            };

            if (previousFirstNode == null)
            {
                this.First = currentSecondNode;
            }
            else
            {
                previousFirstNode.Next = currentSecondNode;
            };

            if (previousSecondNode == null)
            {
                this.First = currentFirstNode;
            }
            else
            {
                previousSecondNode.Next = currentFirstNode;
            };

            if (currentFirstNode == null || currentSecondNode == null || currentFirstNode.Next == null || currentSecondNode.Next == null)
            {
                throw new InvalidOperationException("Invalid Node");
            };

            Node<Type> temp = currentFirstNode.Next;
            currentFirstNode.Next = currentSecondNode.Next;
            currentSecondNode.Next = temp;
        }

        public void Remove(int index)
        {
            if (this.First == null)
            {
                throw new InvalidOperationException("Linked List is empty");
            };

            if ((index < 0) || (this.Length() <= index))
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of bound");
            };

            if (index == 0)
            {
                this.First = this.First.Next;

                return;
            };

            int currentIndex = 0;
            Node<Type> currentNode = this.First;
            while ((currentNode.Next != null) && (currentIndex < (index - 1)))
            {
                currentNode = currentNode.Next;
                currentIndex++;
            };

            if (currentNode.Next == null)
            {
                throw new InvalidOperationException("Invalid Node");
            };

            currentNode.Next = currentNode.Next.Next;
        }

        public override string ToString()
        {
            string text = "";
            Node<Type>? currentNode = this.First;
            while (currentNode != null)
            {
                text += $"{currentNode.Value} -> ";
                currentNode = currentNode.Next;
            };

            text += "null";

            return text;
        }
    };

    class Program
    {
        public static void Main(string[] args)
        {
            LinkedList<char> linkedList1 = new();
            linkedList1.Add('x');
            linkedList1.Add('y');
            linkedList1.Add('z');

            Console.Write("Show: ");
            linkedList1.Show();
            Console.WriteLine();

            linkedList1.Debug();
            Console.WriteLine();

            Console.WriteLine($"Length: {linkedList1.Length()}");
            Console.WriteLine();

            Console.WriteLine($"First: {linkedList1.First}");
            Console.WriteLine();

            Console.WriteLine($"Get 0: {linkedList1.Get(0)}");
            Console.WriteLine($"Get 1: {linkedList1.Get(1)}");
            Console.WriteLine($"Get 2: {linkedList1.Get(2)}");
            Console.WriteLine();

            Console.Write($"Pop: {linkedList1.Pop()} => ");
            linkedList1.Show();
            Console.WriteLine();

            Console.Write("Insert: ");
            linkedList1.Insert('a', 0);
            linkedList1.Insert('b', 1);
            linkedList1.Insert('c', 2);
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
            Console.WriteLine();
            Console.WriteLine();


            LinkedList<int> linkedList2 = new();
            linkedList2.Add(7);
            linkedList2.Add(8);
            linkedList2.Add(9);

            Console.Write("Show: ");
            linkedList2.Show();
            Console.WriteLine();

            linkedList2.Debug();
            Console.WriteLine();

            Console.WriteLine($"Length: {linkedList2.Length()}");
            Console.WriteLine();

            Console.WriteLine($"First: {linkedList2.First}");
            Console.WriteLine();

            Console.WriteLine($"Get 0: {linkedList2.Get(0)}");
            Console.WriteLine($"Get 1: {linkedList2.Get(1)}");
            Console.WriteLine($"Get 2: {linkedList2.Get(2)}");
            Console.WriteLine();

            Console.Write($"Pop: {linkedList2.Pop()} => ");
            linkedList2.Show();
            Console.WriteLine();

            Console.Write("Insert: ");
            linkedList2.Insert(1, 0);
            linkedList2.Insert(2, 1);
            linkedList2.Insert(3, 2);
            linkedList2.Show();
            Console.WriteLine();

            Console.Write("Remove: ");
            linkedList2.Remove(4);
            linkedList2.Remove(3);
            linkedList2.Remove(2);
            linkedList2.Show();
            Console.WriteLine();

            Console.Write("Swap: ");
            linkedList2.Swap(0, 1);
            linkedList2.Show();
            Console.WriteLine();

            Console.Write("ToString: ");
            Console.WriteLine(linkedList2);
            Console.WriteLine();

            linkedList2.Debug();
            Console.WriteLine();
        }
    };
};
