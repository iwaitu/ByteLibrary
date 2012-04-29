using System.Collections.Generic;
using Byte.Library.Collection;
using Xunit;

namespace Byte.Library.UnitTests.Collection
{
    public class BTreeTests
    {
        [Fact]
        public void KeysTest()
        {
            var bt = new BTree<int, string>(8);

            bt.Add(50, "Alpha50");
            bt.Add(70, "Alpha70");
            bt.Add(30, "Alpha30");
            bt.Add(35, "Alpha35");
            bt.Add(25, "Alpha25");
            bt.Add(65, "Alpha65");
            bt.Add(75, "Alpha75");

            Assert.Equal(7, bt.Keys.Count);
            Assert.Equal(true, bt.ContainsKey(50));
            Assert.Equal(true, bt.ContainsKey(70));
            Assert.Equal(true, bt.ContainsKey(30));
            Assert.Equal(true, bt.ContainsKey(35));
            Assert.Equal(true, bt.ContainsKey(25));
            Assert.Equal(true, bt.ContainsKey(65));
            Assert.Equal(true, bt.ContainsKey(75));
            Assert.Equal(false, bt.ContainsKey(555));

            bt.Clear();

            Assert.Equal(0, bt.Keys.Count);
        }

        [Fact]
        public void ValuesTest()
        {
            var bt = new BTree<int, string>(8);

            bt.Add(50, "Alpha50");
            bt.Add(70, "Alpha70");
            bt.Add(30, "Alpha30");
            bt.Add(35, "Alpha35");
            bt.Add(25, "Alpha25");
            bt.Add(65, "Alpha65");
            bt.Add(75, "Alpha75");

            Assert.Equal(7, bt.Values.Count);
            Assert.Equal(true, bt.Contains(new KeyValuePair<int, string>(50, "Alpha50")));
            Assert.Equal(true, bt.Contains(new KeyValuePair<int, string>(70, "Alpha70")));
            Assert.Equal(true, bt.Contains(new KeyValuePair<int, string>(30, "Alpha30")));
            Assert.Equal(true, bt.Contains(new KeyValuePair<int, string>(35, "Alpha35")));
            Assert.Equal(true, bt.Contains(new KeyValuePair<int, string>(25, "Alpha25")));
            Assert.Equal(true, bt.Contains(new KeyValuePair<int, string>(65, "Alpha65")));
            Assert.Equal(true, bt.Contains(new KeyValuePair<int, string>(75, "Alpha75")));
            Assert.Equal(false, bt.Contains(new KeyValuePair<int, string>(50, "Blarg")));
            Assert.Equal(false, bt.Contains(new KeyValuePair<int, string>(5552350, "Alpha50")));

            bt.Clear();

            Assert.Equal(0, bt.Values.Count);
        }

        [Fact]
        public void CountTest()
        {
            var bt = new BTree<int, string>(8);

            Assert.Equal(0, bt.Count);
            bt.Add(50, "Alpha50");
            Assert.Equal(1, bt.Count);
            bt.Add(70, "Alpha70");
            Assert.Equal(2, bt.Count);
            bt.Add(30, "Alpha30");
            Assert.Equal(3, bt.Count);
            bt.Add(35, "Alpha35");
            Assert.Equal(4, bt.Count);
            bt.Add(25, "Alpha25");
            Assert.Equal(5, bt.Count);
            bt.Add(65, "Alpha65");
            Assert.Equal(6, bt.Count);
            bt.Add(75, "Alpha75");
            Assert.Equal(7, bt.Count);
            bt.Remove(50);
            Assert.Equal(6, bt.Count);
            bt.Remove(70);
            bt.Remove(30);
            bt.Remove(35);
            bt.Remove(25);
            bt.Remove(65);
            bt.Remove(75);
            Assert.Equal(0, bt.Count);
        }

        [Fact]
        public void ByIndexTest()
        {
            var bt = new BTree<int, string>(8);

            bt.Add(50, "Alpha50");
            bt.Add(70, "Alpha70");
            bt.Add(30, "Alpha30");
            bt.Add(35, "Alpha35");
            bt.Add(25, "Alpha25");
            bt.Add(65, "Alpha65");
            bt.Add(75, "Alpha75");

            Assert.Equal("Alpha50", bt[50]);
            Assert.Equal("Alpha70", bt[70]);
            Assert.Equal("Alpha30", bt[30]);
            Assert.Equal("Alpha35", bt[35]);
            Assert.Equal("Alpha25", bt[25]);
            Assert.Equal("Alpha65", bt[65]);
            Assert.Equal("Alpha75", bt[75]);

            Assert.Throws<KeyNotFoundException>(
                () =>
                {
                    Assert.NotEqual("Alpha540", bt[540]);
                });
        }

