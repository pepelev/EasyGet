using System;
using System.Globalization;

namespace EasyGet
{
    public sealed class Version : IEquatable<Version>
    {
        public Version(int major, int minor, int patch)
            : this(major, minor, patch, "")
        {
        }

        public Version(int major, int minor, int patch, string suffix)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            Suffix = suffix;
        }

        public int Major { get; }
        public int Minor { get; }
        public int Patch { get; }
        public string Suffix { get; }

        public bool Equals(Version other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Major == other.Major &&
                   Minor == other.Minor &&
                   Patch == other.Patch &&
                   Suffix == other.Suffix;
        }

        public override string ToString()
        {
            var main = $"{Major.ToString(CultureInfo.InvariantCulture)}.{Minor.ToString(CultureInfo.InvariantCulture)}.{Patch.ToString(CultureInfo.InvariantCulture)}";
            if (string.IsNullOrWhiteSpace(Suffix))
            {
                return main;
            }

            return $"{main}-{Suffix}";
        }

        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is Version other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Major;
                hashCode = (hashCode * 397) ^ Minor;
                hashCode = (hashCode * 397) ^ Patch;
                hashCode = (hashCode * 397) ^ Suffix.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Version left, Version right) => Equals(left, right);
        public static bool operator !=(Version left, Version right) => !Equals(left, right);
    }
}