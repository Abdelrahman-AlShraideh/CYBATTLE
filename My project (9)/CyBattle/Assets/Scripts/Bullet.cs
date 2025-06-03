using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
{
    Debug.Log("Bullet hit: " + collision.name);

    // تجاهل اللاعب
    if (collision.CompareTag("Player")) return;

    if (collision.CompareTag("Password"))
    {
FallingPassword pw = collision.GetComponentInChildren<FallingPassword>();
        if (pw != null)
        {
            GameManager.Instance.AddScore(pw.isWeak ? 1 : -1);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}

}