        [Fact]
        public void TryGetValueTest()
        {
            var bt = new BTree<int, string>(8);

            string output = "";
            Assert.Equal(false, bt.TryGetValue(555, out output));
            output = "";
            bt.Add(50, "Alpha50");
            Assert.Equal(true, bt.TryGetValue(50, out output));
            Assert.Equal("Alpha50", output);
            output = "";
            bt.Add(70, "Alpha70");
            Assert.Equal(true, bt.TryGetValue(70, out output));
            Assert.Equal("Alpha70", output);
        }

        [Fact]
        public void ClearTest()
        {
            var bt = new BTree<int, string>(8);

            bt.Clear();
            Assert.Equal(0, bt.Count);
            bt.Add(50, "Alpha50");
            bt.Add(70, "Alpha70");
            bt.Add(30, "Alpha30");
            bt.Add(35, "Alpha35");
            bt.Add(25, "Alpha25");
            bt.Add(65, "Alpha65");
            bt.Add(75, "Alpha75");
            Assert.Equal(7, bt.Count);
            bt.Clear();
            Assert.Equal(0, bt.Count);
        }

        [Fact]
        public void AddTest()
        {
            var bt = new BTree<int, string>(8);

            bt.Add(50, "Alpha50");
            bt.Add(70, "Alpha70");
            bt.Add(30, "Alpha30");
            bt.Add(35, "Alpha35");
            bt.Add(25, "Alpha25");
            bt.Add(65, "Alpha65");
            bt.Add(75, "Alpha75");
            bt.Add(98, "Alpha98");

            //List<int> addedKeys = new List<int>();

            //Random rnd = new Random();

            //for (int i = 1; i < 1000; i++)
            //{
            //    int key = rnd.Next(200, 2000);
            //    string val = "Alpha" + key;
            //    bt.Add(key, val);
            //    addedKeys.Add(key);
            //    //Console.WriteLine("added " + key);
            //}

            Assert.Equal(true, bt.ContainsKey(50));
            Assert.Equal(true, bt.ContainsKey(70));
            Assert.Equal(true, bt.ContainsKey(30));
            Assert.Equal(true, bt.ContainsKey(35));
            Assert.Equal(true, bt.ContainsKey(25));
            Assert.Equal(true, bt.ContainsKey(65));
            Assert.Equal(true, bt.ContainsKey(75));
            Assert.Equal(true, bt.ContainsKey(98));
            Assert.Equal(false, bt.ContainsKey(99999999));

            //foreach (int addedKey in addedKeys)
            //    Assert.Equal(true, bt.ContainsKey(addedKey));

            //Assert.Equal(false, bt.ContainsKey(110));
            //Assert.Equal(false, bt.ContainsKey(150));
            //Assert.Equal(false, bt.ContainsKey(180));

        }

        [Fact]
        public void ContainsKeyTest()
        {
            var bt = new BTree<int, string>(8);

            bt.Add(50, "Alpha50");
            bt.Add(70, "Alpha70");
            bt.Add(30, "Alpha30");
            bt.Add(35, "Alpha35");
            bt.Add(25, "Alpha25");
            bt.Add(65, "Alpha65");
            bt.Add(75, "Alpha75");
            bt.Add(98, "Alpha98");
            bt.Add(1, "Alpha1");

            Assert.Equal(true, bt.ContainsKey(50));
            Assert.Equal(true, bt.ContainsKey(70));
            Assert.Equal(true, bt.ContainsKey(30));
            Assert.Equal(true, bt.ContainsKey(35));
            Assert.Equal(true, bt.ContainsKey(25));
            Assert.Equal(true, bt.ContainsKey(65));
            Assert.Equal(true, bt.ContainsKey(75));

            Assert.Equal(false, bt.ContainsKey(51));
            Assert.Equal(false, bt.ContainsKey(43));
            Assert.Equal(false, bt.ContainsKey(77));
        }

