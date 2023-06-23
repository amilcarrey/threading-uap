using System;
namespace ClaseHilos
{
    public class Tabla
    {
        public static void PrintLine()
        {
            Console.WriteLine(new string('-', 73));
        }

        public static void PrintRow(params string[] columns)
        {
            int width = (73 - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        public static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? string.Concat(text.AsSpan(0, width - 3), "...") : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}

