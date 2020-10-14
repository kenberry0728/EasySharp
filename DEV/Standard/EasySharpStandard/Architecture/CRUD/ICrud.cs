using System.Collections.Generic;

namespace EasySharp.Architecture.CRUD
{
    public interface IKey<TKey>
    {
        TKey Key { get; }
    }

    public interface ICrud<TKey, TEntry>
    where TEntry : IKey<TKey>
    {
        void Create(TEntry entry);

        TEntry ReadOne(TKey identity);

        IEnumerable<TEntry> ReadAll();

        void Update(TEntry entry);

        void Delete(TEntry entry);
    }
}