using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Villagers
{
    public class VillagerManager : Singleton<VillagerManager>
    {
        [SerializeField] ObjectPool pool;
        [SerializeField] Transform[] spawnPoints;
        [SerializeField] private TMP_Text populationText;

        private List<GameObject> activeVillagers = new List<GameObject>();

        public int population;
        public int displayPopulation; 
        [SerializeField] private int maxDisplayedVillagers = 20; // Could be a setting in the menu so we don't see too many on screen

        public void Start()
        {
            if (population == 1000) populationText.text = population + " [MAX]";
            else populationText.text = population.ToString();
            
        }

        public void AddPopulation(int amount)
        {
            // Capping population at 1000 because why not, I said so
            population = Mathf.Min(1000, population + amount);
            
            if (population == 1000) populationText.text = population + " [MAX]";
            else populationText.text = population.ToString();
            
            UpdateDisplayPopulation();
        }

        public void RemovePopulation(int amount)
        {
            population = Mathf.Max(2, population - amount);
            populationText.text = population.ToString();
            UpdateDisplayPopulation();
            
            if (population <= 2) GameEndings.Instance.TriggerBadEnding();
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