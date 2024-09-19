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
            for (int i = 0; i < amount; i++)
            {
                this.Add((Type)(object)random.Next(minimum, maximum + 1));
            };
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