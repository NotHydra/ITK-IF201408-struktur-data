using StackAndQueue.Classes;

namespace StackAndQueue.Structures;

public class Stack<Type>
{
        private Node<Type>? _head = null;

        public void Show()
        {
            Node<Type>? currentNode = this._head;

            while (currentNode != null)
            {
                Console.Write($"{currentNode.Value} -> ");
                
                currentNode = currentNode.Next;
            }
            
            Console.WriteLine("end");
        }
        
        /// <summary>
        /// "Menaruh" value baru di atas stack
        /// </summary>
        /// <param name="value">
        /// Value yang ditumpuk
        /// </param>
        public void Push(Type value)
        {
            Node<Type> newNode = new Node<Type>(value);

            if (this._head == null)
            {
                this._head = newNode;
                return;
            }
            
            newNode.Next = this._head;
            this._head = newNode;
        }

        /// <summary>
        /// Mengeluarkan node paling atas dari tumpukan / stack
        /// </summary>
        /// <returns>
        /// Nilai dari node yang dikeluarkan
        /// </returns>
        public Node<Type>? Pop()
        {
            if (this._head == null)
            {
                return null;
            }
            
            Node<Type> topNode = this._head;
            
            this._head = this._head.Next;

            return topNode;
        }

        public Node<Type>? Peek()
        {
            Node<Type>? topNode = this.Pop();
            
            if (topNode != null)
            {
                this.Push(topNode.Value);
            }
            
            return topNode;
        }

        public void Swap()
        {
            throw new NotImplementedException();
        }

        public bool HasPop()
        {
            return this._head != null;
        }
}