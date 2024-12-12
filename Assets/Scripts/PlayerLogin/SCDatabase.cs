/*
 * Author: Loh Shau Ern Shaun
 * Date: 9/12/2024
 * Description: Handles logging in, signing in and getting player data from the saved databases
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using TMPro;

public class SCDatabase : MonoBehaviour
{
    // Sign In UI input fields 
    public TMP_InputField emailS;
    public TMP_InputField passwordS;
    public TMP_InputField usernameS;

    // Login UI input fields
    public TMP_InputField emailL;
    public TMP_InputField passwordL;

    // Sign up canvas
    public GameObject signupCanvas;
    // Login canvas
    public GameObject loginCanvas;
    // Logout canvas
    public GameObject logoutCanvas;

    // String to display stored uid
    public string storedUID;

    // Database ref
    DatabaseReference dbDataRef;
    // Auth ref
    Firebase.Auth.FirebaseAuth dbAuthRef;
    // Player path ref
    DatabaseReference playerRef;

    // Reference to MissionManager
    public MissionManager missionManager;

    // When the scene is started
    void Awake()
    {
        // Initialize dbDataRef
        dbDataRef = FirebaseDatabase.DefaultInstance.RootReference;
        // Initialize dbAuthRef
        dbAuthRef = Firebase.Auth.FirebaseAuth.DefaultInstance;
        // Initialize playerRef
        playerRef = FirebaseDatabase.DefaultInstance.GetReference("players");
    }

    // When user signs up a new acc
    public void SignUpUser()
    {
        // Get email and password from input fields
        string email = emailS.text.Trim();
        string password = passwordS.text.Trim();

        // pass user info to the firebase project
        // attempts to create a new user / check if alr exists
        dbAuthRef
            .CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                // perform task handling
                if (task.IsFaulted)
                {
                    Debug.LogError("Sorry, there was an error! ERROR: " + task.Exception);
                    return; // exit from attempt
                }

                if (!task.IsCompletedSuccessfully)
                {
                    Debug.Log("Unable to create user!");
                    return;
                }

                // Insert post-sign up actions here
                // Get reference of authentication acc
                Firebase.Auth.AuthResult result = task.Result;
                // Make variable that calls acc user id
                var playerUID = result.User.UserId;
                Debug.LogFormat("Welcome to whack a mole, {0}!", playerUID);

                // Get display name variable
                string displayName = usernameS.text.Trim();
                // Create database based on email provided
                CreatePlayerDatabase(playerUID, displayName);

                // Switch to login canvas after creating account
                signupCanvas.SetActive(false);
                loginCanvas.SetActive(true);
            });
    }

    // When user is created, create corresponding account database 
    public void CreatePlayerDatabase(string uidNew, string displayName)
    {
        Debug.Log("Creating database...");
        // Get constructor from PlayerData
        PlayerData pData = new PlayerData(uidNew, displayName, false, false, false, false, false, false);
        // Convert pData to a json
        string json = JsonUtility.ToJson(pData);

        // Set database with unique acc uid
        playerRef
            .Child(uidNew)
            .SetRawJsonValueAsync(json)
            .ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Failed to set JSON data: " + task.Exception);
                }
                else if (task.IsCompletedSuccessfully)
                {
                    Debug.Log("Data successfully written to database.");
                }


                Debug.LogFormat("UID: {0}, DisplayName: {1}, Mission1: {2}, Mission2: {3}, Mission3: {4}, Mission4: {5}, Mission5: {6}, Mission6: {7}, ", uidNew, displayName, false, false, false, false, false, false);
                Debug.Log("Database created!");
            });
    }

    // When logging in user
    public void LoginUser()
    {
        // Get email and password from input fields
        string email = emailL.text.Trim();
        string password = passwordL.text.Trim();

        // pass user info to the firebase project
        // attempts to create a new user / check if alr exists
        dbAuthRef
            .SignInWithEmailAndPasswordAsync(email, password)
            .ContinueWith(task =>
            {
                // perform task handling
                if (task.IsFaulted)
                {
                    Debug.Log("Sorry, there was an error! ERROR: " + task.Exception);
                    return; // exit from attempt
                }
                else if (!task.IsCompletedSuccessfully)
                {
                    Debug.Log("Unable to login user!");
                    return;
                }

                // Insert post-sign up actions here
                // Get reference of authentication acc
                Firebase.Auth.AuthResult result = task.Result;
                // Make variable that calls acc user id
                var playerUID = result.User.UserId;
                Debug.LogFormat("Welcome back, {0}!", playerUID);

                // Find entered player database based on email
                FindPlayerDatabase(playerUID);
            });
    }

    // When user account is verifed, find corresponding account database 
    public void FindPlayerDatabase(string uidCompare)
    {
        Debug.Log("Finding database...");

        // Find the account database with uid
        playerRef
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Sorry, there was an error! ERROR: " + task.Exception);
                    return; // exit from attempt
                }

                if (!task.IsCompletedSuccessfully)
                {
                    Debug.Log("Unable to create user!");
                    return;
                }

                // start retrieving values and printout
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    // Look thru each existing Json for the acc
                    foreach (DataSnapshot ds in snapshot.Children)
                    {
                        // Set account path reference
                        PlayerData accPath = JsonUtility.FromJson<PlayerData>(ds.GetRawJsonValue());

                        // Check if uid is same as current user's
                        if (uidCompare == accPath.UID)
                        {
                            Debug.Log("Data found!");
                            // Insert post account login here:
                            // Retrieve data from servers
                            Debug.LogFormat("UID: {0}, DisplayName: {1}, Mission 1: {2}, Mission 2: {3}, Mission 3: {4}, Mission 4: {5}, Mission 5: {6}, Mission 6: {7}", accPath.UID, accPath.Username, accPath.Mission1, accPath.Mission2, accPath.Mission3, accPath.Mission4, accPath.Mission5, accPath.Mission6);

                            storedUID = accPath.UID;

                            // Save details in AccountManager
                            //Check thru each individual task progress
                            if (accPath.Mission1)
                            {
                                // Skip base building
                                Debug.Log("Skipping base building mission!");
                                missionManager.CheckProgress("Base");
                            }
                            if (accPath.Mission2)
                            {
                                // Skip eating
                                Debug.Log("Skipping eating mission!");
                                missionManager.CheckProgress("Eat");
                            }
                            if (accPath.Mission3)
                            {
                                // Skip planting flag
                                Debug.Log("Skipping planting the flag mission!");
                                missionManager.CheckProgress("PlantFlag");
                            }
                            if (accPath.Mission4)
                            {
                                // Skip solar panel building
                                Debug.Log("Skipping building solar panel mission!");
                                missionManager.CheckProgress("SolarPanel");
                            }
                            if (accPath.Mission5)
                            {
                                // Skip fixing rover
                                Debug.Log("Skipping fixing rover mission!");
                                missionManager.CheckProgress("FixRover");
                            }
                            if (accPath.Mission6)
                            {
                                // Skip collecting samples
                                Debug.Log("Skipping collecting samples mission!");
                                missionManager.CheckProgress("CollectSamples");
                            }

                            // Switch to logout canvas after creating account
                            loginCanvas.SetActive(false);
                            logoutCanvas.SetActive(true);

                            // After finding player database, end loop
                            break;
                        }
                        else
                        {
                            Debug.Log("No data was found in account!");
                        }
                    } // End of loop here
                }
            });
    }

    // Update status of missions
    public void UpdateTaskStatus(string mission)
    {
        // Set the mission to be updated
        string currentMission = "";

        if (mission == "Base")
        {
            currentMission = "Mission1";
        }
        else if (mission == "Eat")
        {
            currentMission = "Mission2";
        }
        else if (mission == "PlantFlag")
        {
            currentMission = "Mission3";
        }
        else if (mission == "SolarPanel")
        {
            currentMission = "Mission4";
        }
        else if (mission == "FixRover")
        {
            currentMission = "Mission5";
        }
        else if (mission == "CollectSamples")
        {
            currentMission = "Mission6";
        }

        Debug.Log(currentMission + " completed!");

        // Find the account database with uid
        FirebaseDatabase.DefaultInstance
            .GetReference("players")
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Sorry, there was an error! ERROR: " + task.Exception);
                    return; // exit from attempt
                }
            
                if (!task.IsCompletedSuccessfully)
                {
                    Debug.Log("Unable to create user!");
                    return;
                }

                // start retrieving values and printout
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    // Look thru each existing Json for the acc
                    foreach (DataSnapshot ds in snapshot.Children)
                    {
                        // Set account path reference
                        PlayerData accPath = JsonUtility.FromJson<PlayerData>(ds.GetRawJsonValue());

                        if (storedUID == accPath.UID)
                        {
                            Debug.Log("Data found!");
                            // Insert post account login here:
                            // Get path referencing found existing name
                            var playerReference = FirebaseDatabase
                                .DefaultInstance
                                .RootReference
                                .Child("players")
                                .Child(storedUID);
                            // Update the corresponding mission status
                            var updateValues = new Dictionary<string, object>();
                            updateValues.Add(currentMission, true);
                            playerReference.UpdateChildrenAsync(updateValues);
                            Debug.Log("Updated mission status!");
                        }
                    }
                }
            });
        
    }
}