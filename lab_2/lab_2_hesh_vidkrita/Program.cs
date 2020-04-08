using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace lab_2_hesh_list
{    
    class data 
    {
        public int key;
        public string value;
        public bool Equals(data d)
        {
            return key == d.key && value == d.value;
        }
    }
    class Hesh_tabl
    {     
        public data nool = new data
        {
            key = 0,
            value = "delete"
        };
        int size = 1000000;
        public data[] hesh;
        public Hesh_tabl()
        {
            hesh = new data[size];
        }
        public int hesh_func(int key)
        {
            return key % size;
        }
        public int next (int t)
        {
            return (t + 1) % size;
        }
        public void Add(int key2, string value2)
        {
            int t = hesh_func(key2);
            while (hesh[t] != null && !hesh[t].Equals(nool)) t=next(t);
            hesh[t] = new data()
            {
                key = key2,
                value = value2
            };   
        }
        public data search(int key2, out int k)
        {
            int t = hesh_func(key2);
            int end = hesh_func(key2);
            k = t;
            while (hesh[t] != null)
            {
                if (hesh[t].key == key2 && !hesh[t].Equals(nool)) return hesh[t];
                t = next(t);
                k = t;
                if (t == end) break;
            }
            return null;
        }
        public bool del(int key)
        {
            data d = search(key, out int k);
            bool znaishli = (d != null);
            if (znaishli) hesh[k] = nool;
            return znaishli;
        }
        public void vivod()
        {
            for (int i = 0; i < hesh.Length; i++)
            {
                if (hesh[i] != null && !hesh[i].Equals(nool))
                {
                    Console.Write("hesh[" + i + "]: ");
                    Console.WriteLine("key: " + hesh[i].key + " value: " + hesh[i].value);
                }
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string f_name = "data1.csv";
            FileStream file = new FileStream(f_name, FileMode.OpenOrCreate);
            file.Close();
            Hesh_tabl H = new Hesh_tabl();
            string t1, t2;
            Console.WriteLine("тест Hesh_tabl_vidkrita: ");
            StreamWriter swr = new StreamWriter(f_name);
            swr.WriteLine("test Hesh_tabl_vidkrita: \n data;Hesh_tabl_vidkrita write ;Hesh_tabl_vidkrita read");
            Stopwatch sw = new Stopwatch();
            Random r = new Random();
            data d = new data();
            int f;
            int max = 950000;
            for (int j = 1; j <= 5; j++)
            {
                sw.Start();
                for (int i = 0; i < 200000 * j && i< max; i++) H.Add(r.Next(10000000), r.Next(10000000).ToString());      // запис в хеш талицю            
                sw.Stop(); t1 = sw.ElapsedMilliseconds.ToString();
                sw.Restart();
                for (int i = 0; i < 200000 * j; i++) d = H.search(i, out f);  // пошук в хеш таблицi
                sw.Stop(); t2 = sw.ElapsedMilliseconds.ToString();
                sw.Reset();
                if (j!=5)
                    Console.WriteLine(j * 200000 + " чисел записано за " + t1 + " мс i зчитано за " + t2 + " мс ");
                else Console.WriteLine(max + " чисел записано за " + t1 + " мс i зчитано за " + t2 + " мс ");
                if (j != 5)
                    swr.WriteLine(j * 200000 + ";" + t1 + ";" + t2);
                else swr.WriteLine(max + ";" + t1 + ";" + t2);
                H = new Hesh_tabl();
            }
            swr.Close();
            Console.WriteLine("тестовий файл з невеликою кiлькiсю даних для перевiрки роботи хеш таблицi i пошуку lab_2_hesh_vidkrita\bin\\Debug\\data2.csv його вмiст в хешi:");
            H = new Hesh_tabl();
            string Pyt = "data2.csv";
            StreamReader sr = new StreamReader(Pyt);
            string[] mass = (sr.ReadToEnd()).Split('\n');
            sr.Close();
            for (int i = 0; i < mass.Length - 1; i++)
            {
                string[] data1 = mass[i].Split(';');
                H.Add(int.Parse(data1[0]), data1[1]);
            }
            Console.WriteLine("Hesh tab: ");
            H.vivod();
            Console.WriteLine();
            Console.Write("Enter the number you want to find in my Hesh :   ");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine();
            d = H.search(n, out f);
            if (d == null) Console.WriteLine("Не знайдено");
            else
            {
                Console.WriteLine("Знайдено");
                Console.WriteLine("key: " + d.key + "  value: " + d.value);
            }
            Console.ReadKey();
        }
    }
}
