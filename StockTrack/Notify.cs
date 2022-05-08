using System;
using System.Windows;
using System.Windows.Threading;
using Notification.Wpf;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace StockTrack
{
    public enum TypeMsgBox
    {
        Warning,
        Success,
        Error,
        Information
    }

    public class MsgBox
    {
        private Notifier _notify = null;
        private static MsgBox _instance = null;
        private MsgBox()
        {

        }

        public static MsgBox Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MsgBox();
                }
                return _instance;
            }
        }

        public void InitClass()
        {
            _notify = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: ToastNotifications.Position.Corner.TopRight,
                    offsetX: 5,
                    offsetY: 5);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(2),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(3));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }

        public void Dispose()
        {
            _notify.Dispose();
        }

        public void Show(string message, TypeMsgBox type = TypeMsgBox.Information)
        {
            if (string.IsNullOrEmpty(message)) return;
            if (_notify == null) return;

            switch (type)
            {
                case TypeMsgBox.Warning:
                    _notify.ShowWarning(message);
                    break;
                case TypeMsgBox.Success:
                    _notify.ShowSuccess(message);
                    break;
                case TypeMsgBox.Error:
                    _notify.ShowError(message);
                    break;
                case TypeMsgBox.Information:
                    _notify.ShowInformation(message);
                    break;
                default:
                    break;
            }
        }

        public void Show(TypeMsgBox type, string format, params object[] ps)
        {
            if (string.IsNullOrEmpty(format)) return;
            string message = String.Format(format, ps);
            switch (type)
            {
                case TypeMsgBox.Warning:
                    _notify.ShowWarning(message);
                    break;
                case TypeMsgBox.Success:
                    _notify.ShowSuccess(message);
                    break;
                case TypeMsgBox.Error:
                    _notify.ShowError(message);
                    break;
                case TypeMsgBox.Information:
                    _notify.ShowInformation(message);
                    break;
                default:
                    break;
            }
        }
    }
    public class Notification
    {
        public static Notification _instance = null;

        private NotificationManager _notificationManager = null;


        private Notification()
        {
            _notificationManager = new NotificationManager();
        }


        public static Notification Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Notification();
                }

                return _instance;
            }
        }


        public void ShowInfomation(string title, string message)
        {
            _notificationManager.ShowProgressBar(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = NotificationType.Information
            });

        }

        public void ShowInfomation(string title, string message, bool anotherThread = false)
        {
            if (!anotherThread)
            {
                _notificationManager.Show(new NotificationContent
                {
                    Title = title,
                    Message = message,
                    Type = NotificationType.Information
                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    _notificationManager.Show(new NotificationContent
                    {
                        Title = title,
                        Message = message,
                        Type = NotificationType.Information
                    });
                }));
            }
        }



        public void ShowWarning(string title, string message, bool anotherThread = false)
        {
            if (!anotherThread)
            {
                _notificationManager.Show(new NotificationContent
                {
                    Title = title,
                    Message = message,
                    Type = NotificationType.Warning
                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    _notificationManager.Show(new NotificationContent
                    {
                        Title = title,
                        Message = message,
                        Type = NotificationType.Warning
                    });
                }));
            }
        }


        public void ShowSucess(string title, string message, bool anotherThread = false)
        {
            if (!anotherThread)
            {
                _notificationManager.Show(new NotificationContent
                {
                    Title = title,
                    Message = message,
                    Type = NotificationType.Success
                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    _notificationManager.Show(new NotificationContent
                    {
                        Title = title,
                        Message = message,
                        Type = NotificationType.Success
                    });
                }));
            }
        }



        public void ShowError(string title, string message, bool anotherThread = false)
        {
            if (!anotherThread)
            {
                _notificationManager.Show(new NotificationContent
                {
                    Title = title,
                    Message = message,
                    Type = NotificationType.Error
                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    _notificationManager.Show(new NotificationContent
                    {
                        Title = title,
                        Message = message,
                        Type = NotificationType.Error
                    });
                }));
            }
        }


    }
}
