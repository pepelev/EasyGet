using System.IO;
using System.Threading.Tasks;

namespace EasyGet
{
    public abstract class Package
    {
        public abstract Manifest Manifest { get; }
        public abstract ValueTask Write(Stream destination);
    }
}