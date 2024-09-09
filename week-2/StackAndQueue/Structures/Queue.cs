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
            Console.WriteLine("Debug: First");

            DoubleLinkedNode<Type>? currentNode = this.First;
            while (currentNode != null)
            {
                string previousText = (currentNode.Previous != null) ? currentNode.Previous.Value!.ToString()! : "null";
                string nextText = (currentNode.Next != null) ? currentNode.Next.Value!.ToString()! : "null";

                Console.WriteLine($"Debug: {previousText} <- {currentNode.Value} -> {nextText}");

                currentNode = currentNode.Next;
            };

            Console.WriteLine("Debug: Last");
        }

        /// <summary>
        /// Mengecek apakah isi dari Queue dapat dilakukan Dequeue atau tidak
        /// </summary>
        public bool HasDequeue()
        {
            return this.First != null;
        }

        /// <summary>
        /// Mendapatkan dan mengembalikan nilai dari Node yang berada di paling awal Queue tanpa mengeluarkan Node tersebut dari Queue
        /// </summary>
        /// <returns>
        /// Nilai dari Node yang berada di paling awal Queue
        /// </returns>
        public Type? Peek()
        {
            if (First == null)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            return First.Value;
        }

        /// <summary>
        /// Menaruh Node baru dengan Value yang diberikan di paling akhir Queue
        /// </summary>
        /// <param name="value">
        /// Value atau nilai yang akan dimasukkan ke dalam Node baru
        /// </param>
        public void Enqueue(Type value)
        {
            DoubleLinkedNode<Type> newNode = new(value);
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

        /// <summary>
        /// Mengeluarkan Node serta mengembalikan nilai dari Node yang berada di paling awal dari Queue
        /// </summary>
        /// <returns>
        /// Nilai dari Node yang dikeluarkan dari Queue
        /// </returns>
        public Type Dequeue()
        {
            if (this.First == null || this.Last == null)
            {
                throw new InvalidOperationException("Queue is empty");
            };

            Type dequeueValue = this.First.Value;
            if (this.First == this.Last)
            {
                this.First = null;
                this.Last = null;

                return dequeueValue;
            };

            this.First = this.First.Next;
            this.First!.Previous = null;

            return dequeueValue;
        }

        /// <summary>
        /// Menghapus seluruh Node yang ada di dalam Queue
        /// </summary>
        public void Clear()
        {
            this.First = null;
            this.Last = null;
        }

        /// <summary>
        /// Mendapatkan nilai dari Node yang berada di dalam Queue berdasarkan index yang diberikan
        /// </summary>
        /// <param name="index">
        /// Urutan index dari Node yang akan dicari
        /// </param>
        public Type Get(int index)
        {
            if (this.First == null || this.Last == null)
            {
                throw new InvalidCastException("Queue is empty");
            };

            if (index < 0 || (this.Length() <= index))
            {
                throw new IndexOutOfRangeException("Index out of bound");
            };

            int currentIndex;
            DoubleLinkedNode<Type> currentNode;
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

        /// <summary>
        /// Menambahkan Node baru dengan Value yang diberikan ke dalam Queue berdasarkan index yang diberikan
        /// </summary>
        /// <param name="value">
        /// Value atau nilai yang akan dimasukkan ke dalam Node baru
        /// </param>
        /// <param name="index">
        /// Urutan index dari Node yang akan ditambahkan
        /// </param>
        public void Insert(Type value, int index)
        {
            if ((index < 0) || (this.Length() < index))
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of bound");
            };

            DoubleLinkedNode<Type> newNode = new(value);
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
            DoubleLinkedNode<Type> currentNode;
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

        /// <summary>
        /// Menukar posisi Node yang berada di dalam Queue berdasarkan 2 index berbeda yang diberikan
        /// </summary>
        /// <param name="firstIndex">
        /// Urutan index pertama dari Node yang akan ditukar
        /// </param>
        /// <param name="secondIndex">
        /// Urutan index kedua dari Node yang akan ditukar
        /// </param>
        public void Swap(int firstIndex, int secondIndex)
        {
            if (this.First == null || this.Last == null)
            {
                throw new InvalidOperationException("Queue is empty");
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
        /// Menghapus Node yang berada di dalam Queue berdasarkan index yang diberikan
        /// </summary>
        /// <param name="index">
        /// Urutan index dari Node yang akan dihapus
        /// </param>
        public void Remove(int index)
        {
            if (this.First == null || this.Last == null)
            {
                throw new InvalidOperationException("Queue is empty");
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
            DoubleLinkedNode<Type> currentNode;
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

        /// <summary>
        /// Mengembalikan bentuk string dari seluruh Node yang ada di dalam Queue  
        /// </summary>
        public override string ToString()
        {
            string text = "first <- ";
            DoubleLinkedNode<Type>? currentNode = this.First;
            while (currentNode != null)
            {
                text += $"{currentNode.Value} <- ";
                currentNode = currentNode.Next;
            };

            text += "last";

            return text;
        }
    }
}