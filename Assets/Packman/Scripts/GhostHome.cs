using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehavior {
    

    private const string WALL_LAYER = "Wall";


    public Transform Inside;
    public Transform Outside;


    private void OnEnable() {
        StopAllCoroutines();
    }

    private void OnDisable() {
        if (gameObject.activeSelf) {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer(WALL_LAYER)) {
            Ghost.Movement.SetDirection(-Ghost.Movement.Direction);
        }
    }

    private IEnumerator ExitTransition() {
        Ghost.Movement.SetDirection(Vector2.up, true);
        Ghost.Movement.Rigidbody.isKinematic = true;
        Ghost.Movement.enabled = false;

        Vector3 position = transform.position;

        float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed < duration) {
            Vector3 newPosition = Vector3.Lerp(position, Inside.position, elapsed / duration);
            newPosition.z = position.z;
            Ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < duration) {
            Vector3 newPosition = Vector3.Lerp(Inside.position, Outside.position, elapsed / duration);
            newPosition.z = position.z;
            Ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        Ghost.Movement.SetDirection(new Vector2(Random.value < 0.5f ? - 1.0f : 1.0f, 0.0f), true);
        Ghost.Movement.Rigidbody.isKinematic = false;
        Ghost.Movement.enabled = true;
    }
}
