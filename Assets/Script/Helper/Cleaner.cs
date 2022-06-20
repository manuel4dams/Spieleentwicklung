using UnityEngine;

public class Cleaner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
                playerHealth.KillPlayer(true);
                break;
            case "Enemy":
                var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.KillEnemy(true);
                break;
            default:
                Destroy(other.gameObject);
                break;
        }
    }
}
