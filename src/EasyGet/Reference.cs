using System;

namespace EasyGet
{
    public sealed class Reference : IEquatable<Reference>
    {
        public Reference(Id id, Version version)
        {
            Id = id;
            Version = version;
        }

        public Id Id { get; }
        public Version Version { get; }
        public override string ToString() => $"{Id} {Version}";

        public bool Equals(Reference other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other)) 
                return true;

            return Id.Equals(other.Id) && Version.Equals(other.Version);
        }

        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is Reference other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode() * 397) ^ Version.GetHashCode();
            }
        }

        public static bool operator ==(Reference left, Reference right) => Equals(left, right);
        public static bool operator !=(Reference left, Reference right) => !Equals(left, right);
    }
}