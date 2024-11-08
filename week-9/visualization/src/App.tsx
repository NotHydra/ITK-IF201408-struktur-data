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

	const createLine = (
		startNodePostion: {
			x: number;
			y: number;
		},
		endNodePostion: {
			x: number;
			y: number;
		}
	): SVGLineElement => {
		const line = document.createElementNS(
			"http://www.w3.org/2000/svg",
			"line"
		);

		line.setAttribute("x1", `${startNodePostion.x}`);
		line.setAttribute("y1", `${startNodePostion.y}`);
		line.setAttribute("x2", `${endNodePostion.x}`);
		line.setAttribute("y2", `${endNodePostion.y}`);
		line.setAttribute("stroke", "black");
		line.setAttribute("stroke-width", "2");

		return line;
	};

	const drawLine = (): void => {
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
							canvasRef.current!.appendChild(
								createLine(
									nodePosition,
									getElementPosition(leftNodeElement)
								)
							);
						}
					}

					if (rightNode) {
						const rightNodeElement = document.getElementById(
							`node-${rightNode.key}`
						);

						if (rightNodeElement) {
							canvasRef.current!.appendChild(
								createLine(
									nodePosition,
									getElementPosition(rightNodeElement)
								)
							);
						}
					}
				});
			}
		);
	};

	useEffect((): void => {
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
		return (): void => {
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
