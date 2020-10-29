using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Shapes;
using System.Xaml;

namespace Figures.Contols
{
    public class FileControl
    {
        private string FilePath { get; set; }
        private MainWindow MainWindow { get; set; }
        private FileControl(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
        }

        public static FileControl Instantiate(MainWindow mainWindow)
        {
            return new FileControl(mainWindow);
        }

        private void SaveXAML()
        {

            if (SaveFileDialog())
            {
                string xaml = XamlServices.Save(MainWindow.ShapeList);
                File.WriteAllText(FilePath, xaml);
            }
        }

        public void SaveXAML(CancelEventArgs e)
        {
            if (SaveFileDialog())
            {
                string xaml = XamlServices.Save(MainWindow.ShapeList);
                File.WriteAllText(FilePath, xaml);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void LoadXAML()
        {
            if (OpenFileDialog())
            {
                if (MainWindow.ShapeList.Any())
                {
                    foreach (Shape shape in MainWindow.ShapeList)
                    {
                        MainWindow.canvas.Children.Remove(shape);
                    }

                    MainWindow.ShapeList.Clear();
                    MainWindow.MouseUserControl.Clear();
                }

                using FileStream fs = File.Open(FilePath, FileMode.Open, FileAccess.Read);
                MainWindow.ShapeList = XamlServices.Load(fs) as List<UIElement>;
                foreach (Shape shape in MainWindow.ShapeList)
                {
                    MainWindow.canvas.Children.Add(shape);
                    MainWindow.MouseUserControl.InitializeShapes(shape);
                }
            }
        }

        private bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        private bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public void LoadXAMLEvent(object sender, RoutedEventArgs e)
        {
            LoadXAML();
        }

        public void SaveXAMLEvent(object sender, RoutedEventArgs e)
        {
            SaveXAML();
        }
    }
}