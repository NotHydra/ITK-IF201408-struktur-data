namespace StackAndQueue.Classes;

public class DoubleLinkedNode<Type>(Type value): Node<Type>(value)
{
    private Node<Type>? _prev = null;
    
    public Node<Type>? Prev
    {
        get => this._prev;
        set => this._prev = value;
    }
}