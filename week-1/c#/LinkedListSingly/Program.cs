namespace LinkedListSingly
{
    class Node<Type>(Type value)
    {
        private readonly Type value = value;
        private Node<Type>? next = null;

        public Type Value
        {
            get { return this.value; }
        }

        public Node<Type>? Next
        {
            get { return this.next; }
            set { this.next = value; }
        }
    };

    class LinkedList<Type>
    {
        private Node<Type>? first = null;

        public Node<Type>? First
        {
            get { return this.first; }
            set { this.first = value; }
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
            Node<Type>? currentNode = this.First;
            while (currentNode != null)
            {
                Console.Write($"{currentNode.Value} -> ");

                currentNode = currentNode.Next;
            };

            Console.WriteLine("end");
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

        public void Pop()
        {
            if (this.First == null)
            {
                return;
            };

            if (this.First.Next == null)
            {
                this.First = null;

                return;
            };

            Node<Type> currentNode = this.First;
            while (currentNode.Next != null && currentNode.Next.Next != null)
            {
                currentNode = currentNode.Next;
            };

            currentNode.Next = null;
        }

        public Type Get(int index)
        {
            if (this.First == null || this.Length() <= index)
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
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of bound");
            };

            Node<Type> newNode = new(value);
            if (this.First == null || index == 0)
            {
                newNode.Next = this.First;
                this.First = newNode;

                return;
            };

            if (this.Length() <= index)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of bound");
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

        public void Remove(int index){
            if (this.First == null)
            {
                return;
            };

            if (index < 0){
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of bound");
            };

            if (this.First == null || index == 0){
                this.First = this.First?.Next;

                return;
            }

            if (this.Length() <= index){
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of bound");
            };

            int currentIndex = 0;
            Node<Type> currentNode = this.First;
            while ((currentNode.Next != null) && (currentIndex < (index - 1))){
                currentNode = currentNode.Next;
                currentIndex++;
            };

            currentNode.Next = currentNode.Next?.Next;

            return;
        }


    };

    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<char> linkedList1 = new();
            linkedList1.Add('x');
            linkedList1.Add('y');
            linkedList1.Add('z');

            linkedList1.Show();
            Console.WriteLine($"Length: {linkedList1.Length()}");
            Console.WriteLine($"First: {linkedList1.First}");
            Console.WriteLine($"Get 1: {linkedList1.Get(1)}");

            linkedList1.Pop();
            linkedList1.Show();
            Console.WriteLine();

            linkedList1.Remove(1);
            linkedList1.Show();
            Console.WriteLine();

            LinkedList<int> linkedList2 = new();
            linkedList2.Add(7);
            linkedList2.Add(8);
            linkedList2.Add(9);

            linkedList2.Show();
            Console.WriteLine($"Length: {linkedList2.Length()}");
            Console.WriteLine($"First: {linkedList2.First}");
            Console.WriteLine($"Get 1: {linkedList2.Get(1)}");

            linkedList2.Pop();
            linkedList2.Show();
            Console.WriteLine();
        }
    };
};
