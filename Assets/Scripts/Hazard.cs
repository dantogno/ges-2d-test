using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player bumped us. We need to ignore triggers,
        // or the player's ground check trigger will count for hitting hazards.
        // This is bad because the ground check circle collider is bigger than the
        // player's actual physical hitbox, which would result in false positives.
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Player hit hazard!");
        }
    }

}
