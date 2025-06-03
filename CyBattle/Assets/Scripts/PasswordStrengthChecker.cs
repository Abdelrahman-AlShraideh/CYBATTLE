using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class PasswordStrengthChecker : MonoBehaviour
{
    public TMP_InputField passwordInput;
    public TextMeshProUGUI resultText;

    public void CheckPassword()
    {
        string pwd = passwordInput.text;
        int score = 0;

        if (pwd.Length >= 8) score++;
        if (Regex.IsMatch(pwd, @"[A-Z]")) score++;
        if (Regex.IsMatch(pwd, @"[a-z]")) score++;
        if (Regex.IsMatch(pwd, @"[0-9]")) score++;
        if (Regex.IsMatch(pwd, @"[!@#$%^&*(),.?""':{}|<>]")) score++;


        switch (score)
        {
            case >= 5:
                resultText.text = "✅ Strong Password!";
                resultText.color = Color.green;
                break;
            case >= 3:
                resultText.text = "🟡 Medium Strength. Add more complexity.";
                resultText.color = Color.yellow;
                break;
            default:
                resultText.text = "🔴 Weak Password!";
                resultText.color = Color.red;
                break;
        }
    }
}
