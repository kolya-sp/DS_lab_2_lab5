using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace lab_2_hesh_list
{
    class Program
    {
        public static void RadixSort(ref int[] data)
        {
            int i, j;
            int[] temp = new int[data.Length];

            for (int shift = 31; shift > -1; --shift)
            {
                j = 0;

                for (i = 0; i < data.Length; ++i)
                {
                    bool move = (data[i] << shift) >= 0;

                    if (shift == 0 ? !move : move)
                        data[i - j] = data[i];
                    else
                        temp[j++] = data[i];
                }

                Array.Copy(temp, 0, data, data.Length - j, j);
            }
        }
        public static void vivod(ref int[] data)
        {
            for (int i = 0; i < data.Length - 1; i++)
            {
                Console.Write(data[i] + " ");
            }
        }
        static void Main(string[] args)
        {
            string f_name = "data1.csv";
            FileStream file = new FileStream(f_name, FileMode.OpenOrCreate);
            file.Close();
            string t1;
            Console.WriteLine("тест порозрядне сортування RadixSort: ");
            StreamWriter swr = new StreamWriter(f_name);
            swr.WriteLine("test RadixSort: \n data; RadixSort");
            Stopwatch sw = new Stopwatch();
            Random r = new Random();
            int[] data;
            for (int j = 1; j <= 5; j++)
            {
                data = new int[200000 * j];
                for (int i = 0; i < j * 200000; i++) data[i] = r.Next(1000000); // генерація масиву 
                sw.Start();
                RadixSort(ref data);     // сортування масиву            
                sw.Stop(); t1 = sw.ElapsedMilliseconds.ToString();
                sw.Reset();
                Console.WriteLine(j * 200000 + " чисел вiдсортовано за " + t1 + " мс ");
                swr.WriteLine(j * 200000 + ";" + t1);
            }
            swr.Close();
            Console.WriteLine("тестовий файл з невеликою кiлькiсю даних для перевiрки роботи алгоритмiв сортування lab_5_..\\bin\\Debug\\data2.csv його вмiст: ");
            string Pyt = "data2.csv";
            StreamReader sr = new StreamReader(Pyt);
            string[] mass = (sr.ReadToEnd()).Split('\n');
            sr.Close();
            data = new int[mass.Length];
            for (int i = 0; i < mass.Length - 1; i++)
            {
                data[i] = int.Parse(mass[i]);
            }
            Console.WriteLine("масив з lab_5_..\\bin\\Debug\\data2.csv");
            vivod(ref data);
            Console.WriteLine();
            Console.Write("масив відсортований RadixSort ");
            RadixSort(ref data);
            vivod(ref data);
            Console.ReadKey();
        }
    }
}
