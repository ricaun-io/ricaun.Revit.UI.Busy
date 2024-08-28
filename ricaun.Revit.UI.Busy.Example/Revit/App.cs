using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI.Busy;
using ricaun.Revit.UI;
using ricaun.Revit.UI.Tasks;
using System;
namespace ricaun.Revit.UI.Busy.Example.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private static RevitTaskService revitTaskService;
        public static IRevitTask RevitTask => revitTaskService;

        private static RibbonPanel ribbonPanel;
        public static RevitBusyService RevitBusyService;
        public Result OnStartup(UIControlledApplication application)
        {
            revitTaskService = new RevitTaskService(application);
            revitTaskService.Initialize();

            ribbonPanel = application.CreatePanel("Example");
            var ribbonItem = ribbonPanel.CreatePushButton<Commands.Command>("RevitBusy")
                .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico");

            var viewButton = ribbonPanel.CreatePushButton<Commands.CommandView>("View")
                .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico");

            RevitBusyService = new RevitBusyService(application);
            RevitBusyService.SetInterval(100);
            RevitBusyService.PropertyChanged += (s, e) =>
            {
                UpdateLargeImageBusy(ribbonItem, RevitBusyService.IsRevitBusy);
                UpdateLargeImageBusy(viewButton, RevitBusyService.IsRevitBusy);
            };

            RevitBusyControl.Initialize(application);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();

            RevitBusyService?.Dispose();
            revitTaskService?.Dispose();

            return Result.Succeeded;
        }

        private static void UpdateLargeImageBusy(RibbonItem ribbonItem, bool revitBusy)
        {
            const string LargeImageIsBusy = "/UIFrameworkRes;component/ribbon/images/close.ico";
            const string LargeImageNoBusy = "/UIFrameworkRes;component/ribbon/images/add.ico";
            if (revitBusy)
                ribbonItem.SetLargeImage(LargeImageIsBusy);
            else
                ribbonItem.SetLargeImage(LargeImageNoBusy);
        }
    }
}