using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomCordinateGenerator
{
    public class RandomGeneratorService
    {
        private int width = 1280;
        private int height = 1024;
        private int minWidth = 20;
        private int minHeight = 20;
        List<int> container;
        Random random = new Random();

        public List<int> generateCordinate()
        {
            int x = random.Next(0, width - minWidth);
            int y = random.Next(0, height - minHeight);

            int restWIdth = width - x;
            int restHeight = height - y;

            int widthAndHeight = random.Next(20, restWIdth < restHeight ? restWIdth : restHeight);

            container = new List<int>() { x, y, widthAndHeight };

            return container;
        }

    }
}
