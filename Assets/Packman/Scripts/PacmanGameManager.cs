using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanGameManager : MonoBehaviour {


    public Ghost[] Ghosts;
    public Pacman Pacman;
    public Transform Pellets;

    public int Score { get; private set; }
    public int Lives { get; private set; }

    private int _startLives = 3;


    private void Start() {
        NewGame();
    }

    private void Update() {
        if (Lives <=0 && Input.anyKeyDown) {
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

    public void GhostEaten(Ghost ghost) {
        SetScore(Score + ghost.points);
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

}
