using StackAndQueue.Nodes;

namespace StackAndQueue.Structures
{
    public class Queue<Type>
    {
        private DoubleLinkedNode<Type>? _first = null;
        private DoubleLinkedNode<Type>? _last = null;

        /// <summary>
        /// Getter dan Setter untuk Node yang berada di paling awal Queue
        /// </summary>
        public DoubleLinkedNode<Type>? First
        {
            get { return this._first; }
            set { this._first = value; }
        }

        /// <summary>
        /// Getter dan Setter untuk Node yang berada di paling akhir Queue
        /// </summary>
        public DoubleLinkedNode<Type>? Last
        {
            get { return this._last; }
            set { this._last = value; }
        }

        /// <summary>
        /// Mendapatkan dan mengembalikan nilai dari total Node yang ada di dalam Queue
        /// </summary>
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

        /// <summary>
        /// Menampilkan seluruh Node yang ada di dalam Queue
        /// </summary>
        public void Show()
        {
            Console.WriteLine(this);
        }

        /// <summary>
        /// Menampilkan seluruh Node yang ada di dalam Queue satu per satu beserta dengan Node lain yang terhubung dengan Node tersebut
        /// </summary>
        public void Debug()
        {
            Console.WriteLine("Debug: Top");

            Node<Type>? currentNode = this.First;
            while (currentNode != null)
            {
                string nextText = (currentNode.Next != null) ? currentNode.Next.Value!.ToString()! : "null";

                Console.WriteLine($"Debug: {currentNode.Value} -> {nextText}");

                currentNode = currentNode.Next;
            };

            Console.WriteLine("Debug: Bottom");
        }

        public bool HasPop()
        {
            throw new NotImplementedException();
        }

        public Type? Peek()
        {
            throw new NotImplementedException();
        }

        public void Push(Type value)
        {
            throw new NotImplementedException();
        }

        public Type Pop()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Swap(int firstIndex, int secondIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mengembalikan bentuk string dari seluruh Node yang ada di dalam Queue  
        /// </summary>
        public override string ToString()
        {
            string text = "first -> ";
            Node<Type>? currentNode = this.First;
            while (currentNode != null)
            {
                text += $"{currentNode.Value} -> ";
                currentNode = currentNode.Next;
            };

            text += "last";

            return text;
        }
    }
}