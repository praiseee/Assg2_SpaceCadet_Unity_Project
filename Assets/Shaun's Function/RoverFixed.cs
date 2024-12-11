/*
 * Author: Loh Shau Ern Shaun
 * Date: 3/12/2024
 * Description: Checks if fixing rover tasks is complete
 * Sends message that it has been completed to mission manager when completed
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverFixed : MonoBehaviour
{
    // Bool to check if each part has been fixed yet
    public bool wheelFixed = false;
    public bool cameraFixed = false;
    public bool panelFixed = false;
    public bool antennaFixed = false;
    public bool pipeFixed = false;


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
    // When fixed antenna
    public void FixAntenna()
    {
        antennaFixed = true;
        Debug.Log("Antenna fixed");
        CheckProgress();
    }
    // When fixed pipe
    public void FixPipe()
    {
        pipeFixed = true;
        Debug.Log("Pipe fixed");
        CheckProgress();
    }

    // Check the progress of rover fixing
    void CheckProgress()
    {
        // Wheel fixed?
        if (wheelFixed)
        { 
            // Camera fixed?
            if (cameraFixed)
            {
                // Panel fixed?
                if (panelFixed)
                {
                    // Antenna fixed?
                    if (antennaFixed)
                    {
                        // Pipe fixed?
                        if (pipeFixed)
                        {
                            // Completed fix!
                            FixComplete();
                        }
                    }
                }
            }
        }
    }

    // If rover is completely fixed
    void FixComplete()
    {
        Debug.Log("Rover fixed!");
        roverTrigger.MarkMissionAsCompleted();
    }
}
