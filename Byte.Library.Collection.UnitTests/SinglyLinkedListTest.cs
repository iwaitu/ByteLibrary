using System;
using Xunit;

namespace Byte.Library.Collection.UnitTests
{
    public class SinglyLinkedListTest
    {
        [Fact]
        public void CountTest()
        {
            var list = new SinglyLinkedList<string>();

            Assert.Equal(0, list.Count);
            list.AddBack("Alpha");
            Assert.Equal(1, list.Count);
            list.AddBack("Beta");
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void NewLinkedListTest()
        {
            var list = new SinglyLinkedList<string>();

            Assert.NotNull(list);
            Assert.IsType(typeof(SinglyLinkedList<string>), list);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void AddFrontTest()
        {
            var list = new SinglyLinkedList<string>();

            list.AddFront("Charlie");
            list.AddFront("Beta");
            list.AddFront("Alpha");

            Assert.Equal(3, list.Count);
            Assert.Equal("Alpha", list.GetAt(0));
            Assert.Equal("Beta", list.GetAt(1));
            Assert.Equal("Charlie", list.GetAt(2));
        }

        [Fact]
        public void AddMiddleTest()
        {
            var list = new SinglyLinkedList<string>();

            list.AddBack("Alpha");
            list.AddBack("Beta");
            list.AddBack("Delta");
            list.AddBack("Echo");
            list.AddAt(2, "Charlie");

            Assert.Equal(5, list.Count);
            Assert.Equal("Alpha", list.GetAt(0));
            Assert.Equal("Beta", list.GetAt(1));
            Assert.Equal("Charlie", list.GetAt(2));
            Assert.Equal("Delta", list.GetAt(3));
            Assert.Equal("Echo", list.GetAt(4));
        }

        [Fact]
        public void AddMiddleException1Test()
        {
            var list = new SinglyLinkedList<string>();

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    list.AddAt(5, "Alpha");
                });
        }

