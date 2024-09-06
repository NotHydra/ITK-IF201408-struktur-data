using StackAndQueue.Nodes;

namespace StackAndQueue.Structures
{
    public class Stack<Type>
    {
        private Node<Type>? _first = null;


        /// <summary>
        /// Getter dan Setter untuk Node yang berada di paling atas Stack
        /// </summary>
        public Node<Type>? First
        {
            get { return this._first; }
            set { this._first = value; }
        }

        /// <summary>
        /// Mendapatkan dan mengembalikan nilai dari total Node yang ada di dalam Stack
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
        /// Menampilkan seluruh Node yang ada di dalam Stack
        /// </summary>
        public void Show()
        {
            Console.WriteLine(this);
        }

        /// <summary>
        /// Menampilkan seluruh Node yang ada di dalam Stack satu per satu beserta dengan Node lain yang terhubung dengan Node tersebut
        /// </summary>
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

        /// <summary>
        /// Mengecek apakah isi dari Stack dapat dilakukan Pop atau tidak
        /// </summary>
        public bool HasPop()
        {
            return this.First != null;
        }

        /// <summary>
        /// Mendapatkan dan mengembalikan nilai dari Node yang berada di paling atas Stack tanpa mengeluarkan Node tersebut dari Stack
        /// </summary>
        /// <returns>
        /// Nilai dari Node yang berada di paling atas Stack
        /// </returns>
        public Type? Peek()
        {
            if (this.First == null)
            {
                throw new InvalidOperationException("Stack is empty");
            };

            return this.First.Value;
        }

        /// <summary>
        /// Menaruh Node baru dengan Value yang diberikan di paling atas Stack
        /// </summary>
        /// <param name="value">
        /// Value atau nilai yang akan dimasukkan ke dalam Node baru
        /// </param>
        public void Push(Type value)
        {
            Node<Type> newNode = new(value);
            if (this.First == null)
            {
                this.First = newNode;

                return;
            };

            newNode.Next = this.First;
            this.First = newNode;
        }

        /// <summary>
        /// Mengeluarkan Node serta mengembalikan nilai dari Node yang berada di paling atas dari Stack
        /// </summary>
        /// <returns>
        /// Nilai dari Node yang dikeluarkan dari Stack
        /// </returns>
        public Type? Pop()
        {
            if (this.First == null)
            {
                throw new InvalidOperationException("Stack is empty");
            };

            Type popValue = this.First.Value;

            this.First = this.First.Next;

            return popValue;
        }

        public void Swap()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mengembalikan bentuk string dari seluruh Node yang ada di dalam Stack
        /// </summary>
        public override string ToString()
        {
            string text = "top -> ";
            Node<Type>? currentNode = this.First;
            while (currentNode != null)
            {
                text += $"{currentNode.Value} -> ";
                currentNode = currentNode.Next;
            };

            text += "bottom";

            return text;
        }
    }
}

