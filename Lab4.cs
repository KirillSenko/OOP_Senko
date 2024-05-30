using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string filePath = $@"{Environment.CurrentDirectory}\input.txt";
        string text = File.ReadAllText(filePath);
        
        // Перетворення тексту у двовимірний масив
        int[,] array = ConvertTo2DArray(text);

        // Виклик методу розділення масиву
        (int[] zerosArray, int[] onesArray) = SplitArray(array);

        // Виклик методу виводу масивів на екран
        PrintArray(zerosArray, "Масив з нулями:");
        PrintArray(onesArray, "Масив з одиницями:");
        
        // Виконання завдання за допомогою LinkedList<LinkedList<T>>
        LinkedList<LinkedList<int>> linkedListArray = ConvertToLinkedList(array);
        var (zerosLinkedList, onesLinkedList) = SplitLinkedList(linkedListArray);
        
        PrintLinkedList(zerosLinkedList, "LinkedList з нулями:");
        PrintLinkedList(onesLinkedList, "LinkedList з одиницями:");
    }

    static int[,] ConvertTo2DArray(string text)
    {
        string[] lines = text.Trim().Split('\n');
        int rows = lines.Length;
        int cols = lines[0].Split(' ').Length;
        int[,] array = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            string[] values = lines[i].Trim().Split(' ');
            for (int j = 0; j < values.Length; j++)
            {
                array[i, j] = int.Parse(values[j]);
            }
        }

        return array;
    }

    static (int[], int[]) SplitArray(int[,] array)
    {
        List<int> zeros = new List<int>();
        List<int> ones = new List<int>();

        foreach (int value in array)
        {
            if (value == 0)
                zeros.Add(value);
            else if (value == 1)
                ones.Add(value);
        }

        return (zeros.ToArray(), ones.ToArray());
    }

    static void PrintArray(int[] array, string message)
    {
        Console.WriteLine(message);
        Console.WriteLine(string.Join(", ", array));
    }

    static LinkedList<LinkedList<int>> ConvertToLinkedList(int[,] array)
    {
        LinkedList<LinkedList<int>> linkedListArray = new LinkedList<LinkedList<int>>();

        for (int i = 0; i < array.GetLength(0); i++)
        {
            LinkedList<int> row = new LinkedList<int>();
            for (int j = 0; j < array.GetLength(1); j++)
            {
                row.AddLast(array[i, j]);
            }
            linkedListArray.AddLast(row);
        }

        return linkedListArray;
    }

    static (LinkedList<int>, LinkedList<int>) SplitLinkedList(LinkedList<LinkedList<int>> linkedListArray)
    {
        LinkedList<int> zeros = new LinkedList<int>();
        LinkedList<int> ones = new LinkedList<int>();

        foreach (var row in linkedListArray)
        {
            foreach (int value in row)
            {
                if (value == 0)
                    zeros.AddLast(value);
                else if (value == 1)
                    ones.AddLast(value);
            }
        }

        return (zeros, ones);
    }

    static void PrintLinkedList(LinkedList<int> linkedList, string message)
    {
        Console.WriteLine(message);
        Console.WriteLine(string.Join(", ", linkedList));
    }
}
