import random
import time


def generateRandomArray(amount: int, minimum: int, maximum: int) -> list[int]:
    return [random.randint(minimum, maximum) for _ in range(amount)]


def insertionSort(array: list[int]) -> list[int]:
    sortedArray: list[int] = array.copy()

    for i in range(1, len(sortedArray)):
        key: int = sortedArray[i]
        j: int = i - 1

        while (j >= 0) and (key < sortedArray[j]):
            sortedArray[j + 1] = sortedArray[j]
            j -= 1

        sortedArray[j + 1] = key

    return sortedArray


def mergeSort(array: list[int]) -> list[int]:
    if len(array) <= 1:
        return array

    mid: int = len(array) // 2

    left: int = mergeSort(array[:mid])
    right: int = mergeSort(array[mid:])

    return combineArray(left, right)


def combineArray(left: int, right: int) -> list[int]:
    sortedArray: list[int] = []
    i: int = 0
    j: int = 0

    while i < len(left) and j < len(right):
        if left[i] < right[j]:
            sortedArray.append(left[i])
            i += 1
        else:
            sortedArray.append(right[j])
            j += 1

    sortedArray.extend(left[i:])
    sortedArray.extend(right[j:])

    return sortedArray


originalArray: list[int] = generateRandomArray(1000, 1, 1000)

print("Array Before Insertion Sort:", originalArray)
print()

insertionSortStartTime: float = time.time()
insertionSortArray: list[int] = insertionSort(originalArray)
insertionSortEndTime: float = time.time()

print("Array After Insertion Sort:", insertionSortArray)
print(
    f"Insertion Sort Execution Time: {insertionSortEndTime - insertionSortStartTime:.10f} seconds"
)

print()
print()
print()

print("Array Before Merge Sort:", originalArray)
print()

mergeSortStartTime: float = time.time()
mergeSortArray: list[int] = mergeSort(originalArray)
mergeSortEndTime: float = time.time()

print("Array After Merge Sort:", mergeSortArray)
print(
    f"Merge Sort Execution Time: {mergeSortEndTime - mergeSortStartTime:.10f} seconds"
)

print()
print()
print()

print("Execution Time Summary:")
print(f"Insertion Sort: {insertionSortEndTime - insertionSortStartTime:.10f} seconds")
print(f"Merge Sort: {mergeSortEndTime - mergeSortStartTime:.10f} seconds")
