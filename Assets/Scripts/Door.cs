using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField]
    private string levelToLoad;

    private bool isPlayerInDoor;
    // Can't use this because multiple colliders trigger multiple times...
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        Debug.Log("Player in doorway.");
    //        if (Input.GetButtonDown("Activate"))
    //        {
    //            Debug.Log("Activated door!");
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInDoor = false;
        }
    }

    private void Update()
    {
        if (isPlayerInDoor && Input.GetButtonDown("Activate"))
        {
            Debug.Log("Player Activated Door");
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
