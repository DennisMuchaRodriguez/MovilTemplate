using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game/Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Game Configuration")]
    public string gameVersion = "1.0.0";
    public int targetFrameRate = 60;
    public bool vSyncEnabled = false;

    [Header("Notification Settings")]
    public bool enableNotifications = true;
    public string dailyNotificationTitle = "Come back and play!";
    public string dailyNotificationText = "Your adventure awaits!";
    public int dailyNotificationHour = 19; 
}