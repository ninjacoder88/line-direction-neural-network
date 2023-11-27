using System.Drawing;

namespace LineDirectionNeuralNetwork
{
    internal sealed class ImageReader
    {
        public double[][] Read()
        {
            Bitmap bitmap = new Bitmap(@"D:\git\line-direction-neural-network\data\4by4.png");

            double[][] grid = new double[bitmap.Height][];
            for(int row = 0; row < bitmap.Height; row++)
            {
                grid[row] = new double[bitmap.Width];
                for(int column = 0; column < bitmap.Width; column++)
                {
                    grid[row][column] = ConvertRGBToPercent(bitmap.GetPixel(row, column));
                }
            }

            return grid;
        }

        private double ConvertRGBToPercent(Color color)
        {
            if(color.R < 128)
                return ((127 - color.R) / 127) * -1;
            else
                return (color.R - 128) / 127;
        }
    }
}
