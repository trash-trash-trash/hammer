using UnityEngine;

public class NPCStateBase : MonoBehaviour
{
   public NPCBrain brain;

   public virtual void OnEnable()
   {
      brain = GetComponentInParent<NPCBrain>();
   }
}
