from typing import Generic, Optional, TypeVar


Type = TypeVar("Type")


class Node(Generic[Type]):
    def __init__(self, data: Type) -> None:
        self._data: Type = data
        self._next: Optional["Node[Type]"] = None

    @property
    def data(self) -> Type:
        return self._data

    @property
    def next(self) -> Optional["Node[Type]"]:
        return self._next

    @next.setter
    def next(self, node: "Node[Type]") -> None:
        self._next = node


class Stack(Generic[Type]):
    def __init__(self) -> None:
        self._top: Optional[Node[Type]] = None

    @property
    def top(self) -> Optional[Node[Type]]:
        return self._top

    @top.setter
    def top(self, node: Node[Type]) -> None:
        self._top = node

    def length(self) -> int:
        count: int = 0

        currentNode: Optional[Node[Type]] = self.top
        while currentNode is not None:
            count += 1
            currentNode = currentNode.next

        return count

    def show(self) -> None:
        print(self)

    def debug(self) -> None:
        print("Debug: Top")

        count: int = 1
        currentNode: Optional[Node[Type]] = self.top
        while currentNode is not None:
            print(
                f"Debug {count}: {currentNode.data} -> {currentNode.next.data if currentNode.next is not None else 'null'}"
            )

            count += 1
            currentNode = currentNode.next

        print("Debug: Bottom")

    def hasPop(self) -> bool:
        return self.top is not None

    def peek(self) -> Optional[Type]:
        if self.top is None:
            raise Exception("Stack is empty")

        return self.top.data

    def push(self, data: Type) -> None:
        newNode: Node[Type] = Node(data)
        if self.top is None:
            self.top = newNode

            return

        newNode.next = self.top
        self.top = newNode

    def pop(self) -> Optional[Type]:
        if self.top is None:
            raise Exception("Stack is empty")

        popData: Type = self.top.data

        self.top = self.top.next

        return popData

    def clear(self) -> None:
        while self.hasPop():
            self.pop()

    def __str__(self) -> str:
        text: str = "top -> "
        currentNode: Optional[Node[Type]] = self.top
        while currentNode is not None:
            text += f"{currentNode.data} -> "
            currentNode = currentNode.next

        text += "bottom"

        return text


class Program:
    def main():
        stack1: Stack[int] = Stack[int]()

        print("Initialize")
        stack1.push(1)
        stack1.push(2)
        stack1.push(3)
        print(stack1.length())
        stack1.show()
        stack1.debug()
        print()

        print("Has Pop")
        print(stack1.hasPop())
        print()

        print("Peek")
        print(stack1.peek())
        print()

        print("Pop")
        print(stack1.pop())
        print(stack1.length())
        stack1.show()
        stack1.debug()
        print()


if __name__ == "__main__":
    Program.main()
