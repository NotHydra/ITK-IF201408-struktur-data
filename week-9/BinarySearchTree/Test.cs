namespace BinarySearchTree
{
    public class Test()
    {
        public static void BinarySearchTree()
        {
            Structure.BinarySearchTree<char> tree1 = new();
            tree1.Add('R');
            tree1.Add('I');
            tree1.Add('Z');
            tree1.Add('K');
            tree1.Add('Y');
            tree1.Add('I');
            tree1.Add('R');
            tree1.Add('S');
            tree1.Add('W');
            tree1.Add('A');
            tree1.Add('N');
            tree1.Add('D');
            tree1.Add('R');
            tree1.Add('A');
            tree1.Add('R');
            tree1.Add('A');
            tree1.Add('M');
            tree1.Add('A');
            tree1.Add('D');
            tree1.Add('H');
            tree1.Add('A');
            tree1.Add('N');
            tree1.Add('A');

            Console.WriteLine(tree1);
        }
    }
}