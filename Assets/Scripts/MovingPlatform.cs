using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    [SerializeField]
    private float speed = 1;

    private new Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start ()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = speed * Vector2.right;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
