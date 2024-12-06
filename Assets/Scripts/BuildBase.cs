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

    public void AddPole()
    {
        polesSetUp++;
        CheckBaseStatus();
    }

    public void CheckBaseStatus()
    {
        if (polesSetUp == 4)
        {
            moonBase.SetActive(true);
        }
    }
}
