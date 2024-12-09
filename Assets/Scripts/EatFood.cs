/*
 * Author: Jacie Thoo Yixuan
 * Date: 4/12/2024
 * Description: Handles eating food mechanic
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class EatFood : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor socketInteractor;
    [SerializeField] private AudioSource eatingSound;
    public Collider triggerCollider;

    public MissionManager missionManager;

    /// <summary>
    /// Destroys the food after it is in the mouth socket
    /// To be assigned in the mouth xr socket under hover/selected
    /// </summary>
    public void EatingFood()
    {
        var currentFood = socketInteractor.interactablesHovered[0];

        eatingSound.transform.position = socketInteractor.transform.position;
        eatingSound.Play();

        Destroy(currentFood.transform.gameObject);
        MarkMissionAsCompleted();
    }

    /// <summary>
    /// When player enters the fridge trigger area
    /// play the audio to guide player to complete the mission
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            missionManager.PlayMissionDialogue("Eat", false);
            triggerCollider.enabled = false; // Turn off collider so audio doesn't play again
        }
    }

    
    /// <summary>
    /// Marks "Eat" mission as completed and plays audio from MissionManager
    /// </summary>  
    public void MarkMissionAsCompleted()
    {
        if (missionManager != null)
        {
            missionManager.CompleteMission("Eat");
        }
    }
}
