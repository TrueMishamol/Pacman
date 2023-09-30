using UnityEngine;

[RequireComponent (typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour {


    public Ghost Ghost { get; private set; }
    
    public float Duration;


    private void Awake () {
        Ghost = GetComponent<Ghost>();
        enabled = false;
    }

    public void Enable() {
        Enable(Duration);
    }

    public virtual void Enable(float duration) { 
        enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable() {
        enabled = false;

        CancelInvoke();
    }
}
