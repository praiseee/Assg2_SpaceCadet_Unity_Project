/*
 * Author: Loh Shau Ern Shaun
 * Date: 8/12/2024
 * Description: Handles logging out and reseting the scene
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogOut : MonoBehaviour
{
    // When log out function is called
    public void LoggingOut()
    {
        // Reset the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
