using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int spikeDamage;
    [SerializeField] private int healthItemValue;
    [SerializeField] private HealthController _healthController;
    public bool IsInvincible;
    public float InvincibleDuration = 3f;

    void Start()
    {
        IsInvincible = false;

        if (IsInvincible == false)
        {
            Debug.Log("IsInvincible = false");
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("invincibilityItem"))
        {
            StartCoroutine("GetInvincible");
        }

        if (collision.CompareTag("Obstacle") && IsInvincible == false)
        { 
            Damage();
        }

        else if (collision.CompareTag("healthItem"))
        {
            Debug.Log("healthIteem collision");
            AddHealth();
        }
    }

    public void Damage()
    {   
        {
            _healthController.playerHealth = _healthController.playerHealth - spikeDamage;
            _healthController.UpdateHealth();

            if (_healthController.playerHealth <= 0)
            {
                _healthController.Die();
            }
        }
    }

    private void AddHealth()
    {
        _healthController.playerHealth = _healthController.playerHealth + healthItemValue;
        _healthController.UpdateHealth();
    }

    IEnumerator GetInvincible()
    {
        IsInvincible = true;

        yield return new WaitForSeconds(InvincibleDuration);

        IsInvincible = false;
    }
}