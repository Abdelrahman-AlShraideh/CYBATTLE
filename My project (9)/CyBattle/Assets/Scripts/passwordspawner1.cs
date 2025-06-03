using UnityEngine;

public class PasswordSpawner : MonoBehaviour
{
    public GameObject passwordPrefab;
    public float spawnInterval = 2f;

    public Transform[] spawnPoints; // ← نقاط توليد ثابتة

    public string[] weakPasswords = { "123456", "password", "qwerty", "0000" };
    public string[] strongPasswords = { "P@ssw0rd!9", "S3cur3Key!", "H@rd2Gu3ss!" };

    private void Start()
    {
        InvokeRepeating(nameof(SpawnPassword), 1f, spawnInterval);
    }

    void SpawnPassword()
    {
        // نختار نقطة توليد عشوائية من بين النقاط
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 spawnPosition = spawnPoint.position;

        GameObject newPassword = Instantiate(passwordPrefab, spawnPosition, Quaternion.identity);

        var passwordScript = newPassword.GetComponent<FallingPassword>();
        if (passwordScript != null)
        {
            bool isWeak = Random.value > 0.5f;
            passwordScript.isWeak = isWeak;

            string pwText = isWeak ?
                weakPasswords[Random.Range(0, weakPasswords.Length)] :
                strongPasswords[Random.Range(0, strongPasswords.Length)];

            passwordScript.textComponent.text = pwText;
        }
    }
}
