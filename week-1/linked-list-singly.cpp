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

    int length() {
        int count = 0;
        Node<Type> *currentNode = this->first;
        while (currentNode != nullptr) {
            currentNode = currentNode->getNext();
            count += 1;
        };

        return count;
    };

    void show() {
        Node<Type> *currentNode = this->first;
        while (currentNode != nullptr) {
            std::cout << currentNode->getValue() << " -> ";

            currentNode = currentNode->getNext();
        };

        std::cout << "end" << std::endl;
    };

    void add(char value) {
        Node<Type> *newNode = new Node<Type>(value);
        if (this->first == nullptr) {
            this->first = newNode;

            return;
        };

        Node<Type> *currentNode = this->first;
        while (currentNode->getNext() != nullptr) {
            currentNode = currentNode->getNext();
        };

        currentNode->setNext(newNode);
    };

    void pop() {
        if (this->first == nullptr) {
            return;
        };

        if (this->first->getNext() == nullptr) {
            delete this->first;
            this->first = nullptr;

            return;
        };

        Node<Type> *currentNode = this->first;
        while (currentNode->getNext()->getNext() != nullptr) {
            currentNode = currentNode->getNext();
        }

        delete currentNode->getNext();
        currentNode->setNext(nullptr);
    }

    char get(int index) {
        if (this->length() <= index) {
            throw std::runtime_error("Index out of bound");
        };

        int currentIndex = 0;
        Node<Type> *currentNode = this->first;
        while (currentIndex < index) {
            currentNode = currentNode->getNext();
            currentIndex++;
        };

        return currentNode->getValue();
    };

    void insert(char value, int index) {
        if (index < 0) {
            throw std::runtime_error("Index out of bound");
        };

        Node<Type> *newNode = new Node<Type>(value);
        if (index == 0) {
            newNode->setNext(this->first);
            this->first = newNode;

            return;
        };

        if (this->length() <= index) {
            throw std::runtime_error("Index out of bound");
        };

        int currentIndex = 0;
        Node<Type> *currentNode = this->first;
        while (currentIndex < (index - 1)) {
            currentNode = currentNode->getNext();
            currentIndex++;
        };

        newNode->setNext(currentNode->getNext());
        currentNode->setNext(newNode);
    };

    void remove(int index) {
        if (this->first == nullptr) {
            return;
        };

        if (index < 0) {
            throw std::runtime_error("Index out of bound");
        };

        if (index == 0) {
            Node<Type> *tempNode = this->first;
            this->first = tempNode->getNext();

            delete tempNode;

            return;
        };

        if (this->length() <= index) {
            throw std::runtime_error("Index out of bound");
        };

        int currentIndex = 0;
        Node<Type> *currentNode = this->first;
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

    LinkedList<int> *linkedList2 = new LinkedList<int>();
    linkedList2->add(7);
    linkedList2->add(8);
    linkedList2->add(9);

    std::cout << std::endl;
    std::cout << std::endl;
    std::cout << std::endl;
    linkedList1->show();
    linkedList2->show();

    // linkedList1->pop();
    // linkedList1->show();

    // std::cout << linkedList1->get(0) << std::endl;

    // linkedList1->insert('a', 0);
    // linkedList1->show();

    // linkedList1->remove(0);
    // linkedList1->show();

    std::cout << std::endl;
    std::cout << std::endl;
    std::cout << std::endl;
};