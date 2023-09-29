using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(PlayerInput))]
public class Pacman : MonoBehaviour {


    public MovementComponent MovementComponent { get; private set; }


    private void Awake() {
        MovementComponent = GetComponent<MovementComponent>();

        InputActions inputActions = new InputActions();
        inputActions.Player.Enable();
        inputActions.Player.Movement.performed += Movement_performed;
    }

    private void Movement_performed(InputAction.CallbackContext context) {
        Vector2 inputVector = context.ReadValue<Vector2>();
        inputVector = SimplifyVector(inputVector);

        Debug.Log(inputVector);

        MovementComponent.SetDirection(inputVector);
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
}
