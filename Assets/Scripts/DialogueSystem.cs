/*
 * Author: Jacie Thoo Yixuan
 * Date: 7/12/2024
 * Description: Handles audio dialogue and subtitles
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string name;
        public string subtitleText;
        public AudioClip audioClip;
    }

    //Store dialogues
    public List<DialogueLine> dialogueLines;

    // Audio
    public AudioSource audioSource;

    // Subs
    public TextMeshProUGUI subtitleTextUI;

    // Time before next line
    public float delayBetweenLines = 0.5f;

    /// <summary>
    /// Play specific dialogue lines 
    /// </summary>
    /// 

    public void PlayDialogueRange(int startIndex, int endIndex)
    {
        if (startIndex < 0 || endIndex >= dialogueLines.Count || startIndex > endIndex)
        {
            Debug.LogError("Invalid dialogue range.");
            return;
        }
        StopAllCoroutines();
        StartCoroutine(PlayDialogue(startIndex, endIndex));
    }

    private IEnumerator PlayDialogue(int startIndex, int endIndex)
    {
        for (int i = startIndex; i <= endIndex; i++)
        {
            DialogueLine line = dialogueLines[i];
            audioSource.clip = line.audioClip;
            audioSource.Play();

            if (subtitleTextUI != null)
                subtitleTextUI.text = line.subtitleText;

            yield return new WaitForSeconds(line.audioClip.length + delayBetweenLines);
        }

        if (subtitleTextUI != null)
            subtitleTextUI.text = "";
    }

    public void PlaySingleLine(int index)
    {
        PlayDialogueRange(index, index);
    }
}
