using RecordEquality.Tests.Models;

namespace RecordEquality.Tests;

public class Tests
{
    [Test]
    public void TestOrderItemEquality()
    {
        OrderItem item1 = new("ProductA", 2, 10.0m);
        OrderItem item2 = new("ProductA", 2, 10.0m);

        Assert.Multiple(() =>
        {
            Assert.That(item1.ProductName, Is.EqualTo(item2.ProductName));
            Assert.That(item1.Quantity, Is.EqualTo(item2.Quantity));
            Assert.That(item1.Price, Is.EqualTo(item2.Price));
            Assert.That(item1, Is.EqualTo(item2));
        });
    }
    
    [Test]
    public void TestBadOrderEquality()
    {
        OrderItem item1 = new("ProductA", 2, 10.0m);
        OrderItem item2 = new("ProductB", 1, 20.0m);

        BadOrder order1 = new("Order1", "John Doe", [item1, item2]);
        BadOrder order2 = new("Order1", "John Doe", [item1, item2]);

        Assert.Multiple(() =>
        {
            Assert.That(order1.OrderId, Is.EqualTo(order2.OrderId));
            Assert.That(order1.CustomerName, Is.EqualTo(order2.CustomerName));
            Assert.That(order1.Items[0], Is.EqualTo(order2.Items[0]));
            Assert.That(order1.Items[1], Is.EqualTo(order2.Items[1]));
            
            // :-( COMPARISON FAILS BECAUSE THE COLLECTIONS ARE REFERENCE EQUAL
            Assert.That(order1, Is.Not.EqualTo(order2));
        });
    }
    
    [Test]
    public void TestGoodOrderEquality()
    {
        OrderItem item1 = new("ProductA", 2, 10.0m);
        OrderItem item2 = new("ProductB", 1, 20.0m);

        GoodOrder order1 = new("Order1", "John Doe", [item1, item2]);
        GoodOrder order2 = new("Order1", "John Doe", [item1, item2]);

        Assert.Multiple(() =>
        {
            Assert.That(order1.OrderId, Is.EqualTo(order2.OrderId));
            Assert.That(order1.CustomerName, Is.EqualTo(order2.CustomerName));
            Assert.That(order1.Items[0], Is.EqualTo(order2.Items[0]));
            Assert.That(order1.Items[1], Is.EqualTo(order2.Items[1]));
            
            // :-) THIS LINE WILL PASS BECAUSE THE COLLECTIONS ARE VALUE EQUAL
            Assert.That(order1, Is.EqualTo(order2));
        });
    }
    
    [Test]
    public void TestValueCollectionAdd()
    {
        ValueCollection<string> collection =
        [
            "One",
            "Two",
            "Three"

        ];

        Assert.Multiple(() =>
        {
            Assert.That(collection, Has.Count.EqualTo(3));
            Assert.That(collection[0], Is.EqualTo("One"));
            Assert.That(collection[1], Is.EqualTo("Two"));
            Assert.That(collection[2], Is.EqualTo("Three"));
        });
    }

    [Test]
    public void TestValueCollectionToString()
    {
        ValueCollection<string> collection = ["One", "Two", "Three"];
        const string expected = "[ One, Two, Three ]";
        
        Assert.That(collection.ToString(), Is.EqualTo(expected));
    }
    
    [Test]
    public void TestValueCollectionToStringWithMoreThan3Items()
    {
        ValueCollection<string> collection = ["One", "Two", "Three", "Four"];
        const string expected = "[ One, Two, Three, ... ]";
        
        Assert.That(collection.ToString(), Is.EqualTo(expected));
    }
    
    [Test]
    public void TestValueCollectionEquality()
    {
        ValueCollection<string> collection1 = ["One", "Two", "Three"];
        ValueCollection<string> collection2 = ["One", "Two", "Three"];

        Assert.That(collection1, Is.EqualTo(collection2));
    }

    [Test]
    public void TestValueCollectionInequality()
    {
        ValueCollection<string> collection1 = ["One", "Two", "Three"];
        ValueCollection<string> collection2 = ["One", "Two", "Four"];

        Assert.That(collection1, Is.Not.EqualTo(collection2));
    }

    [Test]
    public void TestValueCollectionNullEquality()
    {
        ValueCollection<string> collection = ["One", "Two", "Three"];

        Assert.That(collection.Equals(null), Is.False);
    }

    [Test]
    public void TestValueCollectionHashCode()
    {
        ValueCollection<string> collection1 = ["One", "Two", "Three"];
        ValueCollection<string> collection2 = ["One", "Two", "Three"];

        Assert.That(collection1.GetHashCode(), Is.EqualTo(collection2.GetHashCode()));
    }

    [Test]
    public void TestValueCollectionAddNull()
    {
        ValueCollection<string> collection = [];

        Assert.Throws<ArgumentNullException>(() => collection.Add(null));
    }
    
    [Test]
    public void TestValueCollectionImplicitConversionFromArray()
    {
        string[] items = ["One", "Two", "Three"];
        ValueCollection<string> collection = items;

        Assert.Multiple(() =>
        {
            Assert.That(collection, Has.Count.EqualTo(3));
            Assert.That(collection[0], Is.EqualTo("One"));
            Assert.That(collection[1], Is.EqualTo("Two"));
            Assert.That(collection[2], Is.EqualTo("Three"));
        });
    }

    [Test]
    public void TestValueCollectionImplicitConversionFromList()
    {
        List<string> items = ["One", "Two", "Three"];
        ValueCollection<string> collection = items;

        Assert.Multiple(() =>
        {
            Assert.That(collection, Has.Count.EqualTo(3));
            Assert.That(collection[0], Is.EqualTo("One"));
            Assert.That(collection[1], Is.EqualTo("Two"));
            Assert.That(collection[2], Is.EqualTo("Three"));
        });
    }

    [Test]
    public void TestValueCollectionGetEnumerator()
    {
        ValueCollection<string> collection = ["One", "Two", "Three"];
        var enumerator = collection.GetEnumerator();
        
        Assert.Multiple(() =>
        {
            Assert.That(enumerator.MoveNext(), Is.True);
            Assert.That(enumerator.Current, Is.EqualTo("One"));
            
            Assert.That(enumerator.MoveNext(), Is.True);
            Assert.That(enumerator.Current, Is.EqualTo("Two"));
            
            Assert.That(enumerator.MoveNext(), Is.True);
            Assert.That(enumerator.Current, Is.EqualTo("Three"));
            
            Assert.That(enumerator.MoveNext(), Is.False);
        });
    }
    
    [Test]
    public void TestValueCollectionClone()
    {
        ValueCollection<string> collection = ["One", "Two", "Three"];
        ValueCollection<string> clone = (ValueCollection<string>)collection.Clone();
        
        Assert.Multiple(() =>
        {
            Assert.That(collection, Is.EqualTo(clone));
            Assert.That(collection.GetHashCode(), Is.EqualTo(clone.GetHashCode()));
        });
    }
    
    // Indexer
    [Test]
    public void TestValueCollectionIndexer()
    {
        ValueCollection<string> collection = ["One", "Two", "Three"];
        
        Assert.Multiple(() =>
        {
            Assert.That(collection[0], Is.EqualTo("One"));
            Assert.That(collection[1], Is.EqualTo("Two"));
            Assert.That(collection[2], Is.EqualTo("Three"));
            
            Assert.That(collection[^1], Is.EqualTo("Three"));
            Assert.That(collection[^2], Is.EqualTo("Two"));
            Assert.That(collection[^3], Is.EqualTo("One"));
        });
    }
}