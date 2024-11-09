import { useEffect, useRef, useState } from "react";
import { Node, BinarySearchTree } from "./structures/binary-search-tree";

export default function App() {
    const canvasRef: React.MutableRefObject<SVGSVGElement | null> = useRef<SVGSVGElement | null>(null);
    const [bst, setBST] = useState(new BinarySearchTree<string>());

    const [addValue, setAddValue] = useState("");
    const [removeValue, setRemoveValue] = useState("");
    const [isExistValue, setIsExistValue] = useState("");

    const [responseValue, setResponseValue] = useState("None");
    const [timestampValue, setTimestampValue] = useState(new Date().toLocaleTimeString("en-US", { timeZone: "Asia/Makassar", hour12: false }));

    const getElementPosition = (
        element: HTMLElement | null
    ): {
        x: number;
        y: number;
    } => {
        if (!element) {
            return { x: 0, y: 0 };
        }

        const rect: DOMRect = element.getBoundingClientRect();

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
        const line: SVGLineElement = document.createElementNS("http://www.w3.org/2000/svg", "line");

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

        Object.values(bst.getNodesByDepth()).forEach((nodes: (Node<string> | string)[]) => {
            nodes.forEach((node: Node<string> | string) => {
                if (node === "null" || node === "empty") {
                    return;
                }

                const nodeElement: HTMLElement | null = document.getElementById(`node-${(node as Node<string>).key}`);
                if (nodeElement === null) {
                    return;
                }

                const nodePosition: {
                    x: number;
                    y: number;
                } = getElementPosition(nodeElement);

                const leftNode: Node<string> | null = (node as Node<string>).left;
                const rightNode: Node<string> | null = (node as Node<string>).right;

                if (leftNode) {
                    const leftNodeElement: HTMLElement | null = document.getElementById(`node-${leftNode.key}`);
                    if (leftNodeElement) {
                        canvasRef.current!.appendChild(createLine(nodePosition, getElementPosition(leftNodeElement)));
                    }
                }

                if (rightNode) {
                    const rightNodeElement: HTMLElement | null = document.getElementById(`node-${rightNode.key}`);
                    if (rightNodeElement) {
                        canvasRef.current!.appendChild(createLine(nodePosition, getElementPosition(rightNodeElement)));
                    }
                }
            });
        });
    };

    useEffect((): void => {
        const newBST: BinarySearchTree<string> = new BinarySearchTree<string>();

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
            <div style={{ display: "flex" }}>
                <div>
                    <div className="action">
                        <button
                            className="button"
                            onClick={(): void => {
                                if (isExistValue !== "") {
                                    const newBST = new BinarySearchTree<string>();

                                    Object.assign(newBST, bst);
                                    setResponseValue(newBST.isExist(isExistValue).toString());
                                    setTimestampValue(new Date().toLocaleTimeString("en-US", { timeZone: "Asia/Makassar", hour12: false }));

                                    setBST(newBST);
                                    setIsExistValue("");
                                }
                            }}
                        >
                            Is Exist
                        </button>

                        <input
                            className="input"
                            type="text"
                            onKeyDown={(e: React.KeyboardEvent<HTMLInputElement>): void => {
                                if (!/[a-z]/i.test(e.key)) {
                                    e.preventDefault();
                                }
                            }}
                            maxLength={1}
                            value={isExistValue}
                            onChange={(e: React.ChangeEvent<HTMLInputElement>): void => setIsExistValue(e.target.value)}
                        />
                    </div>

                    <div className="action">
                        <button
                            className="button"
                            onClick={(): void => {
                                if (addValue !== "") {
                                    const newBST = new BinarySearchTree<string>();

                                    Object.assign(newBST, bst);
                                    setResponseValue(newBST.add(addValue).toString());
                                    setTimestampValue(new Date().toLocaleTimeString("en-US", { timeZone: "Asia/Makassar", hour12: false }));

                                    setBST(newBST);
                                    setAddValue("");
                                }
                            }}
                        >
                            Add
                        </button>

                        <input
                            className="input"
                            type="text"
                            onKeyDown={(e: React.KeyboardEvent<HTMLInputElement>): void => {
                                if (!/[a-z]/i.test(e.key)) {
                                    e.preventDefault();
                                }
                            }}
                            maxLength={1}
                            value={addValue}
                            onChange={(e: React.ChangeEvent<HTMLInputElement>): void => setAddValue(e.target.value)}
                        />
                    </div>

                    <div className="action">
                        <button
                            className="button"
                            onClick={(): void => {
                                if (removeValue !== "") {
                                    const newBST = new BinarySearchTree<string>();

                                    Object.assign(newBST, bst);
                                    setResponseValue(newBST.remove(removeValue).toString());
                                    setTimestampValue(new Date().toLocaleTimeString("en-US", { timeZone: "Asia/Makassar", hour12: false }));

                                    setBST(newBST);
                                    setRemoveValue("");
                                }
                            }}
                        >
                            Remove
                        </button>

                        <input
                            className="input"
                            type="text"
                            onKeyDown={(e: React.KeyboardEvent<HTMLInputElement>): void => {
                                if (!/[a-z]/i.test(e.key)) {
                                    e.preventDefault();
                                }
                            }}
                            maxLength={1}
                            value={removeValue}
                            onChange={(e: React.ChangeEvent<HTMLInputElement>): void => setRemoveValue(e.target.value)}
                        />
                    </div>

                    <div className="action">
                        <button
                            className="button"
                            onClick={(): void => {
                                setResponseValue(bst.preOrder());
                                setTimestampValue(new Date().toLocaleTimeString("en-US", { timeZone: "Asia/Makassar", hour12: false }));
                            }}
                        >
                            Pre Order
                        </button>

                        <button
                            className="button"
                            onClick={(): void => {
                                setResponseValue(bst.inOrder());
                                setTimestampValue(new Date().toLocaleTimeString("en-US", { timeZone: "Asia/Makassar", hour12: false }));
                            }}
                        >
                            In Order
                        </button>

                        <button
                            className="button"
                            onClick={(): void => {
                                setResponseValue(bst.postOrder());
                                setTimestampValue(new Date().toLocaleTimeString("en-US", { timeZone: "Asia/Makassar", hour12: false }));
                            }}
                        >
                            Post Order
                        </button>
                    </div>
                </div>

                <div style={{ marginLeft: "16px" }}>
                    <div>
                        Response (<span id="timestamp">{timestampValue}</span>):
                    </div>
                    <div id="response">{responseValue}</div>
                </div>
            </div>

            <div>
                {Object.values(bst.getNodesByDepth()).map(
                    (nodes: (Node<string> | string)[], nodesIndex: number): JSX.Element => (
                        <div key={nodesIndex} className="nodes">
                            {nodes.map((node: Node<string> | string, nodeIndex: number): JSX.Element => {
                                let nodeType: string;
                                let nodeValue: string;

                                // if (node === "null") {
                                // 	nodeType = "null";
                                // 	nodeValue = "null";
                                // } else if (node === "empty") {

                                if (node === "null" || node === "empty") {
                                    nodeType = "empty";
                                    nodeValue = "";
                                } else {
                                    nodeType = "node";
                                    nodeValue = (node as Node<string>).key;
                                }

                                return (
                                    <span key={nodeIndex} id={`node-${nodeValue}`} className={`${nodeType === "empty" ? "empty" : "node"}`}>
                                        {nodeValue}
                                    </span>
                                );
                            })}
                        </div>
                    )
                )}

                <svg ref={canvasRef}></svg>
            </div>
        </>
    );
}
