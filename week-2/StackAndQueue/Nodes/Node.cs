namespace StackAndQueue.Nodes
{
    public class Node<Type>(Type value)
    {
        private readonly Type _value = value;
        private Node<Type>? _next = null;

        public Type Value
        {
            get { return this._value; }
        }

        public Node<Type>? Next
        {
            get { return this._next; }
            set { this._next = value; }
        }
    }
}
