using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject healthBar;
    public Canvas canvas;
    private Camera cam;

    public int maxHealth;
    public int currentHealth;

    [SerializeField] private List<GameObject> quartHearts = new List<GameObject>();

    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private GameObject heartContainerPrefab;
    [SerializeField] private GameObject quartHeartPrefab;


    private void Start()
    {
        cam = Camera.main;

        currentHealth = maxHealth;

        if (gameObject.CompareTag("Enemy"))
        {
            healthBar = Instantiate(healthBarPrefab, canvas.transform);
            healthBar.transform.localScale = new Vector3(0.35f, 0.35f, 1f);
        }

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

    private void Update()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            Vector3 screenPos = cam.WorldToScreenPoint(transform.position + Vector3.up * 1.1f);
            healthBar.transform.position = screenPos;
        }
    }

    public void UpdateHealth(int health)
    {
        currentHealth += health;

        if (gameObject.CompareTag("Enemy") && currentHealth <= 0)
        {
            Destroy(gameObject);
            Destroy(healthBar);
        }

        HealthBarUpdate();
    }

    public void MaxHeal()
    {
        currentHealth = maxHealth;

        HealthBarUpdate();
    }

    public void HealthBarUpdate()
    {
        for (int i = 0; i < quartHearts.Count; i++)
        {
            quartHearts[i].SetActive(i < currentHealth);
        }
    }
}
