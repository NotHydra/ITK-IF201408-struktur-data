import { BinarySearchTree } from "./structures/binary-search-tree";

export default function App() {
	const bst1: BinarySearchTree<string> = new BinarySearchTree<string>();
	bst1.add("C");
	bst1.add("H");
	bst1.add("A");
	bst1.add("N");
	bst1.add("D");
	bst1.add("R");
	bst1.add("A");

	console.log(bst1.isExist("A"));
	console.log(bst1.isExist("B"));
	console.log(bst1.preOder());
	console.log(bst1.inOrder());
	console.log(bst1.postOrder());
	console.log(bst1.toString());

	bst1.show();
	bst1.remove("D");
	bst1.show();

	return <>Test</>;
}
