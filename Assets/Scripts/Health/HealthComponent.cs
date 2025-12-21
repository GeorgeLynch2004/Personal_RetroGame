using Interactables.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour, IHealthModifier, IDestroyable
{
    [Header("Events")]
    [SerializeField] private UnityEvent onDamaged;
    [SerializeField] private UnityEvent onHealed;
    [SerializeField] private UnityEvent onKilled;
    [Header("Values")] 
    [SerializeField] private float minHealth = 0f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField, Range(0f, 100f)] private float currentHealth = 100f;
    
    public void ModifyHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth += amount, minHealth, maxHealth);
        if (amount < 0)
            onDamaged.Invoke();
        else if (amount > 0)
            onHealed.Invoke();
        
        
        if (currentHealth == 0)
            onKilled.Invoke();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
