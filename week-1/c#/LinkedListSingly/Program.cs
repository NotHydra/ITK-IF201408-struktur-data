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

        public void Show()
        {
            Node<Type>? currentNode = this.First;
            while (currentNode != null)
            {
                Console.Write($"{currentNode.Value} -> ");

                currentNode = currentNode.Next;
            }

            Console.WriteLine("end");
        }

        public void Add(Type value)
        {
            Node<Type> newNode = new(value);
            if (this.First == null)
            {
                this.First = newNode;
            }
            else {
                Node<Type> currentNode = this.First;
                while (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                };

                currentNode.Next = newNode;
            }
        }

        public Type Get(int index) {
            int currentIndex = 0;
            Node<Type> currentNode = this.First;
            while(currentIndex < index){
                currentNode = currentNode.Next;
                currentIndex++;
            }
            
            return currentNode.Value;
        }

        public void Insert(Type value, int index){
            Node<Type> newNode = new Node<Type>(value);
            if (index == 0) {
                newNode.Next = this.First;
                this.First = newNode;

                return;
            }

            int currentIndex = 0;
            Node<Type> currentNode = this.First;
            while (currentIndex < (index - 1)){
                currentNode = currentNode.Next;
                currentIndex++;
            }

            newNode.Next = currentNode.Next;
            currentNode.Next = newNode;

        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<char> linkedList1 = new();
            linkedList1.Add('x');
            linkedList1.Add('y');
            linkedList1.Add('z');
            linkedList1.Insert('a', 0);
            linkedList1.Show();

            LinkedList<int> linkedList2 = new();
            linkedList2.Add(7);
            linkedList2.Add(8);
            linkedList2.Add(9);
            linkedList2.Show();

            Console.WriteLine(linkedList1.Get(2));
        }
    }
}
