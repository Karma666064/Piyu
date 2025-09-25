using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject healthBar;

    public int maxHealth;
    public int currentHealth;

    [SerializeField] private GameObject quartHeartPrefab;
    private List<GameObject> quartHearts = new List<GameObject>();

    private void Start()
    {
        healthBar = gameObject.GetComponentsInChildren<GameObject>()[0];

        for (int i = 1; i <= maxHealth; i++)
        {
            quartHearts.Add(quartHeartPrefab);
        }

        foreach (var quartHeart in quartHearts)
        {
            Instantiate(quartHeart, healthBar.transform);
        }
    }

    public void UpdateHealth(GameObject target, int health)
    {
        Health healthTarget = target.GetComponent<Health>();

        if (healthTarget == null) return;

        healthTarget.currentHealth += health;

        HealthBarUpdate();
    }

    public void MaxHeal(GameObject target)
    {
        Health healthTarget = target.GetComponent<Health>();

        if (healthTarget == null) return;

        healthTarget.currentHealth = maxHealth;

        HealthBarUpdate();
    }

    public void HealthBarUpdate()
    {
        for (int i = 0; i <= maxHealth; i++)
        {
            quartHearts[i].SetActive(false);
        }

        for (int i = 0; i <= currentHealth; i++)
        {
            quartHearts[i].SetActive(true);
        }
    }
}
