using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kruskal
{
    public class Program
    {
        struct edge_t
        {
            public int x, y;     // направление
            public int w;        // вес ребра                
        }
        static int[] nodes = new int[100];    //
        static int last_n = 0;                //   

        static void Main(string[] args)
        {
            edge_t[] edges = new edge_t[100];
            int NV;      // Количество вершин в графе
            int NE;      // Количество ребер в графе
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
                string[] OutS = Console.ReadLine().Split(' ');    //массив вершин и веса ребра между ними
                edges[i].x = Convert.ToInt32(OutS[0]);            //вершина "начало"
                edges[i].y = Convert.ToInt32(OutS[1]);            //вершина "конец"
                edges[i].w = Convert.ToInt32(OutS[2]);            //вес ребра между вершинами "начало" и "конец"

            }
            Console.WriteLine("Минимальный остов: ");

            var sVes = from h in edges orderby h.w select h;    //сортируем ребра по весу (ро возрастанию)
            foreach (var s in sVes)           //для каждого ребра
            {
                for (i = 0; i < NE; i++)
                { // пока не прошли все ребра
                    int c = getColor(s.y);     //присваиваем цвет ребра"конец" переменной с
                    if (getColor(s.x) != c)
                    {
                        // Если ребро соединяет вершины различных цветов-мы его добавляем
                        // и перекрашиваем вершины
                        nodes[last_n] = s.y;
                        Console.WriteLine(s.x + " =>> " + s.y + "   с весом:" + s.w);   //вывод строки 
                    }
                }
            }

            Console.ReadLine();
        }

        static public int getColor(int n)
        {
            int c;
            if (nodes[n] < 0)       //если эта вершина не была посещена
            {
                return nodes[last_n = n];     //
            }
            c = getColor(nodes[n]);           //
            nodes[n] = last_n;                // nodes[n] присваиваем n- цвет????
            return c;                         //
        }
    }
}
