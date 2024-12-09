/*
 * Author: Jacie Thoo Yixuan
 * Date: 6/12/2024
 * Description: Handles mechanics for building the base
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using static UnityEngine.EventSystems.EventTrigger;

public class BuildBase : MonoBehaviour
{
    [SerializeField] 
    //private XRSocketInteractor socketInteractor;
    public bool baseBuilt = false;
    public GameObject moonBase;
    public int polesSetUp = 0;

    public Collider triggerCollider;

    public MissionManager missionManager;


    /// <summary>
    /// When player enters the trigger area
    /// play the audio to guide player to complete the mission
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            missionManager.PlayMissionDialogue("Base", false);
            triggerCollider.enabled = false; // Turn off collider so audio doesn't play again
        }
    }

    /// <summary>
    /// When player places a pole (To call in XR Socket Interactor on keycard)
    /// </summary>
    public void AddPole()
    {
        polesSetUp++;
        CheckBaseStatus();
    }

    /// <summary>
    /// Check if base has been built 
    /// </summary>
    public void CheckBaseStatus()
    {
        // Set number of poles needed for the base to be built
        if (polesSetUp == 4)
        {
            moonBase.SetActive(true);
            MarkMissionAsCompleted();
        }
    }

    /// <summary>
    /// Marks "Base" mission as completed and plays audio from MissionManager
    /// </summary>
    public void MarkMissionAsCompleted()
    {
        if (missionManager != null)
        {
            missionManager.CompleteMission("Base");
        }
    }
}
