using UnityEngine;
using TMPro; // ضروري لاستخدام TextMeshProUGUI

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI scoreText; // لربط عنصر ScoreText من المشهد
    public int score = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);

        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void MissedPassword(FallingPassword pw)
{
    Debug.Log("You missed a password! It was " + (pw.isWeak ? "WEAK ❌" : "STRONG ☑️"));

    if (pw.isWeak)
    {
        AddScore(-1); // فقط خسر نقطة إذا الكلمة ضعيفة
    }
}

}
