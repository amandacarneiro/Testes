using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using NUnit.Framework;

namespace Testes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Informe  tamanho do código (Inteiro).");
            SymbolicToOctal("rwxr-x-w-");
            Radical(new string[] { "flower", "float", "fl", "flat" }, 3);
            Tester tester = new Tester();
            tester.ShelfCanAcceptAndReturnItem();
            

        }

        /// <summary>
        ///Decode symbols
        /// </summary>
        /// <param name="symbolSize"></param>
        /// <param name="permString"></param>
        private static void SymbolicToOctal(string permString)
        {
            int symbolSize = 3;
            //Total of divisions
            int totalSection = permString.Length / symbolSize;

            // Get all characters 
            IEnumerable<char> codeStringQuery =
              from ch in permString
              select ch;

            //Symbols in the string
            List<Symbol> elements = BuildList();

            //String converted on a list of Symbols
            List<Symbol> codeInList = new List<Symbol>();

            foreach (char c in codeStringQuery)
            {
                foreach (Symbol e in elements)
                {
                    if (e.Name.Contains(c))
                    {
                        codeInList.Add(e);
                    }
                }
            }

            //sum of sections
            List<int> sumSection = new List<int>();
            int position = 0;
            for (int i = 1; i <= symbolSize; i++)
            {
                int sum = 0;

                for (int j = position; j < codeInList.Count / symbolSize * i; j++)
                {
                    sum = sum + codeInList[j].SymbolValue;

                }
                position = position + 3;
                sumSection.Add(sum);
            }

            //create string to return
            int s = sumSection[0] * 100 + sumSection[1] * 10 + sumSection[2];


            Console.WriteLine("Decoded:" + s);


        }

        private static List<Symbol> BuildList()
        {
            return new List<Symbol>
    {
        { new Symbol() { Name="r", SymbolValue = 4}},
        { new Symbol() { Name="w", SymbolValue = 2}},
        { new Symbol() { Name="x", SymbolValue = 1}},
        { new Symbol() { Name="-", SymbolValue = 0}}
    };
        }

        public class Symbol
        {
            public string Name { get; set; }
            public int SymbolValue { get; set; }
        }

        public static void Radical(string[] words, int prefixLength)
        {
            IEnumerable<string> wordsQuery =
            from wq in words
            where wq.Length >= prefixLength
            select wq.Substring(0, prefixLength);
            foreach (string s in wordsQuery) Console.Write(JsonSerializer.Serialize(s));
        }

        public class Shelf
        {
            private List<string> items = new List<string>();
            public void Put(string item)
            {
                if (!string.IsNullOrEmpty(item)) this.items.Add(item);
            }

            public bool Take(string item)
            {
                if (items.Contains(item))
                {
                    items.Remove(item);
                    return true;
                }
                return false;
            }

        }

        public class Racer
        {
            private readonly string name;
            public Racer(string name) {
                this.name = name;
            }
            public void Run() { Thread.Sleep(100);
                Console.Write(name);
            }
        }
       
        [TestFixture]
        public class Tester
        {
            [Test]
            public void ShelfCanAcceptAndReturnItem()
            {
                Shelf shelf = new Shelf();
                shelf.Put(null);
                Assert.AreEqual(false, shelf.Take(null));

                shelf.Put("");
                Assert.AreEqual(false, shelf.Take(""));

                shelf.Put("Pencil");
                Assert.AreEqual(true, shelf.Take("Pencil"));
                Assert.AreEqual(false, shelf.Take("Book"));

            }
        }


        public class UnloadingTime
        {
            public DateTime Start { get; private set; }
            public DateTime End { get; private set; }

            public UnloadingTime(DateTime start, DateTime end)
            {
                this.Start = start;
                this.End = end;
            }
        }

        public static class UnloadingTrucks
        {
            public static bool CanUnloadAll(IEnumerable<UnloadingTime> unloadingTimes)
            {

                List<UnloadingTime> unloadingTimesCheck = unloadingTimes.ToList();
                unloadingTimesCheck.RemoveAt(0);

                foreach (UnloadingTime ut in unloadingTimes)
                {
                    foreach (UnloadingTime uTeste in unloadingTimesCheck)
                    {
                        if (uTeste.Start == ut.Start)
                        {
                            return false;
                        }
                        if (uTeste.Start == ut.Start || uTeste.End == ut.End)
                        {
                            return false;
                        }
                    }
                    if(unloadingTimesCheck.Count>0)
                    unloadingTimesCheck.RemoveAt(0);
                }
                return true;
            }

          
        }
    }
}
