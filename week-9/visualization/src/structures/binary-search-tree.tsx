export interface NodesByDepthInterface<KeyType> {
    [depth: number]: (Node<KeyType> | string)[];
}

export class Node<KeyType> {
    private readonly _key: KeyType;
    private _left: Node<KeyType> | null = null;
    private _right: Node<KeyType> | null = null;

    public constructor(key: KeyType) {
        this._key = key;
    }

    public get key(): KeyType {
        return this._key;
    }

    public get left(): Node<KeyType> | null {
        return this._left;
    }

    public set left(node: Node<KeyType> | null) {
        this._left = node;
    }

    public get right(): Node<KeyType> | null {
        return this._right;
    }

    public set right(node: Node<KeyType> | null) {
        this._right = node;
    }
}

export class BinarySearchTree<KeyType> {
    private _root: Node<KeyType> | null = null;

    public get root(): Node<KeyType> | null {
        return this._root;
    }

    public set root(node: Node<KeyType> | null) {
        this._root = node;
    }

    public show(): void {
        console.log(this.toString());
    }

    public getNodesByDepth(): NodesByDepthInterface<KeyType> {
        const nodesByDepthDict: NodesByDepthInterface<KeyType> = {};

        let currentDepth: number = 0;
        nodesByDepthDict[0] = [this.root === null ? "null" : this.root];
        while (true) {
            if (
                nodesByDepthDict[currentDepth].every((node: Node<KeyType> | string) => {
                    return node === "null" || node === "empty";
                })
            ) {
                break;
            }

            const newNodesByDepthList: (Node<KeyType> | string)[] = [];
            nodesByDepthDict[currentDepth].forEach((node: Node<KeyType> | string) => {
                if (node === "null" || node === "empty") {
                    newNodesByDepthList.push("empty");
                    newNodesByDepthList.push("empty");
                } else {
                    newNodesByDepthList.push(typeof node === "string" || node.left === null ? "null" : node.left);

                    newNodesByDepthList.push(typeof node === "string" || node.right === null ? "null" : node.right);
                }
            });

            currentDepth += 1;
            nodesByDepthDict[currentDepth] = newNodesByDepthList;
        }

        return nodesByDepthDict;
    }

    public isExist(key: KeyType): boolean {
        return this.isExistRecursively(this.root, key);
    }

    private isExistRecursively(currentNode: Node<KeyType> | null, key: KeyType): boolean {
        if (currentNode === null) {
            return false;
        }

        if (key === currentNode.key) {
            return true;
        }

        if (key < currentNode.key) {
            return this.isExistRecursively(currentNode.left, key);
        }

        return this.isExistRecursively(currentNode.right, key);
    }

    public add(key: KeyType): boolean {
        if (this.isExist(key)) {
            return false;
        }

        if (this.root === null) {
            this.root = new Node<KeyType>(key);

            return true;
        }

        this.addRecursively(this.root, key);

        return true;
    }

    private addRecursively(currentNode: Node<KeyType>, key: KeyType): void {
        if (key < currentNode.key) {
            if (currentNode.left === null) {
                currentNode.left = new Node<KeyType>(key);
            } else {
                this.addRecursively(currentNode.left, key);
            }
        } else {
            if (currentNode.right === null) {
                currentNode.right = new Node<KeyType>(key);
            } else {
                this.addRecursively(currentNode.right, key);
            }
        }
    }

    public remove(key: KeyType, left: boolean = true): boolean {
        if (this.root === null) {
            return false;
        }

        if (!this.isExist(key)) {
            return false;
        }

        this.removeRecursively(this.root, this.root, key, left);

        return true;
    }

