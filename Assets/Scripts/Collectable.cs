using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. Download sunnyland carrot assets
/// 2. Set import settings, slice
/// 3. Drag in first frame
/// 4. Add box collider. Make it a trigger.
/// 5. Add animatior component
/// 6. Open Animation window
/// 7. Create new anim in Animations folder
/// 8. Drag in frames
/// 9. Set frame rate
/// 10. Create a script. 
/// 11. OnTriggerEnter2D Destroy(
/// 12. Create sound on BFXR
/// 13. Import sound into project
/// 14. Add AudioSource, setup clip, no PlayOnAwake
/// 15. Create variable, initialize in Start
/// 16. Play()
/// 17. Delay destroy (start with magic number, explain why bad, use clip time)
/// 18. Disable visual (find it in inspector, toggle it, then do code)
/// 19. Disable collider
/// 20. Make it a prefab. drop a couple in.
/// 21. Create coinCount. Start with instance var, debug Log count.
/// 22. Explain static
/// 24. Next time: UI
/// </summary>

public class Collectable : MonoBehaviour
{
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private static int coinCount;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
        coinCount++;
        Debug.Log(coinCount);
        audioSource.Play();
        Destroy(gameObject, audioSource.clip.length);
    }
}
