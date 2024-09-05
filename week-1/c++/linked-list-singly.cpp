#include <iostream>

template <typename Type>
class Node {
   private:
    Type value;
    Node<Type> *next;

   public:
    Node(Type value) {
        this->value = value;
        this->next = nullptr;
    };

    Type getValue() {
        return this->value;
    };

    Node<Type> *getNext() {
        return this->next;
    };

    void setNext(Node<Type> *next) {
        this->next = next;
    };
};

template <typename Type>
class LinkedList {
   private:
    Node<Type> *first;

   public:
    LinkedList() {
        this->first = nullptr;
    };

    Node<Type> *getFirst() {
        return this->first;
    };

    void setFirst(Node<Type> *node) {
        this->first = node;
    };

    int length() {
        int count = 0;
        Node<Type> *currentNode = this->getFirst();
        while (currentNode != nullptr) {
            currentNode = currentNode->getNext();
            count++;
        };

        return count;
    };

    void show() {
        Node<Type> *currentNode = this->getFirst();
        while (currentNode != nullptr) {
            std::cout << currentNode->getValue() << " -> ";

            currentNode = currentNode->getNext();
        };

        std::cout << "end" << std::endl;
    };

    void add(Type value) {
        Node<Type> *newNode = new Node<Type>(value);
        if (this->getFirst() == nullptr) {
            this->setFirst(newNode);

            return;
        };

        Node<Type> *currentNode = this->getFirst();
        while (currentNode->getNext() != nullptr) {
            currentNode = currentNode->getNext();
        };

        currentNode->setNext(newNode);
    };

    void pop() {
        if (this->getFirst() == nullptr) {
            throw std::runtime_error("Linked List is empty");
        };

        if (this->getFirst()->getNext() == nullptr) {
            delete this->getFirst();
            this->setFirst(nullptr);

            return;
        };

        Node<Type> *currentNode = this->getFirst();
        while (currentNode->getNext()->getNext() != nullptr) {
            currentNode = currentNode->getNext();
        }

        delete currentNode->getNext();
        currentNode->setNext(nullptr);
    }

    char get(int index) {
        if (this->getFirst() == nullptr) {
            throw std::runtime_error("Linked List is empty");
        };

        if ((index < 0) || (this->length() <= index)) {
            throw std::runtime_error("Index out of bound");
        };

        int currentIndex = 0;
        Node<Type> *currentNode = this->getFirst();
        while (currentIndex < index) {
            currentNode = currentNode->getNext();
            currentIndex++;
        };

        return currentNode->getValue();
    };

    void insert(Type value, int index) {
        if ((index < 0) || (this->length() <= index)) {
            throw std::runtime_error("Index out of bound");
        };

        Node<Type> *newNode = new Node<Type>(value);
        if ((this->getFirst() == nullptr) || (index == 0)) {
            newNode->setNext(this->getFirst());
            this->setFirst(newNode);

            return;
        };

        int currentIndex = 0;
        Node<Type> *currentNode = this->getFirst();
        while (currentIndex < (index - 1)) {
            currentNode = currentNode->getNext();
            currentIndex++;
        };

        newNode->setNext(currentNode->getNext());
        currentNode->setNext(newNode);
    };

    void swap(int firstIndex, int secondIndex) {
        if (firstIndex == secondIndex) {
            return;
        };

        if (this->getFirst() == nullptr) {
            throw std::runtime_error("Linked List is empty");
        };

        if ((firstIndex < 0) || (secondIndex < 0) || (this->length() <= firstIndex) || (this->length() <= secondIndex)) {
            throw std::runtime_error("Index out of bound");
        };

        int currentFirstIndex = 0;
        Node<Type> *previousFirstNode = nullptr;
        Node<Type> *currentFirstNode = this->getFirst();
        while (currentFirstIndex < firstIndex) {
            previousFirstNode = currentFirstNode;
            currentFirstNode = currentFirstNode->getNext();
            currentFirstIndex++;
        };

        int currentSecondIndex = 0;
        Node<Type> *previousSecondNode = nullptr;
        Node<Type> *currentSecondNode = this->getFirst();
        while (currentSecondIndex < secondIndex) {
            previousSecondNode = currentSecondNode;
            currentSecondNode = currentSecondNode->getNext();
            currentSecondIndex++;
        };

        if (previousFirstNode == nullptr) {
            this->setFirst(currentSecondNode);
        } else {
            previousFirstNode->setNext(currentSecondNode);
        };

        if (previousSecondNode == nullptr) {
            this->setFirst(currentFirstNode);
        } else {
            previousSecondNode->setNext(currentFirstNode);
        };

        Node<Type> *tempNode = currentFirstNode->getNext();
        currentFirstNode->setNext(currentSecondNode->getNext());
        currentSecondNode->setNext(tempNode);
    };

    void remove(int index) {
        if (this->getFirst() == nullptr) {
            throw std::runtime_error("Linked List is empty");
        };

        if ((index < 0) || (this->length() <= index)) {
            throw std::runtime_error("Index out of bound");
        };

        if (index == 0) {
            Node<Type> *tempNode = this->getFirst();
            this->setFirst(tempNode->getNext());

            delete tempNode;

            return;
        };

        int currentIndex = 0;
        Node<Type> *currentNode = this->getFirst();
        while (currentIndex < (index - 1)) {
            currentNode = currentNode->getNext();
            currentIndex++;
        };

        Node<Type> *tempNode = currentNode->getNext();
        currentNode->setNext(tempNode->getNext());

        delete tempNode;
    };
};

int main() {
    LinkedList<char> *linkedList1 = new LinkedList<char>();
    linkedList1->add('x');
    linkedList1->add('y');
    linkedList1->add('z');

    // LinkedList<int> *linkedList2 = new LinkedList<int>();
    // linkedList2->add(7);
    // linkedList2->add(8);
    // linkedList2->add(9);

    std::cout << std::endl;
    std::cout << std::endl;
    std::cout << std::endl;
    linkedList1->show();
    // linkedList2->show();

    // linkedList1->pop();
    // linkedList1->show();

    // std::cout << linkedList1->get(0) << std::endl;

    // linkedList1->insert('a', 0);
    // linkedList1->show();

    linkedList1->swap(0, 0);
    linkedList1->show();

    // linkedList1->remove(0);
    // linkedList1->show();

    std::cout << std::endl;
    std::cout << std::endl;
    std::cout << std::endl;
};