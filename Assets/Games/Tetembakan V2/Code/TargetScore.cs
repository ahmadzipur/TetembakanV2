using UnityEngine;

public class TargetScore : MonoBehaviour
{
    public int scoreValue = 10;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            FindObjectOfType<GameManager>().AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
