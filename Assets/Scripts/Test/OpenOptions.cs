using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOptions : MonoBehaviour
{
    public GameObject Panel;
    public void Open()
    {
        if (Panel != null)
        {
            Animator animator = Panel.GetComponent<Animator>();
            Debug.Log("animator1");
            if (animator != null)
            {
                Debug.Log("animator2");
                bool isOpen = animator.GetBool("open");
                animator.SetBool("open", !isOpen);
            }
        }
    }

   
}