        [Fact]
        public void ContainsTest()
        {
            var bt = new BTree<int, string>(8);

            bt.Add(50, "Alpha50");
            bt.Add(70, "Alpha70");
            bt.Add(30, "Alpha30");
            bt.Add(35, "Alpha35");
            bt.Add(25, "Alpha25");
            bt.Add(65, "Alpha65");
            bt.Add(75, "Alpha75");
            bt.Add(98, "Alpha98");
            bt.Add(1, "Alpha1");

            Assert.Equal(true, bt.Contains(new KeyValuePair<int, string>(50, "Alpha50")));
            Assert.Equal(true, bt.Contains(new KeyValuePair<int, string>(70, "Alpha70")));
            Assert.Equal(true, bt.Contains(new KeyValuePair<int, string>(30, "Alpha30")));
            Assert.Equal(true, bt.Contains(new KeyValuePair<int, string>(35, "Alpha35")));
            Assert.Equal(true, bt.Contains(new KeyValuePair<int, string>(25, "Alpha25")));
            Assert.Equal(true, bt.Contains(new KeyValuePair<int, string>(65, "Alpha65")));
            Assert.Equal(true, bt.Contains(new KeyValuePair<int, string>(75, "Alpha75")));

            Assert.Equal(false, bt.Contains(new KeyValuePair<int, string>(51, "Alpha51")));
            Assert.Equal(false, bt.Contains(new KeyValuePair<int, string>(43, "Alpha43")));
            Assert.Equal(false, bt.Contains(new KeyValuePair<int, string>(77, "Alpha77")));

            Assert.Equal(false, bt.Contains(new KeyValuePair<int, string>(65, "Alpha777777")));
            Assert.Equal(false, bt.Contains(new KeyValuePair<int, string>(75, "Alpha723435")));

        }

        [Fact]
        public void RemoveInternalTest()
        {
            var bt = new BTree<int, string>(8);

            bt.Add(50, "Alpha50");
            bt.Add(70, "Alpha70");
            bt.Add(30, "Alpha30");
            bt.Add(35, "Alpha35");
            bt.Add(25, "Alpha25");
            bt.Add(65, "Alpha65");
            bt.Add(75, "Alpha75");

            bt.Remove(50);

            Assert.Equal(false, bt.ContainsKey(50));
            Assert.Equal(true, bt.ContainsKey(70));
            Assert.Equal(true, bt.ContainsKey(30));
            Assert.Equal(true, bt.ContainsKey(35));
            Assert.Equal(true, bt.ContainsKey(25));
            Assert.Equal(true, bt.ContainsKey(65));
            Assert.Equal(true, bt.ContainsKey(75));
        }

        [Fact]
        public void RemoveLeafStealSiblingTest()
        {
            var bt = new BTree<int, string>(8);

            bt.Add(50, "50");
            bt.Add(49, "49");
            bt.Add(48, "48");
            bt.Add(47, "47");
            bt.Add(46, "46");
            bt.Add(45, "45");

            bt.Remove(48);

            Assert.Equal(true, bt.ContainsKey(50));
            Assert.Equal(true, bt.ContainsKey(49));
            Assert.Equal(false, bt.ContainsKey(48));
            Assert.Equal(true, bt.ContainsKey(47));
            Assert.Equal(true, bt.ContainsKey(46));
            Assert.Equal(true, bt.ContainsKey(45));
        }

        [Fact]
        public void RemoveLeafStealParentTest()
        {
            var bt = new BTree<int, string>(8);

            bt.Add(50, "50");
            bt.Add(49, "49");
            bt.Add(48, "48");
            bt.Add(47, "47");
            bt.Add(46, "46");
            bt.Add(45, "45");

            bt.Remove(50);

            Assert.Equal(false, bt.ContainsKey(50));
            Assert.Equal(true, bt.ContainsKey(49));
            Assert.Equal(true, bt.ContainsKey(48));
            Assert.Equal(true, bt.ContainsKey(47));
            Assert.Equal(true, bt.ContainsKey(46));
            Assert.Equal(true, bt.ContainsKey(45));
        }

        [Fact]
        public void RemoveLeafFuseRootTest()
        {
            var bt = new BTree<int, string>(8);

            bt.Add(50, "50");
            bt.Add(49, "49");
            bt.Add(48, "48");
            bt.Add(47, "47");
            bt.Add(46, "46");
            bt.Add(45, "45");
            bt.Add(44, "44");
            bt.Add(43, "43");
            bt.Add(42, "42");

            bt.Remove(43);

            Assert.Equal(true, bt.ContainsKey(50));
            Assert.Equal(true, bt.ContainsKey(49));
            Assert.Equal(true, bt.ContainsKey(48));
            Assert.Equal(true, bt.ContainsKey(47));
            Assert.Equal(true, bt.ContainsKey(46));
            Assert.Equal(true, bt.ContainsKey(45));
            Assert.Equal(true, bt.ContainsKey(44));
            Assert.Equal(false, bt.ContainsKey(43));
            Assert.Equal(true, bt.ContainsKey(42));
        }

