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
            DoubleLinkedNode<Type>? currentNode = this.First;
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

            DoubleLinkedNode<Type>? currentNode = this.First;
            while (currentNode != null)
            {
                string nextText = (currentNode.Next != null) ? currentNode.Next.Value!.ToString()! : "null";

                Console.WriteLine($"Debug: {currentNode.Value} -> {nextText}");

                currentNode = currentNode.Next;
            };

            Console.WriteLine("Debug: Bottom");
        }

        /// <summary>
        /// Mengecek apakah isi dari Queue dapat dilakukan Pop atau tidak
        /// </summary>
        public bool HasPop()
        {
            if (this._first != null)
            {
                return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Mendapatkan dan mengembalikan nilai dari Node yang berada di paling awal Queue tanpa mengeluarkan Node tersebut dari Queue
        /// </summary>
        /// <returns>
        /// Nilai dari Node yang berada di paling awal Queue
        /// </returns>
        public Type? Peek()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Menaruh Node baru dengan Value yang diberikan di paling awal Queue
        /// </summary>
        /// <param name="value">
        /// Value atau nilai yang akan dimasukkan ke dalam Node baru
        /// </param>
        public void Push(Type value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mengeluarkan Node serta mengembalikan nilai dari Node yang berada di paling awal dari Queue
        /// </summary>
        /// <returns>
        /// Nilai dari Node yang dikeluarkan dari Queue
        /// </returns>
        public Type Pop()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Menghapus seluruh Node yang ada di dalam Queue
        /// </summary>
        public void Clear()
        {
            this._first = null;
            this._last = null;
        }

        /// <summary>
        /// Menukar posisi Node yang berada di dalam Queue berdasarkan 2 index berbeda yang diberikan
        /// </summary>
        /// <param name="firstIndex">
        /// Nilai index pertama dari Node yang akan ditukar
        /// </param>
        /// <param name="secondIndex">
        /// Nilai index kedua dari Node yang akan ditukar
        /// </param>
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
            DoubleLinkedNode<Type> currentFirstNode;
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
            DoubleLinkedNode<Type> currentSecondNode;
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

            DoubleLinkedNode<Type> temp = currentFirstNode.Next!;
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

        /// <summary>
        /// Mengembalikan bentuk string dari seluruh Node yang ada di dalam Queue  
        /// </summary>
        public override string ToString()
        {
            string text = "first -> ";
            DoubleLinkedNode<Type>? currentNode = this.First;
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