using System;
using UnityEngine;

public class CoinSound : MonoBehaviour
{
   public CoinImpact coinImpact;

   public AudioClip coinHitGroundSound;
   public AudioClip coinFlipSound;
   public AudioSource audioSource;
   
   void Start()
   {
      coinImpact.AnnounceOnImpact += PlayCoinHitGroundSound;
   }

   void OnEnable()
   {
      audioSource.PlayOneShot(coinFlipSound);
   }

   private void PlayCoinHitGroundSound()
   {
      audioSource.PlayOneShot(coinHitGroundSound);
   }

   private void OnDestroy()
   {
      coinImpact.AnnounceOnImpact -= PlayCoinHitGroundSound;
   }
}
