using System.Collections.Generic;

namespace Reference
{


    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> linkedList1 = new();
            linkedList1.AddLast(1);
            Console.WriteLine(linkedList1.Count);
            linkedList1.RemoveLast();
            Console.WriteLine(linkedList1.Count);
        }
    }
}