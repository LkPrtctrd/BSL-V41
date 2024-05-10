namespace BSL.v41.Titan.Mathematical.Massive;

public class LogicArrayList<T>
{
    private T[] _items;

    public LogicArrayList()
    {
        _items = Array.Empty<T>();
    }

    public LogicArrayList(int initialCapacity)
    {
        _items = new T[initialCapacity];
    }

    public int Count { get; private set; }

    public T this[int index]
    {
        get
        {
            if (index < Count && index > -1) return _items[index];

            return default!;
        }
        set
        {
            if (index < Count && index > -1) _items[index] = value;
        }
    }

    public void Add(T item)
    {
        var size = _items.Length;
        if (size == Count) EnsureCapacity(size != 0 ? size * 2 : 5);
        _items[Count++] = item;
    }

    public void Add(int index, T item)
    {
        var size = _items.Length;
        if (size == Count) EnsureCapacity(size != 0 ? size * 2 : 5);

        if (Count > index) Array.Copy(_items, index, _items, index + 1, Count - index);
        _items[index] = item;
        Count += 1;
    }

    public int IndexOf(T item)
    {
        for (var i = 0; i < Count; i++)
            if (_items[i]!.Equals(item))
                return i;

        return -1;
    }

    public void Remove(int index)
    {
        if (index >= Count || index <= -1) return;
        Count -= 1;

        if (index != Count) Array.Copy(_items, index + 1, _items, index, Count - index);
        _items[Count] = default!;
    }

    public void EnsureCapacity(int count)
    {
        var size = _items.Length;
        if (size >= count) return;
        var tmp = new T[count];

        Array.Copy(_items, tmp, size);
        _items = tmp;
    }

    public void Clear()
    {
        Count = 0;
    }

    ~LogicArrayList()
    {
        _items = null!;
        Count = 0;
    }
}