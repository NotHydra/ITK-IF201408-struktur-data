class NodeSingly<Type> {
	private readonly _data: Type;
	private _next: NodeSingly<Type> | null = null;

	public constructor(data: Type) {
		this._data = data;
	}

	public getData(): Type {
		return this._data;
	}

	public getNext(): NodeSingly<Type> | null {
		return this._next;
	}

	public setNext(node: NodeSingly<Type> | null): void {
		this._next = node;
	}
}

class LinkedList<Type> {
	private _first: NodeSingly<Type> | null = null;

	public getFirst(): NodeSingly<Type> | null {
		return this._first;
	}

	public setFirst(node: NodeSingly<Type> | null): void {
		this._first = node;
	}

	public length(): number {
		let count: number = 0;
		let currentNode: NodeSingly<Type> | null = this.getFirst();
		while (currentNode != null) {
			currentNode = currentNode.getNext();
			count++;
		}

		return count;
	}

	public show(): void {
		console.log(this.toString());
	}

	public pop(): Type {
		if (this.getFirst() === null) {
			throw new Error("Linked List is empty");
		}

		if (this.getFirst()!.getNext() === null) {
			const popData: Type = this.getFirst()!.getData();
			this.setFirst(null);

			return popData;
		}

		let currentNode: NodeSingly<Type> | null = this.getFirst();
		while (
			currentNode!.getNext() !== null &&
			currentNode!.getNext()!.getNext() !== null
		) {
			currentNode = currentNode!.getNext();
		}

		const popValue: Type = currentNode!.getNext()!.getData();
		currentNode!.setNext(null);

		return popValue;
	}

	public add(data: Type): void {
		const newNode = new NodeSingly<Type>(data);
		if (this.getFirst() === null) {
			this.setFirst(newNode);

			return;
		}

		let currentNode: NodeSingly<Type> | null = this.getFirst();
		while (currentNode!.getNext() !== null) {
			currentNode = currentNode!.getNext();
		}

		currentNode!.setNext(newNode);
	}

	public toString(): string {
		let text: string = "";
		let currentNode: NodeSingly<Type> | null = this.getFirst();
		while (currentNode != null) {
			text += `${currentNode.getData()} -> `;

			currentNode = currentNode.getNext();
		}

		text += "null";

		return text;
	}
}

const linkedList1 = new LinkedList<number>();
linkedList1.add(1);
linkedList1.add(2);
linkedList1.pop();
linkedList1.add(3);
linkedList1.add(4);

console.log(linkedList1.length());
linkedList1.show();
