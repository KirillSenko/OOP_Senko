using System;
using System.Text;
using System.Text.RegularExpressions;

public class RationFraction
{
    public readonly int Nominator;
    public readonly int Denominator;

    public RationFraction(int nominator, int denominator)
    {
        if (denominator == 0)
        {
            throw new Exception("--Знаменник не може бути нульовим!--");
        }

        if (denominator > 0)
        {
            this.Nominator = nominator;
            this.Denominator = denominator;
        }
        else
        {
            this.Nominator = -nominator;
            this.Denominator = -denominator;
        }

        int nsd = NSD(Math.Abs(nominator), Math.Abs(denominator));

        if (nsd > 1)
        {
            this.Nominator /= nsd;
            this.Denominator /= nsd;
        }
    }

    public string GetFraction()
    {
        return Nominator + "/" + Denominator;
    }

    public static RationFraction operator -(RationFraction rationFraction)
    {
        return new RationFraction(-rationFraction.Nominator, rationFraction.Denominator);
    }

    public static double ToDecimal(RationFraction rF)
    {
        return (double)(rF.Nominator) / (double)(rF.Denominator);
    }

    public static RationFraction FromDecimal(double d)
    {
        string str = d.ToString();

        if (str.IndexOf('.') == -1)
        {
            return new RationFraction((int)Double.Parse(str), 1);
        }
        return new RationFraction((int)Double.Parse(str.Replace(".", "")),
                                  (int)Math.Pow(10, str.Substring(str.IndexOf('.') + 1).Length));
    }

    public static int NSD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static int NSK(int a, int b)
    {
        return a / NSD(a, b) * b;
    }
}

public class RomanNum
{
    public readonly string AsStr;

    private static int[] digitsValues = { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };
    private static string[] romanDigits = { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };

    public RomanNum(int numb)
    {
        if (numb <= 0 || numb > 3999)
        {
            throw new Exception("--Число повинно бути в діапазоні від 1 до 3999!--");
        }
        AsStr = ToRoman(numb);
    }

    public RomanNum(string romanStr)
    {
        if (!IsValidRoman(romanStr))
        {
            throw new Exception("--Введене римське число не є валідним!--");
        }
        AsStr = romanStr;
    }

    public static string ToRoman(int numb)
    {
        StringBuilder result = new StringBuilder();

        while (numb > 0)
        {
            for (int i = digitsValues.Length - 1; i >= 0; i--)
                if (numb / digitsValues[i] >= 1)
                {
                    numb -= digitsValues[i];
                    result.Append(romanDigits[i]);
                    break;
                }
        }
        return result.ToString();
    }

    public static int FromRoman(RomanNum roman)
    {
        int numb = 0;

        for (int i = 0; i < roman.AsStr.Length; i++)
        {
            for (int j = 0; j < romanDigits.Length; j++)
            {
                if (roman.AsStr[i] == romanDigits[j][0])
                {
                    if (i + 1 < roman.AsStr.Length && j + 3 < romanDigits.Length &&
                        (roman.AsStr[i + 1] == romanDigits[j + 1][1] ||
                         roman.AsStr[i + 1] == romanDigits[j + 3][1]))
                    {
                        if (roman.AsStr[i + 1] == romanDigits[j + 1][1])
                        {
                            numb += digitsValues[j + 1];
                        }
                        else
                        {
                            numb += digitsValues[j + 3];
                        }
                        i++;
                    }
                    else
                    {
                        numb += digitsValues[j];
                    }
                    break;
                }
            }
        }

        return numb;
    }

    public static bool IsValidRoman(string romanStr)
    {
        if (Regex.IsMatch(romanStr, "IIII|VV|XXXX|LL|CCCC|DD|MMMM"))
        {
            return false;
        }
        if (Regex.IsMatch(romanStr, "(I{4,}|X{4,}|C{4,}|M{4,})"))
        {
            return false;
        }
        if (Regex.IsMatch(romanStr, "(IL|IC|ID|IM|VX|VL|VC|VD|VM|LC|LD|LM|DM)"))
        {
            return false;
        }
        return Regex.IsMatch(romanStr, "^[IVXLCDM]+$");
    }
}

interface IRationOperations
{
    RationFraction Addition(RationFraction num1, RationFraction num2);
    RationFraction Subtract(RationFraction num1, RationFraction num2);
}

interface IRomanOperations
{
    RomanNum Addition(RomanNum num1, RomanNum num2);
    RomanNum Subtract(RomanNum num1, RomanNum num2);
}

