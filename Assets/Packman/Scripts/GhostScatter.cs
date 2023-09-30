using UnityEngine;

public class GhostScatter : GhostBehavior {

    private void OnDisable() {
        Ghost.Chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Node node = collision.GetComponent<Node>();
        
        if (node != null && enabled && !Ghost.Frighen.enabled) {
            int index = Random.Range(0, node.AvalableDirections.Count);

            //^ Remove backtrack direction if possible
            if (node.AvalableDirections[index] == -Ghost.Movement.Direction
                && node.AvalableDirections.Count > 1) {
                index++;

                if (index >= node.AvalableDirections.Count) {
                    index = 0;
                }
            }

            Ghost.Movement.SetDirection(node.AvalableDirections[index]);
        }
    }
}
