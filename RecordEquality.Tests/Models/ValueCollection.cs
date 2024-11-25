using System.Collections.ObjectModel;
using System.Text;

namespace RecordEquality.Tests.Models;

/// <summary>
/// Represents a collection of values.
/// </summary>
/// <param name="values">The values to initialize the collection with.</param>
/// <typeparam name="T">The type of the values in the collection.</typeparam>
public class ValueCollection<T>(params IList<T> values)
    : ReadOnlyCollection<T>(new List<T>(values))
{
    /// <summary>
    /// Adds an item to the collection.
    /// </summary>
    /// <param name="item">The object to add to the value collection.</param>
    /// <exception cref="ArgumentNullException">Thrown when item is null.</exception>
    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item);
        
        Items.Add(item);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// The comparison is done by comparing the items in the collection.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }
        
        if (ReferenceEquals(this, obj))
        {
            return true;
        }
        
        return obj is ValueCollection<T> other
               && other.Items.SequenceEqual(Items);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        HashCode hashCode = new();
        foreach (T item in Items)
        {
            hashCode.Add(item);
        }
        
        return hashCode.ToHashCode();
    }
    
    /// <summary>
    /// Returns a string that represents the current object.
    /// If the collection has more than 3 items, only the first 3 items are shown.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.Append("[ ");

        // if we have more than 3 items, we only show the first 3 then "..."
        if (Items.Count > 3)
        {
            sb.AppendJoin(", ", Items.Take(3));
            sb.Append(", ...");
        }
        else
        {
            sb.AppendJoin(", ", Items);
        }
        
        sb.Append(" ]");
        
        return sb.ToString();
    }
}