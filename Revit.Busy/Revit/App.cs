using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System;

namespace Revit.Busy.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        private static RibbonItem ribbonItem;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("Revit.Busy");
            ribbonItem = ribbonPanel.CreatePushButton<Commands.Command>()
                .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico");

            RevitBusyControl.Initialize(application);
            RevitBusyControl.Control.PropertyChanged += RevitBusyControlPropertyChanged;

            UpdateLargeImageBusy(ribbonItem, RevitBusyControl.Control);

            return Result.Succeeded;
        }

        private void RevitBusyControlPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Console.WriteLine($"RevitBusyControl PropertyChanged {e.PropertyName} {RevitBusyControl.Control.IsRevitBusy}");

            var control = sender as RevitBusyService;
            UpdateLargeImageBusy(ribbonItem, control);
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            RevitBusyControl.Control.PropertyChanged -= RevitBusyControlPropertyChanged;
            RevitBusyControl.Dispose();
            return Result.Succeeded;
        }

        private static void UpdateLargeImageBusy(RibbonItem ribbonItem, RevitBusyService control)
        {
            const string LargeImageIsBusy = "/UIFrameworkRes;component/ribbon/images/close.ico";
            const string LargeImageNoBusy = "/UIFrameworkRes;component/ribbon/images/add.ico";
            if (control.IsRevitBusy)
                ribbonItem.SetLargeImage(LargeImageIsBusy);
            else
                ribbonItem.SetLargeImage(LargeImageNoBusy);
        }
    }

}