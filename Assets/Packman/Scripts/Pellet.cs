using UnityEngine;

public class Pellet : MonoBehaviour {


    private const string PACMAN_LAYER = "Pacman";


    public int Points = 10;


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer(PACMAN_LAYER)) {
            Eat();
        }
    }

    protected virtual void Eat() {
        FindObjectOfType<PacmanGameManager>().PelletEaten(this);
    }
}
