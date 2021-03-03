using System.Threading.Tasks;

namespace EasyGet
{
    public abstract class Manifest
    {
        public abstract Reference Reference { get; }
        public abstract Task<Package> PackageAsync();
    }
}