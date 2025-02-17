using Firebase.Database;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections;

public class Database : MonoBehaviour
{
    public InputField Username;
    public InputField LPassword;
    public InputField StudentId;
    public InputField RPassword;
    public InputField RPasswordRepeat;
    public TextMeshProUGUI LErrorMessage;
    public TextMeshProUGUI RErrorMessage;
    public TextMeshProUGUI SuccessMessage;
    public InputField COldPassword;
    public InputField CNewPassword;
    public InputField CNewPasswordRepeat;
    public TextMeshProUGUI CErrorMessage;
    public TextMeshProUGUI CSuccessMessage;
    public GameObject loginPanel;
    public GameObject registerPanel;
    public GameObject mainMenu;
    public TextMeshProUGUI UserId;

    private string userID;
    private DatabaseReference databaseReference;
    private Student student;

    FirebaseFirestore dbFirestore;

    // Start is called before the first frame update
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        //InitializeFirebase();
        dbFirestore = FirebaseFirestore.DefaultInstance;

    }

    public void LoginUser()
    {
        LErrorMessage.text = "";

        if (Username.text == "" || LPassword.text == "")
        {
            LErrorMessage.text = "Συμπληρώστε όλα τα πεδία.";
        }
        else
        {
            CheckStudentData(LErrorMessage);
        }

    }
    public void CheckStudentData(TextMeshProUGUI errorText)
    {
        Firebase.Firestore.Query query = dbFirestore.Collection("Student").WhereEqualTo("studentUsername", Username.text);
        query.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            var snapshot = task.Result;
            errorText.text = "Το όνομα χρήστη ή ο κωδικός είναι λανθασμένα.";

            if (snapshot != null)
            {
                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    student = documentSnapshot.ConvertTo<Student>();
                    
                    if(student.passwordHash != null && student.passwordSalt != null)
                    {
                        // Convert Base64 strings back to byte arrays
                        byte[] storedHash = Convert.FromBase64String(student.passwordHash);
                        byte[] salt = Convert.FromBase64String(student.passwordSalt);

                        // Compute hash with stored salt
                        using (var hmac = new HMACSHA512(salt))
                        {
                            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(LPassword.text));

                            // Securely compare hashes
                            if (CryptographicOperations.FixedTimeEquals(computedHash, storedHash))
                            {
                                // Login successful
                                UserId.text = student.studentId;
                                InitializeLoginInputs();
                                mainMenu.SetActive(true);
                                loginPanel.SetActive(false);
                                return;
                            }
                        }
                    }
                };
            }
        });
    }

    public void RegisterUser()
    {
        RErrorMessage.text = "";

        if (StudentId.text == "" || RPassword.text == "" || RPasswordRepeat.text == "")
        {
            RErrorMessage.text = "Συμπληρώστε όλα τα πεδία.";
        }
        else if (RPassword.text != RPasswordRepeat.text)
        {
            RErrorMessage.text = "Τα δύο πεδία του κωδικού δεν ταιριάζουν.";
        }
        else
        {
            Firebase.Firestore.Query query = dbFirestore.Collection("Student").WhereEqualTo("studentId", StudentId.text);

            query.GetSnapshotAsync().ContinueWithOnMainThread(task => {
                var snapshot = task.Result;
                RErrorMessage.text = "Το ID μαθητή ειναι λανθασμένο.";

                if (snapshot != null)
                {
                    foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                    {
                        student = documentSnapshot.ConvertTo<Student>();
                        if (student.passwordHash == null || student.passwordSalt == null)
                        {
                            // Generate a secure salt
                            byte[] salt = new byte[16]; // 128-bit salt
                            using (var rng = new RNGCryptoServiceProvider())
                            {
                                rng.GetBytes(salt);
                            }

                            // Hash the password with the salt
                            byte[] passwordHash;
                            using (var hmac = new HMACSHA512(salt)) // Use salt as the key
                            {
                                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(RPassword.text));
                            }

                            // Convert to Base64 to store in Firestore
                            string passwordHashBase64 = Convert.ToBase64String(passwordHash);
                            string saltBase64 = Convert.ToBase64String(salt);

                            DocumentReference documentReference = dbFirestore.Collection("Student").Document(documentSnapshot.Id);

                            Dictionary<string, object> updates = new Dictionary<string, object>
                            {
                                { "passwordHash", passwordHashBase64},
                                { "passwordSalt", saltBase64 }
                            };
                            documentReference.UpdateAsync(updates);
                            InitializeRegisterInputs();
                            SuccessMessage.text = "Η εγγραφή σας ολοκληρώθηκε με επιτυχία!!!";
                            registerPanel.SetActive(false);
                            loginPanel.SetActive(true);
                        }
                        else
                        {
                            RErrorMessage.text = "Ο χρήστης υπάρχει ήδη.";
                        }
                    };
                }
                Debug.Log(student.studentName);
            });
        }
    }

    public void InitializeLoginInputs()
    {
        Username.text = "";
        LPassword.text = "";
        SuccessMessage.text = "";
        LErrorMessage.text = "";
    }
    public void InitializeRegisterInputs()
    {
        StudentId.text = "";
        RPassword.text = "";
        RPasswordRepeat.text = "";
        RErrorMessage.text = "";
    }
    public void InitializeChangePasswordInputs()
    {
        COldPassword.text = "";
        CNewPassword.text = "";
        CNewPasswordRepeat.text = "";
        CErrorMessage.text = "";
    }

    //public void ChangePassword()
    //{
    //    CErrorMessage.text = "";
    //    bool validOld = true;

    //    if (student.passwordHash != null && student.passwordSalt != null)
    //    {
    //        using var hmacOld = new HMACSHA512(student.passwordSalt);
    //        var computedHash = hmacOld.ComputeHash(Encoding.UTF8.GetBytes(COldPassword.text));

    //        for (int i = 0; i < computedHash.Length; i++)
    //        {
    //            if (computedHash[i] != student.passwordHash[i])
    //            {
    //                validOld = false;
    //            }
    //        }
    //    }

    //    if (COldPassword.text == "" || CNewPassword.text == "" || CNewPasswordRepeat.text == "")
    //    {
    //        CErrorMessage.text = "Συμπληρώστε όλα τα πεδία.";
    //    }
    //    else if (!validOld)
    //    {
    //        CErrorMessage.text = "Συμπληρώστε σωστά τον παλιό κωδικό.";
    //    }
    //    else if (COldPassword.text == CNewPassword.text || COldPassword.text == CNewPasswordRepeat.text)
    //    {
    //        CErrorMessage.text = "Ο παλιός κωδικός δεν μπορεί να είναι ίδιος με τον νέο.";
    //    }
    //    else if (CNewPassword.text != CNewPasswordRepeat.text)
    //    {
    //        CErrorMessage.text = "Τα δύο πεδία του νέου κωδικού δεν ταιριάζουν.";
    //    }
    //    else
    //    {
    //        Firebase.Firestore.Query query = dbFirestore.Collection("Student").WhereEqualTo("studentId", student.studentId);

    //        query.GetSnapshotAsync().ContinueWithOnMainThread(task => {
    //            var snapshot = task.Result;
    //            CErrorMessage.text = "Δεν βρέθηκε μαθητής.";

    //            if (snapshot != null)
    //            {
    //                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
    //                {

    //                    Student st = documentSnapshot.ConvertTo<Student>();

    //                    using var hmac = new HMACSHA512();

    //                    DocumentReference documentReference = dbFirestore.Collection("Student").Document(documentSnapshot.Id);

    //                    Dictionary<string, object> updates = new Dictionary<string, object>
    //                        {
    //                            { "passwordHash", hmac.ComputeHash(Encoding.UTF8.GetBytes(CNewPassword.text))},
    //                            { "passwordSalt", hmac.Key }
    //                        };
    //                    documentReference.UpdateAsync(updates);
    //                    InitializeChangePasswordInputs();
    //                    CSuccessMessage.text = "Ο κωδικός άλλαξε με επιτυχία!!!";

    //                };
    //            }
    //        });
    //    }
    //}
    public void ChangePassword()
    {
        StartCoroutine(ChangePasswordCoroutine());
    }

    // Coroutine to wait for async Task function
    private IEnumerator ChangePasswordCoroutine()
    {
        // Call async Task function
        Task task = OnChangePassword();

        // Wait until the task is complete
        while (!task.IsCompleted) yield return null;

        // Handle errors if any
        if (task.Exception != null)
        {
            Debug.LogError(task.Exception);
        }
    }

    public async Task OnChangePassword()
    {
        CErrorMessage.text = "";

        if (string.IsNullOrEmpty(COldPassword.text) || string.IsNullOrEmpty(CNewPassword.text) || string.IsNullOrEmpty(CNewPasswordRepeat.text))
        {
            CErrorMessage.text = "Συμπληρώστε όλα τα πεδία.";
            return;
        }

        if (COldPassword.text == CNewPassword.text)
        {
            CErrorMessage.text = "Ο παλιός κωδικός δεν μπορεί να είναι ίδιος με τον νέο.";
            return;
        }

        if (CNewPassword.text != CNewPasswordRepeat.text)
        {
            CErrorMessage.text = "Τα δύο πεδία του νέου κωδικού δεν ταιριάζουν.";
            return;
        }

        if (student.passwordHash == null || student.passwordSalt == null)
        {
            CErrorMessage.text = "Δεν υπάρχει εγγεγραμένος χρήστης.";
            return;
        }

        // Decode Base64 values from Firestore
        byte[] storedHash = Convert.FromBase64String(student.passwordHash);
        byte[] storedSalt = Convert.FromBase64String(student.passwordSalt);

        // Verify old password
        using (var hmac = new HMACSHA512(storedSalt))
        {
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(COldPassword.text));

            if (!CryptographicOperations.FixedTimeEquals(computedHash, storedHash))
            {
                CErrorMessage.text = "Συμπληρώστε σωστά τον παλιό κωδικό.";
                return;
            }
        }

        // Generate a new random salt
        byte[] newSalt = new byte[16]; // 128-bit salt
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(newSalt);
        }

        // Hash new password with new salt
        byte[] newHash;
        using (var hmac = new HMACSHA512(newSalt))
        {
            newHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(CNewPassword.text));
        }

        // Convert to Base64 to store in Firestore
        string newHashBase64 = Convert.ToBase64String(newHash);
        string newSaltBase64 = Convert.ToBase64String(newSalt);

        // Update Firestore
        Firebase.Firestore.Query query = dbFirestore.Collection("Student").WhereEqualTo("studentId", student.studentId);

        await query.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            var snapshot = task.Result;
            CErrorMessage.text = "Δεν βρέθηκε μαθητής.";

            if (snapshot != null)
            {
                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {

                    Student st = documentSnapshot.ConvertTo<Student>();

                    DocumentReference documentReference = dbFirestore.Collection("Student").Document(documentSnapshot.Id);

                    Dictionary<string, object> updates = new Dictionary<string, object>
                       {
                           { "passwordHash", newHashBase64 },
                           { "passwordSalt", newSaltBase64 }
                        };
                    documentReference.UpdateAsync(updates);
                    InitializeChangePasswordInputs();
                    CSuccessMessage.text = "Ο κωδικός άλλαξε με επιτυχία!!!";

                };
            }
        });
    }

    public void testText()
    {
        //NewText = gameObject.AddComponent<TextMeshProUGUI>();
        //user = new User(Name.text, Surname.text, Nickname.text, 0);
        //string userJson = JsonUtility.ToJson(user);
    }
        public void InitializeUser()
    {
        //user = new User(Name.text, Surname.text, Nickname.text, 0);
        //string userJson = JsonUtility.ToJson(user);
        //Debug.Log(userJson);
    }

    

    public void CreateUser()
    {
        //User newUser = new User(Name.text, Surname.text, Nickname.text, 0);
        //string json = JsonUtility.ToJson(newUser);
        //Debug.Log(json);
        //Debug.Log(userID);
       // Debug.Log(databaseReference);

       // databaseReference.Child("students").Child(userID).SetRawJsonValueAsync(json);
    }

    private void InitializeFirebase()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            if (task.Result == Firebase.DependencyStatus.Available)
            {
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("All good");
            }
            else
            {
                Debug.LogError("Firebase initialization failed.");
            }
        });
    }
}
