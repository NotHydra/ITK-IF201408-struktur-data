import { useEffect, useRef, useState } from "react";
import { Node, BinarySearchTree } from "./structures/binary-search-tree";

export default function App() {
    const [isShowNullValue, setIsShowNullValue] = useState(false);
    const canvasRef: React.MutableRefObject<SVGSVGElement | null> = useRef<SVGSVGElement | null>(null);
    const [bst, setBST] = useState(new BinarySearchTree<string>());

    const [addValue, setAddValue] = useState("");
    const [removeFromLeftValue, setRemoveFromLeftValue] = useState("");
    const [removeFromRightValue, setRemoveFromRightValue] = useState("");
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
        },
        color: string = "black"
    ): SVGLineElement => {
        const line: SVGLineElement = document.createElementNS("http://www.w3.org/2000/svg", "line");

        line.setAttribute("x1", `${startNodePostion.x}`);
        line.setAttribute("y1", `${startNodePostion.y}`);
        line.setAttribute("x2", `${endNodePostion.x}`);
        line.setAttribute("y2", `${endNodePostion.y}`);
        line.setAttribute("stroke", color);
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
                        canvasRef.current!.appendChild(createLine(nodePosition, getElementPosition(leftNodeElement), "blue"));
                    }
                }

                if (rightNode) {
                    const rightNodeElement: HTMLElement | null = document.getElementById(`node-${rightNode.key}`);
                    if (rightNodeElement) {
                        canvasRef.current!.appendChild(createLine(nodePosition, getElementPosition(rightNodeElement), "green"));
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
                                    setResponseValue(bst.isExist(isExistValue).toString());
                                    setTimestampValue(new Date().toLocaleTimeString("en-US", { timeZone: "Asia/Makassar", hour12: false }));

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
                                    const newBST: BinarySearchTree<string> = new BinarySearchTree<string>();

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
                                if (removeFromLeftValue !== "") {
                                    const newBST: BinarySearchTree<string> = new BinarySearchTree<string>();

                                    Object.assign(newBST, bst);
                                    setResponseValue(newBST.remove(removeFromLeftValue).toString());
                                    setTimestampValue(new Date().toLocaleTimeString("en-US", { timeZone: "Asia/Makassar", hour12: false }));

                                    setBST(newBST);
                                    setRemoveFromLeftValue("");
                                }
                            }}
                        >
                            Remove From Left
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
                            value={removeFromLeftValue}
                            onChange={(e: React.ChangeEvent<HTMLInputElement>): void => setRemoveFromLeftValue(e.target.value)}
                        />
                    </div>

                    <div className="action">
                        <button
                            className="button"
                            onClick={(): void => {
                                if (removeFromRightValue !== "") {
                                    const newBST: BinarySearchTree<string> = new BinarySearchTree<string>();

                                    Object.assign(newBST, bst);
                                    setResponseValue(newBST.remove(removeFromRightValue, false).toString());
                                    setTimestampValue(new Date().toLocaleTimeString("en-US", { timeZone: "Asia/Makassar", hour12: false }));

                                    setBST(newBST);
                                    setRemoveFromRightValue("");
                                }
                            }}
                        >
                            Remove From Right
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
                            value={removeFromRightValue}
                            onChange={(e: React.ChangeEvent<HTMLInputElement>): void => setRemoveFromRightValue(e.target.value)}
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

                    <div className="action">
                        <button
                            className="button"
                            onClick={(): void => {
                                const newBST: BinarySearchTree<string> = new BinarySearchTree<string>();

                                Object.assign(newBST, bst);
                                newBST.addRandomLetters("az");
                                setResponseValue("Finished");
                                setTimestampValue(new Date().toLocaleTimeString("en-US", { timeZone: "Asia/Makassar", hour12: false }));

                                setBST(newBST);
                            }}
                        >
                            Add Random a-z
                        </button>

                        <button
                            className="button"
                            onClick={(): void => {
                                const newBST: BinarySearchTree<string> = new BinarySearchTree<string>();

                                Object.assign(newBST, bst);
                                newBST.addRandomLetters("AZ");
                                setResponseValue("Finished");
                                setTimestampValue(new Date().toLocaleTimeString("en-US", { timeZone: "Asia/Makassar", hour12: false }));

                                setBST(newBST);
                            }}
                        >
                            Add Random A-Z
                        </button>

                        <button
                            className="button"
                            onClick={(): void => {
                                const newBST: BinarySearchTree<string> = new BinarySearchTree<string>();

                                Object.assign(newBST, bst);
                                newBST.addRandomLetters("aZ");
                                setResponseValue("Finished");
                                setTimestampValue(new Date().toLocaleTimeString("en-US", { timeZone: "Asia/Makassar", hour12: false }));

                                setBST(newBST);
                            }}
                        >
                            Add Random a-Z
                        </button>
                    </div>

                    <div className="action">
                        <button
                            className="button"
                            onClick={(): void => {
                                const newBST: BinarySearchTree<string> = new BinarySearchTree<string>();

                                Object.assign(newBST, bst);
                                newBST.clear();
                                setResponseValue("Finished");
                                setTimestampValue(new Date().toLocaleTimeString("en-US", { timeZone: "Asia/Makassar", hour12: false }));

                                setBST(newBST);
                            }}
                        >
                            Clear
                        </button>

                        <button
                            className="button"
                            onClick={(): void => {
                                const newBST: BinarySearchTree<string> = new BinarySearchTree<string>();

                                setIsShowNullValue(!isShowNullValue);
                                setResponseValue("Finished");
                                setTimestampValue(new Date().toLocaleTimeString("en-US", { timeZone: "Asia/Makassar", hour12: false }));

                                setBST(newBST);
                            }}
                        >
                            {isShowNullValue ? "Hide" : "Show"} Null
                        </button>
                    </div>
                </div>

                <div style={{ marginLeft: "16px" }}>
                    <div>
                        Response (<span id="timestamp">{timestampValue}</span>):
                    </div>
                    <div id="response">{responseValue}</div>

                    <div style={{ marginTop: "16px" }}>
                        <div>Blue Line: Arrow To Left Node</div>
                        <div>Green Line: Arrow To Right Node</div>
                    </div>
                </div>
            </div>

            <div>
                {Object.values(bst.getNodesByDepth()).map(
                    (nodes: (Node<string> | string)[], nodesIndex: number): JSX.Element => (
                        <div key={nodesIndex} className="nodes">
                            {nodes.map((node: Node<string> | string, nodeIndex: number): JSX.Element => {
                                let nodeType: string;
                                let nodeValue: string;

                                if (node === "empty" || (node === "null" && isShowNullValue === false)) {
                                    nodeType = "empty";
                                    nodeValue = "";
                                } else if (node === "null") {
                                    nodeType = "null";
                                    nodeValue = "*";
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
