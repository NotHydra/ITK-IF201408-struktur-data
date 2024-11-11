export enum KeyDirection {
    Left,
    Middle,
    Right,
}

export type SortableKeyType = number | string

export enum NodeAddCase {
    ONE = 1,
    TWO = 2,
    THREE = 3,
    FOUR = 4,
    FIVE = 5,
    SIX = 6,
    FIVE_AND_SIX = 56,
}
export interface NodesByDepthInterface<T extends SortableKeyType> {
    [depth: number]: (Node<T> | string)[];
}

export class Node<T extends SortableKeyType> {
    private _key: T;
    private _red: boolean;
    private _parent: Node<T> | null = null;
    private _left: Node<T> | null = null;
    private _right: Node<T> | null = null;

    public constructor(key: T, red: boolean, parent: Node<T> | null) {
        this._key = key;
        this._red = red;
        this._parent = parent;
    }

    public get key(): T {
        return this._key;
    }

    public set key(value: T) {
        this._key = value;
    }

    public get red(): boolean {
        return this._red;
    }

    public set red(value: boolean) {
        this._red = value;
    }

    public get parent(): Node<T> | null {
        return this._parent;
    }

    public set parent(value: Node<T> | null) {
        this._parent = value;
    }

    public get left(): Node<T> | null {
        return this._left;
    }

    public set left(node: Node<T> | null) {
        this._left = node;
    }

    public get right(): Node<T> | null {
        return this._right;
    }

    public set right(node: Node<T> | null) {
        this._right = node;
    }
}

export class RedBlackTree<T extends SortableKeyType> {
    private _root: Node<T> | null = null;

    public get root(): Node<T> | null {
        return this._root;
    }

    public set root(node: Node<T> | null) {
        this._root = node;
    }

    public show(): void {
        console.log(this.toString());
    }

    public getTotal(): number {
        return this.getTotalRecursively(this.root);
    }

    private getTotalRecursively(currentNode: Node<T> | null): number {
        if (currentNode === null) {
            return 0;
        }

        return 1 + this.getTotalRecursively(currentNode.left) + this.getTotalRecursively(currentNode.right);
    }

