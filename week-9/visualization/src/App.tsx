import { useEffect, useRef, useState } from "react";
import { Node, BinarySearchTree } from "./structures/binary-search-tree";

export default function App() {
	const canvasRef = useRef<SVGSVGElement | null>(null);
	const [bst, setBST] = useState(new BinarySearchTree<string>());

	const getElementPosition = (
		element: HTMLElement | null
	): {
		x: number;
		y: number;
	} => {
		if (!element) {
			return { x: 0, y: 0 };
		}

		const rect = element.getBoundingClientRect();
		return {
			x: rect.left + rect.width / 2,
			y: rect.top + rect.height / 2,
		};
	};

	const drawLine = () => {
		if (canvasRef.current === null) {
			return;
		}

		canvasRef.current.innerHTML = "";

		Object.values(bst.getNodesByDepth()).forEach(
			(nodes: (Node<string> | string)[]) => {
				nodes.forEach((node: Node<string> | string) => {
					if (node === "null" || node === "empty") {
						return;
					}

					const nodeElement = document.getElementById(
						`node-${(node as Node<string>).key}`
					);

					if (nodeElement === null) {
						return;
					}

					const nodePosition = getElementPosition(nodeElement);

					const leftNode = (node as Node<string>).left;
					const rightNode = (node as Node<string>).right;

					if (leftNode) {
						const leftNodeElement = document.getElementById(
							`node-${leftNode.key}`
						);

						if (leftNodeElement) {
							const leftChildPosition =
								getElementPosition(leftNodeElement);

							const line = document.createElementNS(
								"http://www.w3.org/2000/svg",
								"line"
							);
							line.setAttribute("x1", `${nodePosition.x}`);
							line.setAttribute("y1", `${nodePosition.y}`);
							line.setAttribute("x2", `${leftChildPosition.x}`);
							line.setAttribute("y2", `${leftChildPosition.y}`);
							line.setAttribute("stroke", "black");
							line.setAttribute("stroke-width", "2");

							canvasRef.current!.appendChild(line);
						}
					}

					if (rightNode) {
						const rightNodeElement = document.getElementById(
							`node-${rightNode.key}`
						);

						if (rightNodeElement) {
							const rightChildPosition =
								getElementPosition(rightNodeElement);

							const line = document.createElementNS(
								"http://www.w3.org/2000/svg",
								"line"
							);
							line.setAttribute("x1", `${nodePosition.x}`);
							line.setAttribute("y1", `${nodePosition.y}`);
							line.setAttribute("x2", `${rightChildPosition.x}`);
							line.setAttribute("y2", `${rightChildPosition.y}`);
							line.setAttribute("stroke", "black");
							line.setAttribute("stroke-width", "2");

							canvasRef.current!.appendChild(line);
						}
					}
				});
			}
		);
	};

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

	useEffect(() => {
		drawLine();

		window.addEventListener("resize", drawLine);
		return () => {
			window.removeEventListener("resize", drawLine);
		};
	}, [bst]);

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

								// if (node === "null") {
								// 	nodeType = "null";
								// 	nodeValue = "null";
								// }

								if (node === "null" || node === "empty") {
									nodeType = "empty";
									nodeValue = "";
								} else {
									nodeType = "node";
									nodeValue = (node as Node<string>).key;
								}

								return (
									<span
										key={nodeIndex}
										id={`node-${nodeValue}`}
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

			<svg ref={canvasRef}></svg>
		</>
	);
}
