using UnityEngine;
using TMPro;

public class FallingPassword : MonoBehaviour
{
    public bool isWeak = true;
    public float speed = 2f;
    public TextMeshPro textComponent;

    void Start()
    {
        if (textComponent == null)
            textComponent = GetComponentInChildren<TextMeshPro>();
    }

    void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Password hit by: " + other.name);
}

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            // Missed password (reach bottom)
            GameManager.Instance.MissedPassword(this);
            Destroy(gameObject);
        }
    }
}
