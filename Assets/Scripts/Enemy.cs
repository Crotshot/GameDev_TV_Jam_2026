using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 10f;
    private bool isAlive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    private void Die()
    {
        isAlive = false;
        gameObject.SetActive(false);
    }

    public void SetIsAlive(bool isAlive)
    {
        this.isAlive = isAlive;
    } 

    public bool GetIsAlive()
    {
        return isAlive;
    }
}
