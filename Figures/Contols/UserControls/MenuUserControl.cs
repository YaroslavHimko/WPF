using System.Windows;
using System.Windows.Controls;

namespace Figures.Contols.UserControls
{
    public class MenuUserControl
    {
        private readonly MainWindow MainWindow;
        private readonly FileControl FileControl;
        private readonly ShapesUserControl ShapesControl;

        public MenuUserControl(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            FileControl = FileControl.Instantiate(MainWindow);
            ShapesControl = new ShapesUserControl(MainWindow);
            InitializeMenuEvents();
        }

        public static MenuUserControl Instantiate(MainWindow mainWindow)
        {
            return new MenuUserControl(mainWindow);
        }

        private void InitializeMenuEvents()
        {
            MainWindow.Save.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(FileControl.SaveXAMLEvent));
            MainWindow.Open.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(FileControl.LoadXAMLEvent));
            MainWindow.Exit.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(MainWindow.ExitEvent));
            MainWindow.Circle.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(ShapesControl.Circle_Click));
            MainWindow.Rectangle.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(ShapesControl.Rectangle_Click));
            MainWindow.Triangle.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(ShapesControl.Triangle_Click));
        }
    }
}