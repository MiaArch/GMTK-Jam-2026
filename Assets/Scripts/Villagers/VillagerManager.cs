using System.Collections.Generic;
using Unity.Mathematics.Geometry;
using UnityEngine;
using Utils;

namespace Villagers
{
    public class VillagerManager : Singleton<VillagerManager>
    {
        [SerializeField] ObjectPool pool;
        [SerializeField] Transform[] spawnPoints;

        private List<GameObject> activeVillagers = new List<GameObject>();

        public int population;
        public int displayPopulation; 
        [SerializeField] private int maxDisplayedVillagers = 20; // Could be a setting in the menu so we don't see too many on screen
        

        public void AddPopulation(int amount)
        {
            population += amount;
            UpdateDisplayPopulation();
        }

        public void RemovePopulation(int amount)
        {
            population = Mathf.Max(0, population - amount);
            UpdateDisplayPopulation();
        }

        private void UpdateDisplayPopulation()
        {
            displayPopulation = Mathf.Min(maxDisplayedVillagers, population);
        }

        private void Update()
        {
            // UpdateDisplayPopulation(); - For testing in Engine
            while (activeVillagers.Count < displayPopulation)
            {
                SpawnVillager();
            }

            while (activeVillagers.Count > displayPopulation)
            {
                DespawnVillager();
            }
        }

        private void SpawnVillager()
        {
            GameObject villager = pool.Get();
            activeVillagers.Add(villager);

            int randomSpawn = Random.Range(0, spawnPoints.Length);
            villager.transform.position = spawnPoints[randomSpawn].position;

            villager.GetComponent<Animator>().Play(0, 0, Random.value);
        }

        private void DespawnVillager()
        {
            if (activeVillagers.Count != 0)
            {
                GameObject villager = activeVillagers[^1];
                activeVillagers.Remove(villager);
                pool.Return(villager);
            }
            
        }
    }
}