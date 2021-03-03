using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EasyGet
{
    public sealed class StrictVersionFormat : VersionFormat
    {
        private static readonly Regex pattern = new Regex(
            @"^\s*(?<Major>\d+)\.(?<Minor>\d+)\.(?<Patch>\d+)(-(?<Suffix>[0-9A-Za-z-]+))?\s*$",
            RegexOptions.CultureInvariant | RegexOptions.Singleline | RegexOptions.Compiled
        );

        public override Version Parse(string argument)
        {
            if (pattern.Match(argument) is {Success: true} match)
            {
                var suffix = match.Groups["Suffix"];
                return new Version(
                    int.Parse(match.Groups["Major"].Value, CultureInfo.InvariantCulture),
                    int.Parse(match.Groups["Minor"].Value, CultureInfo.InvariantCulture),
                    int.Parse(match.Groups["Patch"].Value, CultureInfo.InvariantCulture),
                    suffix.Success
                        ? suffix.Value
                        : ""
                );
            }

            throw new FormatException("Could not parse version");
        }
    }
}