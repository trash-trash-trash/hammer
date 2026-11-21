using UnityEngine;

public class NailView : MonoBehaviour
{
   public Health health;
   
   public Material normalMaterial;
   
   public Material deathMaterial;
   
   public MeshRenderer meshRenderer;

   void Start()
   {
      health.AnnounceIsAlive += Ded;
   }

   private void Ded(bool obj)
   {
      if (!obj)
      {
         meshRenderer.material = deathMaterial;
      }
      else
      {
         meshRenderer.material = normalMaterial;
      }
   }

   void OnDestroy()
   {
      health.AnnounceIsAlive -= Ded;
   }
}