    private removeRecursively(parentNode: Node<KeyType> | null, currentNode: Node<KeyType> | null, key: KeyType, left: boolean = true): void {
        if (key < (currentNode as Node<KeyType>).key) {
            this.removeRecursively(currentNode, (currentNode as Node<KeyType>).left, key, left);

            return;
        } else if (key > (currentNode as Node<KeyType>).key) {
            this.removeRecursively(currentNode, (currentNode as Node<KeyType>).right, key, left);

            return;
        } else {
            let replacementNode: Node<KeyType> | null;
            if ((currentNode as Node<KeyType>).left === null && (currentNode as Node<KeyType>).right === null) {
                replacementNode = null;
            } else if ((currentNode as Node<KeyType>).left === null) {
                replacementNode = (currentNode as Node<KeyType>).right;
            } else if ((currentNode as Node<KeyType>).right === null) {
                replacementNode = (currentNode as Node<KeyType>).left;
            } else {
                if (left) {
                    let rightMostNode: Node<KeyType> | null = (currentNode as Node<KeyType>).left;
                    let rightMostNodeParent: Node<KeyType> | null = currentNode;
                    while ((rightMostNode as Node<KeyType>).right !== null) {
                        rightMostNodeParent = rightMostNode;
                        rightMostNode = (rightMostNode as Node<KeyType>).right;
                    }

                    if (rightMostNodeParent !== currentNode) {
                        (rightMostNodeParent as Node<KeyType>).right = (rightMostNode as Node<KeyType>).left;
                        (rightMostNode as Node<KeyType>).left = (currentNode as Node<KeyType>).left;
                    }

                    (rightMostNode as Node<KeyType>).right = (currentNode as Node<KeyType>).right;
                    replacementNode = rightMostNode;
                } else {
                    let leftMostNode: Node<KeyType> | null = (currentNode as Node<KeyType>).right;
                    let leftMostNodeParent: Node<KeyType> | null = currentNode;
                    while ((leftMostNode as Node<KeyType>).left !== null) {
                        leftMostNodeParent = leftMostNode;
                        leftMostNode = (leftMostNode as Node<KeyType>).left;
                    }

                    if (leftMostNodeParent !== currentNode) {
                        (leftMostNodeParent as Node<KeyType>).left = (leftMostNode as Node<KeyType>).right;
                        (leftMostNode as Node<KeyType>).right = (currentNode as Node<KeyType>).right;
                    }

                    (leftMostNode as Node<KeyType>).left = (currentNode as Node<KeyType>).left;
                    replacementNode = leftMostNode;
                }
            }

            if (parentNode === currentNode) {
                this.root = replacementNode;
            } else if ((parentNode as Node<KeyType>).left === currentNode) {
                (parentNode as Node<KeyType>).left = replacementNode;
            } else {
                (parentNode as Node<KeyType>).right = replacementNode;
            }
        }
    }

    public preOrder(): string {
        const result: KeyType[] = [];

        this.preOrderRecursively(this.root, result);

        return result.join(", ");
    }

    private preOrderRecursively(currentNode: Node<KeyType> | null, result: KeyType[]): void {
        if (currentNode !== null) {
            result.push(currentNode.key);
            this.preOrderRecursively(currentNode.left, result);
            this.preOrderRecursively(currentNode.right, result);
        }
    }

    public inOrder(): string {
        const result: KeyType[] = [];

        this.inOrderRecursively(this.root, result);

        return result.join(", ");
    }

    private inOrderRecursively(currentNode: Node<KeyType> | null, result: KeyType[]): void {
        if (currentNode !== null) {
            this.inOrderRecursively(currentNode.left, result);
            result.push(currentNode.key);
            this.inOrderRecursively(currentNode.right, result);
        }
    }

    public postOrder(): string {
        const result: KeyType[] = [];

        this.postOrderRecursively(this.root, result);

        return result.join(", ");
    }

    private postOrderRecursively(currentNode: Node<KeyType> | null, result: KeyType[]): void {
        if (currentNode !== null) {
            this.postOrderRecursively(currentNode.left, result);
            this.postOrderRecursively(currentNode.right, result);
            result.push(currentNode.key);
        }
    }

    public toString(): string {
        if (this.root === null) {
            return "Root(0): null";
        }

        const textList: string[] = [`Root(0): ${this.root.key}\n`];
        this.toStringRecursively(textList, this.root.left, "L");
        this.toStringRecursively(textList, this.root.right, "R");

        return textList.join("");
    }

    private toStringRecursively(textList: string[], currentNode: Node<KeyType> | null, type: string, level: number = 1): void {
        if (currentNode === null) {
            textList.push(`${"  ".repeat(level)}${type}(${level}): null\n`);

            if (type === "R") {
                textList.push("\n");
            }

            return;
        }

        textList.push(`${"  ".repeat(level)}${type}(${level}): ${currentNode.key}\n`);
        this.toStringRecursively(textList, currentNode.left, "L", level + 1);
        this.toStringRecursively(textList, currentNode.right, "R", level + 1);
    }
}
