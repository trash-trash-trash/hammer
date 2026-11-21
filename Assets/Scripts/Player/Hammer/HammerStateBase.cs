using UnityEngine;

public class HammerStateBase : MonoBehaviour
{
    public HammerBrain hammerBrain;
    
    public virtual void OnEnable()
    {
        hammerBrain = GetComponentInParent<HammerBrain>();
    }
}
