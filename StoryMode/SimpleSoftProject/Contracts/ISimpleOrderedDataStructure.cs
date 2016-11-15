using System;
using System.Collections.Generic;

namespace SimpleSoftProject.Contracts
{
    public interface ISimpleOrderedDataStructure<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        bool Remove(T element);

        int Capacity { get; }

        int Size { get; }

        void Add(T element);

        void AddAll(ICollection<T> elements);

        string JoinWith(string joiner);
    }
}
