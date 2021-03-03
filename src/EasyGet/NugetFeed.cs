using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace EasyGet
{
    public sealed class NugetFeed : Feed
    {
        private readonly Uri address;
        private readonly HttpClient http;
        private readonly Cached<Uri> packageBaseAddress;
        private readonly VersionFormat versionFormat;

        public NugetFeed(Uri address, HttpClient http)
            : this(address, new StrictVersionFormat(), http)
        {
        }

        public NugetFeed(Uri address, VersionFormat versionFormat, HttpClient http)
        {
            this.address = address;
            this.http = http;
            this.versionFormat = versionFormat;
            packageBaseAddress = new Cached<Uri>(PackageBaseAddressAsync);
        }

        private async Task<Uri> PackageBaseAddressAsync()
        {
            var response = await http.GetAsync(new Uri(address, "v3/index.json"));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);
            var token = json.SelectToken("$.resources[?(@.@type == 'PackageBaseAddress/3.0.0')].@id", true);
            return new Uri(token.Value<string>());
        }

        public override async Task<IReadOnlyList<Version>> VersionsAsync(Id packageId)
        {
            var baseAddress = await packageBaseAddress.ValueAsync();
            var indexAddress = new Uri(baseAddress, $"{Uri.EscapeUriString(packageId.ToString())}/index.json");
            var message = await http.GetAsync(indexAddress);
            message.EnsureSuccessStatusCode();
            var content = await message.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);
            var versions = json["versions"].Values<string>();
            return versions
                .Select(token => versionFormat.Parse(token))
                .ToList();
        }

        public override async Task<Manifest> ManifestAsync(Reference reference)
        {
            throw new NotImplementedException();
        }

        public override async Task<Package> PackageAsync(Reference reference)
        {
            throw new NotImplementedException();
        }
    }
}