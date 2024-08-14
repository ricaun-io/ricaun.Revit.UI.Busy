using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace Revit.Busy.Example.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand, IExternalCommandAvailability
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            System.Windows.MessageBox.Show(uiapp.Application.VersionName);

            return Result.Succeeded;
        }

        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories) { return true; }
    }

    [Transaction(TransactionMode.Manual)]
    public class CommandView : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            new Views.BusyView().Show();

            return Result.Succeeded;
        }
    }
}
