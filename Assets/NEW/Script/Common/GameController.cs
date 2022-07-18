using Unity.VisualScripting;
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
        public float maximumZombies = 30f;
        public GameObject loseMenuContainer;
        public GameObject wonMenuContainer;
        private bool hasWon;

        public Bounds bound;

        public void Update()
        {
            if (hasWon && Time.realtimeSinceStartup > nextZombieSpawn && maximumZombies > 0f)
                SpawnZombies();

            if (player.IsUnityNull() && !hasWon)
                ShowDeathMenu(true);
            if (player.IsUnityNull() && hasWon)
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

        private void SpawnZombies()
        {
            Instantiate(
                zombiePrefab,
                new Vector3(
                    Random.Range(bound.min.x, bound.max.x),
                    bound.center.y,
                    bound.center.z),
                Quaternion.Euler(Vector3.zero));

            nextZombieSpawn = Time.realtimeSinceStartup + zombieSpawnRate;
            maximumZombies--;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(bound.center, bound.extents);
        }
    }
}
