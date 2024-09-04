namespace StackAndQueue
{
    public class Program
    {
        static void Main(string[] args)
        {
            Structures.Stack<int> stack = new Structures.Stack<int>();
            
            stack.Push(1);
            stack.Push(4);
            stack.Push(8);
            
            stack.Show();
            
            Console.WriteLine($"Popped {stack.Pop()?.Value}");
            stack.Show();
            
            Console.WriteLine($"Peek {stack.Peek()?.Value}");
            stack.Show();

            stack.Pop();
            stack.Show();
            Console.WriteLine($"HasPop {stack.HasPop()}");
            
            stack.Pop();
            stack.Show();
            Console.WriteLine($"HasPop {stack.HasPop()}");
            
            stack.Pop();
            stack.Show();
            Console.WriteLine($"HasPop {stack.HasPop()}");
            
            stack.Pop();
            stack.Show();
            Console.WriteLine($"HasPop {stack.HasPop()}");
        }
    }
}