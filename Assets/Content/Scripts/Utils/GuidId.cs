using System;

namespace Helpers
{
    [Serializable]
    public struct GuidId : IEquatable<GuidId>
    {
        public uint first;
        public uint second;
        public uint third;
        public uint fourth;

        public GuidId(Guid guid) : this()
        {
            first = 0;
            second = 0;
            third = 0;
            fourth = 0;
            ConvertGuidToFields(guid);
        }

        public GuidId(uint first, uint second, uint third, uint fourth)
        {
            this.first = first;
            this.second = second;
            this.third = third;
            this.fourth = fourth;
        }

        public static GuidId ToEmpty() => new GuidId(0, 0, 0, 0);
        
        public bool Equals(GuidId other)
        {
            return first == other.first && second == other.second && third == other.third &&
                   fourth == other.fourth;
        }

        public override bool Equals(object obj)
        {
            return obj is GuidId guid && Equals(guid);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(first, second, third, fourth);
        }

        public static implicit operator Guid(GuidId guidId) => guidId.ToGuid();
        public static implicit operator GuidId(Guid guid) => new (guid);
        public static Guid NewGuid() => Guid.NewGuid();

        public static bool operator ==(GuidId first, GuidId second) => first.Equals(second);

        public static bool operator !=(GuidId first, GuidId second) => !(first == second);
        
        public Guid ToGuid()
        {
            var bytes = new byte[16];
            BitConverter.GetBytes(first).CopyTo(bytes, 0);
            BitConverter.GetBytes(second).CopyTo(bytes, 4);
            BitConverter.GetBytes(third).CopyTo(bytes, 8);
            BitConverter.GetBytes(fourth).CopyTo(bytes, 12);

            return new Guid(bytes);
        }

        private void ConvertGuidToFields(Guid guid)
        {
            var bytes = guid.ToByteArray();
            first = BitConverter.ToUInt32(bytes, 0);
            second = BitConverter.ToUInt32(bytes, 4);
            third = BitConverter.ToUInt32(bytes, 8);
            fourth = BitConverter.ToUInt32(bytes, 12);
        }
    }
}