    public getNodesByDepth(): NodesByDepthInterface<T> {
        const nodesByDepthDict: NodesByDepthInterface<T> = {};

        let currentDepth: number = 0;
        nodesByDepthDict[0] = [this.root === null ? "null" : this.root];
        while (true) {
            if (
                nodesByDepthDict[currentDepth].every((node: Node<T> | string) => {
                    return node === "null" || node === "empty";
                })
            ) {
                break;
            }

            const newNodesByDepthList: (Node<T> | string)[] = [];
            nodesByDepthDict[currentDepth].forEach((node: Node<T> | string) => {
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

    public isExist(key: T): boolean {
        return this.isExistRecursively(this.root, key);
    }

    private isExistRecursively(currentNode: Node<T> | null, key: T): boolean {
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

    public add(key: T): boolean {
        if (this.isExist(key)) {
            return false;
        }

        if (this.root === null) {
            this.root = new Node(key, false, null);

            return true;
        }

        let node = this.addNode(this.root, key);
        let childDirection = this.compareKeyDirection(node.key, node.parent!.key);
        node = (node.parent!);

        let addCase: NodeAddCase;
        let oldParent: Node<T> | null;

        do {
            addCase = this.getAddCase(node)

            switch (addCase) {
                case NodeAddCase.ONE:
                    break;
                case NodeAddCase.TWO:
                    oldParent = (node.parent!);
                    node = this.addCaseTwo(node)!;

                    if (node) {
                        childDirection = this.compareKeyDirection(oldParent.key, oldParent.parent!.key);
                    }

                    oldParent = null;
                    break;
                case NodeAddCase.FOUR:
                    node.red = false;
                    break;
                case NodeAddCase.FIVE_AND_SIX:
                    this.addCaseFiveSix(node, this.compareKeyDirection(node.key, node.parent!.key), childDirection)
                    break;
            }
        } while (addCase === NodeAddCase.TWO && node);

        return true;
    }

    private addNode(node: Node<T>, key: T): Node<T> {
        let newNode: Node<T>;

        while (true) {
            if (this.compareKeyDirection(key, node.key) === KeyDirection.Left) {
                if (!node.left) {
                    newNode = new Node<T>(key, true, node);
                    node.left = newNode;
                    break;
                }
                else {
                    node = node.left;
                }
            }
            else {
                if (!node.right) {
                    newNode = new Node<T>(key, true, node);
                    node.right = newNode;
                    break;
                }
                else {
                    node = node.right;
                }
            }
        }

        return newNode;
    }

    private addCaseTwo(node: Node<T>): Node<T> | null {
        const grandParent = node.parent!
        const parentDirection = this.compareKeyDirection(node.key, node.parent!.key);
        const uncle = parentDirection === KeyDirection.Left ? grandParent.right! : grandParent.left!;

        node.red = false;
        uncle.red = false;
        grandParent.red = true;

        if (!node.parent!.parent) {
            node.parent!.red = false;
        }

        return node.parent!.parent;
    }

    private addCaseFiveSix(node: Node<T>, parentDirection: KeyDirection, childDirection: KeyDirection): Node<T> {
        if (parentDirection === KeyDirection.Left) {
            if (childDirection === KeyDirection.Right) {
                node = this.rotateLeft(node);
            }

            node = this.rotateRight(node.parent!);
            node.red = false;
            node.right!.red = true;
        }
        else {
            if (childDirection === KeyDirection.Left) {
                node = this.rotateRight(node);
            }

            node = this.rotateLeft(node.parent!);
            node.red = false;
            node.left!.red = true;
        }

        return node;
    }

    private rotateLeft(node: Node<T>): Node<T> {
        const temp1: Node<T> | null = node;
        const temp2: Node<T> | null = node.right!.left;

        node = node.right!;
        node.parent = temp1.parent;

        if (node.parent) {
            if (this.compareKeyDirection(node.key, node.parent!.key) === KeyDirection.Left) {
                node.parent.left = node;
            }
            else {
                node.parent.right = node;
            }
        }

        node.left = temp1;
        node.left.parent = node;
        node.left.right = temp2;

        if (temp2) {
            node.left.right!.parent = temp1;
        }

        if (!node.parent) {
            this.root = node;
        }

        return node;
    }

    private rotateRight(node: Node<T>): Node<T> {
        const temp1: Node<T> | null = node;
        const temp2: Node<T> | null = node.left!.right;

        node = node.left!;
        node.parent = temp1.parent;

        if (node.parent) {
            if (this.compareKeyDirection(node.key, node.parent!.key) === KeyDirection.Left) {
                node.parent.left = node;
            }
            else {
                node.parent.right = node;
            }
        }

        node.right = temp1;
        node.right.parent = node;
        node.right.left = temp2;

        if (temp2) {
            node.right.left!.parent = temp1;
        }

        if (!node.parent) {
            this.root = node;
        }

        return node;
    }

    private getAddCase(node: Node<T>): NodeAddCase {
        if (!node.red) {
            return NodeAddCase.ONE;
        }
        else if (!node.parent) {
            return NodeAddCase.FOUR;
        }
        else {
            const grandParent = node.parent
            const parentDirection = this.compareKeyDirection(node.key, node.parent!.key);
            const uncle = parentDirection === KeyDirection.Left ? grandParent.right : grandParent.left;

            if (!uncle || !uncle.red) {
                return NodeAddCase.FIVE_AND_SIX;
            }

            return NodeAddCase.TWO;
        }
    }

    public remove(key: T): boolean {
        if (this.root === null) {
            return false;
        }

        if (!this.isExist(key)) {
            return false;
        }

        let node = this.removeNode(this.root, key);
        node = this.removeSimpleCases(node)!;

        if (!node){
            return true;
        }

        this.removeLeaf(node.parent!, this.compareKeyDirection(node.key, node.parent!.key));

        do {
            node = this.removeColor(node)!;
        }
        while (node && node.parent)

        return true;
    }

    private removeNode(node: Node<T>, key: T): Node<T> {
        while (true) {
            const direction = this.compareKeyDirection(key, node.key);

            if (direction === KeyDirection.Left) {
                node = node.left!;
            }
            else if (direction === KeyDirection.Right) {
                node = node.right!;
            }
            else {
                break;
            }
        }

        return node;
    }

    private removeSimpleCases(node: Node<T>): Node<T> | null {
        if (!node.parent && !node.left && !node.right) {
            this.root = null;

            return null;
        }

        if (node.left && node.right) {
            const successor = this.getMin(node.right!);
            node.key = successor.key
            node = successor;
        }

        if (node.red) {
            this.removeLeaf(node.parent!, this.compareKeyDirection(node.key, node.parent!.key));

            return null;
        }

        return this.removeBlackNode(node);
    }

    private removeLeaf(node: Node<T>, childDirection: KeyDirection): void {
        if (childDirection === KeyDirection.Left) {
            node.left = null;
        }
        else {
            node.right = null;
        }
    }

    private removeBlackNode(node: Node<T>): Node<T> | null {
        const child = node.left ?? node.right;

        if (!child) {
            return node;
        }

        child.red = false;
        child.parent = node.parent;

        const childDirection = !node.parent
            ? KeyDirection.Middle
            : this.compareKeyDirection(node.key, node.parent!.key);

        this.transplant(node.parent!, child, childDirection);

        return null;
    }

    private transplant(node: Node<T>, child: Node<T>, childDirection: KeyDirection): void {
        if (!node)
        {
            this.root = child;
        }
        else if (!child)
        {
            this.removeLeaf(node, childDirection);
        }
        else if (childDirection === KeyDirection.Left)
        {
            node.left = child;
        }
        else
        {
            node.right = child;
        }
    }

    private removeColor(node: Node<T>): Node<T> | null {
        const removeCase = this.getRemoveCase(node);

        const direction = this.compareKeyDirection(node.key, node.parent!.key);

        const sibling = direction === KeyDirection.Left ? node.parent!.right! : node.parent!.left!;
        const closeNephew = direction === KeyDirection.Left ? sibling.left! : sibling.right!;
        const farNephew = direction === KeyDirection.Left ? sibling.right! : sibling.left!;

        switch (removeCase) {
            case NodeAddCase.ONE:
                sibling.red = true;
                return node.parent;
            case NodeAddCase.THREE:
                this.removeCase3(node, closeNephew, direction);
                break;
            case NodeAddCase.FOUR:
                this.removeCase4(sibling);
                break;
            case NodeAddCase.FIVE:
                this.removeCase5(node, sibling, direction);
                break;
            case NodeAddCase.SIX:
                this.removeCase6(node, farNephew, direction);
                break;
        }

        return null;
    }

    private removeCase3(node: Node<T>, closeNephew: Node<T>, childDirection: KeyDirection): void {
        let sibling = childDirection == KeyDirection.Left ? this.rotateLeft(node.parent!) : this.rotateRight(node.parent!);

        sibling.red = false;

        if (childDirection == KeyDirection.Left)
        {
            sibling.left!.red = true;
        }
        else
        {
            sibling.right!.red = true;
        }

        sibling = closeNephew!;

        const farNephew = childDirection === KeyDirection.Left ? sibling.right! : sibling.left!;

        if (farNephew && farNephew.red)
        {
            this.removeCase6(node, farNephew, childDirection);
            return;
        }

        closeNephew = childDirection === KeyDirection.Left ? sibling.left! : sibling.right!;
        if (closeNephew && closeNephew.red)
        {
            this.removeCase5(node, sibling, childDirection);
            return;
        }

        this.removeCase4(sibling);
    }

    private removeCase4(sibling: Node<T>): void {
        sibling.red = true;
        sibling.parent!.red = false;
    }

    private removeCase5(node: Node<T>, sibling: Node<T>, childDirection: KeyDirection): void {
        sibling = childDirection == KeyDirection.Left ? this.rotateRight(sibling) : this.rotateLeft(sibling);
        const farNephew = childDirection === KeyDirection.Left ? sibling.right! : sibling.left!;

        sibling.red = false;
        farNephew.red = true;

        this.removeCase6(node, farNephew, childDirection);
    }

    private removeCase6(node: Node<T>, farNephew: Node<T>, childDirection: KeyDirection): void {
        const oldParent = node.parent!;
        node = childDirection == KeyDirection.Left ? this.rotateLeft(oldParent) : this.rotateRight(oldParent);

        node.red = oldParent.red;
        oldParent.red = false;
        farNephew.red = false;
    }

    private getRemoveCase(node: Node<T>): NodeAddCase {
        const direction = this.compareKeyDirection(node.key, node.parent!.key);

        const sibling = direction === KeyDirection.Left ? node.parent!.right! : node.parent!.left!;
        const closeNephew = direction === KeyDirection.Left ? sibling.left! : sibling.right!;
        const farNephew = direction === KeyDirection.Left ? sibling.right! : sibling.left!;

        if (sibling.red) {
            return NodeAddCase.THREE;
        }
        else if (farNephew && farNephew.red) {
            return NodeAddCase.SIX;
        }
        else if (closeNephew && closeNephew.red) {
            return NodeAddCase.FIVE;
        }
        else if (node.parent!.red) {
            return NodeAddCase.FOUR;
        }
        else {
            return NodeAddCase.ONE
        }
    }

    // === Utilities ===
    private getMax(node: Node<T>): Node<T> {
        while (node.right) {
            node = node.right
        }

        return node;
    }

    private getMin(node: Node<T>): Node<T> {
        while (node.left) {
            node = node.left
        }

        return node;
    }

    private compareKeyDirection(key1: T, key2: T): KeyDirection {
        if (key1 < key2) {
            return KeyDirection.Left;
        }

        return key1 === key2
            ? KeyDirection.Middle
            : KeyDirection.Right;
    }

    public clear(): void {
        this.root = null;
    }

    public preOrder(): string {
        const result: T[] = [];

        this.preOrderRecursively(this.root, result);

        return result.join(", ");
    }

    private preOrderRecursively(currentNode: Node<T> | null, result: T[]): void {
        if (currentNode !== null) {
            result.push(currentNode.key);
            this.preOrderRecursively(currentNode.left, result);
            this.preOrderRecursively(currentNode.right, result);
        }
    }

    public inOrder(): string {
        const result: T[] = [];

        this.inOrderRecursively(this.root, result);

        return result.join(", ");
    }

    private inOrderRecursively(currentNode: Node<T> | null, result: T[]): void {
        if (currentNode !== null) {
            this.inOrderRecursively(currentNode.left, result);
            result.push(currentNode.key);
            this.inOrderRecursively(currentNode.right, result);
        }
    }

    public postOrder(): string {
        const result: T[] = [];

        this.postOrderRecursively(this.root, result);

        return result.join(", ");
    }

    private postOrderRecursively(currentNode: Node<T> | null, result: T[]): void {
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

    private toStringRecursively(textList: string[], currentNode: Node<T> | null, type: string, level: number = 1): void {
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

    public setRandomLetters(type: string): void {
        const alphabet: string[] = (
            type === "az" ? "abcdefghijklmnopqrstuvwxyz" : type === "AZ" ? "ABCDEFGHIJKLMNOPQRSTUVWXYZ" : "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
        ).split("");

        for (let i: number = alphabet.length - 1; i > 0; i--) {
            const j: number = Math.floor(Math.random() * (i + 1));
            [alphabet[i], alphabet[j]] = [alphabet[j], alphabet[i]];
        }

        this.clear();
        for (let i: number = 1; i <= 10; i++) {
            this.add(alphabet[i] as T);
        }
    }
}
