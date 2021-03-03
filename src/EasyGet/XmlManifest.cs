using System.Threading.Tasks;
using System.Xml;

namespace EasyGet
{
    public sealed class XmlManifest : Manifest
    {
        private readonly XmlDocument content;
        private readonly Feed feed;

        public XmlManifest(Feed feed, XmlDocument content)
        {
            this.feed = feed;
            this.content = content;
        }

        public override Reference Reference { get; }

        public override async Task<Package> PackageAsync()
        {
            return await feed.PackageAsync(Reference);
        }
    }
}