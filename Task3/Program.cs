using System;
using System.Collections.Generic;

Console.WriteLine("<=========================>");
Console.WriteLine("<=========================>");
Console.WriteLine("<=========================>");
//------------------------
Strategy sort = new SelectionSort();
var context = new Context(sort);
double timeS = context.Sort();
context.PrintArray();
Console.WriteLine($"Selection Sort took: {timeS} ms\n");
//---------------------
sort = new InsertionSort();
context = new Context(sort);
double timeI = context.Sort();
context.PrintArray();
Console.WriteLine($"Insertion Sort took: {timeI} ms\n");
//---------------------
sort = new MergeSort();
context = new Context(sort);
double timeM = context.Sort();
context.PrintArray();
Console.WriteLine($"Merge Sort took: {timeM} ms\n");
//----------------------
sort = new ShellSort();
context = new Context(sort);
double timeShell = context.Sort();
context.PrintArray();
Console.WriteLine($"Shell Sort took: {timeShell} ms\n");





class Context
{
    Strategy strategy;
    int[] array = { 3, 5, 1, 2, 4 };
    public Context(Strategy strategy)
    {
        this.strategy = strategy;
    }
    public double Sort()
    {
        //Using Stopwatch
        /*var watch = System.Diagnostics.Stopwatch.StartNew();
        strategy.Sort(ref array);
        watch.Stop();
        return watch.Elapsed.TotalMilliseconds;*/

        //Using datetime
        DateTime startTime = DateTime.Now;
        strategy.Sort(ref array);
        DateTime endTime = DateTime.Now;
        TimeSpan duration = endTime - startTime;
        return duration.TotalMilliseconds;
    }
    public void PrintArray()

    {
        for (int i = 0; i < array.Length; i++)
            Console.Write(array[i] + " ");
        Console.WriteLine();
    }
}
abstract class Strategy
{
    public abstract void Sort(ref int[] array);
}

class SelectionSort : Strategy
{
    public override void Sort(ref int[] array)

    {
        Console.WriteLine("SelectionSort");
        for (int i = 0; i < array.Length- 1; i++)

        {
            int k = i;
            for (int j = i + 1; j < array.Length; j++)
                if (array[k] > array[j])
                    k = j;
            if (k != i)
            {
                int temp = array[k];
                array[k] = array[i];
                array[i] = temp;
            }
        }
    }
}
class InsertionSort : Strategy
{
    public override void Sort(ref int[] array)
    {
        Console.WriteLine("InsertionSort");
        for (int i = 1; i < array.Length; i++)
        {
            int j = 0;
            int buffer = array[i];
            for (j = i - 1; j >= 0; j--)
            {
                if (array[j] < buffer)
                    break;
                array[j + 1] = array[j];
            }
            array[j + 1] = buffer;
        }
    }
}

class MergeSort : Strategy
{
    public override void Sort(ref int[] array)
    {
        Console.WriteLine("MergeSort");
        MergeSortArray(array, 0, array.Length - 1);
    }

    private void MergeSortArray(int[] array, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;
            MergeSortArray(array, left, middle);
            MergeSortArray(array, middle + 1, right);
            Merge(array, left, middle, right);
        }
    }

    private void Merge(int[] array, int left, int middle, int right)
    {
        int leftArraySize = middle - left + 1;
        int rightArraySize = right - middle;

        int[] leftArray = new int[leftArraySize];
        int[] rightArray = new int[rightArraySize];

        Array.Copy(array, left, leftArray, 0, leftArraySize);
        Array.Copy(array, middle + 1, rightArray, 0, rightArraySize);

        int i = 0, j = 0, k = left;

        while (i < leftArraySize && j < rightArraySize)
        {
            if (leftArray[i] <= rightArray[j])
            {
                array[k++] = leftArray[i++];
            }
            else
            {
                array[k++] = rightArray[j++];
            }
        }

        while (i < leftArraySize)
        {
            array[k++] = leftArray[i++];
        }

        while (j < rightArraySize)
        {
            array[k++] = rightArray[j++];
        }
    }
}

class ShellSort : Strategy
{
    public override void Sort(ref int[] array)
    {
        Console.WriteLine("ShellSort");
        int n = array.Length;

        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            for (int i = gap; i < n; i++)
            {
                int temp = array[i];
                int j;

                for (j = i; j >= gap && array[j - gap] > temp; j -= gap)
                {
                    array[j] = array[j - gap];
                }
                array[j] = temp;
            }
        }
    }
}