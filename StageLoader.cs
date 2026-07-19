using System;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mafia
{
    internal static class StageLoader
    {
        public static Stage Load(string fileName)
        {
            using Stream stream = TitleContainer.OpenStream($"Content/{fileName}");
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            try
            {
                string title = reader.ReadLine();
                int numRows = int.Parse(reader.ReadLine());
                int numCols = int.Parse(reader.ReadLine());
                string[] source = new string[numRows];

                for (int row = 0; row < numRows; row++)
                {
                    source[row] = reader.ReadLine();
                }

                return new Stage(fileName, title, numRows, numCols, source);
            }
            catch (FormatException error)
            {
                throw new InvalidDataException($"Failed to parse stage: {fileName}", error);
            }
            catch (OverflowException error)
            {
                throw new InvalidDataException($"Stage is too large: {fileName}", error);
            }
        }
    }
}
