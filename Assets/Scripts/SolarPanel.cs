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

    public void PlaceSolarPanel()
    {
        if (!missionCompleted)
        {
            var solarPanel = socketInteractor.interactablesHovered[0];

            solarPanelSound.transform.position = socketInteractor.transform.position;
            solarPanelSound.Play();

            MarkMissionAsCompleted();
        }
    }

    public void StartMission()
    {
        if (!missionStarted)
        {
            missionStarted = true;
            missionManager.PlayMissionDialogue("SolarPanel", false);
        }
    }

    public void MarkMissionAsCompleted()
    {
        if (missionManager != null)
        {
            missionManager.CompleteMission("SolarPanel");
            missionCompleted = true;
        }
    }
}
