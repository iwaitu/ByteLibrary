using Byte.Library.Collection.Sorting;
using Xunit;

namespace Byte.Library.Collection.UnitTests.Sorting
{
    public class SortTest
    {
        [Fact]
        public void GeneralSortTest()
        {
            int[] arr = { 5, 67, 31, 9, 7, 52, 1, 92 };

            Sorter.Sort(arr);

            int lastValue = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                Assert.True(arr[i] > lastValue);
            }
        }

        [Fact]
        public void BubbleSortTest()
        {
            int[] arr = { 5, 67, 31, 9, 7, 52, 1, 92 };

            Sorter.Sort(arr, SortType.BubbleSort);

            int lastValue = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                Assert.True(arr[i] > lastValue);
            }
        }

        [Fact]
        public void InsertionSortTest()
        {
            int[] arr = { 5, 67, 31, 9, 7, 52, 1, 92 };

            Sorter.Sort(arr, SortType.InsertionSort);

            int lastValue = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                Assert.True(arr[i] > lastValue);
            }
        }

        [Fact]
        public void SelectionSortTest()
        {
            int[] arr = { 5, 67, 31, 9, 7, 52, 1, 92 };

            Sorter.Sort(arr, SortType.SelectionSort);

            int lastValue = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                Assert.True(arr[i] > lastValue);
            }
        }

        [Fact]
        public void QuickSortTest()
        {
            int[] arr = { 5, 67, 31, 9, 7, 52, 1, 92 };

            Sorter.Sort(arr, SortType.QuickSort);

            int lastValue = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                Assert.True(arr[i] > lastValue);
            }
        }
    }
}
