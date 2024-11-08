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
				nodesByDepthDict[currentDepth].every(
					(node: Node<KeyType> | string) => {
						return node === "null" || node === "empty";
					}
				)
			) {
				break;
			}

			const newNodesByDepthList: (Node<KeyType> | string)[] = [];
			nodesByDepthDict[currentDepth].forEach(
				(node: Node<KeyType> | string) => {
					if (node === "null" || node === "empty") {
						newNodesByDepthList.push("empty");
						newNodesByDepthList.push("empty");
					} else {
						newNodesByDepthList.push(
							typeof node === "string" || node.left === null
								? "null"
								: node.left
						);

						newNodesByDepthList.push(
							typeof node === "string" || node.right === null
								? "null"
								: node.right
						);
					}
				}
			);

			currentDepth += 1;
			nodesByDepthDict[currentDepth] = newNodesByDepthList;
		}

		return nodesByDepthDict;
	}

	public isExist(key: KeyType): boolean {
		return this.isExistRecursively(this.root, key);
	}

	private isExistRecursively(
		currentNode: Node<KeyType> | null,
		key: KeyType
	): boolean {
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

	public remove(key: KeyType): boolean {
		const [newRoot, deleted] = this.removeRecursively(this.root, key);

		this.root = newRoot;

		return deleted;
	}

	private removeRecursively(
		currentNode: Node<KeyType> | null,
		key: KeyType
	): [Node<KeyType> | null, boolean] {
		if (currentNode === null) {
			return [null, false];
		}

		if (key < currentNode.key) {
			const [newLeft, deleted] = this.removeRecursively(
				currentNode.left,
				key
			);

			currentNode.left = newLeft;

			return [currentNode, deleted];
		} else if (key > currentNode.key) {
			const [newRight, deleted] = this.removeRecursively(
				currentNode.right,
				key
			);

			currentNode.right = newRight;

			return [currentNode, deleted];
		} else {
			if (!currentNode.left) {
				return [currentNode.right, true];
			}

			if (!currentNode.right) {
				return [currentNode.left, true];
			}

			let successor: Node<KeyType> = currentNode.right;
			while (successor.left) {
				successor = successor.left;
			}

			currentNode = new Node<KeyType>(successor.key);
			currentNode.left = currentNode.left;
			currentNode.right = this.removeRecursively(
				currentNode.right,
				successor.key
			)[0];

			return [currentNode, true];
		}
	}

	public preOder(): string {
		const result: KeyType[] = [];

		this.preOrderRecursively(this.root, result);

		return result.join(", ");
	}

	private preOrderRecursively(
		currentNode: Node<KeyType> | null,
		result: KeyType[]
	): void {
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

	private inOrderRecursively(
		currentNode: Node<KeyType> | null,
		result: KeyType[]
	): void {
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

	private postOrderRecursively(
		currentNode: Node<KeyType> | null,
		result: KeyType[]
	): void {
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

	private toStringRecursively(
		textList: string[],
		currentNode: Node<KeyType> | null,
		type: string,
		level: number = 1
	): void {
		if (currentNode === null) {
			textList.push(`${"  ".repeat(level)}${type}(${level}): null\n`);

			if (type === "R") {
				textList.push("\n");
			}

			return;
		}

		textList.push(
			`${"  ".repeat(level)}${type}(${level}): ${currentNode.key}\n`
		);
		this.toStringRecursively(textList, currentNode.left, "L", level + 1);
		this.toStringRecursively(textList, currentNode.right, "R", level + 1);
	}
}
