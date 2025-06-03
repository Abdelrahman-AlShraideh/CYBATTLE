using UnityEngine;

public class TestTriggerScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Trigger Area!");
        }
    }
}
