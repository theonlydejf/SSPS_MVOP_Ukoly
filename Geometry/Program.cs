using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geometry
{
    class Program
    {
        public static readonly Circle[] constantCircles =
        {
            new Circle(new Point2D(10, 10), 2),
            new Circle(new Point2D(10, 10), 3)
        };
        public const int userCirclesCnt = 3;

        static void Main(string[] args)
        {
            Circle[] circles = new Circle[constantCircles.Length + userCirclesCnt];
            constantCircles.CopyTo(circles, 0);
            for (int i = 0; i < userCirclesCnt; i++)
            {
                Console.WriteLine($"Creating circle { constantCircles.Length + i + 1 }...");
                circles[constantCircles.Length + i] = Circle.Factory.CreateFromUserInput();
            }

            for (int i = 0; i < circles.Length; i++)
            {
                Circle circle = circles[i];
                Console.WriteLine($"Circle { i + 1 }:");
                Console.WriteLine("  " + circle);
                Console.WriteLine("  Area: " + circle.GetArea());
                Console.WriteLine("  Circumference: " + circle.GetPerimter());
                Console.WriteLine("  Distance from zero: " + circle.CenterOfGravity.DistanceFrom(new Point2D()));
            }

            Console.WriteLine("Circles that intersect with eachother:");
            bool foundIntersectingCircles = false;
            for (int i = 0; i < circles.Length; i++)
            {
                for (int j = i + 1; j < circles.Length; j++)
                {
                    if(circles[i].IntersectsWith(circles[j]))
                    {
                        Console.WriteLine($"  Circle { i + 1 } and { j + 1 }");
                        foundIntersectingCircles = true;
                    }
                }
            }
            if(!foundIntersectingCircles)
                Console.WriteLine("  [none]");
        }
    }

    public struct Point2D
    {
        public static Point2DFactory Factory { get; }

        static Point2D()
        {
            Factory = new Point2DFactory();
        }

        public Point2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public float DistanceFrom(Point2D other)
        {
            float dx = other.X - X;
            float dy = other.Y - Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public override string ToString()
        {
            return $"[{ X }; { Y }]";
        }
    }

    public class GeometricObject : IGeometricObject, ICloneable
    {
        public GeometricObject()
        {
            centerOfGravity = new Point2D(0, 0);
        }

        public GeometricObject(Point2D centerOfGravity)
        {
            this.centerOfGravity = centerOfGravity;
            Identifier = Guid.NewGuid();
        }

        private Point2D centerOfGravity;
        public Point2D CenterOfGravity => centerOfGravity;
        public Guid Identifier { get; }

        public object Clone()
        {
            return new GeometricObject(CenterOfGravity);
        }
    }

    public class Circle : GeometricObject, IFilledGeometricObject, ICloneable
    {
        public static CircleFactory Factory { get; }

        static Circle()
        {
            Factory = new CircleFactory();
        }

        public Circle(float radius) : base()
        {
            Init(radius);
        }

        public Circle(Point2D centerOfGravity, float radius) : base(centerOfGravity)
        {
            Init(radius);
        }

        private void Init(float radius)
        {
            Radius = radius;
        }

        private float radius;
        public float Radius
        {
            get => radius;
            set { if (value <= 0) throw new ArgumentException("Radius must be larger than zero!"); radius = value; }
        }

        public float GetArea() => (float)(Math.PI * Radius * Radius);

        public float GetPerimter() => (float)(2 * Math.PI * Radius);

        public bool IntersectsWith(GeometricObject other)
        {
            if(other is Circle)
            {
                Circle otherCircle = other as Circle;
                return CenterOfGravity.DistanceFrom(otherCircle.CenterOfGravity) < otherCircle.Radius + Radius;
            }
            throw new NotImplementedException();
        }

        public new object Clone()
        {
            return new Circle(CenterOfGravity, Radius);
        }

        public override string ToString()
        {
            return $"Locatoin: { CenterOfGravity }, Radius: { Radius }";
        }
    }

    public class CircleFactory : IUserInputFactory<Circle>
    {
        public Circle Create() => CreateFromUserInput();

        public Circle CreateFromUserInput(bool doOutput = true)
        {
            FactoryUtils.ConditionalWrite(doOutput, "Enter center of gravity of the circle:");
            Point2D cog = Point2D.Factory.CreateFromUserInput(true);
            FactoryUtils.ConditionalWrite(doOutput, "Enter radius of the circle:");
            string s_r = Console.ReadLine();
            return new Circle(cog, Convert.ToSingle(s_r));
        }
    }

    public class Point2DFactory : IUserInputFactory<Point2D>
    {
        public Point2D Create() => CreateFromUserInput();

        public Point2D CreateFromUserInput(bool doOutput = true)
        {
            FactoryUtils.ConditionalWrite(doOutput, "Enter X Coordinate and press enter:");
            string s_x = Console.ReadLine();
            FactoryUtils.ConditionalWrite(doOutput, "Enter Y Coordinate and press enter:");
            string s_y = Console.ReadLine();
            return new Point2D(Convert.ToSingle(s_x), Convert.ToSingle(s_y));
        }
    }

    public interface IGeometricObject
    {
        Point2D CenterOfGravity { get; }
        Guid Identifier { get; }
    }

    public interface IFilledGeometricObject : IGeometricObject
    {
        float GetArea();
        float GetPerimter();
        bool IntersectsWith(GeometricObject other);
    }

    public interface IUserInputFactory<T> : IFactory<T>
    {
        T CreateFromUserInput(bool doOutput = true);
    }

    public interface IFactory<T>
    {
        T Create();
    }

    public static class FactoryUtils
    {
        public static void ConditionalWrite(bool condition, string message)
        {
            if (condition)
                Console.WriteLine(message);
        }
    }
}
