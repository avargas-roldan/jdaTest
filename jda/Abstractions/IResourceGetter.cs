using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace jda.Abstractions
{
    public interface IResourceGetter
    {
        Task<byte[]> GetResource();
    }
}
