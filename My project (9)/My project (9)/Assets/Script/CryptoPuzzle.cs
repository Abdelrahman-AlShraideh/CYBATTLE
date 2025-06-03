using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CryptoPuzzle : MonoBehaviour
{
    public TMP_InputField keywordInput;
    public TMP_Text cipherText;
    public TMP_Text resultText;
    public GameObject winCanvas; // ‘«‘… «·›Ê“

    private string correctKeyword = "NATURE";
    private string encryptedMessage = "URZHQ RVTME WIFXS";
    private string decryptedMessage = "LAT 31.9539 LON 35.9106";

    void Start()
    {
        cipherText.text = "Encrypted Message: " + encryptedMessage;
        resultText.text = "";
        winCanvas.SetActive(false); //  √ﬂœ √‰Â« „Œ›Ì… ⁄‰œ «·»œ«Ì…
    }

    public void CheckKeyword()
    {
        string input = keywordInput.text.ToUpper();

        if (input == correctKeyword)
        {
            resultText.text = "Coordinates Unlocked:\n" + decryptedMessage;
            Win();
        }
        else
        {
            resultText.text = "Wrong keyword. Check the wall frames!";
        }
    }

    void Win()
    {
        winCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Player Won!");
    }
}
