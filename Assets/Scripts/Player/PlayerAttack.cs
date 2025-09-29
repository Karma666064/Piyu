using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private PlayerSystemAction inputActions;
    public RandomSelector randomSelector;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private bool isAttacking;

    [SerializeField] private float bulletLifeTime;

    [SerializeField] private List<int> damages = new List<int>() { 1, 2, 6 };
    [SerializeField] private List<int> damageProbabilities = new List<int>() { 60, 30, 10 };

    void Awake()
    {
        randomSelector = new RandomSelector();
        inputActions = new PlayerSystemAction();
        inputActions.Enable();

        inputActions.Player.Attack.started += OnAttack;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        int damage = randomSelector.ChooseWithWeights<int>(damages, damageProbabilities);

        if (!isAttacking)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<TriggerDamage>().damageToMake = damage;
            bullet.GetComponent<TriggerDamage>().shooter = gameObject;
            bullet.GetComponent<BulletMovement>().lifeTime = bulletLifeTime;
            bullet.GetComponent<BulletMovement>().isFacingRight = gameObject.GetComponent<PlayerMove>().GetIsFacingRight();
            
            StartCoroutine(BulletLife(bulletLifeTime));
        }
    }

    IEnumerator BulletLife(float lifeTime)
    {
        isAttacking = true;
        yield return new WaitForSeconds(lifeTime - 1.5f);
        isAttacking = false;
    }
}
/*
 * Things to Add
 * - The L1 to add for Attack button
*/