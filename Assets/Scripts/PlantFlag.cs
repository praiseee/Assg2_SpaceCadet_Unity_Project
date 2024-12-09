using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PlantFlag : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor socketInteractor;
    [SerializeField] private AudioSource plantingFlagSound;

    private bool missionStarted = false;
    private bool missionCompleted = false;

    public MissionManager missionManager;

    public void PlantingFlag()
    {
        if (!missionCompleted)
        {
            var flag = socketInteractor.interactablesHovered[0];

            plantingFlagSound.transform.position = socketInteractor.transform.position;
            plantingFlagSound.Play();

            MarkMissionAsCompleted();
        }
    }

    public void StartMission()
    {
        if (!missionStarted)
        {
            missionStarted = true;
            missionManager.PlayMissionDialogue("PlantFlag", false);
        }
    }

    public void MarkMissionAsCompleted()
    {
        if (missionManager != null)
        {
            missionManager.CompleteMission("PlantFlag");
            missionCompleted = true;
        }
    }
}
