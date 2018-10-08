using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    [SerializeField]
    private float inactivatedRotationSpeed = 1, activatedRotationSpeed = 2;

    [SerializeField]
    private float inactivatedScale = 1, activatedScale = 1.5f;

    [SerializeField]
    private Color inactivatedColor, activatedColor; 

    private bool isActivated;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    public void SetIsActivated(bool value)
    {
        isActivated = value;
        UpdateScale();
        UpdateColor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivated)
        {
            Debug.Log("Player touched checkpoint.");
            PhysicsCharacterController player = collision.GetComponent<PhysicsCharacterController>();
            player.SetCheckpoint(this);
            audioSource.Play();
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        UpdateScale();
        UpdateColor();
    }


    // Update is called once per frame
    void Update ()
    {
        UpdateRotation();	
	}

    private void UpdateRotation()
    {
        float rotationSpeed = inactivatedRotationSpeed;
        if (isActivated)
        {
            rotationSpeed = activatedRotationSpeed;
        }
        transform.Rotate(Vector2.up * rotationSpeed * Time.deltaTime);
    }
    private void UpdateScale()
    {
        float scale = inactivatedScale;
        if (isActivated)
            scale = activatedScale;
        transform.localScale = Vector3.one * scale;
    }

    private void UpdateColor()
    {
        Color color = inactivatedColor;
        if (isActivated)
            color = activatedColor;
        spriteRenderer.color = color;
    }
}

// 1. create art
// 2. color
// 3. Rotation
// 4. Detect player
// 5. Saving current checkpoint in the player.
// 6. SetCheckpoint function in Player. Explain arguments.
// 7. GetComponent<Player>() in checkpoint. Change SetCheckpoint to public
// 8. Respawn function in player
// 9. Call respawn from hazard
// 10. Handle null checkpoint

    // New video?
// 11. Better player feedback on activating checkpoint
// 12. two speeds
// 13. Call ActivateCheckpoint  13A check for null!
// 14. make a prefab out of checkpoint, test two checkpoints
// 15. More feedback: scale. Don't call it in Update
// 16. more feedback: color 16a spriterenderer getcomponent in start 16B applying changes to prefab
// 17. sorting layers
// 18. sound effects
// 19. BFXR 19A extract 19B export
// 20. import into unity
// 21. Add audio source
// 22. Test w/ play on awake. no play on awake. update prefab
// 23. Getcomponent and play in code
// 24. check if isActivated before playing sfx

 