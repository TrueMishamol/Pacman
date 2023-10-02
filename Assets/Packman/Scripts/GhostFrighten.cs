using UnityEngine;

public class GhostFrighten : GhostBehavior {


    private const string PACMAN_LAYER = "Pacman";

    public SpriteRenderer Body;
    public SpriteRenderer Eyes;
    public SpriteRenderer Blue;
    public SpriteRenderer White;

    public bool IsEaten { get; private set; }


    public override void Enable(float duration) {
        base.Enable(duration);

        Body.enabled = false;
        Eyes.enabled = false;
        Blue.enabled = true;
        White.enabled = false;

        Invoke(nameof(Flash), duration / 2.0f);
    }

    public override void Disable() {
        base.Disable();

        Body.enabled = true;
        Eyes.enabled = true;
        Blue.enabled = false;
        White.enabled = false;
    }

    private void Eaten() {
        IsEaten = true;

        //! Return to home not TP
        //! GhostEaten state
        Vector3 position = Ghost.Home.Inside.position;
        position.z = Ghost.transform.position.z;
        Ghost.transform.position = position;

        Ghost.Home.Enable(Duration);

        Body.enabled = false;
        Eyes.enabled = true;
        Blue.enabled = false;
        White.enabled = false;
    }

    private void Flash() {
        if (!IsEaten) {
            Blue.enabled = false;
            White.enabled = true;
        }
    }

    private void OnEnable() {
        Ghost.Movement.SpeedMultiplier = 0.5f;
        IsEaten = false;
    }

    private void OnDisable() {
        Ghost.Movement.SpeedMultiplier = 1.0f;
        IsEaten = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer(PACMAN_LAYER)) {
            if (enabled) {
                Eaten();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Node node = collision.GetComponent<Node>();

        if (node != null && enabled) {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 avalableDirection in node.AvalableDirections) {
                Vector3 newPosition = transform.position + new Vector3(avalableDirection.x, avalableDirection.y, 0.0f);
                float distance = (Ghost.Pacman.position - newPosition).sqrMagnitude;

                if (distance > maxDistance) {
                    direction = avalableDirection;
                    maxDistance = distance;
                }
            }

            Ghost.Movement.SetDirection(direction);
        }
    }
}
