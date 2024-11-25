namespace RecordEquality.Tests.Models;

public record BadOrder(string OrderId, string CustomerName, List<OrderItem> Items);

public record GoodOrder(string OrderId, string CustomerName, ValueCollection<OrderItem> Items);

public record OrderItem(string ProductName, int Quantity, decimal Price);