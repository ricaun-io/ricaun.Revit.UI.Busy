using Autodesk.Revit.UI;

namespace ricaun.Revit.UI.Busy
{
    /// <summary>
    /// RevitBusyControl
    /// </summary>
    public static class RevitBusyControl
    {
        /// <summary>
        /// RevitBusyControl
        /// </summary>
        public static RevitBusyService Control { get; private set; }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="application"></param>
        public static void Initialize(UIControlledApplication application)
        {
            if (Control is null)
                Control = new RevitBusyService(application);
        }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="uiapp"></param>
        public static void Initialize(UIApplication uiapp)
        {
            if (Control is null)
                Control = new RevitBusyService(uiapp);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        internal static void Dispose()
        {
            if (Control is not null)
            {
                Control.Dispose();
                Control = null;
            }
        }
    }
}
