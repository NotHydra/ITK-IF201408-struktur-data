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

            // => (Line 26-31) Melakukan perulangan serta increment pada variable count hingga mendapatkan node paling bawah
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
            // => (Line 42) Menampilkan versi string dari objek dengan menggunakan ToString()
            Console.WriteLine(this);
        }

        /// <summary>
        /// Menampilkan seluruh Node yang ada di dalam Stack satu per satu beserta dengan Node lain yang terhubung dengan Node tersebut
        /// </summary>
        public void Debug()
        {
            Console.WriteLine("Debug: Top");

            // => (Line 53-61) Melakukan perulangan untuk menampilkan setiap node beserta hubungannya dengan node lain di dalam stack
            Node<Type>? currentNode = this.First;
            while (currentNode != null)
            {
                string nextText = (currentNode.Next != null) ? currentNode.Next.Value!.ToString()! : "null";

                Console.WriteLine($"Debug: {currentNode.Value} -> {nextText}");

                currentNode = currentNode.Next;
            };

            Console.WriteLine("Debug: Bottom");
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

        /// <summary>
        /// Menghapus seluruh Node yang ada di dalam Stack
        /// </summary>
        public void Clear()
        {
            // => (Line 136-139) Melakukan perulangan untuk mengeluarkan setiap node yang ada di dalam stack hingga HasPop() bernilai false
            while (this.HasPop())
            {
                this.Pop();
            };
        }

        /// <summary>
        /// Mendapatkan nilai dari Node yang berada di dalam Stack berdasarkan index yang diberikan
        /// </summary>
        /// <param name="index">
        /// Urutan index dari Node yang akan dicari
        /// </param>
        public Type Get(int index)
        {
            if (this.First == null)
            {
                throw new InvalidOperationException("Stack is empty");
            };

            if ((index < 0) || (this.Length() <= index))
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of bound");
            };


            // => (Line 162-168) Melakukan perulangan hingga node yang dicari berdasarkan index yang diberikan
            int currentIndex = 0;
            Node<Type> currentNode = this.First;
            while (currentIndex < index)
            {
                currentNode = currentNode.Next!;
                currentIndex++;
            };

            return currentNode.Value;
        }

        /// <summary>
        ///  Menambahkan Node baru dengan Value yang diberikan di dalam Stack dengan urutan berdasarkan index yang diberikan
        /// </summary>
        /// <param name="value">
        /// Value atau nilai yang akan dimasukkan ke dalam Node baru
        /// </param>
        /// <param name="index">
        /// Urutan index dari Node yang akan ditambahkan
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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

            // => (Line 200-206) Melakukan perulangan hingga 1 node sebelum node yang dicari berdasarkan index yang diberikan
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

        /// <summary>
        /// Menukar posisi Node yang berada di dalam Stack berdasarkan 2 index berbeda yang diberikan
        /// </summary>
        /// <param name="firstIndex">
        /// Urutan index pertama dari Node yang akan ditukar
        /// </param>
        /// <param name="secondIndex">
        /// Urutan index kedua dari Node yang akan ditukar
        /// </param>
        public void Swap(int firstIndex, int secondIndex)
        {
            if (this.First == null)
            {
                throw new InvalidOperationException("Stack is empty");
            };

            if (this.Length() == 1)
            {
                throw new InvalidOperationException("Stack has only one element");
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

            // => (Line 249-257) Melakukan perulangan hingga node yang dicari berdasarkan index yang diberikan
            int currentFirstIndex = 0;
            Node<Type>? previousFirstNode = null;
            Node<Type> currentFirstNode = this.First;
            while (currentFirstIndex < firstIndex)
            {
                previousFirstNode = currentFirstNode;
                currentFirstNode = currentFirstNode.Next!;
                currentFirstIndex++;
            };

            // => (Line 260-268) Melakukan perulangan hingga node yang dicari berdasarkan index yang diberikan
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

        /// <summary>
        /// Menghapus Node yang berada di dalam Stack berdasarkan index yang diberikan
        /// </summary>
        /// <param name="index">
        /// Urutan index dari Node yang akan dihapus
        /// </param>
        public void Remove(int index)
        {
            if (this.First == null)
            {
                throw new InvalidOperationException("Stack is empty");
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

            // => (Line 319-325) Melakukan perulangan hingga 1 node sebelum node yang dicari berdasarkan index yang diberikan
            int currentIndex = 0;
            Node<Type> currentNode = this.First;
            while (currentIndex < (index - 1))
            {
                currentNode = currentNode.Next!;
                currentIndex++;
            };

            currentNode.Next = currentNode.Next!.Next;
        }

        /// <summary>
        /// Mengembalikan bentuk string dari seluruh Node yang ada di dalam Stack
        /// </summary>
        public override string ToString()
        {
            string text = "top -> ";

            // => (Line 338-343) Melakukan perulangan untuk menambahkan bentuk string dari setaip node yang ada di dalam stack
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

