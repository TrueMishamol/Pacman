using UnityEngine;
using UnityEngine.Tilemaps;

public class DisableTilemapRendererAtStart : MonoBehaviour {

    private void Start() {
        GetComponent<TilemapRenderer>().enabled = false;
    }
}