        [Fact]
        public void CopyToTest()
        {
            var bt = new BTree<int, string>(4);

            bt.Add(50, "Alpha50");
            bt.Add(70, "Alpha70");
            bt.Add(30, "Alpha30");
            bt.Add(35, "Alpha35");
            bt.Add(25, "Alpha25");
            bt.Add(65, "Alpha65");
            bt.Add(75, "Alpha75");

            KeyValuePair<int, string>[] arr = new KeyValuePair<int, string>[7];

            bt.TraversalType = TreeTraversalType.LevelOrder;

            bt.CopyTo(arr, 0);

            Assert.Equal(50, arr[0].Key);
            Assert.Equal("Alpha50", arr[0].Value);

            Assert.Equal(25, arr[1].Key);
            Assert.Equal("Alpha25", arr[1].Value);

            Assert.Equal(30, arr[2].Key);
            Assert.Equal("Alpha30", arr[2].Value);

            Assert.Equal(35, arr[3].Key);
            Assert.Equal("Alpha35", arr[3].Value);

            Assert.Equal(65, arr[4].Key);
            Assert.Equal("Alpha65", arr[4].Value);

            Assert.Equal(70, arr[5].Key);
            Assert.Equal("Alpha70", arr[5].Value);

            Assert.Equal(75, arr[6].Key);
            Assert.Equal("Alpha75", arr[6].Value);
        }

        [Fact]
        public void ForEachEnumeratorTest()
        {
            var bt = new BTree<int, string>(4);

            bt.Add(50, "Alpha50");
            bt.Add(70, "Alpha70");
            bt.Add(30, "Alpha30");
            bt.Add(35, "Alpha35");
            bt.Add(25, "Alpha25");
            bt.Add(65, "Alpha65");
            bt.Add(75, "Alpha75");

            bt.TraversalType = TreeTraversalType.PreOrder;

            System.Collections.Generic.List<int> lst = new System.Collections.Generic.List<int>();
            foreach (KeyValuePair<int, string> thisOne in bt)
            {
                lst.Add(thisOne.Key);
            }

            Assert.Equal(50, lst[0]);
            Assert.Equal(25, lst[1]);
            Assert.Equal(30, lst[2]);
            Assert.Equal(35, lst[3]);
            Assert.Equal(65, lst[4]);
            Assert.Equal(70, lst[5]);
            Assert.Equal(75, lst[6]);

            lst.Clear();

            bt.TraversalType = TreeTraversalType.PostOrder;
            foreach (KeyValuePair<int, string> thisOne in bt)
            {
                lst.Add(thisOne.Key);
            }

            Assert.Equal(25, lst[0]);
            Assert.Equal(30, lst[1]);
            Assert.Equal(35, lst[2]);
            Assert.Equal(65, lst[3]);
            Assert.Equal(70, lst[4]);
            Assert.Equal(75, lst[5]);
            Assert.Equal(50, lst[6]);

            lst.Clear();

            bt.TraversalType = TreeTraversalType.InOrder;
            foreach (KeyValuePair<int, string> thisOne in bt)
            {
                lst.Add(thisOne.Key);
            }

            Assert.Equal(25, lst[0]);
            Assert.Equal(30, lst[1]);
            Assert.Equal(35, lst[2]);
            Assert.Equal(50, lst[3]);
            Assert.Equal(65, lst[4]);
            Assert.Equal(70, lst[5]);
            Assert.Equal(75, lst[6]);

            lst.Clear();

            bt.TraversalType = TreeTraversalType.LevelOrder;
            foreach (KeyValuePair<int, string> thisOne in bt)
            {
                lst.Add(thisOne.Key);
            }

            Assert.Equal(50, lst[0]);
            Assert.Equal(25, lst[1]);
            Assert.Equal(30, lst[2]);
            Assert.Equal(35, lst[3]);
            Assert.Equal(65, lst[4]);
            Assert.Equal(70, lst[5]);
            Assert.Equal(75, lst[6]);

        }

    }
}
