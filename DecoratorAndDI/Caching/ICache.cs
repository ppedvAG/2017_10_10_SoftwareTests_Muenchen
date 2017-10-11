namespace DecoratorAndDI.Caching
{
    internal interface ICache
    {
        bool Contains(string key);
        void Add<T>(string key, T value);
        T Get<T>(string key);
    }
}
