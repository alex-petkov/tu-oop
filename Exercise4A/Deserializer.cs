using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise4A
{
    public interface ISerializable
    {
        byte[] Serialize();
    }

    public interface IDeserializer
    {
        ISerializable Deserialize(byte[] from,
                                  ref int readIndex);
        int ForTypeDescriptor { get; }
    }

    class TypeDeserializer
    {
        Dictionary<int, IDeserializer> deserializers
            = new Dictionary<int, IDeserializer>();

        public void RegisterDeserializer(IDeserializer d)
        {
            deserializers.Add(d.ForTypeDescriptor, d);
        }

        public ISerializable Deserialize(byte[] bytes,
                                  ref int readIndex)
        {
            var type = BitConverter.ToInt32(bytes, readIndex);
            readIndex += sizeof(int);

            var d = deserializers[type];
            return d.Deserialize(bytes, ref readIndex);
        }
    }
}
