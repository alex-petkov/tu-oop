using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise4A
{
    public interface IShape: ISerializable
    {
        double Area();
        string Name { get; }
    }

    enum ShapeTypes
    {
        Circle = 1,
        Rectangle
    }

    public class Circle : IShape
    {
        private double radius;

        public Circle(double r)
        {
            radius = r;
        }

        public string Name
        {
            get { return "Кръг"; }
        }

        public double Area()
        {
            return Math.PI * radius * radius;
        }

        public static int TypeDescriptor
        {
            get
            { return (int)ShapeTypes.Circle; }
        }



        public byte[] Serialize()
        {
            var result = new List<byte>();
            result.AddRange(
                BitConverter.GetBytes(TypeDescriptor));
            result.AddRange(
                BitConverter.GetBytes(radius));

            return result.ToArray();
        }

        public class Deserializer : IDeserializer
        {
            public int ForTypeDescriptor
            { get { return Circle.TypeDescriptor; } }

            public ISerializable Deserialize(byte[] from,
                                    ref int readIndex)
            {
                var r = BitConverter.ToDouble(from, readIndex);
                readIndex += sizeof(double);

                return new Circle(r);
            }
        }
    }

    public class Rectangle : IShape
    {
        private double width, height;

        public Rectangle(double w, double h)
        {
            width = w;
            height = h;
        }

        public string Name
        {
            get { return "Правоъгълник"; }
        }

        public static int TypeDescriptor
        { get { return (int)ShapeTypes.Rectangle; } }

        public double Area()
        {
            return width * height;
        }

        public byte[] Serialize()
        {
            var result = new List<byte>();
            result.AddRange(
                BitConverter.GetBytes(TypeDescriptor));
            result.AddRange(
                BitConverter.GetBytes(width));
            result.AddRange(
                BitConverter.GetBytes(height));

            return result.ToArray();
        }

        public class Deserializer : IDeserializer
        {
            public int ForTypeDescriptor
            { get { return TypeDescriptor; } }

            public ISerializable Deserialize(byte[] from,
                                    ref int readIndex)
            {
                var w = BitConverter.ToDouble(from, readIndex);
                readIndex += sizeof(double);
                var h = BitConverter.ToDouble(from, readIndex);
                readIndex += sizeof(double);

                return new Rectangle(w, h);
            }
        }
    }
}
