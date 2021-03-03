using NUnit.Framework;

namespace EasyGet.Tests
{
    public sealed class StrictVersionFormatShould
    {
        private readonly StrictVersionFormat sut = new();

        public static TestCaseData[] ParseCases => new[]
        {
            new TestCaseData("1.0.0").Returns(new Version(1, 0, 0)),
            new TestCaseData("1.41.9").Returns(new Version(1, 41, 9)),
            new TestCaseData("2.5.1-Dev").Returns(new Version(2, 5, 1, "Dev")),
            new TestCaseData("2.0.1-00").Returns(new Version(2, 0, 1, "00")),
            new TestCaseData("2.0.0001-dev-00890").Returns(new Version(2, 0, 1, "dev-00890"))
        };

        [Test]
        [TestCaseSource(nameof(ParseCases))]
        public Version Parse(string argument) => sut.Parse(argument);
    }
}