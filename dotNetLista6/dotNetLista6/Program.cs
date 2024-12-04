using System;


class Program
{
    static void Main(string[] args)
    {
        //zad1
        Console.WriteLine("Zadanie 1\n");

        var personalData = ("Jan", "Kowalski", 23, 1234.5);
        Console.WriteLine(personalData);
        PrintPersonalData(personalData);

        //zad2
        Console.WriteLine("\n\nZadanie 2\n");

        string @class = "zmienna o nazwie class";
        Console.WriteLine(@class);


        //zad3
        Console.WriteLine("\n\nZadanie 3\n");

        int[] myArray = { 5, 2, 8, 0, -1,2};
        Console.Write($"myArray: ");
        PrintArray(myArray);

        //sort
        Array.Sort(myArray);
        PrintArray(myArray);

        //reverse
        Array.Reverse(myArray);
        PrintArray(myArray);

        //fill
        Array.Fill(myArray, 16);
        PrintArray(myArray);

        //resize
        Array.Resize(ref myArray, 10);
        myArray[6] = 7;
        myArray[7] = 8;
        myArray[8] = 9;
        myArray[9] = 10;
        PrintArray(myArray);


        //forEach
        Array.ForEach(myArray, number => Console.Write($"{number * 2}, "));


        //zad4
        Console.WriteLine("\n\nZadanie 4\n");
        var person1 = new { name = "John", surname = "Smith", age = 32, salary = 5432.1 };
        PrintAnonymusData(person1);

        //zad5
        Console.WriteLine("\n\nZadanie 5\n");
        DrawCard("Jan");
        DrawCard("John","Smith");
        DrawCard("Marry",borderChar: '*', cardWidth: 10);
        DrawCard("Marry","Smith", '#',3,15);

        //zad6
        Console.WriteLine("\n\nZadanie 6\n");
        var result = CountMyTypes(7, "Hello World", "hi", -2, 8, 15.2, "bye bye", -1.1, true, 'a','x');
        Console.WriteLine($"even int: {result.even}, positive double: {result.positiveDoubles}, string at least 5 chars: {result.stringFives}, other types: {result.other}");


    }

    static void PrintPersonalData((string Name,string Surname,int Age,double Salary) person)
    {
        //1st
        System.Console.WriteLine($"Name: {person.Name}, Surname: {person.Surname}, Age: {person.Age}, Salary: {person.Salary} PLN");

        //2nd
        System.Console.WriteLine($"Name: {person.Item1}, Surname: {person.Item2}, Age: {person.Item3}, Salary: {person.Item4} PLN");

        //3rd
        var(imie, nazwisko, wiek, placa) = person;
        System.Console.WriteLine($"Imię: {imie}, Nazwisko: {nazwisko}, Wiek: {wiek}, Płaca: {placa} PLN");
    }

    static void PrintAnonymusData(dynamic person1)
    {
        Console.WriteLine($"Name: {person1.name}, Surname: {person1.surname}, Age: {person1.age}, Salary: {person1.salary} PLN");
    }



    static void PrintArray(int[] array)
    {
        System.Console.WriteLine(string.Join(", ", array));
    }

    static void DrawCard(string firstLine, string secondLine = "", char borderChar = 'x', int borderWidth = 1, int cardWidth = 20)
    {
        Console.WriteLine();
        borderWidth = Math.Max(borderWidth, 1);

        int maxContentWidth = Math.Max(firstLine.Length, secondLine.Length);
        cardWidth = Math.Max(cardWidth, maxContentWidth + 2 * borderWidth);

        string upDownBorder = new string(borderChar, cardWidth);
 
        for(int i = 0; i < borderWidth; i++) 
            Console.WriteLine(upDownBorder);

        DrawBodyLine(firstLine, borderChar, borderWidth, cardWidth);

        if(!string.IsNullOrEmpty(secondLine))
            DrawBodyLine(secondLine,borderChar, borderWidth, cardWidth);

        for (int i = 0; i < borderWidth; i++)
            Console.WriteLine(upDownBorder);
        Console.WriteLine();
    }

    static void DrawBodyLine(string text, char borderChar, int borderWidth, int cardWidth)
    {
        int contentWidth = cardWidth - 2 * borderWidth;
        int paddingLeft = (contentWidth - text.Length) / 2;
        int paddingRight = contentWidth - text.Length - paddingLeft;

        Console.Write(new string(borderChar,borderWidth));
        Console.Write(new string(' ', paddingLeft));
        Console.Write(text);
        Console.Write(new string(' ', paddingRight));
        Console.WriteLine(new string(borderChar, borderWidth));
    }

    static (int even, int positiveDoubles, int stringFives, int other) CountMyTypes(params object[]items)
    {
        int even = 0;
        int positiveDoubles = 0;
        int stringFives = 0;
        int other = 0;

        foreach (object item in items)
        {
            switch (item)
            {
                case int i when i % 2 == 0:
                    even++;
                    break;
                case double d when d > 0:
                    positiveDoubles++;
                    break;
                case string s when s.Length >= 5:
                    stringFives++;
                    break;
                default:
                    if(!(item is int || item is double || item is string))
                        other++;
                    break;
            }
        }

        return (even, positiveDoubles, stringFives, other);
    }
}
