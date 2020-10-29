using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    public class CustomShape : UIElement
    {
        public Shape Shape { get; set; }
        public CustomShape(ShapeType shapeType)
        {
            switch (shapeType)
            {
                case ShapeType.Ellipse:
                    Shape = CreateEllipse();
                    break;
                case ShapeType.Rectangle:
                    Shape = CreateRectangle();
                    break;
                case ShapeType.Polygon:
                    Shape = CreatePolygon();
                    break;
            }
        }

        public Shape CreateEllipse()
        {
            Random r = new Random();

            Brush Custombrush = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255),
                   (byte)r.Next(1, 255), (byte)r.Next(1, 233)));

            Ellipse ellipse = new Ellipse
            {
                Width = 50,
                Height = 50,
                StrokeThickness = 3,
                Fill = Custombrush,
                Stroke = Brushes.Black
            };

            return ellipse;
        }

        public Shape CreateRectangle()
        {
            Random r = new Random();

            Brush Custombrush = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255),
                   (byte)r.Next(1, 255), (byte)r.Next(1, 233)));

            Rectangle rectangle = new Rectangle
            {
                Width = 50,
                Height = 50,
                StrokeThickness = 3,
                Fill = Custombrush,
                Stroke = Brushes.Black
            };

            return rectangle;
        }

        public Shape CreatePolygon()
        {
            Random r = new Random();

            var Point1 = new Point(0, 50);
            var Point2 = new Point(50, 50);
            var Point3 = new Point(25, 0);
            Brush Custombrush = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255),
                (byte)r.Next(1, 255), (byte)r.Next(1, 233)));

            Polygon polygon = new Polygon
            {
                Points = new PointCollection() { Point1, Point2, Point3 },
                StrokeThickness = 3,
                Fill = Custombrush,
                Stroke = Brushes.Black
            };

            return polygon;
        }
    }
}
