/*
 * Author: Jacie Thoo Yixuan
 * Date: 6/12/2024
 * Description: Handles mechanics for building the base
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class BuildBase : MonoBehaviour
{
    [SerializeField] 
    //private XRSocketInteractor socketInteractor;
    public bool baseBuilt = false;
    public GameObject moonBase;
    public int polesSetUp = 0;

    public Collider triggerCollider;

    public MissionManager missionManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            missionManager.PlayMissionDialogue("Eat", false);
            triggerCollider.enabled = false;
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

    public void CheckBaseStatus()
    {
        // Set number of poles needed for the base to be built
        if (polesSetUp == 4)
        {
            moonBase.SetActive(true);
            MarkMissionAsCompleted();
        }
    }

    public void MarkMissionAsCompleted()
    {
        if (missionManager != null)
        {
            missionManager.CompleteMission("Base");
        }
    }
}
