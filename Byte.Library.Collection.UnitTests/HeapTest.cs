using System;
using System.Collections.Generic;
using Xunit;

namespace Byte.Library.Collection.UnitTests
{
    public class HeapTest
    {
        [Fact]
        public void CountTest()
        {
            var hp = new Heap<int, string>();

            Assert.Equal(0, hp.Count);
            hp.Push(5, "Alpha5");
            Assert.Equal(1, hp.Count);
            hp.Push(123, "Alpha123");
            hp.Push(56, "Alpha56");
            Assert.Equal(3, hp.Count);
            hp.Pop();
            Assert.Equal(2, hp.Count);
            hp.Pop();
            Assert.Equal(1, hp.Count);
            hp.Pop();
            Assert.Equal(0, hp.Count);

        }

        [Fact]
        public void AddPeekPopTest()
        {
            var hp = new Heap<int, string>();

            Assert.Equal(0, hp.Count);
            hp.Push(123, "Alpha123");
            hp.Push(456, "Alpha456");
            Assert.Equal(2, hp.Count);
            KeyValuePair<int, string> peeked = hp.Peek();
            Assert.Equal(new KeyValuePair<int, string>(456, "Alpha456"), peeked);
            Assert.Equal(2, hp.Count);
            hp.Push(4, "Alpha4");
            hp.Push(6, "Alpha6");
            Assert.Equal(4, hp.Count);
            hp.Push(1006, "Alpha1006");
            Assert.Equal(5, hp.Count);
            peeked = hp.Peek();
            Assert.Equal(new KeyValuePair<int, string>(1006, "Alpha1006"), peeked);
            KeyValuePair<int, string> popped = hp.Pop();
            Assert.Equal(4, hp.Count);
            Assert.Equal(new KeyValuePair<int, string>(1006, "Alpha1006"), popped);
            peeked = hp.Peek();
            Assert.Equal(new KeyValuePair<int, string>(456, "Alpha456"), peeked);
            popped = hp.Pop();
            Assert.Equal(new KeyValuePair<int, string>(456, "Alpha456"), popped);
        }

        [Fact]
        public void RangeExceptionTest1()
        {
            var hp = new Heap<int, string>();

            Assert.Throws<InvalidOperationException>(
                () =>
                {
                    hp.Peek();
                });
        }

        [Fact]
        public void RangeExceptionTest2()
        {
            var hp = new Heap<int, string>();

            Assert.Throws<InvalidOperationException>(
                () =>
                {
                    hp.Pop();
                });
        }

        [Fact]
        public void RangeExceptionTest3()
        {
            var hp = new Heap<int, string>();

            hp.Push(12, "Alpha12");
            hp.Pop();

            Assert.Throws<InvalidOperationException>(
                () =>
                {
                    hp.Pop();
                });
        }

        [Fact]
        public void ClearTest()
        {
            var hp = new Heap<int, string>();

            Assert.Equal(0, hp.Count);
            hp.Push(5, "Alpha5");
            hp.Push(15, "Alpha15");
            hp.Push(25, "Alpha25");
            Assert.Equal(3, hp.Count);
            hp.Clear();
            Assert.Equal(0, hp.Count);
        }

        [Fact]
        public void ContainsTest()
        {
            var hp = new Heap<int, string>();

            Assert.Equal(0, hp.Count);
            hp.Push(900, "Alpha900");
            hp.Push(100, "Alpha100");
            hp.Push(555, "Alpha555");
            Assert.Equal(true, hp.Contains(new KeyValuePair<int, string>(900, "Alpha900")));
            Assert.Equal(true, hp.Contains(new KeyValuePair<int, string>(100, "Alpha100")));
            Assert.Equal(true, hp.Contains(new KeyValuePair<int, string>(555, "Alpha555")));
            Assert.Equal(false, hp.Contains(new KeyValuePair<int, string>(997, "Alpha997")));
            hp.Pop();
            Assert.Equal(false, hp.Contains(new KeyValuePair<int, string>(900, "Alpha900")));
            hp.Pop();
            Assert.Equal(false, hp.Contains(new KeyValuePair<int, string>(555, "Alpha555")));
            Assert.Equal(true, hp.Contains(new KeyValuePair<int, string>(100, "Alpha100")));
        }

        [Fact]
        public void EnumeratorTest()
        {
            var hp = new Heap<int, string>();

            hp.Push(1, "Alpha1");
            hp.Push(2, "Alpha2");
            hp.Push(34, "Alpha34");
            hp.Push(4, "Alpha4");
            hp.Push(50, "Alpha50");

            foreach (KeyValuePair<int, string> thisOne in hp)
            {
                Assert.True(thisOne.Key > 0);
            }
        }

    }
}
