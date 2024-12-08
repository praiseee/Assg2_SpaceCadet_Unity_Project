/*
 * Author: Jacie Thoo Yixuan
 * Date: 8/12/2024
 * Description: Handles missions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    /// <summary>
    /// Stores DialogueSystem
    /// </summary>
    public DialogueSystem dialogueSystem;

    /// <summary>
    /// Stores state of missions
    /// </summary>
    public Dictionary<string, bool> missionStates = new Dictionary<string, bool>();

    /// <summary>
    /// Initialise missions
    /// </summary>
    private void Start()
    {
        // False = not completed, True = completed
        missionStates.Add("Keycard", false);
        missionStates.Add("Base", false);
        missionStates.Add("Eat", false);
        missionStates.Add("PlantFlag", false);
        missionStates.Add("SolarPanel", false);
        missionStates.Add("FixRover", false);
        missionStates.Add("CollectSamples", false);
    }

    /// <summary>
    /// Marks mission as completed and play post-completion dialogue for the mission
    /// </summary>
    /// <param name="missionName"></param>
    public void CompleteMission(string missionName)
    {
        if (missionStates.ContainsKey(missionName))
        {
            missionStates[missionName] = true;
            Debug.Log($"Mission '{missionName}' completed!");

            // Play post-mission dialogue
            PlayMissionDialogue(missionName, true);
        }
        else
        {
            Debug.LogError($"Mission '{missionName}' not found in MissionManager.");
        }
    }

    /// <summary>
    /// Plays dialogue according to the mission
    /// </summary>
    /// <param name="missionName"></param>
    /// <param name="isCompleted"></param>
    public void PlayMissionDialogue(string missionName, bool isCompleted)
    {
        if (dialogueSystem == null)
        {
            Debug.LogError("DialogueSystem not assigned");
            return;
        }

        // Dialogue range for every mission
        switch (missionName)
        {
            case "Keycard":
                if (isCompleted) // Already completed
                    dialogueSystem.PlayDialogueRange(0, 1);
                else // Not completed
                    dialogueSystem.PlayDialogueRange(2, 3);
                break;

            case "Base":
                if (isCompleted) // Already completed
                    dialogueSystem.PlayDialogueRange(4, 5);
                else // Not completed
                    dialogueSystem.PlayDialogueRange(6, 7);
                break;

            case "Eat":
                if (isCompleted) // Already completed
                    dialogueSystem.PlayDialogueRange(2, 3);
                else // Not completed
                    dialogueSystem.PlayDialogueRange(0, 1);
                break;

            case "PlantFlag":
                if (isCompleted) // Already completed
                    dialogueSystem.PlayDialogueRange(12, 13);
                else // Not completed
                    dialogueSystem.PlayDialogueRange(14, 15);
                break;

            case "SolarPanel":
                if (isCompleted) // Already completed
                    dialogueSystem.PlayDialogueRange(16, 17);
                else // Not completed
                    dialogueSystem.PlayDialogueRange(18, 19);
                break;

            case "FixRover":
                if (isCompleted) // Already completed
                    dialogueSystem.PlayDialogueRange(20, 21);
                else // Not completed
                    dialogueSystem.PlayDialogueRange(22, 23);
                break;

            case "CollectSamples":
                if (isCompleted) // Already completed
                    dialogueSystem.PlayDialogueRange(24, 25);
                else // Not completed
                    dialogueSystem.PlayDialogueRange(26, 27);
                break;

            default:
                Debug.Log("No dialogue");
                break;
        }
    }

    /// <summary>
    /// Check if all missions completed
    /// </summary>
    /// <returns></returns>
    public bool AllMissionsCompleted()
    {
        foreach (var mission in missionStates)
        {
            if (!mission.Value)
                return false;
        }
        return true;
    }
}
