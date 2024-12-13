/*
 * Author: Jacie Thoo Yixuan
 * Author: Hoo Ying Qi Praise
 * Author: Loh Shau Ern Shaun
 * Date: 8/12/2024
 * Description: Handles missions and final cutscenes
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MissionManager : MonoBehaviour
{
    /// <summary>
    ///  Reference to the PlayableDirector that manages the Timeline
    /// </summary>
    public PlayableDirector timelineDirector;

    /// <summary>
    /// Stores DialogueSystem
    /// </summary>
    public DialogueSystem dialogueSystem;

    /// <summary>
    /// Reference to SCDatabase that manages user authentication
    /// </summary>
    public SCDatabase scDatabase;

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
        missionStates.Add("LogIn", false);
        missionStates.Add("Keycard", false);
        missionStates.Add("Base", false);
        missionStates.Add("Eat", false);
        missionStates.Add("PlantFlag", false);
        missionStates.Add("SolarPanel", false);
        missionStates.Add("FixRover", false);
        missionStates.Add("CollectSamples", false);
    }

    /// <summary>
    /// Using database, skip missions that have been completed on this account before, and check if all tasks are completed
    /// </summary>
    public void CheckProgress(string missionName)
    {
        if (missionStates.ContainsKey(missionName))
        {
            missionStates[missionName] = true;
            Debug.Log($"Mission '{missionName}' has already been completed!");

            // Check if all missions are completed
            if (AllMissionsCompleted())
            {
                Debug.Log("All missions completed! Playing cutscene...");
                PlayTimeline(); // Play the Timeline
            }
        }
        else
        {
            Debug.LogError($"Mission '{missionName}' not found in MissionManager.");
        }
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
            
            // Update the task status in database
            scDatabase.UpdateTaskStatus(missionName);
            
            // Play post-mission dialogue
            PlayMissionDialogue(missionName, true);

            // Check if all missions are completed
            if (AllMissionsCompleted())
            {
                Debug.Log("All missions completed! Playing cutscene...");
                PlayTimeline(); // Play the Timeline
            }
        }
        else
        {
            Debug.LogError($"Mission '{missionName}' not found in MissionManager.");
        }
    }

    /// <summary>
    /// Plays the Timeline cutscene
    /// </summary>
    public void PlayTimeline()
    {
        if (timelineDirector != null)
        {
            timelineDirector.Play(); // Start the Timeline playback
        }
        else
        {
            Debug.LogError("TimelineDirector not assigned in MissionManager.");
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
            case "LogIn":
                if (!isCompleted) // Not completed
                    dialogueSystem.PlayDialogueRange(0, 2);
                else // Already completed
                    dialogueSystem.PlayDialogueRange(3, 4);
                break;

            case "Keycard":
                if (!isCompleted) // Not completed
                    dialogueSystem.PlayDialogueRange(5, 5);
                else // Already completed
                    dialogueSystem.PlayDialogueRange(6, 6);
                break;

            case "Base":
                if (!isCompleted) // Not completed
                    dialogueSystem.PlayDialogueRange(7, 8);
                else // Already completed
                    dialogueSystem.PlayDialogueRange(9, 10);
                break;

            case "Eat":
                if (!isCompleted) // Not completed
                    dialogueSystem.PlayDialogueRange(11, 11);
                else // Already completed
                    dialogueSystem.PlayDialogueRange(12, 12);
                break;

            case "PlantFlag":
                if (!isCompleted) // Not completed
                    dialogueSystem.PlayDialogueRange(13, 13);
                else // Already completed
                    dialogueSystem.PlayDialogueRange(14, 14);
                break;

            case "SolarPanel":
                if (!isCompleted) // Not completed
                    dialogueSystem.PlayDialogueRange(15, 15);
                else // Already completed
                    dialogueSystem.PlayDialogueRange(16, 16);
                break;

            case "FixRover":
                if (!isCompleted) // Not completed
                    dialogueSystem.PlayDialogueRange(17, 17);
                else // Already completed
                    dialogueSystem.PlayDialogueRange(18, 18);
                break;

            case "CollectSamples":
                if (!isCompleted) // Not completed
                    dialogueSystem.PlayDialogueRange(19, 19);
                else // Already completed
                    dialogueSystem.PlayDialogueRange(20, 20);
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
