using Autodesk.Revit.UI;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Revit.Busy
{
    /// <summary>
    /// RevitBusyService 
    /// Each 500 millis check if Idling updates
    /// </summary>
    public class RevitBusyService : IDisposable, INotifyPropertyChanged
    {
        private const double Millis = 500;
        private readonly UIControlledApplication application;
        private readonly UIApplication uiapp;
        private DispatcherTimer dispatcher;

        /// <summary>
        /// RevitBusyService
        /// </summary>
        /// <param name="application"></param>
        public RevitBusyService(UIControlledApplication application)
        {
            this.application = application;
            this.application.Idling += Application_Idling;
            InitializeDispatcherTimer();
        }

        /// <summary>
        /// RevitBusyService
        /// </summary>
        /// <param name="uiapp"></param>
        public RevitBusyService(UIApplication uiapp)
        {
            this.uiapp = uiapp;
            this.uiapp.Idling += Application_Idling;
            InitializeDispatcherTimer();
        }

        #region Timer
        /// <summary>
        /// SetInterval
        /// </summary>
        /// <param name="millis"></param>
        public void SetInterval(double millis = Millis)
        {
            dispatcher.Interval = TimeSpan.FromMilliseconds(millis);
        }

        private void InitializeDispatcherTimer()
        {
            dispatcher = new DispatcherTimer(DispatcherPriority.Background);
            dispatcher.Interval = TimeSpan.FromMilliseconds(Millis);
            dispatcher.Tick += (s, e) =>
            {
                IsRevitBusy = (DateTime.Now - LastateTime).TotalMilliseconds > Millis;
            };
            dispatcher.Start();
        }

        private DateTime LastateTime;

        private void Application_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
        {
            LastateTime = DateTime.Now;
            if (NeedToDispose) Dispose();
        }
        #endregion

        #region Dispose
        private bool NeedToDispose;
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            NeedToDispose = true;
            try
            {
                if (application is not null)
                    application.Idling -= Application_Idling;

                if (uiapp is not null)
                    uiapp.Idling -= Application_Idling;

                dispatcher.Stop();
            }
            catch { }
        }
        #endregion

        #region IsRevitBusy
        private bool isRevitBusy;
        /// <summary>
        /// IsRevitBusy
        /// </summary>
        public bool IsRevitBusy
        {
            get { return isRevitBusy; }
            private set
            {
                isRevitBusy = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PropertyChanged
        /// <summary>
        /// PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropertyChanged
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
