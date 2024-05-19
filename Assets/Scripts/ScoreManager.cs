using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;
    public GameObject ball;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI winMessageText;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;

    public void Player2Score()
    {
        scorePlayer2++;
        UpdateScoreText(player2ScoreText);
        Debug.Log($"Player 2 scored! Score is now {scorePlayer1}-{scorePlayer2}");
        StartCoroutine(HandleScore());
    }

    public void Player1Score()
    {
        scorePlayer1++;
        UpdateScoreText(player1ScoreText);
        Debug.Log($"Player 1 scored! Score is now {scorePlayer1}-{scorePlayer2}");
        StartCoroutine(HandleScore());
    }

    void UpdateScoreText(TextMeshProUGUI scoreText)
    {
        player1ScoreText.text = scorePlayer1.ToString();
        player2ScoreText.text = scorePlayer2.ToString();
        StartCoroutine(ScaleTextEffect(scoreText));
    }

    IEnumerator HandleScore()
    {
        yield return StartCoroutine(ShowCountdown(3));
        CheckGameOver();
    }

    IEnumerator ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            countdownText.text = $"Time(sec) to launch ball: {i}";
            yield return new WaitForSeconds(1);
        }
        countdownText.text = "";
    }

    void CheckGameOver()
    {
        bool gameOver = false;
        if (scorePlayer1 == 11)
        {
            Debug.Log("Game Over, Left Paddle Wins");
            ShowWinMessage("Left Paddle Wins!");
            gameOver = true;
        }
        else if (scorePlayer2 == 11)
        {
            Debug.Log("Game Over, Right Paddle Wins");
            ShowWinMessage("Right Paddle Wins!");
            gameOver = true;
        }

        if (gameOver)
        {
            scorePlayer1 = 0;
            scorePlayer2 = 0;
            UpdateScoreText(null);
            Debug.Log($"Scores reset to {scorePlayer1}-{scorePlayer2}");
        }
    }

    void ShowWinMessage(string message)
    {
        winMessageText.text = message;
        StartCoroutine(ClearWinMessageAfterDelay(3)); // Clear the message after 3 seconds
    }

    IEnumerator ClearWinMessageAfterDelay(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        winMessageText.text = "";
    }

    IEnumerator ScaleTextEffect(TextMeshProUGUI text)
    {
        if (text == null) yield break;

        Vector3 originalScale = text.transform.localScale;
        Vector3 targetScale = originalScale * 1.5f;
        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            text.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            text.transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        text.transform.localScale = originalScale;
    }
}
