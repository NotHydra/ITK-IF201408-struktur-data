using StackAndQueue.Classes;

namespace StackAndQueue.Structures;

public class Queue<Type>
{
    private DoubleLinkedNode<Type> _first = null;

    public DoubleLinkedNode<Type> First
    {
        get => this._first;
        set => this._first = value;
    }

    public void Push(Type value)
    {
        throw new NotImplementedException();
    }
    
    public Node<Type> Pop()
    {
        throw new NotImplementedException();
    }
}