using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;
namespace Villagers
{
    public class VillagerManager : Singleton<VillagerManager>
    {
        [Header("Villagers")]
        [SerializeField] private ObjectPool pool;
        [SerializeField] private Transform[] spawnPoints;

        [Header("Houses")]
        [SerializeField] private ObjectPool housePool;
        [SerializeField] private List<Sprite> houseSprites;

        [SerializeField] private Transform startPos;
        [SerializeField] private int peoplePerHouse = 4;
        [SerializeField] private int houseColumns = 6;
        [SerializeField] private float houseSpacingX = 2.5f;
        [SerializeField] private float houseSpacingY = 2f;
        [SerializeField] private float houseOffset = 0.2f;
        [SerializeField] private int maxHouses;

        [Header("UI")]
        [SerializeField] private TMP_Text populationText;

        public int population;
        public int displayPopulation;

        [SerializeField] private int maxDisplayedVillagers = 20;

        private readonly List<GameObject> activeVillagers = new();
        private readonly List<GameObject> activeHouses = new();

        private Camera _camera;


        private void Start()
        {
            _camera = Camera.main;

            UpdatePopulationText();
            UpdateDisplayPopulation();
        }

        public void AddPopulation(int amount)
        {
            population = Mathf.Min(1000, population + amount);
            UpdatePopulationText();
            UpdateDisplayPopulation();
        }
        
        public void RemovePopulation(int amount)
        {
            population = Mathf.Max(2, population - amount);

            UpdatePopulationText();
            UpdateDisplayPopulation();

            if (population <= 2)
                GameEndings.Instance.TriggerBadEnding();
        }
        
        private void UpdatePopulationText()
        {
            populationText.text =
                population == 1000
                ? population + " [MAX]"
                : population.ToString();
        }

        private void UpdateDisplayPopulation()
        {
            displayPopulation = Mathf.Min(maxDisplayedVillagers, population);
        }

        private void Update()
        {
            UpdateVillagers();
            UpdateHouses();
            
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);

                Collider2D hit = Physics2D.OverlapPoint(worldPoint);

                if (hit != null)
                    hit.GetComponent<VillagerAnim>()?.Speak();
            }
        }
        
        private void UpdateVillagers()
        {
            while (activeVillagers.Count < displayPopulation)
                SpawnVillager();
            
            while (activeVillagers.Count > displayPopulation)
                DespawnVillager();
        }
        
        private void UpdateHouses()
        {
            int requiredHouses = Mathf.Min(Mathf.CeilToInt(
                displayPopulation / (float)peoplePerHouse
            ), maxHouses);
            
            while (activeHouses.Count < requiredHouses)
                SpawnHouse();
            
            while (activeHouses.Count > requiredHouses)
                DespawnHouse();
        }
        
        private void SpawnVillager()
        {
            GameObject villager = pool.Get();
            activeVillagers.Add(villager);
            int randomSpawn = Random.Range(0, spawnPoints.Length);

            villager.transform.position =
                spawnPoints[randomSpawn].position;

            villager.GetComponent<Animator>()
                .Play(0, 0, Random.value);
        }
        
        private void DespawnVillager()
        {
            if (activeVillagers.Count == 0)
                return;
            GameObject villager = activeVillagers[^1];
            activeVillagers.RemoveAt(activeVillagers.Count - 1);
            pool.Return(villager);
        }
        
        private void SpawnHouse()
        {
            int index = activeHouses.Count;

            int column = index % houseColumns;
            int row = index / houseColumns;
            
            Vector3 position = startPos.position +
                               new Vector3(
                                   column * houseSpacingX,
                                   -row * houseSpacingY,
                                   0);
            position.y += Random.Range(-houseOffset, houseOffset);
            
            GameObject house = housePool.Get();

            house.transform.position = position;
            House houseComponent = house.GetComponent<House>();
            if (houseComponent != null && houseSprites.Count > 0)
            {
                houseComponent.SetSprite(
                    houseSprites[Random.Range(0, houseSprites.Count)]
                );
            }
            activeHouses.Add(house);
        }
        
        private void DespawnHouse()
        {
            if (activeHouses.Count == 0)
                return;
            GameObject house = activeHouses[^1];
            activeHouses.RemoveAt(activeHouses.Count - 1);
            housePool.Return(house);
        }
    }
}