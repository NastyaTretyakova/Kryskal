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
        static int[] nodes = new int[100];    //цвета вершин, если - то уникальны,  5 с 6, то у nodes5=6
        static int last_n = 0;                //
        static bool consoleInput = false;
        static string path = "Data.txt";
        static string[] readLine = null;

        static void Main(string[] args)
        {
            edge_t[] edges = new edge_t[100];
            int NV;      // Количество вершин в графе
            int NE;      // Количество ребер в графе
            int i;


            if (File.Exists(path)) //если файл с данными существует
            {
                readLine = File.ReadAllLines(path);
                consoleInput = false; //будем читать с файла
            }
            else
                consoleInput = true; //будем читать с клавиатуры

            Console.WriteLine("Введите количество вершин и ребер: ");
            NV = Convert.ToInt32(consoleInput ? Console.ReadLine() : ReadFromFile(0));
            NE = Convert.ToInt32(consoleInput ? Console.ReadLine() : ReadFromFile(1));
            for (i = 0; i < NV; i++)
            {
                nodes[i] = -1 - i;  //присваиваем уникальный цвет для каждой вершины
            }
            Console.WriteLine("Введите матрицу: Начало, Конец, Вес ");
            for (i = 0; i < NE; i++)
            {
                string[] OutS = (consoleInput ? Console.ReadLine() : ReadFromFile(2 + i)).Split(' ');    //массив вершин и веса ребра между ними
                edges[i].x = Convert.ToInt32(OutS[0]);            //вершина "начало"
                edges[i].y = Convert.ToInt32(OutS[1]);            //вершина "конец"
                edges[i].w = Convert.ToInt32(OutS[2]);            //вес ребра между вершинами "начало" и "конец"

            }

            Console.WriteLine("Минимальный остов: ");

            var sVes = from h in edges orderby h.w select h;    //сортируем ребра по весу (ро возрастанию)
            foreach (var s in sVes)           //для каждого ребра
            {

                int c = getColor(s.y);     //сохраняем цвет с номером ребра "конец"
                if (getColor(s.x) != c)
                {
                    // Если ребро соединяет вершины различных цветов-мы его добавляем
                    // и перекрашиваем вершину в last_n
                    nodes[last_n] = s.y;
                    Console.WriteLine(s.x + " =>> " + s.y + "   с весом:" + s.w);   //вывод строки 
                }

            }

            Console.ReadLine();
        }

        private static string ReadFromFile(int number)
        {
            if (number > readLine.Length - 1)
                return null;
            Console.WriteLine(readLine[number]);
            return readLine[number];
        }

        static public int getColor(int n)
        {
            int c;
            if (nodes[n] < 0)       //если эта вершина не была перекрашена
            {
                return nodes[last_n = n];     // выводим цвет вершины и запоминаем номер последней точки текущего множества 
            }
            c = getColor(nodes[n]);           //запрашиваем цвет вершины в которую она была перекрашена
            nodes[n] = last_n;                // закрашиваем точку в цвет последней не перекрашенной вершины
            return c;
        }
    }

}