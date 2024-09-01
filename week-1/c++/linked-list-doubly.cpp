#include <iostream>

template <typename Type>
class Node {
   private:
    Type value;
    Node<Type> *next;
    Node<Type> *previous;

   public:
    Node(Type value) {
        this->value = value;
        this->next = nullptr;
        this->previous = nullptr;
    };

    Type getValue() {
        return this->value;
    };

    Node<Type> *getNext() {
        return this->next;
    };

    Node<Type> *getPrevious() {
        return this->previous;
    };

    void setNext(Node<Type> *next) {
        this->next = next;
    };

    void setPrevious(Node<Type> *previous) {
        this->previous = previous;
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
    }

    int length() {
        int count = 0;
        Node<Type> *currentNode = this->getFirst();
        while (currentNode != nullptr) {
            currentNode = currentNode->getNext();
            count += 1;
        };

        return count;
    };

    void show() {
        Node<Type> *currentNode = this->getFirst();

        std::cout << "end -> ";
        while (currentNode != nullptr) {
            std::cout << currentNode->getValue() << " -> ";

            currentNode = currentNode->getNext();
        };

        std::cout << "end" << std::endl;
    };

    void showReverse() {
        Node<Type> *currentNode = this->getFirst();

        std::cout << "end -> ";
        if (currentNode != nullptr) {
            while (currentNode->getNext() != nullptr) {
                currentNode = currentNode->getNext();
            };

            while (currentNode != nullptr) {
                std::cout << currentNode->getValue() << " -> ";

                currentNode = currentNode->getPrevious();
            };
        };

        std::cout << "end" << std::endl;
    }

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

        newNode->setPrevious(currentNode);
        currentNode->setNext(newNode);
    };

    void pop() {
        if (this->getFirst() == nullptr) {
            return;
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
};

int main() {
    LinkedList<char> *linkedList1 = new LinkedList<char>();
    linkedList1->add('x');
    linkedList1->add('y');
    linkedList1->add('z');

    std::cout << std::endl;
    std::cout << std::endl;
    std::cout << std::endl;

    linkedList1->show();
    linkedList1->showReverse();
    std::cout << std::endl;

    linkedList1->pop();

    linkedList1->show();
    linkedList1->showReverse();
    std::cout << std::endl;

    linkedList1->pop();

    linkedList1->show();
    linkedList1->showReverse();
    std::cout << std::endl;

    linkedList1->add('z');

    linkedList1->show();
    linkedList1->showReverse();
    std::cout << std::endl;
};