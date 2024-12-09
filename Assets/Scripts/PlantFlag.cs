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

    /// <summary>
    /// Plant flag function
    /// To be assigned in inspector under FlagSocket Select entered
    /// </summary>
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

    /// <summary>
    /// Play start mission dialogue when the flag is held
    /// </summary>
    public void StartMission()
    {
        if (!missionStarted)
        {
            missionStarted = true;
            missionManager.PlayMissionDialogue("PlantFlag", false);
        }
    }

    /// <summary>
    /// Marks "PlantFlag" mission as completed and plays audio from MissionManager
    /// </summary>
    public void MarkMissionAsCompleted()
    {
        if (missionManager != null)
        {
            missionManager.CompleteMission("PlantFlag");
            missionCompleted = true;
        }
    }
}
