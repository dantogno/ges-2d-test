using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField]
    private float test = 1;
    private void Update()
    {
        Debug.Log("test");
    }
    private void Start()
    {
        Debug.Log("Test");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit hazard!");
        }
    }

}
