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


class LinkedList(Generic[Type]):
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

    def add(self, data: Type) -> None:
        newNode: Node[Type] = Node[Type](data)
        if (self.first is None) or (self.last is None):
            self.first = newNode
            self.last = newNode

            return

        self.last.next = newNode
        newNode.previous = self.last
        self.last = newNode

    def pop(self) -> Type:
        if (self.first is None) or (self.last is None):
            raise Exception("Linked List is empty")

        popData: Type = self.last.data
        if self.first == self.last:
            self.first = None
            self.last = None

            return popData

        self.last = self.last.previous
        if self.last is not None:
            self.last.next = None

        return popData

    def get(self, index: int) -> Type:
        if (self.first is None) or (self.last is None):
            raise Exception("Linked List is empty")

        if (index < 0) or (self.length() <= index):
            raise Exception("Index out of bound")

        if index < (self.length() / 2):
            currentIndex: int = 0
            currentNode: Node[Type] = self.first
            while currentIndex < index:
                currentIndex += 1
                currentNode = currentNode.next

            return currentNode.data

        currentIndex: int = self.length() - 1
        currentNode: Node[Type] = self.last
        while currentIndex > index:
            currentIndex -= 1
            currentNode = currentNode.previous

        return currentNode.data

    def insert(self, index: int, data: Type) -> None:
        if (index < 0) or (self.length() < index):
            raise Exception("Index out of bound")

        newNode: Node[Type] = Node[Type](data)
        if index == 0:
            if (self.first is None) or (self.last is None):
                self.first = newNode
                self.last = newNode

                return

            newNode.next = self.first
            self.first.previous = newNode
            self.first = newNode

            return

        if index == self.length():
            self.last.next = newNode
            newNode.previous = self.last
            self.last = newNode

            return

        if index < (self.length() / 2):
            currentIndex: int = 0
            currentNode = self.first
            while currentIndex < index:
                currentIndex += 1
                currentNode = currentNode.next

        else:
            currentIndex: int = self.length() - 1
            currentNode = self.last
            while currentIndex > index:
                currentIndex -= 1
                currentNode = currentNode.previous

        newNode.next = currentNode
        newNode.previous = currentNode.previous
        currentNode.previous.next = newNode
        currentNode.previous = newNode

    def swap(self, firstIndex: int, secondIndex: int) -> None:
        if (self.first is None) or (self.last is None):
            raise Exception("Linked List is empty")

        if (firstIndex < 0) or (self.length() <= firstIndex):
            raise Exception("First index out of bound")

        if (secondIndex < 0) or (self.length() <= secondIndex):
            raise Exception("Second index out of bound")

        if firstIndex == secondIndex:
            return

        if firstIndex < (self.length() / 2):
            firstCurrentIndex: int = 0
            firstCurrentNode: Node[Type] = self.first
            while firstCurrentIndex < firstIndex:
                firstCurrentIndex += 1
                firstCurrentNode = firstCurrentNode.next

        else:
            firstCurrentIndex: int = self.length() - 1
            firstCurrentNode: Node[Type] = self.last
            while firstCurrentIndex > firstIndex:
                firstCurrentIndex -= 1
                firstCurrentNode = firstCurrentNode.previous

        if secondIndex < (self.length() / 2):
            secondCurrentIndex: int = 0
            secondCurrentNode: Node[Type] = self.first
            while secondCurrentIndex < secondIndex:
                secondCurrentIndex += 1
                secondCurrentNode = secondCurrentNode.next

        else:
            secondCurrentIndex: int = self.length() - 1
            secondCurrentNode: Node[Type] = self.last
            while secondCurrentIndex > secondIndex:
                secondCurrentIndex -= 1
                secondCurrentNode = secondCurrentNode.previous

        if firstCurrentNode == self.first:
            self.first = secondCurrentNode

        elif secondCurrentNode == self.first:
            self.first = firstCurrentNode

        if firstCurrentNode == self.last:
            self.last = secondCurrentNode

        elif secondCurrentNode == self.last:
            self.last = firstCurrentNode

        firstCurrentNode.next, secondCurrentNode.next = (
            secondCurrentNode.next,
            firstCurrentNode.next,
        )

        if firstCurrentNode.next is not None:
            firstCurrentNode.next.previous = firstCurrentNode

        if secondCurrentNode.next is not None:
            secondCurrentNode.next.previous = secondCurrentNode

        firstCurrentNode.previous, secondCurrentNode.previous = (
            secondCurrentNode.previous,
            firstCurrentNode.previous,
        )

        if firstCurrentNode.previous is not None:
            firstCurrentNode.previous.next = firstCurrentNode

        if secondCurrentNode.previous is not None:
            secondCurrentNode.previous.next = secondCurrentNode

    def remove(self, index: int) -> None:
        if (self.first is None) or (self.last is None):
            raise Exception("Linked List is empty")

        if (index < 0) or (self.length() <= index):
            raise Exception("Index out of bound")

        if index == 0:
            if self.first == self.last:
                self.first = None
                self.last = None

                return

            self.first = self.first.next
            self.first.previous = None

            return

        if index == (self.length() - 1):
            self.last = self.last.previous
            self.last.next = None

            return

        if index < (self.length() / 2):
            currentIndex: int = 0
            currentNode: Node[Type] = self.first
            while currentIndex < index:
                currentIndex += 1
                currentNode = currentNode.next

        else:
            currentIndex: int = self.length() - 1
            currentNode: Node[Type] = self.last
            while currentIndex > index:
                currentIndex -= 1
                currentNode = currentNode.previous

        currentNode.previous.next = currentNode.next
        currentNode.next.previous = currentNode.previous

    def __str__(self) -> str:
        text: str = "null -> "
        currentNode: Optional[Node[Type]] = self.first
        while currentNode is not None:
            text += f"{currentNode.data} -> "
            currentNode = currentNode.next

        text += "null"

        return text


class Program:
    def main():
        linkedList1: LinkedList[int] = LinkedList[int]()

        print("Initialize")
        linkedList1.add(1)
        linkedList1.add(2)
        linkedList1.add(3)
        print(linkedList1.length())
        linkedList1.show()
        linkedList1.debug()
        print()

        print("Pop")
        linkedList1.pop()
        print(linkedList1.length())
        linkedList1.show()
        linkedList1.debug()
        print()

        print("Get")
        print(linkedList1.get(0))
        print(linkedList1.get(1))
        print()

        print("Insert")
        linkedList1.insert(0, 0)
        linkedList1.insert(3, 4)
        linkedList1.insert(3, 3)
        print(linkedList1.length())
        linkedList1.show()
        linkedList1.debug()
        print()

        print("Swap")
        linkedList1.swap(0, 4)
        print(linkedList1.length())
        linkedList1.show()
        linkedList1.debug()
        print()

        print("Remove")
        linkedList1.remove(0)
        linkedList1.remove(3)
        print(linkedList1.length())
        linkedList1.show()
        linkedList1.debug()
        print()


if __name__ == "__main__":
    Program.main()
