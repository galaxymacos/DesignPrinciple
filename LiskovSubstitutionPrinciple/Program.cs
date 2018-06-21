using System;
using System.Runtime.InteropServices;

namespace LiskovSubstitutionPrinciple
{
    interface ITwoDimension
    {
        double X { get; set; }
        double Y { get; set; }
    }

    interface IThreeDimension:ITwoDimension
    {
        double Z { get; set; }
    }
    public abstract class TwoDimensionShape:ITwoDimension
    {
        public abstract double Area { get; }
        public abstract double Parimeter { get; }
        public double X { get; set; }
        public double Y { get; set; }

        public TwoDimensionShape()
        {
            X = 0;
            Y = 0;
        }
        
        public TwoDimensionShape(double x,double y)
        {
            X = x;
            Y = y;
        }

        public void Move(int x, int y)
        {
            X += x;
            Y += y;
        }
        
        public override string ToString()
        {
            return $"{GetType().Name} is located at ({X},{Y}) with the area of {Area} and parimeter {Parimeter}";
        }
        
        
    }

    public class Rectangle:TwoDimensionShape
    {
        public override double Area => Length * Width;
        public override double Parimeter => 2 * (Length + Width);
        public virtual double Length { get; set; }
        public virtual double Width { get; set; }

        public Rectangle(double length, double width)
        {
            Length = length;
            Width = width;
        }
        
        public Rectangle(double x, double y,double length,double width) : base(x, y)
        {
            Length = length;
            Width = width;
        }
    }
    
    public class Square:Rectangle
    {
        public Square(double size) : base(size,size)
        {
        }

        public Square(double x, double y, double size) : base(x, y, size,size)
        {
        }

        public override double Length
        {
            set => base.Width = base.Length = value;
        }

        public override double Width
        {
            set => base.Width = base.Length = value;
        }

        
    }

    public abstract class ThreeDimensionShape:IThreeDimension
    {
        public abstract double SurfaceArea { get; }
        public abstract double Volume { get; }

        public ThreeDimensionShape()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }
        public ThreeDimensionShape(double x, double y,double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public ThreeDimensionShape Move(int x, int y, int z)
        {
            X += x;
            Y += y;
            Z += z;
            return this;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
    
    public class Cylinder:ThreeDimensionShape
    {
        public double BottomRadius { get; set; }
        public double Height { get; set; }

        public override double SurfaceArea =>
            2 * BottomRadius * BottomRadius * Math.PI + 2 * BottomRadius * Math.PI * Height;
        
        public override double Volume => BottomRadius * BottomRadius * Math.PI * Height;

        public Cylinder(double bottomRadius, double height)
        {
            BottomRadius = bottomRadius;
            Height = height;
        }
        
        public Cylinder(double x, double y, double z,double bottomRadius,double height) : base(x, y, z)
        {
            BottomRadius = bottomRadius;
            Height = height;
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            Rectangle square = new Rectangle(10, 20) {Width = 5};
            Console.WriteLine(square);
            
        }
    }
}