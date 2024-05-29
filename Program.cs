using System;
using System.Linq;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("Виберіть завдання:");
            Console.WriteLine("1. Задача 1.1 - Знайти добуток від'ємних елементів вектора");
            Console.WriteLine("2. Задача 1.2 - Обчислити значення виразу s");
            Console.WriteLine("3. Задача 1.3 - Перетворити масив");
            Console.WriteLine("4. Задача 2.1 - Розмістити елементи непарних стовпців у порядку спадання");
            Console.WriteLine("5. Задача 2.2 - Переставити стовпці матриці");
            Console.WriteLine("6. Задача 2.3 - Знайти суму елементів у стовпцях");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Неправильне введення. Введіть правильне число.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Task1_1();
                    break;
                case 2:
                    Task1_2();
                    break;
                case 3:
                    Task1_3();
                    break;
                case 4:
                    Task2_1();
                    break;
                case 5:
                    Task2_2();
                    break;
                case 6:
                    Task2_3();
                    break;
                default:
                    Console.WriteLine("Невідомий вибір. Спробуйте ще раз.");
                    break;
            }

            Console.WriteLine("Бажаєте продовжити? (так/ні)");
            string continueInput = Console.ReadLine().ToLower();
            if (continueInput != "так")
                break;
        }
    }

    static void Task1_1()
    {
        Console.WriteLine("Введіть розмірність вектора:");
        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Неправильне введення. Введіть правильне додатне число.");
            return;
        }

        double[] vector = new double[n];
        Console.WriteLine("Введіть елементи вектора:");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"a[{i + 1}]: ");
            if (!double.TryParse(Console.ReadLine(), out vector[i]))
            {
                Console.WriteLine("Неправильне введення. Введіть правильне число.");
                i--;
            }
        }

        double product = vector.Where(e => e < 0).Aggregate(1.0, (acc, val) => acc * val);
        Console.WriteLine($"Добуток від'ємних елементів вектора: {product}");
    }

    static void Task1_2()
    {
        Console.WriteLine("Введіть розмірність векторів:");
        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Неправильне введення. Введіть правильне додатне число.");
            return;
        }

        double[] a = InputVector(n, "a");
        double[] b = InputVector(n, "b");
        double[] c = InputVector(n, "c");

        double dotProductAB = DotProduct(a, b);
        double dotProductAC = DotProduct(a, c);

        double s = 2 * dotProductAB - 3 * dotProductAC;
        Console.WriteLine($"Значення виразу s: {s}");
    }

    static double[] InputVector(int n, string vectorName)
    {
        double[] vector = new double[n];
        Console.WriteLine($"Введіть елементи вектора {vectorName}:");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"{vectorName}[{i}]: ");
            while (!double.TryParse(Console.ReadLine(), out vector[i]))
            {
                Console.WriteLine("Неправильне введення. Введіть правильне число.");
            }
        }
        return vector;
    }

    static double DotProduct(double[] x, double[] y)
    {
        return x.Zip(y, (xi, yi) => xi * yi).Sum();
    }

    static void Task1_3()
    {
        Console.WriteLine("Введіть розмірність масиву:");
        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Неправильне введення. Введіть правильне додатне число.");
            return;
        }

        double[] array = InputVector(n, "element");
        double[] transformedArray = TransformArray(array);

        Console.WriteLine("Перетворений масив:");
        foreach (var item in transformedArray)
        {
            Console.WriteLine(item);
        }
    }

    static double[] TransformArray(double[] array)
    {
        return array.OrderBy(x => Math.Abs(x) > 1).ThenBy(x => x).ToArray();
    }

    static void Task2_1()
    {
        Console.WriteLine("Введіть розміри матриці (рядки і стовпці):");
        if (!int.TryParse(Console.ReadLine(), out int rows) || rows <= 0 || 
            !int.TryParse(Console.ReadLine(), out int cols) || cols <= 0)
        {
            Console.WriteLine("Неправильне введення. Введіть правильні додатні числа.");
            return;
        }

        double[,] matrix = InputMatrix(rows, cols);
        Console.WriteLine("Початкова матриця:");
        PrintMatrix(matrix);

        double[,] transformedMatrix = TransformMatrix(matrix);
        Console.WriteLine("Перетворена матриця:");
        PrintMatrix(transformedMatrix);
    }

    static double[,] InputMatrix(int rows, int cols)
    {
        double[,] matrix = new double[rows, cols];
        Console.WriteLine("Виберіть метод введення: 1 - вручну, 2 - випадкові значення(діапазон від -9 до 9)");
        if (!int.TryParse(Console.ReadLine(), out int inputChoice) || (inputChoice != 1 && inputChoice != 2))
        {
            Console.WriteLine("Неправильний вибір. Введення буде виконано вручну.");
            inputChoice = 1;
        }

        if (inputChoice == 1)
        {
            Console.WriteLine("Введіть елементи матриці:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"matrix[{i},{j}]: ");
                    while (!double.TryParse(Console.ReadLine(), out matrix[i, j]))
                    {
                        Console.WriteLine("Неправильне введення. Введіть правильне число.");
                    }
                }
            }
        }
        else
        {
            Random rand = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rand.Next(-9, 10);
                    Console.Write($"matrix[{i},{j}]: {matrix[i, j]} ");
                }
                Console.WriteLine();
            }
        }
        return matrix;
    }

    static void PrintMatrix(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static double[,] TransformMatrix(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        double[,] result = new double[rows, cols];

        for (int j = 0; j < cols; j++)
        {
            if (j % 2 == 0)
            {
                double[] column = Enumerable.Range(0, rows).Select(i => matrix[i, j]).ToArray();
                Array.Sort(column);
                Array.Reverse(column);
                for (int i = 0; i < rows; i++)
                {
                    result[i, j] = column[i];
                }
            }
            else
            {
                for (int i = 0; i < rows; i++)
                {
                    result[i, j] = matrix[i, j];
                }
            }
        }
        return result;
    }

    static void Task2_2()
    {
        Console.WriteLine("Введіть розміри матриці (рядки і стовпці):");
        if (!int.TryParse(Console.ReadLine(), out int rows) || rows <= 0 || 
            !int.TryParse(Console.ReadLine(), out int cols) || cols <= 0)
        {
            Console.WriteLine("Неправильне введення. Введіть правильні додатні числа.");
            return;
        }

        int[,] matrix = InputMatrixInt(rows, cols);
        Console.WriteLine("Початкова матриця:");
        PrintMatrix(matrix);

        int[] characteristics = new int[cols];
        for (int j = 0; j < cols; j++)
        {
            characteristics[j] = Enumerable.Range(0, rows)
                .Where(i => matrix[i, j] < 0 && matrix[i, j] % 2 != 0)
                .Sum(i => Math.Abs(matrix[i, j]));
        }

        int[,] sortedMatrix = new int[rows, cols];
        var sortedIndices = characteristics
            .Select((value, index) => new { value, index })
            .OrderBy(x => x.value)
            .Select(x => x.index)
            .ToArray();

        for (int j = 0; j < cols; j++)
        {
            for (int i = 0; i < rows; i++)
            {
                sortedMatrix[i, j] = matrix[i, sortedIndices[j]];
            }
        }

        Console.WriteLine("Матриця з переставленими стовпцями:");
        PrintMatrix(sortedMatrix);
    }

    static int[,] InputMatrixInt(int rows, int cols)
    {
        int[,] matrix = new int[rows, cols];
        Console.WriteLine("Виберіть метод введення: 1 - вручну, 2 - випадкові значення(діапазон від -9 до 9)");
        if (!int.TryParse(Console.ReadLine(), out int inputChoice) || (inputChoice != 1 && inputChoice != 2))
        {
            Console.WriteLine("Неправильний вибір. Введення буде виконано вручну.");
            inputChoice = 1;
        }

        if (inputChoice == 1)
        {
            Console.WriteLine("Введіть елементи матриці:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"matrix[{i},{j}]: ");
                    while (!int.TryParse(Console.ReadLine(), out matrix[i, j]))
                    {
                        Console.WriteLine("Неправильне введення. Введіть правильне число.");
                    }
                }
            }
        }
        else
        {
            Random rand = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rand.Next(-9, 10);
                    Console.Write($"matrix[{i},{j}]: {matrix[i, j]} ");
                }
                Console.WriteLine();
            }
        }
        return matrix;
    }

    static void PrintMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static void Task2_3()
{
    Console.WriteLine("Введіть розміри матриці (рядки і стовпці):");
    if (!int.TryParse(Console.ReadLine(), out int rows) || rows <= 0 || 
        !int.TryParse(Console.ReadLine(), out int cols) || cols <= 0)
    {
        Console.WriteLine("Неправильне введення. Введіть правильні додатні числа.");
        return;
    }

    double[,] matrix = InputMatrixDouble(rows, cols);
    Console.WriteLine("Початкова матриця:");
    PrintMatrix(matrix);

    double[] columnSums = new double[cols];
    bool[] hasNegativeOnDiagonal = new bool[cols];

    for (int j = 0; j < cols; j++)
    {
        for (int i = 0; i < rows; i++)
        {
            if (i == j && matrix[i, j] < 0)
            {
                hasNegativeOnDiagonal[j] = true;
            }
            if (hasNegativeOnDiagonal[j])
            {
                columnSums[j] += matrix[i, j];
            }
        }
    }

    Console.WriteLine("Сума елементів у стовпцях з від'ємними елементами на діагоналі:");
    for (int j = 0; j < cols; j++)
    {
        if (hasNegativeOnDiagonal[j])
        {
            Console.WriteLine($"Стовпець {j + 1}: {columnSums[j]}");
        }
    }
}

static double[,] InputMatrixDouble(int rows, int cols)
{
    double[,] matrix = new double[rows, cols];
    Console.WriteLine("Виберіть метод введення: 1 - вручну, 2 - випадкові значення (діапазон від -9 до 9)");
    if (!int.TryParse(Console.ReadLine(), out int inputChoice) || (inputChoice != 1 && inputChoice != 2))
    {
        Console.WriteLine("Неправильний вибір. Введення буде виконано вручну.");
        inputChoice = 1;
    }

    if (inputChoice == 1)
    {
        Console.WriteLine("Введіть елементи матриці:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"matrix[{i},{j}]: ");
                while (!double.TryParse(Console.ReadLine(), out matrix[i, j]))
                {
                    Console.WriteLine("Неправильне введення. Введіть правильне число.");
                }
            }
        }
    }
    else
    {
        Random rand = new Random();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = rand.Next(-9, 10);
                Console.Write($"matrix[{i},{j}]: {matrix[i, j]} ");
            }
            Console.WriteLine();
        }
    }
    return matrix;
}

}
