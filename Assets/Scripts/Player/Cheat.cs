using UnityEngine;
using UnityEngine.InputSystem;

public class Cheat : MonoBehaviour
{
    private PlayerSystemAction inputActions;
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();

        inputActions = new PlayerSystemAction();
        inputActions.Enable();

        inputActions.Cheat.MakeDamage.started += OnMakeDamage;
        inputActions.Cheat.Heal.started += OnHeal;
        inputActions.Cheat.FullHeal.started += OnFullHeal;
    }

    void OnMakeDamage(InputAction.CallbackContext context)
    {
        health.UpdateHealth(health.currentHealth > 0 ? health.currentHealth - 1 : 0);
    }

    void OnHeal(InputAction.CallbackContext context)
    {
        health.UpdateHealth(health.currentHealth < health.maxHealth ? health.currentHealth + 1 : health.maxHealth);
    }

    void OnFullHeal(InputAction.CallbackContext context)
    {
        health.UpdateHealth(health.maxHealth);
    }
}
