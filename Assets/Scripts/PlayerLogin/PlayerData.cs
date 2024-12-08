using System;
public class PlayerData
{
    // Data to be stored
    public string UID;
    public string Username;
    public bool Mission1;
    public bool Mission2;
    public bool Mission3;
    public bool Mission4;
    public bool Mission5;
    public bool Mission6;

    // Initialize empty constructor
    public PlayerData()
    {

    }

    // Filling up the constructor
    public PlayerData(string uid, string displayName, bool mission1, bool mission2, bool mission3, bool mission4, bool mission5, bool mission6)
    {
        this.UID = uid;
        this.Username = displayName;
        this.Mission1 = mission1;
        this.Mission2 = mission2;
        this.Mission3 = mission3;
        this.Mission4 = mission4;
        this.Mission5 = mission5;
        this.Mission6 = mission6;
    }
}