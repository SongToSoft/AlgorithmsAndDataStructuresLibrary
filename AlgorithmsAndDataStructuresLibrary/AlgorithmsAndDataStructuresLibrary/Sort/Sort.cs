using System;
using System.Linq;

namespace AlgorithmsAndDataStructuresLibrary.Sort
{
    public static class Sort<T>
    {
        public static void PrintArray(T[] array)
        {
            if (array == null)
                return;
            for (int i = 0; i < array.Length; ++i)
                System.Console.WriteLine(array[i]);
        }

        public static bool CheckArray<T>(T[] array) where T : IComparable<T>
        {
            if (array == null)
                return true;
            for (int i = 0; i < array.Length - 1; ++i)
            {
                if (array[i].CompareTo(array[i + 1]) > 0)
                    return false;
            }
            return true;
        }

        private static void Swap<T>(T[] array, int i, int j) where T : IComparable<T>
        {
            var tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }

        public static T[] Buble<T>(T[] array) where T : IComparable<T>
        {
            try
            {
                for (int i = 0; i < array.Length; ++i)
                    for (int j = 0; j < array.Length - 1; ++j)
                        if (array[j].CompareTo(array[j + 1]) > 0)
                            Swap(array, j, j + 1);
            }
            catch (NullReferenceException)
            {
                System.Console.WriteLine("Array is Null");
                return null;
            }
            return array;
        }

        public static int[] Bucket(int[] array)
        {
            if (array == null)
                return null;
            var maxValue = array[0];
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] < 0)
                    return array;
                if (maxValue.CompareTo(array[i]) < 0)
                    maxValue = array[i];
            }
            int[] NewBucket = new int[maxValue + 1];
            for (int i = 0; i < array.Length; ++i)
                ++NewBucket[array[i]];
            int position = 0;
            for (int i = 0; i < NewBucket.Length; ++i)
            {
                if (NewBucket[i] != 0)
                {
                    for (int j = 0; j < NewBucket[i]; ++j)
                    {
                        array[position] = i;
                        ++position;
                    }
                }
            }
            return array;
        }

        private static int Partition<T>(T[] array, int start, int end) where T : IComparable<T>
        {
            int i = start;
            for (int j = start; j <= end; ++j)
            {
                if (array[j].CompareTo(array[end]) <= 0)
                {
                    Swap(array, i, j);
                    i++;
                }
            }
            return i - 1;
        }

        public static void Quick<T>(T[] array, int start, int end) where T : IComparable<T>
        {
            if (array == null)
                return;
            if (start >= end)
                return;
            int key = Partition(array, start, end);
            Quick<T>(array, start, key - 1);
            Quick<T>(array, key + 1, end);
        }

        public static T[] Selection<T>(T[] array) where T : IComparable<T>
        {
            if (array == null)
                return null;
            for (int i = 0; i < array.Length - 1; ++i)
            {
                int minValue = i;
                for (int j = i + 1; j < array.Length; ++j)
                {
                    if (array[j].CompareTo(array[minValue]) < 0)
                        minValue = j;
                }
                if (minValue != i)
                    Swap(array, i, minValue);
            }
            return array;
        }

        public static T[] Shaker<T>(T[] array) where T : IComparable<T>
        {
            if (array == null)
                return null;
            int left = 0;
            int right = array.Length - 1;
            while (left <= right)
            {
                for (int i = left; i < right; ++i)
                {
                    if (array[i].CompareTo(array[i + 1]) > 0)
                        Swap(array, i, i + 1);
                }
                --right;
                for (int i = right; i > left; --i)
                {
                    if (array[i - 1].CompareTo(array[i]) > 0)
                        Swap(array, i - 1, i);
                }
                ++left;
            }
            return array;
        }

        private static int CreatePyramid<T>(T[] array, int i, int N) where T : IComparable<T>
        {
            int imax;
            if ((2 * i + 2) < N)
            {
                if (array[2 * i + 1].CompareTo(array[2 * i + 2]) < 0)
                    imax = 2 * i + 2;
                else
                    imax = 2 * i + 1;
            }
            else
                imax = 2 * i + 1;
            if (imax >= N)
                return i;
            if (array[i].CompareTo(array[imax]) < 0)
            {
                Swap(array, i, imax);
                if (imax < N / 2)
                    i = imax;
            }
            return i;
        }

        public static T[] Pyramid<T>(T[] array) where T : IComparable<T>
        {
            if (array == null)
                return null;
            for (int i = array.Length / 2 - 1; i >= 0; --i)
            {
                long prev_i = i;
                i = CreatePyramid(array, i, array.Length);
                if (prev_i != i)
                    ++i;
            }
            for (int k = array.Length - 1; k > 0; --k)
            {
                Swap(array, 0, k);
                int i = 0, prev_i = -1;
                while (i != prev_i)
                {
                    prev_i = i;
                    i = CreatePyramid(array, i, k);
                }
            }
            return array;
        }

        private static T[] OneMerge<T>(T[] array1, T[] array2) where T : IComparable<T>
        {
            int a = 0, b = 0;
            T[] merged = new T[array1.Length + array2.Length];
            for (int i = 0; i < array1.Length + array2.Length; i++)
            {
                if ((a < array1.Length) && (b < array2.Length))
                    if (array1[a].CompareTo(array2[b]) > 0)
                        merged[i] = array2[b++];
                    else
                        merged[i] = array1[a++];
                else
                if (b < array2.Length)
                    merged[i] = array2[b++];
                else
                    merged[i] = array1[a++];
            }
            return merged;
        }

        public static T[] Merge<T>(T[] array) where T : IComparable<T>
        {
            if (array == null)
                return null;
            if (array.Length == 1)
                return array;
            int mid_point = array.Length / 2;
            return OneMerge(Merge(array.Take(mid_point).ToArray()), Merge(array.Skip(mid_point).ToArray()));
        }
    }
}