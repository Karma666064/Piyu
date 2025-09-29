using System.Collections;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float speed = 9;
    [HideInInspector] public float lifeTime;
    [HideInInspector] public bool isFacingRight;

    void Start()
    {
        StartCoroutine(DespawnBullet(lifeTime));
    }

    private void Update()
    {
        Vector3 direction = isFacingRight ? Vector3.right : Vector3.left;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    IEnumerator DespawnBullet(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
