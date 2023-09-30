using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour {


    public event EventHandler OnDirectionSwitched;

    public float Speed = 8.0f;
    public float SpeedMultiplier = 1.0f;
    public Vector2 InitialDirection;
    public LayerMask ObstacleLayer;

    public Rigidbody2D Rigidbody { get; private set; }
    public Vector2 Direction { get; private set; }
    public Vector2 NextDirection { get; private set; }
    public Vector3 StartPosition { get; private set; }


    private void Awake() {
        Rigidbody = GetComponent<Rigidbody2D>();
        StartPosition = transform.position;
    }

    private void Start() {
        ResetState();
    }

    public void ResetState() {
        SpeedMultiplier = 1.0f;
        Direction = InitialDirection;
        NextDirection = Vector2.zero;
        transform.position = StartPosition;
        Rigidbody.isKinematic = false;
        enabled = true;
    }

    private void Update() {
        if (NextDirection != Vector2.zero) {
            SetDirection(NextDirection);
        }
    }

    private void FixedUpdate() {
        Vector2 position = transform.position;
        Vector2 translation = Direction * Speed * SpeedMultiplier * Time.fixedDeltaTime;
        Rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false) {
        if (forced || !Occupied(direction)) {
            Direction = direction;
            NextDirection = Vector2.zero;
            OnDirectionSwitched?.Invoke(this, EventArgs.Empty);
        } else {
            NextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction) {
        Vector2 raycastSize = Vector2.one * 0.75f;
        float raycastDistance = 1.5f; //^ From Center, so we add 0.5f
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, raycastSize, 0.0f, direction, raycastDistance, ObstacleLayer);

        return hit.collider != null;
    }
}
