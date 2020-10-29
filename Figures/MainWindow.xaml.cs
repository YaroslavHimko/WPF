using Figures.Contols;
using Figures.Contols.UserControls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Figures
{
    public partial class MainWindow : Window
    {
        FileControl FileControl { get; set; }
        public MouseUserControl MouseUserControl { get; set; }
        public List<UIElement> ShapeList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ShapeList = new List<UIElement>();
            FileControl = FileControl.Instantiate(this);
            ShapesUserControl.Instantiate(this);
            MouseUserControl = MouseUserControl.Instantiate(this);
            MenuUserControl.Instantiate(this);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            ProcessExit(e);
        }

        public void ProcessExit(CancelEventArgs e)
        {
            string messageBoxText = "Do you want to save changes?";
            string caption = "Save changes";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    FileControl.SaveXAML(e);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        public void ExitEvent(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}