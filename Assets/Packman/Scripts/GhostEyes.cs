using UnityEngine;

public class GhostEyes : MonoBehaviour {


    public SpriteRenderer spriteRenderer { get; private set; }
    public MovementComponent Movement { get; private set; }

    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;


    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Movement = GetComponentInParent<MovementComponent>();
    }


    private void Update() {
        switch (Movement.Direction) {
            case var value when value == Vector2.up:
                spriteRenderer.sprite = Up;
                break;
            case var value when value == Vector2.down:
                spriteRenderer.sprite = Down;
                break;
            case var value when value == Vector2.left:
                spriteRenderer.sprite = Left;
                break;
            case var value when value == Vector2.right:
                spriteRenderer.sprite = Right;
                break;
        }
    }
}
