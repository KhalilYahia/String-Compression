
using System.Text;
using System.Text.RegularExpressions;

class StringCompression
{
    // Метод для сжатия строки
    public static string Compress(string input)
    {
        
        if (string.IsNullOrEmpty(input) || !Regex.IsMatch(input, "^[a-z]+$"))
            throw new ArgumentException("Строка должна содержать только строчные латинские буквы.");

        StringBuilder compressedResult = new StringBuilder();
        int count = 1;

        // Проходим по строке и считаем количество повторяющихся символов
        for (int i = 0; i < input.Length-1; i++)
        {
            if (input[i] == input[i + 1])
            {
                count++;
            }
            else
            {
                compressedResult.Append(input[i]);
                compressedResult.Append(count > 1 ? count.ToString() : "");
               
                count = 1;
            }
        }
        compressedResult.Append(input.Last());
        compressedResult.Append(count > 1 ? count.ToString() : "");
        
        return compressedResult.ToString();
    }

    // Метод для восстановления исходной строки
    public static string Decompress(string compressed)
    {
        if (string.IsNullOrEmpty(compressed) || !Regex.IsMatch(compressed, "^[a-z0-9]+$"))
            throw new ArgumentException("Строка должна содержать только строчные латинские буквы и цифры.");

        StringBuilder decompressed = new StringBuilder();
        Span<char> span = new Span<char>(compressed.ToCharArray());//;.AsSpan(0);//.AsSpan();

        int i = 0;
        while (i < span.Length)
        {
            char letter = span[i++];
            int count = 0;

            // Читаем число, если оно есть
            while (i < span.Length && char.IsDigit(span[i]))
            {
                count = count * 10 + (span[i++] - '0');
            }

            decompressed.Append(letter, count > 0 ? count : 1);
        }

        return decompressed.ToString();
    }


    static void Main()
    {

        Console.Write("Enter string: ");
        string original = Console.ReadLine();  // aaabccnnnnnnnnnnnne

        string compressed = Compress(original);
        string decompressed = Decompress(compressed);

        // Вывод результатов
        Console.WriteLine("Original: " + original);
        Console.WriteLine("Compressed: " + compressed);
        Console.WriteLine("Decompressed: " + decompressed);
     

    }
}
