from typing import Generic, Optional, TypeVar


Type = TypeVar("Type")


class Node(Generic[Type]):
    def __init__(self, data: Type) -> None:
        self._data: Type = data
        self._next: Optional[Node[Type]] = None

    @property
    def data(self) -> Type:
        return self._data

    @property
    def next(self) -> Optional["Node[Type]"]:
        return self._next

    @next.setter
    def next(self, node: Optional["Node[Type]"]) -> None:
        self._next = node


class LinkedList(Generic[Type]):
    def __init__(self):
        self._first: Optional[Node[Type]] = None

    @property
    def first(self) -> Optional[Node[Type]]:
        return self._first

    @first.setter
    def first(self, node: Optional[Node[Type]]) -> None:
        self._first = node

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
                f"Debug {count}: {currentNode.data} -> {currentNode.next.data if currentNode.next is not None else 'null'}"
            )

            count += 1
            currentNode = currentNode.next

        print("Debug: End")

    def __str__(self) -> str:
        text: str = "null -> "
        currentNode: Optional[Node[Type]] = self.first
        while currentNode is not None:
            text += f"{currentNode.data} -> "
            currentNode = currentNode.next

        text += "null"

        return text


class Book:
    counter: int = 0

    def __init__(self, title: str, isbn: str, author: str, publicationDate: str):
        Book.counter += 1

        self._id: int = Book.counter
        self._title: str = title
        self._isbn: str = isbn
        self._author: str = author
        self._publicationDate: str = publicationDate

    @property
    def id(self) -> int:
        return self._id

    @property
    def title(self) -> str:
        return self._title

    @property
    def isbn(self) -> str:
        return self._isbn

    @property
    def author(self) -> str:
        return self._author

    @property
    def publicationDate(self) -> str:
        return self._publicationDate

    def __str__(self) -> str:
        return f"Book({self.id}, {self.title}, {self.isbn}, {self.author}, {self.publicationDate})"


class Catalogue(LinkedList[Book]):
    def insertBook(self, book: Book) -> None:
        newNode: Node[Book] = Node[Book](book)
        if self.first is None:
            self.first = newNode

            return

        currentNode: Optional[Node[Book]] = self.first
        while currentNode.next is not None:
            currentNode = currentNode.next

        currentNode.next = newNode

    def removeBook(self, isbn: str) -> None:
        if self.first is None:
            raise Exception("Catalogue is empty")

        if self.first.data.isbn == isbn:
            self.first = self.first.next

            return

        currentNode: Node[Book] = self.first
        while currentNode.next is not None:
            if currentNode.next.data.isbn == isbn:
                currentNode.next = currentNode.next.next

                return

            currentNode = currentNode.next

        raise Exception("Book not found")

    def findBook(self, isbn: str) -> Book:
        if self.first is None:
            raise Exception("Catalogue is empty")

        currentNode: Node[Book] = self.first
        while currentNode.next is not None:
            if currentNode.next.data.isbn == isbn:
                return currentNode.next.data

            currentNode = currentNode.next

        raise Exception("Book not found")

    def displayCatalogue(self) -> None:
        self.show()

    def countBooks(self) -> int:
        return self.length()


class Program:
    def main():
        catalogue1 = Catalogue()

        print("Initialize")
        catalogue1.insertBook(
            Book("The Great Gatsby", "9780743273565", "F. Scott Fitzgerald", "1925")
        )
        catalogue1.insertBook(
            Book("To Kill a Mockingbird", "9780061120084", "Harper Lee", "1960")
        )
        catalogue1.insertBook(Book("1984", "9780451524935", "George Orwell", "1949"))
        print(catalogue1.countBooks())
        catalogue1.displayCatalogue()
        catalogue1.debug()
        print()

        print("Find")
        print(catalogue1.findBook("9780061120084"))
        print()

        print("Remove")
        catalogue1.removeBook("9780061120084")
        print(catalogue1.countBooks())
        catalogue1.displayCatalogue()
        catalogue1.debug()
        print()


if __name__ == "__main__":
    Program.main()
