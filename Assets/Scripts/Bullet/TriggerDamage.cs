using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    public int damageToMake;
    public GameObject shooter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && shooter.CompareTag("Player"))
        {
            //Debug.Log("Enemy touch!");
            collision.gameObject.GetComponent<Health>().UpdateHealth(-damageToMake);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player") && shooter.CompareTag("Enemy"))
        {
            Debug.Log("Player touch!");
            collision.gameObject.GetComponent<Health>().UpdateHealth(-damageToMake);
            Destroy(gameObject);
        }
    }
}
