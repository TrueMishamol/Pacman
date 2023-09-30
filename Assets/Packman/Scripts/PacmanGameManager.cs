using UnityEngine;

public class PacmanGameManager : MonoBehaviour {


    public Ghost[] Ghosts;
    public Pacman Pacman;
    public Transform Pellets;

    public int GhostMultiplier { get; private set; } = 1;
    public int Score { get; private set; }
    public int Lives { get; private set; }

    private int _startLives = 3;


    private void Start() {
        NewGame();
    }

    private void Update() {
        if (Lives <= 0 && Input.anyKeyDown) {
            NewGame();
        }
    }

    private void NewGame() {
        SetScore(0);
        SetLives(_startLives);
        ResetWholeGame();
    }

    private void ResetWholeGame() {
        foreach (Transform pellet in Pellets) {
            pellet.gameObject.SetActive(true);
        }

        ResetNewAttempt();
    }

    private void ResetNewAttempt() {
        ResetGhostMiltiplier();

        for (int i = 0; i < Ghosts.Length; i++) {
            Ghosts[i].gameObject.SetActive(true);
        }

        Pacman.gameObject.SetActive(true);
    }

    private void GameOver() {
        for (int i = 0; i < Ghosts.Length; i++) {
            Ghosts[i].gameObject.SetActive(false);
        }

        Pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score) {
        Score = score;
    }

    private void SetLives(int lives) {
        Lives = lives;
    }

    public void PelletEaten(Pellet pellet) {
        Debug.Log("Eaten");

        SetScore(Score + pellet.Points);
        pellet.gameObject.SetActive(false);

        if (!ArePelletsLeft()) {
            Pacman.gameObject.SetActive(false);
            Invoke(nameof(ResetWholeGame), 3.0f);
        }
    }

    public void PelletPowerEaten(PelletPower pellet) {
        CancelInvoke();
        Invoke(nameof(ResetGhostMiltiplier),pellet.Duration);
        PelletEaten(pellet);
    }

    public void GhostEaten(Ghost ghost) {
        int points = ghost.points * GhostMultiplier;
        SetScore(Score + points);
        GhostMultiplier++;
    }

    public void PacmanEaten() {
        Pacman.gameObject.SetActive(false);

        SetLives(Lives - 1);

        if (Lives > 0) {
            Invoke(nameof(ResetNewAttempt), 3.0f);
        } else {
            GameOver();
        }
    }
    
    private bool ArePelletsLeft() {
        foreach (Transform pellet in Pellets) {
            if (pellet.gameObject.activeSelf) {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMiltiplier() {
        GhostMultiplier = 0;
    }

}
