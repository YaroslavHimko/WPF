using System;
using System.Windows;

namespace Figures.Contols.UserControls
{
    public class ShapesUserControl
    {
        private MainWindow MainWindow { get; set; }

        public ShapesUserControl(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
        }

        public static ShapesUserControl Instantiate(MainWindow mainWindow)
        {
            return new ShapesUserControl(mainWindow);
        }

        public void Circle_Click(object sender, RoutedEventArgs e)
        {
            var ellipse = new CustomShape(ShapeType.Ellipse).Shape;
            ellipse.Uid = Guid.NewGuid().ToString();
            MainWindow.ShapeList.Add(ellipse);

            MainWindow.canvas.Children.Add(ellipse);
        }

        public void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            var rectangle = new CustomShape(ShapeType.Rectangle).Shape;
            rectangle.Uid = Guid.NewGuid().ToString();

            MainWindow.ShapeList.Add(rectangle);

            MainWindow.canvas.Children.Add(rectangle);
        }

        public void Triangle_Click(object sender, RoutedEventArgs e)
        {
            var polygon = new CustomShape(ShapeType.Polygon).Shape;
            polygon.Uid = Guid.NewGuid().ToString();

            MainWindow.ShapeList.Add(polygon);

            MainWindow.canvas.Children.Add(polygon);
        }
    }
}