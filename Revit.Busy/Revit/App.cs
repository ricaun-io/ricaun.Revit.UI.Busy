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
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("Revit.Busy");
            ribbonPanel.CreatePushButton<Commands.Command>()
                .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico");

            RevitBusyControl.Initialize(application);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();

            RevitBusyControl.Dispose();
            return Result.Succeeded;
        }
    }

}