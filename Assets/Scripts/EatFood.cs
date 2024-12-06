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

    public void EatingFood()
    {
        var currentFood = socketInteractor.interactablesHovered[0];

        eatingSound.transform.position = socketInteractor.transform.position;
        eatingSound.Play();

        Destroy(currentFood.transform.gameObject);
    }
}
