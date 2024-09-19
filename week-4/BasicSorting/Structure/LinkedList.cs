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

        private int Length()
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

            int currentIndex = 0;
            Node<Type>? currentNode = this.First;
            while (currentNode != null)
            {
                string previousText = (currentNode.Previous != null) ? currentNode.Previous.Value!.ToString()! : "null";
                string nextText = (currentNode.Next != null) ? currentNode.Next.Value!.ToString()! : "null";

                Console.WriteLine($"Debug {currentIndex + 1}: {previousText} <- {currentNode.Value} -> {nextText}");

                currentIndex += 1;
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

        private void Swap(int firstIndex, int secondIndex)
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
            int currentIndex;
            Node<Type>? currentNode;
            do
            {
                swapped = false;

                currentIndex = 0;
                currentNode = this.First;
                while (currentNode != null && currentNode.Next != null)
                {
                    if (((int)(object)currentNode.Value!) > ((int)(object)currentNode.Next.Value!))
                    {
                        this.Swap(currentIndex, currentIndex + 1);

                        swapped = true;
                    };

                    currentIndex++;
                    currentNode = currentNode.Next;
                };
            } while (swapped);

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