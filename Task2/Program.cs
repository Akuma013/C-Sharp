using System;
using System.Diagnostics;


Sorter obB = new bubbleSort();
double bubbleTime = obB.DoSort();

Sorter obS = new selectSort();
double selectionTime = obS.DoSort();

Sorter obI = new insertSort();
double insertionTime = obI.DoSort();


double m = Math.Min(Math.Min(bubbleTime, selectionTime), insertionTime );
Console.WriteLine();
if (m == bubbleTime)
    Console.WriteLine($"The fastest algorithm is bubble Sort: {bubbleTime} ms");
else if (m == selectionTime)
    Console.WriteLine($"The fastest algorithm is selection Sort: {selectionTime} ms");
else if (m == insertionTime)
    Console.WriteLine($"The fastest algorithm is insertion Sort: {insertionTime} ms");



abstract class Sorter
{

    protected int[] arr = {54, 93, 77, 43, 67, 32, 12, 87, 65, 19, 23};
    protected int N;

    public Sorter()
    {
        N = arr.Length;
    }
 
    public void output()
    {
        for (int i = 0; i < N; i++)
            Console.Write(arr[i] + " ");
    }
    public double DoSort()
    {
        Console.WriteLine("initial array");
        output();
        Console.Write("\n");

        Console.WriteLine("sorted array");
        Stopwatch stopwatch = Stopwatch.StartNew();
        Sort();
        stopwatch.Stop();

        output();
        Console.WriteLine();
        double time = stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1_000_000.0);
        Console.WriteLine( RetrnName() + " Algorithm took " + stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1_000_000.0) + " ms");
        Console.WriteLine("<============================>");

        return time;
        
    }
    protected void Swap(int n1, int n2)
    {
        int temp = arr[n1];
        arr[n1] = arr[n2];
        arr[n2] = temp;
    }
    protected abstract void Sort();

    protected abstract string RetrnName();
}
class bubbleSort : Sorter
{
    protected override void Sort()
    {
        for (int j = N - 1; j > 0; j--)
            for (int i = 0; i < N - 1; i++)
                if (arr[i] > arr[i + 1])
                    Swap(i,i+1);
    }
    protected override string RetrnName()
    {
        return "Bubble Sort";
    }

}

class selectSort : Sorter
{
    protected override void Sort()
    {
        for (int j = 0; j < N - 1; j++)
        {
            int min = j;
            for (int i = j+1; i < N; i++)
                if (arr[min] > arr[i])
                    min = i;
            Swap(j, min);
        }  

    }
    protected override string RetrnName()
    {
        return "Selection Sort";
    }

}
class insertSort : Sorter
{
    protected override void Sort()
    {
        for (int j = 1; j < N; j++)
        {
            int key = arr[j];
            int i = j - 1;
            while (i >= 0 && key < arr[i])
            {
                arr[i + 1] = arr[i];
                i--;
            }
            arr[i+1] = key;
        }
    }
    protected override string RetrnName()
    {
        return "Insertion Sort";
    }

}

