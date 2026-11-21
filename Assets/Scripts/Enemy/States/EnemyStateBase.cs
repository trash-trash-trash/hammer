using UnityEngine;

public class EnemyStateBase : MonoBehaviour
{
    public EnemyBrain enemyBrain;
    
    public virtual void OnEnable()
    {
        enemyBrain = GetComponentInParent<EnemyBrain>();
    }
}
