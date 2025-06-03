using UnityEngine;
using TMPro; // مهم لاستخدام TextMeshPro
using System.Collections.Generic;

public class PasswordClassifier : MonoBehaviour
{
    public TextMeshProUGUI passwordText, feedbackText, scoreText;
    public GameObject feedbackPanel;

    private int score = 0, currentIndex = 0;

    private List<(string password, bool isStrong, string reason)> passwords = new()
    {
        ("123456", false, "Very common password."),
        ("P@ssw0rd!", true, "Includes symbols, numbers, and uppercase."),
        ("abcdef", false, "No complexity."),
        ("My$ecurePass2025", true, "Strong structure with mix of symbols.")
    };

    void Start() => ShowNext();

    public void Choose(bool guessStrong)
    {
        var p = passwords[currentIndex];

        if (guessStrong == p.isStrong)
        {
            score++;
            feedbackText.text = "✅ Correct!\n" + p.reason;
        }
        else
        {
            feedbackText.text = "❌ Wrong!\n" + p.reason;
        }

        scoreText.text = $"Score: {score}";
        feedbackPanel.SetActive(true);
        Invoke(nameof(ShowNext), 2f);
    }

    void ShowNext()
    {
        feedbackPanel.SetActive(false);
        currentIndex++;
        if (currentIndex >= passwords.Count)
        {
            passwordText.text = "Done!";
            return;
        }

        passwordText.text = passwords[currentIndex].password;
    }
}
