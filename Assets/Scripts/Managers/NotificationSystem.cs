using UnityEngine;
using Unity.Notifications.Android;

public class NotificationSystem : MonoBehaviour
{
    public static NotificationSystem Instance { get; private set; }

    private const string CHANNEL_ID = "game_notifications";
    private const string DEFAULT_ICON = "icon_default";

    [Header("Configuración")]
    public bool EnableNotifications = true;
    public string DefaultTitle = "¡Nuevo mensaje!";
    public string DefaultText = "Tienes una recompensa esperando";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeNotificationChannel();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeNotificationChannel()
    {
        AndroidNotificationCenter.DeleteNotificationChannel(CHANNEL_ID);

        var channel = new AndroidNotificationChannel
        {
            Id = CHANNEL_ID,
            Name = "Notificaciones del Juego",
            Importance = Importance.High,
            Description = "Actualizaciones y recompensas",
            EnableLights = true,
            EnableVibration = true
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification(string title, string text, int delaySeconds = 0)
    {
        if (!EnableNotifications) return;

        var notification = new AndroidNotification
        {
            Title = title,
            Text = text,
            SmallIcon = DEFAULT_ICON,
            FireTime = System.DateTime.Now.AddSeconds(delaySeconds),
            ShouldAutoCancel = true
        };

        AndroidNotificationCenter.SendNotification(notification, CHANNEL_ID);
    }

    public void ScheduleDailyNotification(string title, string text, int hourOfDay)
    {
        if (!EnableNotifications) return;

        var notification = new AndroidNotification
        {
            Title = title,
            Text = text,
            SmallIcon = DEFAULT_ICON,
            FireTime = System.DateTime.Today.AddHours(hourOfDay),
            RepeatInterval = System.TimeSpan.FromDays(1)
        };

        AndroidNotificationCenter.SendNotification(notification, CHANNEL_ID);
    }

    public void CancelAllNotifications()
    {
        AndroidNotificationCenter.CancelAllNotifications();
    }
    private void OnApplicationQuit()
    {
        CancelAllNotifications();
    }
}