namespace Tree
{
    public class Test()
    {
        public static void Tree()
        {
            Structure.Tree<int> tree1 = new();
            Console.WriteLine(tree1.Add(1));
            Console.WriteLine(tree1.Add(4));
            Console.WriteLine(tree1.Add(8));

            Console.WriteLine(tree1.Remove(4));

            Console.WriteLine(tree1.IsExist(4));
        }
    }
}