using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverFixed : MonoBehaviour
{
    // Bool to check if each part has been fixed yet
    public bool wheelFixed = false;
    public bool cameraFixed = false;
    public bool panelFixed = false;


    public MissionManager missionManager;
    public RoverTrigger roverTrigger;

    // When fixed wheel
    public void FixWheel()
    {
        wheelFixed = true;
        Debug.Log("Wheel fixed");
        CheckProgress();
    }
    // When fixed camera
    public void FixCam()
    {
        cameraFixed = true;
        Debug.Log("Cam fixed");
        CheckProgress();
    }
    // When fixed panel
    public void FixPanel()
    {
        panelFixed = true;
        Debug.Log("Panel fixed");
        CheckProgress();
    }

    void CheckProgress()
    {
        if (wheelFixed)
        { 
            if (cameraFixed)
            {
                if (panelFixed)
                {
                    FixComplete();
                }
            }
        }
    }

    void FixComplete()
    {
        Debug.Log("Nice.");
        roverTrigger.MarkMissionAsCompleted();
    }
}
