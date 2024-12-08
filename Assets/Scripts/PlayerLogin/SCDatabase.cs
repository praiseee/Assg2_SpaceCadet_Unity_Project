using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using TMPro;

public class Database : MonoBehaviour
{
    // Sign In UI input fields 
    public TMP_InputField emailS;
    public TMP_InputField passwordS;
    public TMP_InputField usernameS;

    // Login UI input fields
    public TMP_InputField emailL;
    public TMP_InputField passwordL;

    // Database ref
    DatabaseReference dbDataRef;
    // Auth ref
    Firebase.Auth.FirebaseAuth dbAuthRef;
    // Player path ref
    DatabaseReference playerRef;

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

                // Create and save details in AccountManager
                //CopyToAccManager(playerUID, displayName, false, false, false, false, false, false);
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
            .ContinueWith(task => {
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
                    Debug.LogError("Sorry, there was an error! ERROR: " + task.Exception);
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

                // Initialize data variables
                /*string uidSave = "";
                string displayName = "";
                bool mission1Status = null;
                bool mission2Status = null;
                bool mission3Status = null;
                bool mission4Status = null;
                bool mission5Status = null;
                bool mission6Status = null;
                */

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
                            /* Retrieve data from servers
                            uidSave = accPath.uid;
                            displayName = accPath.displayName;
                            bool mission1Status = accPath.mission1;
                            bool mission2Status = accPath.mission2;
                            bool mission3Status = accPath.mission3;
                            bool mission4Status = accPath.mission4;
                            bool mission5Status = accPath.mission5;
                            bool mission6Status = accPath.mission6;
                            */
                            Debug.LogFormat("UID: {0}, DisplayName: {1}, Mission 1: {2}, Mission 2: {3}, Mission 3: {4}, Mission 4: {5}, Mission 5: {6}, Mission 6: {7}", accPath.UID, accPath.Username, accPath.Mission1, accPath.Mission2, accPath.Mission3, accPath.Mission4, accPath.Mission5, accPath.Mission6);

                            // Save details in AccountManager
                            //CopyToAccManager(accPath.UID, accPath.Username, accPath.Mission1, accPath.Mission2, accPath.Mission3, accPath.Mission4, accPath.Mission5, accPath.Mission6);

                            // After finding player database, end loop
                            break;
                        }
                        else
                        {
                            Debug.Log("No data was found in account!");
                        }
                    }
                }
            });
    }

    // When called, save to the account manager object
    /*public void CopyToAccManager(string uid, string username, bool mission1, bool mission2, bool mission3, bool mission4, bool mission5, bool mission6)
    {
        // Get reference to Account Manager obj
        AccountManager accManager = GameObject.Find("/AccountManager").GetComponent<AccountManager>();
        // Save the data to the Account Manager obj
        accManager.SetPlayerData(uid, username, mission1, mission2, mission3, mission4, mission5, mission6);

        Debug.Log("Details copied");
    }*/

    // Update is called once per frame
    void Update()
    {

    }
}
