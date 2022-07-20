using UnityEngine;

namespace ScriptGG
{
    public class GameController : MonoBehaviour
    {
        [Header("References")] //
        public GameObject player;
        public GameObject zombiePrefab;
        public float zombieSpawnRate = 1f;
        private float nextZombieSpawn;
        public int numberOfZombiesToSpawn = 10;
        public GameObject loseMenuContainer;
        public GameObject wonMenuContainer;
        private bool hasWon;

        public Bounds gameEndedZombieSpawnBounds;

        public void Update()
        {
            MaySpawnZombies();

            if (!player.activeSelf && !hasWon)
                ShowDeathMenu(true);
            if (!player.activeSelf && hasWon)
                ShowWonMenu(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            hasWon = true;
        }

        private void ShowDeathMenu(bool active)
        {
            loseMenuContainer.SetActive(active);
        }

        private void ShowWonMenu(bool active)
        {
            wonMenuContainer.SetActive(active);
        }

        private void MaySpawnZombies()
        {
            if (!(hasWon && Time.realtimeSinceStartup > nextZombieSpawn && numberOfZombiesToSpawn > 0))
                return;

            Instantiate(
                zombiePrefab,
                new Vector3(
                    Random.Range(gameEndedZombieSpawnBounds.min.x, gameEndedZombieSpawnBounds.max.x),
                    Random.Range(gameEndedZombieSpawnBounds.min.y, gameEndedZombieSpawnBounds.max.y),
                    Random.Range(gameEndedZombieSpawnBounds.min.z, gameEndedZombieSpawnBounds.max.z)
                ),
                Quaternion.Euler(Vector3.zero));

            nextZombieSpawn = Time.realtimeSinceStartup + zombieSpawnRate;
            numberOfZombiesToSpawn--;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(gameEndedZombieSpawnBounds.center, gameEndedZombieSpawnBounds.size);
        }
    }
}
