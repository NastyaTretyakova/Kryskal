using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication4
{
    public class Program
    {
        struct edge_t
        {
            public int x, y;          // направление
            public int w;            // вес ребра                
        }
        static int[] nodes = new int[100];
        static int last_n = 0;

        static void Main(string[] args)
        {
            edge_t[] edges = new edge_t[100];
            int NV;                  // Количество вершин в графе
            int NE;                 // Количество ребер в графе
            int i;
            Console.WriteLine("Введите количество вершин и ребер: ");
            NV = Convert.ToInt32(Console.ReadLine());
            NE = Convert.ToInt32(Console.ReadLine());
            for (i = 0; i < NV; i++)
            {
                nodes[i] = -1 - i;
            }
            Console.WriteLine("Введите матрицу: ");
            for (i = 0; i < NE; i++)
            {
                edges[i].x = Convert.ToInt32(Console.ReadLine());
                edges[i].y = Convert.ToInt32(Console.ReadLine());
                edges[i].w = Convert.ToInt32(Console.ReadLine());

            }
            Console.WriteLine("Минимальный остов: ");

            var sVes = from h in edges orderby h.w select h;
            foreach (var s in sVes)
            {
                for (i = 0; i < NE; i++)
                { // пока не прошли все ребра
                    int c = getColor(s.y);
                    if (getColor(s.x) != c)
                    {
                        // Если ребро соединяет вершины различных цветов-мы его добавляем
                        // и перекрашиваем вершины
                        nodes[last_n] = s.y;
                        Console.Write(s.x + " " + s.y + " " + s.w + " ");
                    }
                }
            }


            Console.ReadLine();
        }

        static public int getColor(int n)
        {
            int c;
            if (nodes[n] < 0)
            {
                return nodes[last_n = n];
            }
            c = getColor(nodes[n]);
            nodes[n] = last_n;
            return c;
        }
    }
}