using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantFollower : MonoBehaviour
{
    /// <summary>
    /// Stores player Transform
    /// </summary>
    public Transform player; 

    /// <summary>
    /// Speed the assistant follows player at
    /// </summary>
    public float followSpeed = 2.0f;

    /// <summary>
    /// Position from the player
    /// </summary>
    public Vector3 offset = new Vector3(2, 1, 1); 

    /// <summary>
    /// Update the assistant's position
    /// </summary>
    private void Update()
    {
        // Where assistant should be based on player position
        Vector3 targetPosition = player.position + player.TransformDirection(offset);

        // Move the assistant towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

        // Face player
        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0; // Ignore y
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}
