using System;

namespace EasyGet
{
    public sealed class Id : IEquatable<Id>
    {
        private readonly string value;

        public Id(string value)
        {
            this.value = value.ToLowerInvariant();
        }

        public bool Equals(Id other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return value == other.value;
        }

        public override string ToString() => value;
        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is Id other && Equals(other);
        public override int GetHashCode() => value.GetHashCode();
        public static bool operator ==(Id left, Id right) => Equals(left, right);
        public static bool operator !=(Id left, Id right) => !Equals(left, right);
    }
}