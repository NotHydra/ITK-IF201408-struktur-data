from typing import Generic, Optional, TypeVar


Type = TypeVar("Type")


class Node(Generic[Type]):
    def __init__(self, data: Type):
        self._data: Type = data
        self._next: Optional["Node[Type]"] = None
        self._previous: Optional["Node[Type]"] = None

    @property
    def data(self) -> Type:
        return self._data

    @property
    def next(self) -> Optional["Node[Type]"]:
        return self._next

    @property
    def previous(self) -> Optional["Node[Type]"]:
        return self._previous

    @next.setter
    def next(self, node: "Node[Type]") -> None:
        self._next = node

    @previous.setter
    def previous(self, node: "Node[Type]") -> None:
        self._previous = node


class Queue(Generic[Type]):
    def __init__(self):
        self._first: Optional[Node[Type]] = None
        self._last: Optional[Node[Type]] = None

    @property
    def first(self) -> Optional[Node[Type]]:
        return self._first

    @property
    def last(self) -> Optional[Node[Type]]:
        return self._last

    @first.setter
    def first(self, node: Node[Type]) -> None:
        self._first = node

    @last.setter
    def last(self, node: Node[Type]) -> None:
        self._last = node

    def length(self) -> int:
        count: int = 0
        currentNode: Optional[Node[Type]] = self.first
        while currentNode is not None:
            count += 1
            currentNode = currentNode.next

        return count

    def show(self) -> None:
        print(self)

    def debug(self) -> None:
        print("Debug: Start")

        count: int = 1
        currentNode: Optional[Node[Type]] = self.first
        while currentNode is not None:
            print(
                f"Debug {count}: {currentNode.previous.data if currentNode.previous is not None else 'null'} <- {currentNode.data} -> {currentNode.next.data if currentNode.next is not None else 'null'}"
            )

            count += 1
            currentNode = currentNode.next

        print("Debug: End")

    def hasDequeue(self) -> bool:
        return self.first is not None

    def peek(self) -> Optional[Type]:
        if self.first is None:
            raise Exception("Queue is empty")

        return self.first.data

    def enqueue(self, data: Type) -> None:
        newNode: Node[Type] = Node[Type](data)
        if (self.first is None) or (self.last is None):
            self.first = newNode
            self.last = newNode

            return

        self.last.next = newNode
        newNode.previous = self.last
        self.last = newNode

    def dequeue(self) -> Type:
        if (self.first is None) or (self.last is None):
            raise Exception("Queue is empty")

        dequeueData: Type = self.first.data
        if self.first == self.last:
            self.first = None
            self.last = None

            return dequeueData

        self.first = self.first.next
        self.first.previous = None

        return dequeueData

    def clear(self) -> None:
        self.first = None
        self.last = None

    def __str__(self) -> str:
        text: str = "first -> "
        currentNode: Optional[Node[Type]] = self.first
        while currentNode is not None:
            text += f"{currentNode.data} -> "
            currentNode = currentNode.next

        text += "last"

        return text


class Program:
    def main():
        queue1: Queue[int] = Queue[int]()

        print("Initialize")
        queue1.enqueue(1)
        queue1.enqueue(2)
        queue1.enqueue(3)
        queue1.enqueue(4)
        queue1.enqueue(5)
        print(queue1.length())
        queue1.show()
        queue1.debug()
        print()

        print("Has Dequeue")
        print(queue1.hasDequeue())
        print()

        print("Peek")
        print(queue1.peek())
        print()

        print("Dequeue")
        print(queue1.dequeue())
        print(queue1.length())
        queue1.show()
        queue1.debug()
        print()


if __name__ == "__main__":
    Program.main()
