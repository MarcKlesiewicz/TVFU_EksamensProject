using System;

namespace Persistence.Data
{
    public interface IData
    {
        void Save(string input);
        void Update(string input, Guid Id);
    }
}
