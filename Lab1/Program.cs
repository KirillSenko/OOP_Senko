using System;

public class TCircle
{
    public double Radius { get; set; }

    public TCircle() // конструктор за замовчуванням
    {
        Radius = 0.0;
    }

    public TCircle(double radius) // конструктор з параметром
    {
        Radius = radius;
    }

    public TCircle(TCircle otherCircle) // конструктор копіювання
    {
        Radius = otherCircle.Radius;
    }

    public void InputRadius() // введення радіусу 
    {
        while (true)
        {
            Console.Write("Введіть радіус кола: ");
            string input = Console.ReadLine();

            if (double.TryParse(input, out double radius) && radius >= 0)
            {
                Radius = radius;
                break;
            }

            Console.WriteLine("\nВведіть коректне додатнє число!\n");
        }
    }

    public void OutputRadius() // виведення радіусу
    {
        Console.WriteLine($"\nРадіус: {Radius}\n");
    }

    public virtual double GetSquare() // отримати площу кола
    {
        return Math.PI * Radius * Radius;
    }

    public virtual double GetSectorSquare(double sectorAngleInDegrees) // отримати площу сектора кола
    {
        return GetSquare() * (sectorAngleInDegrees / 360.0);
    }

    public double GetCircuit() // отримати довжину кола
    {
        return 2 * Math.PI * Radius;
    }

    public int CompareWith(TCircle otherCircle) // порівняти коло з іншим колом
    {
        return Radius.CompareTo(otherCircle.Radius);
    }

    public static TCircle operator+(TCircle firstCircle, TCircle secondCircle) // перевантажуємо оператор додавання кіл (радіусів)
    {
        return new TCircle(firstCircle.Radius + secondCircle.Radius);
    }

    public static TCircle operator-(TCircle firstCircle, TCircle secondCircle) // перевантажуємо оператор віднімання кіл (радіусів)
    {
        return new TCircle(firstCircle.Radius - secondCircle.Radius);
    }

    public static TCircle operator*(TCircle circle, double number) // перевантаження оператора множення радіуса на число
    {
        return new TCircle(circle.Radius * number);
    }
}

public class TCone : TCircle
{
    public double Height { get; set; }

    public TCone() : base() // конструктор за замовчуванням
    {
        Height = 0.0;
    }

    public TCone(double radius, double height) : base(radius) // конструктор з параметрами
    {
        Height = height;
    }

    public TCone(TCone other) : base(other) // конструктор копіювання
    {
        Height = other.Height;
    }

    public double GetConeVolume() // отримати об'єм конуса
    {
        return (1.0 / 3.0) * GetSquare() * Height;
    }

    public override double GetSquare() // площа повної поверхні конуса
    {
        return base.GetSquare() + Math.PI * Radius * Math.Sqrt(Math.Pow(Radius/2, 2) + Math.Pow(Height, 2)) ;
    }

    public override double GetSectorSquare(double sectorAngleInDegrees) // площа сектору повної поверхні конуса
    {
        return GetSquare() * (sectorAngleInDegrees / 360.0);
    }
}

public class TestClass
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("Демонструємо конструктор за замовчуванням для TCircle:");

        TCircle myCircle1 = new TCircle();
        myCircle1.OutputRadius();

        Console.WriteLine("Тепер вводимо радіус з клавіатури:");
        myCircle1.InputRadius();
        myCircle1.OutputRadius();

        Console.WriteLine("Демонструємо конструктор з параметром (випадкове значення) для TCircle:");
        TCircle myCircle2 = new TCircle(new Random().Next(10, 51)); // генерує випадкове ціле число від 10 до 50
        myCircle2.OutputRadius();

        Console.WriteLine("Демонструємо конструктор копіювання для TCircle:");
        TCircle myCircle3 = new TCircle(myCircle2);
        myCircle3.OutputRadius();

        double angle = new Random().Next(0, 361); // генерує випадкове ціле число від 0 до 360
        Console.WriteLine("\nОбчислимо площу нашого кола: " + myCircle3.GetSquare());
        Console.WriteLine("Тепер обчислимо площу сектора нашого кола (з випадковим кутом сектора): " + myCircle3.GetSectorSquare(angle) + " де кут сектора: " + angle);
        Console.WriteLine("Обчислимо довжину нашого кола: " + myCircle3.GetCircuit());

        Console.WriteLine("Порівняємо наше коло з першим колом: ");

        switch (myCircle3.CompareWith(myCircle1))
        {
            case -1:
                Console.WriteLine("Наше коло менше за інше!");
                break;
            case 0:
                Console.WriteLine("Кола рівні!");
                break;
            case 1:
                Console.WriteLine("Наше коло більше за інше!");
                break;

        }

        double numb = new Random().NextDouble() * 10.0 + 1.0;
        Console.WriteLine("\nСтворимо нове коло, використавши перевантажений оператор * множення нашого кола (радіуса) на випадкове число : " + numb);
        TCircle myCircle4 = myCircle3 * numb;
        myCircle4.OutputRadius();

        double height = new Random().NextDouble() * 10.0 + 1.0;
        Console.WriteLine("\nНа основі нового кола створимо новий конус з випадковою висотою: " + height);

        TCone myCone = new TCone(myCircle4.Radius, height);
        myCone.OutputRadius();

        Console.WriteLine("Об'єм конуса: " + myCone.GetConeVolume());
        Console.WriteLine("Площа повної поверхні конуса: " + myCone.GetSquare());
        
        angle = new Random().NextDouble() * 360.0;
        Console.WriteLine("Площа сектора повної поверхні конуса: " + myCone.GetSectorSquare(angle) + " де кут: " + angle);

    }
}