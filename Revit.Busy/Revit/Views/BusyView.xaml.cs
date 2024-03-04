using ricaun.Revit.Mvvm;
using ricaun.Revit.UI.Tasks;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Revit.Busy.Revit.Views
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class BusyView : Window
    {
        public IAsyncRelayCommand ButtonCommand { get; private set; }
        public BusyView()
        {
            ButtonCommand = new AsyncRelayCommand(async () =>
            {
                await App.RevitTask.Run((uiapp) =>
                {
                    uiapp.PostCommand(Autodesk.Revit.UI.RevitCommandId.LookupPostableCommandId(Autodesk.Revit.UI.PostableCommand.ArchitecturalWall));
                });
                await Task.Delay(2000);
            });

            InitializeComponent();
            InitializeWindow();
        }

        #region InitializeWindow
        private void InitializeWindow()
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            new System.Windows.Interop.WindowInteropHelper(this) { Owner = Autodesk.Windows.ComponentManager.ApplicationWindow };
        }
        #endregion
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}