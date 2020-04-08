﻿using System;
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
    }
    class Hesh_tabl
    {
        int size = 1000000;        
        public List<data>[] hesh;
        public Hesh_tabl()
        {
            hesh = new List<data>[size];
        }
        public int hesh_func(int key)
        {
            return key % size;
        }
        public void Add(int key2, string value2)
        {
            if (hesh[hesh_func(key2)] == null)
            {
                hesh[hesh_func(key2)] = new List<data>();
            }
            hesh[hesh_func(key2)].Add(new data { key = key2, value = value2 });
        }
        public data search(int key2)
        {
            if (hesh[hesh_func(key2)] != null)
            {
                for (int i = 0; i < hesh[hesh_func(key2)].Count; i++) if (hesh[hesh_func(key2)][i].key == key2)
                {
                        return hesh[hesh_func(key2)][i];
                }
            }
            return null;
        }
        public bool del(int key)
        {
            return hesh[hesh_func(key)].Remove(search(key));
        }
        public void vivod()
        {
            for (int i = 0; i < hesh.Length; i++)
            {
                if (hesh[i] != null)
                {
                    Console.WriteLine("hesh[" + i + "]:");
                    foreach (data k in hesh[i])
                    {
                        Console.WriteLine("key: " + k.key + " value: " + k.value);
                    }
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
            Console.WriteLine("тест Hesh_tabl_list: ");
            StreamWriter swr = new StreamWriter(f_name);
            swr.WriteLine("test Hesh_tabl_list: \n data;Hesh_tabl_list write ;Hesh_tabl_list read");
            Stopwatch sw = new Stopwatch();
            Random r = new Random();
            data d = new data();
            for (int j = 1; j <= 5; j++)
            {
                sw.Start();
                for (int i = 0; i < 200000 * j; i++) H.Add(r.Next(10000000), r.Next(10000000).ToString());     // запис в хеш талицю            
                sw.Stop(); t1 = sw.ElapsedMilliseconds.ToString();
                sw.Restart();
                for (int i = 0; i < 200000 * j; i++) d = H.search(i);  // пошук в хеш таблицi
                sw.Stop(); t2 = sw.ElapsedMilliseconds.ToString();
                sw.Reset();
                Console.WriteLine(j * 200000 + " чисел записано за " + t1 + " мс i зчитано за " + t2 + " мс ");
                swr.WriteLine(j * 200000 + ";" + t1 + ";" + t2);
                H = new Hesh_tabl();
            }
            swr.Close();
            Console.WriteLine("тестовий файл з невеликою кiлькiсю даних для перевiрки роботи хеш таблицi i пошуку lab_2_hesh_list\bin\\Debug\\data2.csv його вмiст в хешi:");
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
            d = H.search(n);
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
