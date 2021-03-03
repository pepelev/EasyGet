using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyGet
{
    public abstract class Feed
    {
        public abstract Task<IReadOnlyList<Version>> VersionsAsync(Id packageId);
        public abstract Task<Manifest> ManifestAsync(Reference reference);
        public abstract Task<Package> PackageAsync(Reference reference);
    }
}