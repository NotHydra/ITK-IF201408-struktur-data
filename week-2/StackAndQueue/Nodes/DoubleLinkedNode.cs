namespace StackAndQueue.Nodes
{
    public class DoubleLinkedNode<Type>(Type value)
    {
        private readonly Type _value = value;
        private DoubleLinkedNode<Type>? _next = null;
        private DoubleLinkedNode<Type>? _previous = null;

        public Type Value
        {
            get { return this._value; }
        }

        public DoubleLinkedNode<Type>? Next
        {
            get { return this._next; }
            set { this._next = value; }
        }

        public DoubleLinkedNode<Type>? Previous
        {
            get { return this._previous; }
            set { this._previous = value; }
        }
    }
}