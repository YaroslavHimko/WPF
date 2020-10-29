using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    public class MouseUserControl
    {
        private readonly MainWindow MainWindow;

        private bool isDragging;
        private Point clickPosition;
        private TranslateTransform originTT;
        private Shape firstShape;
        private Shape secondShape;
        private Path connection;

        public MouseUserControl(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            InitializeMouseEvents();
        }

        public static MouseUserControl Instantiate(MainWindow mainWindow)
        {
            return new MouseUserControl(mainWindow);
        }

        public void InitializeShapes(Shape shape)
        {
            if (firstShape == null && !(shape is Path))
            {
                firstShape = shape;
            }
            else if (secondShape == null && !(shape is Path))
            {
                secondShape = shape;
            }
            else if (shape is Path path)
            {
                connection = path;
            }
        }

        public void Clear()
        {
            firstShape = null;
            secondShape = null;
            connection = null;
            originTT = null;
        }

        private void InitializeMouseEvents()
        {
            MainWindow.MouseRightButtonDown += new MouseButtonEventHandler(Canvas_MouseRightButtonDown);
            MainWindow.MouseLeftButtonUp += new MouseButtonEventHandler(Canvas_MouseLeftButtonUp);
            MainWindow.MouseMove += new MouseEventHandler(Canvas_MouseMove);
            MainWindow.MouseLeftButtonDown += new MouseButtonEventHandler(Canvas_MouseLeftButtonDown);
        }

        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Shape shape)
            {
                InitializeShapes(shape);
                ConnectShapes();
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Shape activeShape)
            {
                originTT = activeShape.RenderTransform as TranslateTransform ?? new TranslateTransform();
                isDragging = true;
                clickPosition = e.GetPosition(MainWindow);
                activeShape.CaptureMouse();
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            if (e.OriginalSource is Shape activeShape)
            {
                activeShape.ReleaseMouseCapture();
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is Shape activeShape)
            {
                if (isDragging && activeShape != null)
                {
                    Point currentPosition = e.GetPosition(MainWindow);
                    var transform = activeShape.RenderTransform as TranslateTransform ?? new TranslateTransform();
                    transform.X = originTT.X + (currentPosition.X - clickPosition.X);
                    transform.Y = originTT.Y + (currentPosition.Y - clickPosition.Y);
                    activeShape.RenderTransform = new TranslateTransform(transform.X, transform.Y);
                    ConnectShapes();
                }
            }
        }

        private void ConnectShapes()
        {
            if (firstShape != null && secondShape != null)
            {
                var transform1 = firstShape.TransformToVisual(firstShape.Parent as UIElement);
                var transform2 = secondShape.TransformToVisual(secondShape.Parent as UIElement);

                var lineGeometry = new LineGeometry()
                {
                    StartPoint = transform1.Transform(new Point(firstShape.ActualWidth / 2, firstShape.ActualHeight / 2)),
                    EndPoint = transform2.Transform(new Point(secondShape.ActualWidth / 2, secondShape.ActualHeight / 2))
                };

                var path = new Path()
                {
                    Data = lineGeometry,
                    Stroke = new SolidColorBrush(Colors.Green)
                };

                if (MainWindow.canvas.Children.Contains(connection))
                {
                    MainWindow.canvas.Children.Remove(connection);
                    MainWindow.ShapeList.Remove(connection);
                };

                connection = path;
                MainWindow.ShapeList.Add(connection);
                MainWindow.canvas.Children.Add(connection);
            }
        }
    }
}