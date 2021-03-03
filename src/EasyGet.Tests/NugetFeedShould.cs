using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace EasyGet.Tests
{
    public sealed class NugetFeedShould
    {
        [Test]
        public async Task ListVersions()
        {
            var feed = new NugetFeed(
                new Uri("https://api.nuget.org"),
                new HttpClient()
            );

            var versions = await feed.VersionsAsync(new Id("Serilog"));

            versions.Should().Contain(new Version(2, 9, 1, "dev-01154"));
        }
    }
}