from typing import Generic, Optional, TypeVar

import random

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

    def addRandom(self, amount: int, minimum: int, maximum: int) -> None:
        for _ in range(amount):
            self.add(random.randint(minimum, maximum))

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

    def swapIndex(self, firstIndex: int, secondIndex: int) -> None:
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

    def swapNode(self, firstNode: Node[Type], secondNode: Node[Type]) -> None:
        if (self.first is None) or (self.last is None):
            raise Exception("Linked List is empty")

        if firstNode == self.first:
            self.first = secondNode

        elif secondNode == self.first:
            self.first = firstNode

        if firstNode == self.last:
            self.last = secondNode

        elif secondNode == self.last:
            self.last = firstNode

        firstNode.next, secondNode.next = secondNode.next, firstNode.next

        if firstNode.next is not None:
            firstNode.next.previous = firstNode

        if secondNode.next is not None:
            secondNode.next.previous = secondNode

        firstNode.previous, secondNode.previous = (
            secondNode.previous,
            firstNode.previous,
        )

        if firstNode.previous is not None:
            firstNode.previous.next = firstNode

        if secondNode.previous is not None:
            secondNode.previous.next = secondNode

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

    def insertionSort(self) -> str:
        if (self.first is None) or (self.last is None):
            raise Exception("Linked List is empty")

        if not isinstance(self.first.data, int):
            raise Exception("Type must be int")

        extractedNode: Optional[Node[Type]] = self.first.next
        while extractedNode is not None:
            nextNode: Optional[Node[Type]] = extractedNode.next

            currentNode: Optional[Node[Type]] = extractedNode.previous
            while currentNode is not None:
                if currentNode.data > extractedNode.data:
                    tempNode: Optional[Node[Type]] = currentNode.previous

                    self.swapNode(currentNode, extractedNode)

                    currentNode = tempNode

                    continue

                break

            extractedNode = nextNode

        return self

    def mergeSort(self) -> str:
        if (self.first is None) or (self.last is None):
            raise Exception("Linked List is empty")

        if not isinstance(self.first.data, int):
            raise Exception("Type must be int")

        self.first = self.splitLinkedList(self.first)

        currentNode: Node[Type] = self.first
        while currentNode.next is not None:
            currentNode = currentNode.next

        self.last = currentNode

        return self

    def rightLinkedList(self, headNode: Node[Type]) -> Node[Type]:
        middleNode: Node[Type] = headNode
        lastNode: Node[Type] = headNode
        while (lastNode.next is not None) and (lastNode.next.next is not None):
            middleNode = middleNode.next
            lastNode = lastNode.next.next

        rightNode: Node[Type] = middleNode.next
        rightNode.previous = None
        middleNode.next = None

        return rightNode

    def splitLinkedList(self, headNode: Node[Type]) -> Node[Type]:
        if headNode.next is None:
            return headNode

        rightNode: Node[Type] = self.rightLinkedList(headNode)

        headNode = self.splitLinkedList(headNode)
        rightNode = self.splitLinkedList(rightNode)

        return self.mergeLinkedList(headNode, rightNode)

    def mergeLinkedList(
        self, leftNode: Optional[Node[Type]], rightNode: Optional[Node[Type]]
    ) -> Node[Type]:
        if leftNode is None:
            return rightNode

        if rightNode is None:
            return leftNode

        if leftNode.data < rightNode.data:
            leftNode.next = self.mergeLinkedList(leftNode.next, rightNode)
            leftNode.next.previous = leftNode
            leftNode.previous = None

            return leftNode

        rightNode.next = self.mergeLinkedList(leftNode, rightNode.next)
        rightNode.next.previous = rightNode
        rightNode.previous = None

        return rightNode

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
        linkedList1.add(3)
        linkedList1.add(2)
        linkedList1.add(1)
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
        linkedList1.insert(0, 4)
        linkedList1.insert(3, 0)
        linkedList1.insert(3, 1)
        print(linkedList1.length())
        linkedList1.show()
        linkedList1.debug()
        print()

        print("Swap")
        linkedList1.swapIndex(0, 4)
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

        print("Insertion Sort")
        linkedList1.show()
        linkedList1.debug()
        print()
        linkedList1.insertionSort()
        linkedList1.show()
        linkedList1.debug()
        print()

        print("Merge Sort")
        linkedList1.swapIndex(0, 2)
        linkedList1.swapIndex(1, 2)

        linkedList1.show()
        linkedList1.debug()
        print()
        linkedList1.mergeSort()
        linkedList1.show()
        linkedList1.debug()
        print()


if __name__ == "__main__":
    Program.main()
