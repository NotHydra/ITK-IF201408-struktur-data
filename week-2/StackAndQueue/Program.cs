namespace StackAndQueue
{
    public class Program
    {
        static void Main(string[] args)
        {
            Structures.Stack<int> stack = new();
            stack.Push(1);
            stack.Push(4);
            stack.Push(8);

            stack.Show();
            Console.WriteLine();

            stack.Show();
            Console.WriteLine($"Pop: {stack.Pop()}");
            stack.Show();
            Console.WriteLine();

            stack.Show();
            Console.WriteLine($"Peek: {stack.Peek()}");
            stack.Show();
            Console.WriteLine();

            stack.Show();
            Console.WriteLine($"HasPop: {stack.HasPop()}");
            Console.WriteLine($"Pop: {stack.Pop()}");
            stack.Show();
            Console.WriteLine();

            stack.Show();
            Console.WriteLine($"HasPop: {stack.HasPop()}");
            Console.WriteLine($"Pop: {stack.Pop()}");
            stack.Show();
            Console.WriteLine();

            // Akan error, karena Stack sudah kosong
            stack.Show();
            Console.WriteLine($"HasPop: {stack.HasPop()}");
            Console.WriteLine($"Pop: {stack.Pop()}");
            stack.Show();
            Console.WriteLine();
        }
    }
}