using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRadius = 2f;
    public float attackDuration = 0.5f;
    public LayerMask attackLayers;

    public float explosionForce = 500f;
    public float explosionRadius = 5f;
    public float upwardsModifier = 1f;

    public int attackDamage = 0;

    public List<GameObject> hitObjects = new List<GameObject>();
    
    public void Attack()
    {
        Vector3 attackPosition = attackPoint.position;
        StartCoroutine(AttackRoutine(attackPosition));
    }

    private IEnumerator AttackRoutine(Vector3 attackPosition)
    {
        float timer = 0f;

        while (timer < attackDuration)
        {
            Collider[] hits = Physics.OverlapSphere(
                attackPosition,
                attackRadius,
                attackLayers
            );

            foreach (Collider hit in hits)
            {
                if (hitObjects.Contains(hit.gameObject))
                    continue;
                    
                hitObjects.Add(hit.gameObject);
                
                Debug.Log("Hit: " + hit.name);

                Health health = hit.gameObject.GetComponent<Health>();
                if(health != null)
                    health.ChangeHP(attackDamage);

                Rigidbody rb = hit.attachedRigidbody;

                if (rb == null) continue;

                rb.AddExplosionForce(
                    explosionForce,
                    attackPosition,
                    explosionRadius,
                    upwardsModifier,
                    ForceMode.Impulse
                );
            }

            timer += Time.deltaTime;
            yield return null;
        }
        
        hitObjects.Clear();
    }
}