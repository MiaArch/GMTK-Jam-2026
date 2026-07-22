using UnityEngine;
using Utils;

namespace Villagers
{
    public class VillagerSpawner : MonoBehaviour
    {
        [SerializeField] ObjectPool pool;
        [SerializeField] Transform[] spawnPoints;

        public int population;
        public int displayPopulation; // Could be a setting in the menu so we don't see too many on screen

        int currentVillagers;

        void Update()
        {
            while (currentVillagers < displayPopulation)
            {
                GameObject villager = pool.Get();
                int randomSpawn = Random.Range(0, spawnPoints.Length);
                villager.transform.position = spawnPoints[randomSpawn].position;
                Animator animator = villager.GetComponent<Animator>();
                animator.Play(0, 0, Random.value);

                currentVillagers++;
            }
        }
    }
}