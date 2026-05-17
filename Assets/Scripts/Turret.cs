using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{

    //[SerializeField] GameObject 
    private GameObject primaryTarget = null;
    private Queue<GameObject> targets = new Queue<GameObject>();

    [SerializeField] public float damage = 1f;
    [SerializeField] public float range = 10f;
    [SerializeField] public float ROF = 1f;
    [SerializeField] public GameObject turret;
    [SerializeField] public int layerValue = 5;
    [SerializeField] public Collider targetRange;

    private float timeSinceLastAttack = 999;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<SphereCollider>().radius = range;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (primaryTarget == null) 
        {
            GetTarget();
            turret.transform.rotation = Quaternion.identity;
            return; 
        }

        turret.transform.LookAt(primaryTarget.transform.position);

        if(timeSinceLastAttack >= ROF)
        {
            Attack(primaryTarget);
            timeSinceLastAttack = 0;
        }
    }

    private void Attack(GameObject target)
    {
        Enemy enemyComponent = target.GetComponent<Enemy>();
        enemyComponent.TakeDamage(damage);

        if (!enemyComponent.GetIsAlive())
        {
            primaryTarget = null;
            targets.Dequeue();
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        GameObject gameObject = target.gameObject;

        if (gameObject.layer == layerValue)
        {
            targets.Enqueue(gameObject);
        }
    }

    private void OnTriggerExit(Collider target)
    {
        primaryTarget = null;
        targets.Dequeue();
    }

    private bool GetTarget()
    {
        if (targets.Any())
        {
            primaryTarget = targets.Peek();
            return true;
        }

        return false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
