using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Lab4
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        string filePath = $@"{Environment.CurrentDirectory}\input.txt";

        // Читання всього тексту з файлу з правильним кодуванням
        string text;
        try
        {
            text = File.ReadAllText(filePath, Encoding.UTF8);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Помилка при читанні файлу: {e.Message}");
            return;
        }

        Console.WriteLine("Вміст файлу:");
        Console.WriteLine(text);

        // Перетворення тексту в двовимірний масив нулів і одиниць
        int[,] matrix;
        try
        {
            matrix = ConvertTextToMatrix(text);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Помилка при обробці файлу: {e.Message}");
            return;
        }

        // Розділення на масиви нулів і одиниць
        var (zeros, ones) = SplitMatrix(matrix);

        // Вивід результатів
        Console.WriteLine("Масив нулів:");
        PrintArray(zeros);

        Console.WriteLine("Масив одиниць:");
        PrintArray(ones);

        // Використання LinkedList
        var linkedListMatrix = ConvertToLinkedListMatrix(matrix);
        Console.WriteLine("Вміст двовимірної колекції LinkedList:");
        PrintLinkedListMatrix(linkedListMatrix);

        // Розділення за допомогою LinkedList
        var (linkedZeros, linkedOnes) = SplitMatrix(linkedListMatrix);
        Console.WriteLine("Масив нулів (LinkedList):");
        PrintLinkedList(linkedZeros);

        Console.WriteLine("Масив одиниць (LinkedList):");
        PrintLinkedList(linkedOnes);
    }

    static int[,] ConvertTextToMatrix(string text)
    {
        string[] lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        int rows = lines.Length;
        int cols = lines[0].Split(new[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries).Length;

        int[,] matrix = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            string[] elements = lines[i].Split(new[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (elements.Length != cols)
            {
                throw new Exception("Некоректні дані у файлі. Кількість елементів у рядках не співпадає.");
            }

            for (int j = 0; j < cols; j++)
            {
                if (elements[j] != "0" && elements[j] != "1")
                {
                    throw new Exception("Некоректні дані у файлі. Дані повинні містити лише 0 або 1.");
                }

                matrix[i, j] = int.Parse(elements[j]);
            }
        }

        return matrix;
    }

    static (int[], int[]) SplitMatrix(int[,] matrix)
    {
        List<int> zeros = new List<int>();
        List<int> ones = new List<int>();

        foreach (int value in matrix)
        {
            if (value == 0)
            {
                zeros.Add(value);
            }
            else if (value == 1)
            {
                ones.Add(value);
            }
        }

        return (zeros.ToArray(), ones.ToArray());
    }

    static void PrintArray(int[] array)
    {
        Console.WriteLine(string.Join(", ", array));
    }

    static LinkedList<LinkedList<int>> ConvertToLinkedListMatrix(int[,] matrix)
    {
        LinkedList<LinkedList<int>> linkedListMatrix = new LinkedList<LinkedList<int>>();

        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            LinkedList<int> row = new LinkedList<int>();
            for (int j = 0; j < cols; j++)
            {
                row.AddLast(matrix[i, j]);
            }
            linkedListMatrix.AddLast(row);
        }

        return linkedListMatrix;
    }

    static void PrintLinkedListMatrix(LinkedList<LinkedList<int>> linkedListMatrix)
    {
        foreach (var row in linkedListMatrix)
        {
            Console.WriteLine(string.Join(", ", row));
        }
    }

    static (LinkedList<int>, LinkedList<int>) SplitMatrix(LinkedList<LinkedList<int>> matrix)
    {
        LinkedList<int> zeros = new LinkedList<int>();
        LinkedList<int> ones = new LinkedList<int>();

        foreach (var row in matrix)
        {
            foreach (var value in row)
            {
                if (value == 0)
                {
                    zeros.AddLast(value);
                }
                else if (value == 1)
                {
                    ones.AddLast(value);
                }
            }
        }

        return (zeros, ones);
    }

    static void PrintLinkedList(LinkedList<int> list)
    {
        Console.WriteLine(string.Join(", ", list));
    }
}
