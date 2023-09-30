using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {


    public LayerMask ObstacleLayer;

    public List<Vector2> AvalableDirections { get; private set; }


    private void Start () {
        AvalableDirections = new List<Vector2>();

        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.right);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
    }

    private void CheckAvailableDirection(Vector2 direction) {
        Vector2 raycastSize = Vector2.one * 0.5f;
        float raycastDistance = 1.0f;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, raycastSize, 0.0f, direction, raycastDistance, ObstacleLayer);

        if (hit.collider == null) {
            AvalableDirections.Add(direction);
        }
    }
}
