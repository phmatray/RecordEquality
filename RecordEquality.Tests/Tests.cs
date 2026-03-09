using RecordEquality.Tests.Models;

namespace RecordEquality.Tests;

public class Tests
{
    [Fact]
    public void TestOrderItemEquality()
    {
        OrderItem item1 = new("ProductA", 2, 10.0m);
        OrderItem item2 = new("ProductA", 2, 10.0m);

        
            Assert.Equal(item2.ProductName, item1.ProductName);
            Assert.Equal(item2.Quantity, item1.Quantity);
            Assert.Equal(item2.Price, item1.Price);
            Assert.Equal(item2, item1);
    }
    
    [Fact]
    public void TestBadOrderEquality()
    {
        OrderItem item1 = new("ProductA", 2, 10.0m);
        OrderItem item2 = new("ProductB", 1, 20.0m);

        BadOrder order1 = new("Order1", "John Doe", [item1, item2]);
        BadOrder order2 = new("Order1", "John Doe", [item1, item2]);

        
            Assert.Equal(order2.OrderId, order1.OrderId);
            Assert.Equal(order2.CustomerName, order1.CustomerName);
            Assert.Equal(order2.Items[0], order1.Items[0]);
            Assert.Equal(order2.Items[1], order1.Items[1]);
            
            // :-( COMPARISON FAILS BECAUSE THE COLLECTIONS ARE REFERENCE EQUAL
            Assert.NotEqual(order2, order1);
    }
    
    [Fact]
    public void TestGoodOrderEquality()
    {
        OrderItem item1 = new("ProductA", 2, 10.0m);
        OrderItem item2 = new("ProductB", 1, 20.0m);

        GoodOrder order1 = new("Order1", "John Doe", [item1, item2]);
        GoodOrder order2 = new("Order1", "John Doe", [item1, item2]);

        
            Assert.Equal(order2.OrderId, order1.OrderId);
            Assert.Equal(order2.CustomerName, order1.CustomerName);
            Assert.Equal(order2.Items[0], order1.Items[0]);
            Assert.Equal(order2.Items[1], order1.Items[1]);
            
            // :-) THIS LINE WILL PASS BECAUSE THE COLLECTIONS ARE VALUE EQUAL
            Assert.Equal(order2, order1);
    }
    
    [Fact]
    public void TestValueCollectionAdd()
    {
        ValueCollection<string> collection =
        [
            "One",
            "Two",
            "Three"

        ];

        
            Assert.Equal(3, collection.Count);
            Assert.Equal("One", collection[0]);
            Assert.Equal("Two", collection[1]);
            Assert.Equal("Three", collection[2]);
    }

    [Fact]
    public void TestValueCollectionToString()
    {
        ValueCollection<string> collection = ["One", "Two", "Three"];
        const string expected = "[ One, Two, Three ]";
        
        Assert.Equal(expected, collection.ToString());
    }
    
    [Fact]
    public void TestValueCollectionToStringWithMoreThan3Items()
    {
        ValueCollection<string> collection = ["One", "Two", "Three", "Four"];
        const string expected = "[ One, Two, Three, ... ]";
        
        Assert.Equal(expected, collection.ToString());
    }
    
    [Fact]
    public void TestValueCollectionEquality()
    {
        ValueCollection<string> collection1 = ["One", "Two", "Three"];
        ValueCollection<string> collection2 = ["One", "Two", "Three"];

        Assert.Equal(collection2, collection1);
    }

    [Fact]
    public void TestValueCollectionInequality()
    {
        ValueCollection<string> collection1 = ["One", "Two", "Three"];
        ValueCollection<string> collection2 = ["One", "Two", "Four"];

        Assert.NotEqual(collection2, collection1);
    }

    [Fact]
    public void TestValueCollectionNullEquality()
    {
        ValueCollection<string> collection = ["One", "Two", "Three"];

        Assert.False(collection.Equals(null));
    }

    [Fact]
    public void TestValueCollectionHashCode()
    {
        ValueCollection<string> collection1 = ["One", "Two", "Three"];
        ValueCollection<string> collection2 = ["One", "Two", "Three"];

        Assert.Equal(collection2.GetHashCode(), collection1.GetHashCode());
    }

    [Fact]
    public void TestValueCollectionAddNull()
    {
        ValueCollection<string> collection = [];

        Assert.Throws<ArgumentNullException>(() => collection.Add(null));
    }
    
    [Fact]
    public void TestValueCollectionImplicitConversionFromArray()
    {
        string[] items = ["One", "Two", "Three"];
        ValueCollection<string> collection = items;

        
            Assert.Equal(3, collection.Count);
            Assert.Equal("One", collection[0]);
            Assert.Equal("Two", collection[1]);
            Assert.Equal("Three", collection[2]);
    }

    [Fact]
    public void TestValueCollectionImplicitConversionFromList()
    {
        List<string> items = ["One", "Two", "Three"];
        ValueCollection<string> collection = items;

        
            Assert.Equal(3, collection.Count);
            Assert.Equal("One", collection[0]);
            Assert.Equal("Two", collection[1]);
            Assert.Equal("Three", collection[2]);
    }

    [Fact]
    public void TestValueCollectionGetEnumerator()
    {
        ValueCollection<string> collection = ["One", "Two", "Three"];
        var enumerator = collection.GetEnumerator();
        
        
            Assert.True(enumerator.MoveNext());
            Assert.Equal("One", enumerator.Current);
            
            Assert.True(enumerator.MoveNext());
            Assert.Equal("Two", enumerator.Current);
            
            Assert.True(enumerator.MoveNext());
            Assert.Equal("Three", enumerator.Current);
            
            Assert.False(enumerator.MoveNext());
    }
    
    [Fact]
    public void TestValueCollectionClone()
    {
        ValueCollection<string> collection = ["One", "Two", "Three"];
        ValueCollection<string> clone = (ValueCollection<string>)collection.Clone();
        
        
            Assert.Equal(clone, collection);
            Assert.Equal(clone.GetHashCode(), collection.GetHashCode());
    }
    
    // Indexer
    [Fact]
    public void TestValueCollectionIndexer()
    {
        ValueCollection<string> collection = ["One", "Two", "Three"];
        
        
            Assert.Equal("One", collection[0]);
            Assert.Equal("Two", collection[1]);
            Assert.Equal("Three", collection[2]);
            
            Assert.Equal("Three", collection[^1]);
            Assert.Equal("Two", collection[^2]);
            Assert.Equal("One", collection[^3]);
    }
}