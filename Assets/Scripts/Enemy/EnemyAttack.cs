using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    OrientationEntity orientationEntity;
    RandomSelector randomSelector;

    Collider2D isPlayerTargeted;

    [SerializeField] private Transform detectorObj;
    [SerializeField] private float detectorRadius = 6.5f;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private float enemyAttackTime = 1f;

    bool isTargeted = true;
    public bool isFacingRight = true;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private List<int> damages = new List<int>() { 1, 2, 3 };
    [SerializeField] private List<int> damageProbabilities = new List<int>() { 50, 30, 20 };
    [SerializeField] private float bulletLifeTime = 2f;

    public enum EnemyType { Swordman, Gunner, Turret };
    public EnemyType enemyType;

    private void Start()
    {
        orientationEntity = GetComponent<OrientationEntity>();
        randomSelector = new RandomSelector();
    }

    private void Update()
    {
        // For know when the player is targeted for attack him
        isPlayerTargeted = Physics2D.OverlapCircle(detectorObj.position, detectorRadius, playerLayer);

        if (isPlayerTargeted && isTargeted)
        {
            isTargeted = false;
            StartCoroutine(Attack());
        }
        else if (!isPlayerTargeted) isTargeted = true;
    }

    public IEnumerator Attack()
    {
        while (isPlayerTargeted)
        {
            switch (enemyType)
            {
                case EnemyType.Swordman:
                    Debug.Log("Swordman Attack");

                    break;
                case EnemyType.Turret:
                    Debug.Log("Turret Attack");

                    break;
                case EnemyType.Gunner:
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    bullet.GetComponent<TriggerDamage>().damageToMake = randomSelector.ChooseWithWeights<int>(damages, damageProbabilities);
                    bullet.GetComponent<TriggerDamage>().shooter = gameObject;
                    bullet.GetComponent<BulletMovement>().lifeTime = bulletLifeTime;
                    bullet.GetComponent<BulletMovement>().isFacingRight = orientationEntity.isFacingRight;

                    break;
                default:
                    break;
            }
            
            yield return new WaitForSeconds(enemyAttackTime);
        }
    }

    public Collider2D GetIsPlayerTargeted()
    {
        return isPlayerTargeted;
    }

    void OnDrawGizmosSelected()
    {
        if (detectorObj != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectorObj.position, detectorRadius);
        }
    }
}
