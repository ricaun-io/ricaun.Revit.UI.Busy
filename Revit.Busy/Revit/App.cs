using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using ricaun.Revit.UI.Tasks;
using System;

namespace Revit.Busy.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private static RevitTaskService revitTaskService;
        public static IRevitTask RevitTask => revitTaskService;

        private static RibbonPanel ribbonPanel;
        private static RibbonItem ribbonItem;
        public Result OnStartup(UIControlledApplication application)
        {
            revitTaskService = new RevitTaskService(application);
            revitTaskService.Initialize();

            ribbonPanel = application.CreatePanel("Revit.Busy");
            ribbonItem = ribbonPanel.CreatePushButton<Commands.Command>("View")
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
            revitTaskService?.Dispose();

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