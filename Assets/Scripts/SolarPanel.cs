using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SolarPanel : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor socketInteractor;
    [SerializeField] private AudioSource solarPanelSound;

    private bool missionStarted = false;
    private bool missionCompleted = false;

    public MissionManager missionManager;

    /// <summary>
    /// Functionality for placing solar panel
    /// </summary>
    public void PlaceSolarPanel()
    {
        if (!missionCompleted)
        {

            solarPanelSound.transform.position = socketInteractor.transform.position;
            solarPanelSound.Play();

            MarkMissionAsCompleted();
        }
    }

    /// <summary>
    /// Play start mission dialogue when the solar panel is held
    /// </summary>
    public void StartMission()
    {
        if (!missionStarted)
        {
            missionStarted = true;
            missionManager.PlayMissionDialogue("SolarPanel", false);
        }
    }

    /// <summary>
    /// Marks "SolarPanel" mission as completed and plays audio from MissionManager
    /// </summary>
    public void MarkMissionAsCompleted()
    {
        if (missionManager != null)
        {
            missionManager.CompleteMission("SolarPanel");
            missionCompleted = true;
        }
    }
}
