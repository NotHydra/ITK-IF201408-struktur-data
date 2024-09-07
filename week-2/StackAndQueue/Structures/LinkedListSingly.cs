using StackAndQueue.Nodes;

namespace StackAndQueue.Structures
{
    class LinkedListSingly<Type>
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
                string nextText = (currentNode.Next != null) ? currentNode.Next.Value!.ToString()! : "null";

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

            Type popValue;
            if (this.First.Next == null)
            {
                popValue = this.First.Value;
                this.First = null;

                return popValue;
            };

            Node<Type> currentNode = this.First;
            while ((currentNode.Next != null) && (currentNode.Next.Next != null))
            {
                currentNode = currentNode.Next;
            };

            popValue = currentNode.Next!.Value;
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
            while (currentIndex < index)
            {
                currentNode = currentNode.Next!;
                currentIndex++;
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
            if ((this.First == null) || (index == 0))
            {
                newNode.Next = this.First;
                this.First = newNode;

                return;
            };

            int currentIndex = 0;
            Node<Type> currentNode = this.First;
            while (currentIndex < (index - 1))
            {
                currentNode = currentNode.Next!;
                currentIndex++;
            };

            newNode.Next = currentNode.Next;
            currentNode.Next = newNode;
        }

        public void Swap(int firstIndex, int secondIndex)
        {
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

            if (firstIndex == secondIndex)
            {
                return;
            };

            int currentFirstIndex = 0;
            Node<Type>? previousFirstNode = null;
            Node<Type> currentFirstNode = this.First;
            while (currentFirstIndex < firstIndex)
            {
                previousFirstNode = currentFirstNode;
                currentFirstNode = currentFirstNode.Next!;
                currentFirstIndex++;
            };

            int currentSecondIndex = 0;
            Node<Type>? previousSecondNode = null;
            Node<Type> currentSecondNode = this.First;
            while (currentSecondIndex < secondIndex)
            {
                previousSecondNode = currentSecondNode;
                currentSecondNode = currentSecondNode.Next!;
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

            Node<Type> temp = currentFirstNode.Next!;
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
            while (currentIndex < (index - 1))
            {
                currentNode = currentNode.Next!;
                currentIndex++;
            };

            currentNode.Next = currentNode.Next!.Next;
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
}