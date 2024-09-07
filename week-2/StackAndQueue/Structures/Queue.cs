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

        public int Length()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }

        public void Debug()
        {
            throw new NotImplementedException();
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

        public void Swap(int firstIndex, int secondIndex)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}