public class Operations : IRationOperations, IRomanOperations
{
    public RationFraction Addition(RationFraction num1, RationFraction num2)
    {
        if (num1.Denominator == num2.Denominator)
        {
            return new RationFraction(num1.Nominator + num2.Nominator, num1.Denominator);
        }
        int nsk = RationFraction.NSK(Math.Abs(num1.Denominator), Math.Abs(num2.Denominator));

        return new RationFraction(nsk / num1.Denominator * num1.Nominator +
                                  nsk / num2.Denominator * num2.Nominator, nsk);
    }

    public RomanNum Addition(RomanNum num1, RomanNum num2)
    {
        int result = RomanNum.FromRoman(num1) + RomanNum.FromRoman(num2);
        if (result > 3999)
        {
            throw new Exception("--Результат додавання перевищує максимальне значення римського числа (3999)--");
        }
        return new RomanNum(result);
    }

    public RationFraction Subtract(RationFraction num1, RationFraction num2)
    {
        return Addition(num1, -num2);
    }

    public RomanNum Subtract(RomanNum num1, RomanNum num2)
    {
        int result = RomanNum.FromRoman(num1) - RomanNum.FromRoman(num2);
        if (result <= 0)
        {
            throw new Exception("--Результат віднімання менший або дорівнює нулю, що не допускається у римських числах--");
        }
        return new RomanNum(result);
    }
}

public class TestClass
{
    public static int InputInt(string instruction, int from, int to)
    {
        while (true)
        {
            Console.WriteLine(instruction);
            string buffer = Console.ReadLine();
            if (int.TryParse(buffer, out int value) && value >= from && value <= to)
            {
                return value;
            }
            Console.WriteLine("Введено некоректне значення або значення не в діапазоні!\n");
        }
    }

    public static RationFraction InputFraction()
    {
        while (true)
        {
            Console.WriteLine("Введіть раціональний дріб у вигляді: [чисельник]/[знаменник]. Наприклад: 1/3, -5/9 і т.п.");
            string buffer = Console.ReadLine();
            string[] parts = buffer.Split('/');
            if (parts.Length == 2 && int.TryParse(parts[0], out int nominator) && int.TryParse(parts[1], out int denominator))
            {
                try
                {
                    return new RationFraction(nominator, denominator);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Введено некоректне значення!\n");
        }
    }

    public static RomanNum InputRoman()
    {
        while (true)
        {
            Console.WriteLine("Введіть римське число. Наприклад: XVII, III і т.п.");
            string buffer = Console.ReadLine();
            try
            {
                return new RomanNum(buffer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public static void Menu()
    {
        Operations operations = new Operations();

        while (true)
        {
            int choice = InputInt("Меню:\n1. Додавання раціональних чисел\n2. Віднімання раціональних чисел\n3. Додавання римських чисел\n4. Віднімання римських чисел\n0. Вийти\nВаш вибір:", 0, 4);

            if (choice == 0)
            {
                Console.WriteLine("Завершення роботи програми.");
                break;
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Введіть перший дріб:");
                    RationFraction fraction1 = InputFraction();
                    Console.WriteLine("Введіть другий дріб:");
                    RationFraction fraction2 = InputFraction();
                    Console.WriteLine($"{fraction1.GetFraction()} + {fraction2.GetFraction()} = {operations.Addition(fraction1, fraction2).GetFraction()}");
                    break;
                case 2:
                    Console.WriteLine("Введіть перший дріб:");
                    RationFraction fraction3 = InputFraction();
                    Console.WriteLine("Введіть другий дріб:");
                    RationFraction fraction4 = InputFraction();
                    Console.WriteLine($"{fraction3.GetFraction()} - {fraction4.GetFraction()} = {operations.Subtract(fraction3, fraction4).GetFraction()}");
                    break;
                case 3:
                    Console.WriteLine("Введіть перше римське число:");
                    RomanNum roman1 = InputRoman();
                    Console.WriteLine("Введіть друге римське число:");
                    RomanNum roman2 = InputRoman();
                    try
                    {
                        Console.WriteLine($"{roman1.AsStr} + {roman2.AsStr} = {operations.Addition(roman1, roman2).AsStr}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 4:
                    Console.WriteLine("Введіть перше римське число:");
                    RomanNum roman3 = InputRoman();
                    Console.WriteLine("Введіть друге римське число:");
                    RomanNum roman4 = InputRoman();
                    try
                    {
                        Console.WriteLine($"{roman3.AsStr} - {roman4.AsStr} = {operations.Subtract(roman3, roman4).AsStr}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
            }
        }
    }

    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        Menu();
    }
}
