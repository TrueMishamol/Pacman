using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(PlayerInput))]
public class Pacman : MonoBehaviour {


    public MovementComponent Movement { get; private set; }


    private void Awake() {
        Movement = GetComponent<MovementComponent>();

        InputActions inputActions = new InputActions();
        inputActions.Player.Enable();
        inputActions.Player.Movement.performed += Movement_performed;

        Movement.OnDirectionSwitched += MovementComponent_OnDirectionSwitched;
    }

    private void Movement_performed(InputAction.CallbackContext context) {
        Vector2 inputVector = context.ReadValue<Vector2>();
        inputVector = SimplifyVector(inputVector);

        Movement.SetDirection(inputVector);
    }

    private Vector2 SimplifyVector(Vector2 inputVector) {
        //^ Simplify inputVector to only horizontal or vertical direction
        if (Math.Abs(inputVector.x) > Math.Abs(inputVector.y)) {
            inputVector.y = 0;
        } else {
            inputVector.x = 0;
        }
        inputVector.Normalize();
        return inputVector;
    }

    private void MovementComponent_OnDirectionSwitched(object sender, System.EventArgs e) {
        RotateFacingDirection();
    }

    private void RotateFacingDirection() {
        float angle = Mathf.Atan2(Movement.Direction.y, Movement.Direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState() {
        Movement.ResetState();
        gameObject.SetActive(true);
        RotateFacingDirection();
    }
}
