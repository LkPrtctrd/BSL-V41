namespace BSL.v41.Tools.LaserMap;

public class MultiMap<TKey, TValue> where TKey : notnull
{
    private readonly Dictionary<TKey, IList<TValue>> _storage = new();

    public IEnumerable<TKey> Keys => _storage.Keys;

    public IList<TValue> this[TKey key]
    {
        get
        {
            if (!_storage.TryGetValue(key, out var value))
                throw new KeyNotFoundException(
                    $"The given key {key} was not found in the collection.");
            return value;
        }
    }

    public void Add(TKey key, TValue value)
    {
        if (!_storage.ContainsKey(key)) _storage.Add(key, new List<TValue>());
        _storage[key].Add(value);
    }

    public bool ContainsKey(TKey key)
    {
        return _storage.ContainsKey(key);
    }
}