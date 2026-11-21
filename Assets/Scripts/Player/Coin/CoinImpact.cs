using System;
using System.Collections;
using UnityEngine;

public class CoinImpact : MonoBehaviour
{
    public float impactThreshold = 5f;
    public float sphereRadius = 5f;
    public float sphereDuration = 0.5f;
    public LayerMask hitLayers = ~0;
    public LayerMask layerToIgnore;

    public bool impacted = false;
    
    public event Action AnnounceOnImpact;
    
    void Start()
    {
        Physics.IgnoreLayerCollision(7, 9);
        Physics.IgnoreLayerCollision(9, 9);
    }

    private void OnCollisionEnter(Collision collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;

        if (impactForce < impactThreshold || impacted)
            return;

        impacted = true;
        
        Vector3 impactPosition = collision.GetContact(0).point;

        StartCoroutine(ImpactSphere(impactPosition));
    }

    private IEnumerator ImpactSphere(Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(
            position,
            sphereRadius,
            hitLayers
        );

        foreach (Collider hit in hits)
        {
            IHear hearer = hit.GetComponent<IHear>();

            if (hearer != null)
            {
                hearer.HeardNoise(position);
            }
            Debug.Log("Impact sphere hit: " + hit.name);
        }
        
        AnnounceOnImpact?.Invoke();
        
        yield return new WaitForSeconds(sphereDuration);
    }
}