        [Fact]
        public void AddMiddleException2Test()
        {
            var list = new SinglyLinkedList<string>();

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    list.AddAt(-1, "Alpha");
                });
        }

        [Fact]
        public void AddBackTest()
        {
            var list = new SinglyLinkedList<string>();

            list.AddBack("Alpha");
            list.AddBack("Beta");
            list.AddBack("Charlie");

            Assert.Equal(3, list.Count);
            Assert.Equal("Alpha", list.GetAt(0));
            Assert.Equal("Beta", list.GetAt(1));
            Assert.Equal("Charlie", list.GetAt(2));
        }

        [Fact]
        public void RemoveFrontTest()
        {
            var list = new SinglyLinkedList<string>();

            list.AddBack("Alpha");
            list.AddBack("Beta");
            list.AddBack("Charlie");

            list.RemoveFront();
            Assert.Equal(2, list.Count);

            Assert.Equal("Beta", list.GetAt(0));
            Assert.Equal("Charlie", list.GetAt(1));
            string shouldBeBeta = list.RemoveFront();
            Assert.Equal("Beta", shouldBeBeta);

            string shouldBeCharlie = list.RemoveFront();
            Assert.Equal("Charlie", shouldBeCharlie);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void RemoveFrontExceptionTest()
        {
            var list = new SinglyLinkedList<string>();

            Assert.Throws<InvalidOperationException>(
                () =>
                {
                    list.RemoveFront();
                });
        }

        [Fact]
        public void RemoveMiddleTest()
        {
            var list = new SinglyLinkedList<string>();

            list.AddBack("Alpha");
            list.AddBack("Beta");
            list.AddBack("Charlie");

            list.RemoveAt(1);
            Assert.Equal(2, list.Count);
            Assert.Equal("Alpha", list.GetAt(0));
            Assert.Equal("Charlie", list.GetAt(1));
        }

        [Fact]
        public void RemoveMiddleException1Test()
        {
            var list = new SinglyLinkedList<string>();

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    list.RemoveAt(5);
                });
        }

        [Fact]
        public void RemoveMiddleException2Test()
        {
            var list = new SinglyLinkedList<string>();

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    list.RemoveAt(-1);
                });
        }

        [Fact]
        public void RemoveBackTest()
        {
            var list = new SinglyLinkedList<string>();

            list.AddBack("Alpha");
            list.AddBack("Beta");
            list.AddBack("Charlie");

            string shouldBeCharlie = list.RemoveBack();
            Assert.Equal(2, list.Count);
            Assert.Equal("Charlie", shouldBeCharlie);

            string shouldBeBeta = list.RemoveBack();
            Assert.Equal(1, list.Count);
            Assert.Equal("Beta", shouldBeBeta);
        }

        [Fact]
        public void RemoveBackExceptionTest()
        {
            var list = new SinglyLinkedList<string>();

            Assert.Throws<InvalidOperationException>(
                () =>
                {
                    list.RemoveBack();
                });
        }

        [Fact]
        public void ClearTest()
        {
            var list = new SinglyLinkedList<string>();

            list.AddBack("Alpha");
            list.AddBack("Beta");
            list.AddBack("Charlie");

            Assert.Equal(3, list.Count);

            list.Clear();
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void ContainsTest()
        {
            var list = new SinglyLinkedList<string>();

            list.AddBack("Alpha");
            list.AddBack("Beta");
            list.AddBack("Charlie");

            Assert.Equal(3, list.Count);
            Assert.Equal(true, list.Contains("Alpha"));
            Assert.Equal(true, list.Contains("Beta"));
            Assert.Equal(true, list.Contains("Charlie"));
            Assert.Equal(false, list.Contains("Delta"));
        }

        [Fact]
        public void IndexOfTest()
        {
            var list = new SinglyLinkedList<string>();

            list.AddBack("Alpha");
            list.AddBack("Beta");
            list.AddBack("Charlie");

            Assert.Equal(0, list.IndexOf("Alpha"));
            Assert.Equal(1, list.IndexOf("Beta"));
            Assert.Equal(2, list.IndexOf("Charlie"));
            Assert.Equal(-1, list.IndexOf("Delta"));
        }

        [Fact]
        public void GetFrontTest()
        {
            var list = new SinglyLinkedList<string>();

            list.AddBack("Alpha");
            list.AddBack("Beta");
            list.AddBack("Charlie");

            Assert.Equal(3, list.Count);
            Assert.Equal("Alpha", list.GetFront());

            list.RemoveFront();
            Assert.Equal("Beta", list.GetFront());

            list.RemoveFront();
            Assert.Equal("Charlie", list.GetFront());

            list.RemoveFront();
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void GetFrontExceptionTest()
        {
            var list = new SinglyLinkedList<string>();

            Assert.Throws<InvalidOperationException>(
                () =>
                {
                    list.GetFront();
                });
        }

        [Fact]
        public void GetAtTest()
        {
            var list = new SinglyLinkedList<string>();

            list.AddBack("Alpha");
            list.AddBack("Beta");
            list.AddBack("Charlie");

            Assert.Equal(3, list.Count);
            Assert.Equal("Alpha", list.GetAt(0));
            Assert.Equal("Beta", list.GetAt(1));
            Assert.Equal("Charlie", list.GetAt(2));
        }

        [Fact]
        public void GetAtException1Test()
        {
            var list = new SinglyLinkedList<string>();

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    list.GetAt(1);
                });
        }

        [Fact]
        public void GetAtException2Test()
        {
            var list = new SinglyLinkedList<string>();

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    list.GetAt(-1);
                });
        }

        [Fact]
        public void GetBackTest()
        {
            var list = new SinglyLinkedList<string>();

            list.AddBack("Alpha");
            list.AddBack("Beta");
            list.AddBack("Charlie");

            Assert.Equal("Charlie", list.GetBack());
            list.RemoveBack();
            Assert.Equal("Beta", list.GetBack());
            list.RemoveBack();
            Assert.Equal("Alpha", list.GetBack());
            list.RemoveBack();
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void GetBackExceptionTest()
        {
            var list = new SinglyLinkedList<string>();

            Assert.Throws<InvalidOperationException>(
                () =>
                {
                    list.GetBack();
                });
        }

        [Fact]
        public void ReverseListTest()
        {
            var list = new SinglyLinkedList<string>();
            list.ReverseList();
            Assert.Equal(0, list.Count);

            list.AddBack("Alpha");
            list.AddBack("Beta");
            list.AddBack("Charlie");

            list.ReverseList();

            Assert.Equal(3, list.Count);
            Assert.Equal("Charlie", list.GetAt(0));
            Assert.Equal("Beta", list.GetAt(1));
            Assert.Equal("Alpha", list.GetAt(2));
        }
    }
}
