using System;
using System.Drawing;
using ImAText;

namespace ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = args[0];

            var bitmap = new Bitmap(file);

            var converter = new ImATextConverter(bitmap);
           
            Console.WriteLine(converter.GetTextifiedImage());
        }
    }
}
