using System; 
using System.Linq;
using Shouldly;
using Xunit;

namespace MultiValueDictionary
{
    public class MultiValueDictionaryTests
    {
        private MultiValueDictionary multiValueDictionary = new MultiValueDictionary();


        [Fact]
        public void Add_ShouldAdd_WhenNotExists()
        {
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "baz");
            multiValueDictionary.Add("bang", "baz");
            multiValueDictionary.Add("bang", "bar");

            var items = multiValueDictionary.Items();

            items.Count().ShouldBe(4);

            items.Count(e => e.Item1 == "foo" && e.Item2 == "bar").ShouldBe(1);
            items.Count(e => e.Item1 == "foo" && e.Item2 == "baz").ShouldBe(1);
            items.Count(e => e.Item1 == "bang" && e.Item2 == "bar").ShouldBe(1);
            items.Count(e => e.Item1 == "bang" && e.Item2 == "baz").ShouldBe(1);
        }


        [Fact]
        public void Add_ShouldThrowException_WhenAlreadyExists()
        {
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "bar2");

            Should.Throw<Exception>(() => multiValueDictionary.Add("foo", "bar"))
                .Message.ShouldBe("ERROR, value already exists");
        }
        

        [Fact]
        public void Remove_ShouldThrowException_WhenValueNotExists()
        {
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "baz");
            multiValueDictionary.Add("bang", "baz");
            multiValueDictionary.Add("bang", "bar");

            Should.Throw<Exception>(() => multiValueDictionary.Remove("bang", "barbarbar"))
                .Message.ShouldBe("ERROR, value does not exist");


            var items = multiValueDictionary.Items();

            items.Count().ShouldBe(4);

            items.Count(e => e.Item1 == "foo" && e.Item2 == "bar").ShouldBe(1);
            items.Count(e => e.Item1 == "foo" && e.Item2 == "baz").ShouldBe(1);
            items.Count(e => e.Item1 == "bang" && e.Item2 == "bar").ShouldBe(1);
            items.Count(e => e.Item1 == "bang" && e.Item2 == "baz").ShouldBe(1);
        }

        [Fact]
        public void Remove_ShouldThrowException_WhenKeyNotExists()
        {
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "baz");
            multiValueDictionary.Add("bang", "baz");
            multiValueDictionary.Add("bang", "bar");

            Should.Throw<Exception>(() => multiValueDictionary.Remove("notexistKey", "bar"))
                .Message.ShouldBe("ERROR, key does not exist");

            var items = multiValueDictionary.Items();

            items.Count().ShouldBe(4);

            items.Count(e => e.Item1 == "foo" && e.Item2 == "bar").ShouldBe(1);
            items.Count(e => e.Item1 == "foo" && e.Item2 == "baz").ShouldBe(1);
            items.Count(e => e.Item1 == "bang" && e.Item2 == "bar").ShouldBe(1);
            items.Count(e => e.Item1 == "bang" && e.Item2 == "baz").ShouldBe(1);
        }

        [Fact]
        public void Clear_ShouldClearDictionary()
        {
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "bar2");

            multiValueDictionary.Clear();

            multiValueDictionary.Items().Count().ShouldBe(0);
        }

        [Fact]
        public void Remove_ShouldRemoveValue_WhenExists()
        {
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "baz");
            multiValueDictionary.Add("bang", "baz");
            multiValueDictionary.Add("bang", "bar");

            multiValueDictionary.Remove("foo","baz");
            
            var items = multiValueDictionary.Items();

            items.Count().ShouldBe(3);

            items.Count(e => e.Item1 == "foo" && e.Item2 == "bar").ShouldBe(1);
            items.Count(e => e.Item1 == "foo" && e.Item2 == "baz").ShouldBe(0);
            items.Count(e => e.Item1 == "bang" && e.Item2 == "bar").ShouldBe(1);
            items.Count(e => e.Item1 == "bang" && e.Item2 == "baz").ShouldBe(1);
        }


        [Fact]
        public void RemoveAll_ShouldRemoveKey_WhenExists()
        {
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "baz");
            multiValueDictionary.Add("bang", "baz");
            multiValueDictionary.Add("bang", "bar");

            multiValueDictionary.RemoveAll("foo");


            var items = multiValueDictionary.Items();

            items.Count().ShouldBe(2);

            items.Count(e => e.Item1 == "foo" && e.Item2 == "bar").ShouldBe(0);
            items.Count(e => e.Item1 == "foo" && e.Item2 == "baz").ShouldBe(0);
            items.Count(e => e.Item1 == "bang" && e.Item2 == "bar").ShouldBe(1);
            items.Count(e => e.Item1 == "bang" && e.Item2 == "baz").ShouldBe(1);
        }

        [Fact]
        public void RemoveAll_ShouldThrowException_WhenNotExists()
        {
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "baz");
            multiValueDictionary.Add("bang", "baz");
            multiValueDictionary.Add("bang", "bar");

            Should.Throw<Exception>(() => multiValueDictionary.RemoveAll("foo123"))
                .Message.ShouldBe("ERROR, key does not exist");

            var items = multiValueDictionary.Items();

            items.Count().ShouldBe(4);

            items.Count(e => e.Item1 == "foo" && e.Item2 == "bar").ShouldBe(1);
            items.Count(e => e.Item1 == "foo" && e.Item2 == "baz").ShouldBe(1);
            items.Count(e => e.Item1 == "bang" && e.Item2 == "bar").ShouldBe(1);
            items.Count(e => e.Item1 == "bang" && e.Item2 == "baz").ShouldBe(1);
        }


        [Fact]
        public void AllMembers_ShouldReturnAllMembers_WhenFound()
        {
            multiValueDictionary.Add("x", "y1");
            multiValueDictionary.Add("x", "y2");
            multiValueDictionary.Add("x", "y3");
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "baz");
            multiValueDictionary.Add("bang", "baz");
            multiValueDictionary.Add("bang", "bar");

            var members = multiValueDictionary.Members("foo");

            members.Count().ShouldBe(2);

            members.Count(e => e == "baz").ShouldBe(1);
            members.Count(e => e == "bar").ShouldBe(1);

            members = multiValueDictionary.AllMembers();

            members.Count().ShouldBe(7);

            members.Count(e => e == "y1").ShouldBe(1);
            members.Count(e => e == "y2").ShouldBe(1);
            members.Count(e => e == "y3").ShouldBe(1);
            members.Count(e => e == "bar").ShouldBe(2);
            members.Count(e => e == "baz").ShouldBe(2);
        }


        [Fact]
        public void Members_ShouldReturnMembers_WhenFound()
        {
            multiValueDictionary.Add("x", "y1");
            multiValueDictionary.Add("x", "y2");
            multiValueDictionary.Add("x", "y3");
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "baz");
            multiValueDictionary.Add("bang", "baz");
            multiValueDictionary.Add("bang", "bar");

            var members = multiValueDictionary.Members("foo");

            members.Count().ShouldBe(2);

            members.Count(e => e == "baz").ShouldBe(1);
            members.Count(e => e == "bar").ShouldBe(1);

            members = multiValueDictionary.Members("x");

            members.Count().ShouldBe(3);

            members.Count(e => e == "y1").ShouldBe(1);
            members.Count(e => e == "y2").ShouldBe(1);
            members.Count(e => e == "y3").ShouldBe(1);
        }


        [Fact]
        public void Members_ShouldThrowException_WhenKeyNotFound()
        {
            multiValueDictionary.Add("x", "y1");
            multiValueDictionary.Add("x", "y2");
            multiValueDictionary.Add("x", "y3");
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "baz");
            multiValueDictionary.Add("bang", "baz");
            multiValueDictionary.Add("bang", "bar");

            Should.Throw<Exception>(() => multiValueDictionary.Members("foo123567"))
                .Message.ShouldBe("ERROR, key does not exist");

        }
         
        [Fact]
        public void KeyExists_ShouldReturnFalse_WhenNotExists()
        {
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "baz");
            multiValueDictionary.Add("bang", "baz");
            multiValueDictionary.Add("bang", "bar");

            multiValueDictionary.KeyExists("foo1234").ShouldBeFalse();
        }

        [Fact]
        public void KeyExists_ShouldReturnTrue_WhenExists()
        {
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "baz");
            multiValueDictionary.Add("bang", "baz");
            multiValueDictionary.Add("bang", "bar");

            multiValueDictionary.KeyExists("foo").ShouldBeTrue();
        }

        [Fact]
        public void ValueExists_ShouldReturnTrue_WhenBothKeyAndValueExist()
        {
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "baz");
            multiValueDictionary.Add("bang", "baz");
            multiValueDictionary.Add("bang", "bar");

            multiValueDictionary.ValueExists("bang", "baz").ShouldBeTrue();
        }

        [Fact]
        public void ValueExists_ShouldReturnFalse_WhenKeyNotExists()
        {
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "baz");
            multiValueDictionary.Add("bang", "baz");
            multiValueDictionary.Add("bang", "bar");

            multiValueDictionary.ValueExists("bang12345", "foo1234").ShouldBeFalse();
        }

        [Fact]
        public void ValueExists_ShouldReturnFalse_WhenValueNotExists()
        {
            multiValueDictionary.Add("foo", "bar");
            multiValueDictionary.Add("foo", "baz");
            multiValueDictionary.Add("bang", "baz");
            multiValueDictionary.Add("bang", "bar");

            multiValueDictionary.ValueExists("bang","foo1234").ShouldBeFalse();
        }
    }
}