using System;

namespace TG.Exam.Algorithms
{
   class Program
   {
      /// <summary>
      /// Return n = c element in sequence of elements as a sum of two elements current and previous
      /// Where c is amount of element
      /// Despite more elegant code style recursion has an extra resource costs 
      /// </summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <param name="c"></param>
      /// <returns></returns>
      static int Foo(int a, int b, int c)
      {
         if (1 < c)
            return Foo(b, b + a, c - 1);
         else
            return a;
      }

      static int FooWhile(int a, int b, int c)
      {
         while (c > 1)
         {
            var tmp = a;
            a = b;
            b = b + tmp;
            c--;
         }
         return a;
      }
      /// <summary>
      /// Array bubble sort
      /// In my view the implementation is Ok
      /// </summary>
      /// <param name="arr"></param>
      /// <returns></returns>
      static int[] Bar(int[] arr)
      {
         for (int i = 0; i < arr.Length; i++)
         for (int j = 0; j < arr.Length - 1; j++)
            if (arr[j] > arr[j + 1])
            {
               int t = arr[j];
               arr[j] = arr[j + 1];
               arr[j + 1] = t;
            }

         return arr;
      }
      /// <summary>
      /// :)
      /// </summary>
      /// <param name="arr"></param>
      /// <returns></returns>
      static int[] BarSort(int[] arr)
      {
         Array.Sort(arr);
         return arr;
      }

      static void Main(string[] args)
      {
         Console.WriteLine("Foo result: {0}", Foo(7, 2, 8));
         Console.WriteLine("FooWhile result: {0}", FooWhile(7, 2, 8));
         Console.WriteLine("Bar result: {0}", string.Join(", ", Bar(new[] {7, 2, 8})));
         Console.WriteLine("BarSort result: {0}", string.Join(", ", BarSort(new[] { 7, 2, 8 })));

         Console.ReadKey();
      }
   }
}