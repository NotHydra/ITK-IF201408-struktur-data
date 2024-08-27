#include <iostream>

class Node {
   private:
    char value;
    Node *next;

   public:
    Node(char value) {
        this->value = value;
        this->next = nullptr;
    };

    char getValue() {
        return this->value;
    };

    Node *getNext() {
        return this->next;
    };

    void setNext(Node *next) {
        this->next = next;
    };
};

class LinkedList {
   private:
    Node *first;

   public:
    LinkedList() {
        this->first = nullptr;
    };

    Node *getFirst() {
        return this->first;
    };

    void show() {
        Node *currentNode = this->first;
        while (currentNode != nullptr) {
            std::cout << currentNode->getValue() << " -> ";

            currentNode = currentNode->getNext();
        };

        std::cout << "end" << std::endl;
    };

    void add(char value) {
        Node *newNode = new Node(value);

        if (this->first == nullptr) {
            this->first = newNode;

            return;
        };

        Node *currentNode = this->first;
        while (currentNode->getNext() != nullptr) {
            currentNode = currentNode->getNext();
        };

        currentNode->setNext(newNode);
    };

    void insert(char value, int index) {
        Node *newNode = new Node(value);

        if (index == 0) {
            if (this->first != nullptr) {
                newNode->setNext(this->first);
            }

            this->first = newNode;

            return;
        };

        int length = 0;
        Node *currentNode = this->first;
        while (currentNode != nullptr) {
            currentNode = currentNode->getNext();
            length++;
        };

        if ((length - 1) < index) {
            throw std::runtime_error("Index out of bound");
        };

        int position = 0;
        currentNode = this->first;
        while (position != (index - 1)) {
            currentNode = currentNode->getNext();
            position++;
        };

        newNode->setNext(currentNode->getNext());
        currentNode->setNext(newNode);
    };

    void remove(int index) {
        int length = 0;
        Node *currentNode = this->first;
        while (currentNode != nullptr) {
            currentNode = currentNode->getNext();
            length++;
        };

        if ((length - 1) < index) {
            throw std::runtime_error("Index out of bound");
        };

        if (index == 0) {
            this->first = this->first->getNext();

            return;
        };

        int position = 0;
        currentNode = this->first;
        while (position != (index - 1)) {
            currentNode = currentNode->getNext();
            position++;
        }

        currentNode->setNext(currentNode->getNext()->getNext());
    };
};

int main() {
    LinkedList *linkedList1 = new LinkedList();
    linkedList1->add('a');
    linkedList1->add('b');
    linkedList1->add('c');
    // linkedList1->add('d');
    // linkedList1->add('e');

    std::cout << std::endl;
    std::cout << std::endl;
    std::cout << std::endl;
    linkedList1->show();

    // linkedList1->insert('x', 1);
    // linkedList1->show();

    // linkedList1->insert('y', 2);
    // linkedList1->show();

    // linkedList1->insert('z', 3);
    // linkedList1->show();

    linkedList1->remove(0);
    linkedList1->show();

    std::cout << std::endl;
    std::cout << std::endl;
    std::cout << std::endl;
};