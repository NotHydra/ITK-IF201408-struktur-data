namespace StackAndQueue.Classes;

public class Node<Type>(Type value)
{
        private Type _value = value;
        private Node<Type>? _next = null;

        public Type Value
        {
            get => this._value;
            set => this._value = value;
        }

        public Node<Type>? Next
        {
            get => this._next;
            set => this._next = value;
        }
}