import { useEffect, useState } from "react";
import { Node, BinarySearchTree } from "./structures/binary-search-tree";

export default function App() {
	const [bst, setBST] = useState(new BinarySearchTree<string>());

	useEffect(() => {
		const newBST = new BinarySearchTree<string>();
		newBST.add("C");
		newBST.add("H");
		newBST.add("A");
		newBST.add("N");
		newBST.add("D");
		newBST.add("R");
		newBST.add("A");
		newBST.add("S");
		newBST.add("U");
		newBST.add("T");
		newBST.add("V");

		setBST(newBST);
	}, []);

	return (
		<>
			{Object.values(bst.getNodesByDepth()).map(
				(
					nodes: (Node<string> | string)[],
					nodesIndex: number
				): JSX.Element => (
					<div key={nodesIndex} className="nodes">
						{nodes.map(
							(
								node: Node<string> | string,
								nodeIndex: number
							): JSX.Element => {
								let nodeType: string;
								let nodeValue: string;
								if (node === "null") {
									nodeType = "null";
									nodeValue = "null";
								} else if (node === "empty") {
									nodeType = "empty";
									nodeValue = "";
								} else {
									nodeType = "node";
									nodeValue = (node as Node<string>).key;
								}

								return (
									<span
										key={nodeIndex}
										className={`${
											nodeType === "empty"
												? "empty"
												: "node"
										}`}
									>
										{nodeValue}
									</span>
								);
							}
						)}
					</div>
				)
			)}
		</>
	);
}
