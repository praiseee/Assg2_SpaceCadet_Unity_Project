/*
 * Author: Jacie Thoo Yixuan
 * Date: 6/12/2024
 * Description: Handles keycard and door mechanics
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class DoorAccessPanel : MonoBehaviour
{
    /// <summary>
    /// Stores doors animator
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Stores door
    /// </summary>
    public Transform door;

    /// <summary>
    /// Stores door opening audio
    /// </summary>
    public AudioSource doorAudio;

    [Header("Control Panel Text")]
    /// <summary>
    /// Prompts player to tap keycard
    /// </summary>
    public GameObject tapCardText;

    /// <summary>
    /// Tells the player they are authorised
    /// </summary>
    public GameObject authorisedText;

    /// <summary>
    /// Tracks if the player is holding the keycard
    /// </summary>
    [SerializeField] private bool isHoldingKeyCard = false;

    /// <summary>
    /// Tracks whether door is opened
    /// </summary>
    [SerializeField] public bool doorOpened = false;

    public MissionManager missionManager;

    /// <summary>
    /// When player picks up keycard (To call in XR Grab Interactable on keycard)
    /// </summary>
    public void PickUpKeyCard()
    {
        isHoldingKeyCard = true;
    }

    /// <summary>
    /// When player drops the keycard (To call in XR Grab Interactable on keycard)
    /// </summary>
    public void DropKeyCard()
    {
        isHoldingKeyCard = false;
    }

    /// <summary>
    /// When colliding with the control panel
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (isHoldingKeyCard == true && doorOpened == false) // If player has keycard in hand and the door is not opened
        {
            OpenDoor();

            // Change the text on the panel
            tapCardText.SetActive(false);
            authorisedText.SetActive(true);

        }
        else 
        {
            Debug.Log("Problem with opening door, either not holding keycard or door is already open");
        }
    }

    /// <summary>
    /// To play sound when door opens
    /// </summary>
    private void PlaySound()
    {
        if (doorAudio != null)
        {
            doorAudio.Play();
        }
        else
        {
            Debug.Log("Problem with playing sound");
        }
    }

    /// <summary>
    /// To handle door opening
    /// </summary>
    private void OpenDoor()
    {
        // Set bool in Animator to true
        animator.SetBool("KeyCardTapped", true);
        PlaySound(); // Play door opening sound
        doorOpened = true; // Mark door as opened
        Debug.Log("Door is opening");
        MarkMissionAsCompleted();
    }

    public void MarkMissionAsCompleted()
    {
        if (missionManager != null)
        {
            missionManager.CompleteMission("Keycard");
        }
    }
}
