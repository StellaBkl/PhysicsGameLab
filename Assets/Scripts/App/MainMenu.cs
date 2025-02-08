using Firebase.Extensions;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI UserId;
    public TextMeshProUGUI UserName;
    public TextMeshProUGUI UserSurname;
    public TextMeshProUGUI UserAvatar;

    private Student student;

    FirebaseFirestore dbFirestore;


    private void Start()
    {
        dbFirestore = FirebaseFirestore.DefaultInstance;

        Debug.Log(UserId.text);
        if (UserId != null && UserId.text != "")
        {
            LoadMainMenu();
        }
    }

    private void LoadMainMenu()
    {
        GetCurrentStudent();
    }

    private void GetCurrentStudent()
    {
        Firebase.Firestore.Query query = dbFirestore.Collection("Student").WhereEqualTo("studentId", UserId.text);
        query
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task => {
                var snapshot = task.Result;
                if (snapshot != null)
                {
                    foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                    {
                        student = documentSnapshot.ConvertTo<Student>();
                        UserAvatar.text = student.studentName.ToCharArray()[0].ToString() + student.studentSurname.ToCharArray()[0].ToString();
                        UserName.text = student.studentName;
                        UserSurname.text = student.studentSurname;
                    }
                }
            });

        }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
