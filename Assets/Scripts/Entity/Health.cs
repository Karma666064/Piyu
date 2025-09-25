using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject healthBar;

    public int maxHealth;
    public int currentHealth;

    [SerializeField] private List<GameObject> quartHearts = new List<GameObject>();

    [SerializeField] private GameObject heartContainerPrefab;
    [SerializeField] private GameObject quartHeartPrefab;

    private void Start()
    {
        for (int i = 0; i < maxHealth / 4; i++)
        {
            GameObject heartContainer = Instantiate(heartContainerPrefab, healthBar.transform);

            for (int j = 0; j < 4; j++)
            {
                GameObject quartHeartObj = Instantiate(quartHeartPrefab, heartContainer.transform);

                quartHearts.Add(quartHeartObj);
            }
        }
    }

    public void UpdateHealth(int health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);

        HealthBarUpdate();
    }

    public void MaxHeal()
    {
        currentHealth = maxHealth;

        HealthBarUpdate();
    }

    public void HealthBarUpdate()
    {
        //for (int i = 0; i <= maxHealth; i++)
        //{
        //    quartHearts[i].SetActive(false);
        //}

        //for (int i = 0; i <= currentHealth; i++)
        //{
        //    quartHearts[i].SetActive(true);
        //}

        for (int i = 0; i < quartHearts.Count; i++)
        {
            quartHearts[i].SetActive(i < currentHealth);
        }
    }
}
