using UnityEngine;

public class Ghost : MonoBehaviour {


    private const string PACMAN_LAYER = "Pacman";

    public MovementComponent Movement { get; private set; }
    public GhostHome Home { get; private set; }
    public GhostScatter Scatter { get; private set; }
    public GhostChase Chase { get; private set; }
    public GhostFrighten Frighen { get; private set; }

    public GhostBehavior InitialBehavior;
    public Transform Pacman; //^ Chase target

    public int Points = 200;


    private void Awake() {
        Movement = GetComponent<MovementComponent>();
        Home = GetComponent<GhostHome>();
        Scatter = GetComponent<GhostScatter>();
        Chase = GetComponent<GhostChase>();
        Frighen = GetComponent<GhostFrighten>();
    }

    private void Start() {
        ResetState();
    }

    public void ResetState() {
        gameObject.SetActive(true);
        Movement.ResetState();

        Frighen.Disable();
        Chase.Disable();
        Scatter.Enable();

        if (Home != InitialBehavior) {
            Home.Disable();
        }

        if (InitialBehavior != null) {
            InitialBehavior.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer(PACMAN_LAYER)) {
            if (Frighen.enabled) {
                FindObjectOfType<PacmanGameManager>().GhostEaten(this);
            } else {
                FindObjectOfType<PacmanGameManager>().PacmanEaten();
            }
        }
    }
}
