using Autodesk.Revit.UI;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace ricaun.Revit.UI.Busy
{
    /// <summary>
    /// RevitBusyService 
    /// Each 1000 millis check if Idling updates
    /// </summary>
    public class RevitBusyService : IDisposable, INotifyPropertyChanged
    {
        private const double IntervalMillis = 1000;
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
        /// <param name="intervalMillis"></param>
        public void SetInterval(double intervalMillis = IntervalMillis)
        {
            dispatcher.Interval = TimeSpan.FromMilliseconds(intervalMillis);
        }
        private void InitializeDispatcherTimer()
        {
            dispatcher = new DispatcherTimer(DispatcherPriority.Background);
            dispatcher.Interval = TimeSpan.FromMilliseconds(IntervalMillis);
            dispatcher.Tick += (s, e) =>
            {
                IsRevitBusy = countIdling == 0;
                countIdling = 0;
            };
            dispatcher.Start();
        }

        private int countIdling = 0;

        private void Application_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
        {
            countIdling++;
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
        private bool isRevitBusy = true;
        /// <summary>
        /// IsRevitBusy
        /// </summary>
        public bool IsRevitBusy
        {
            get { return isRevitBusy; }
            private set
            {
                var changed = isRevitBusy != value;
                isRevitBusy = value;
                if (changed)
                {
                    OnPropertyChanged();
                }
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
