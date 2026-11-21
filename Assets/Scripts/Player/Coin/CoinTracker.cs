using System;
using UnityEngine;

public class CoinTracker : MonoBehaviour
{
   public int currentNumberOfCoins;

   public int maxCoins = 99;

   public event Action<int> AnnounceCoinCount;
   
   void Start()
   {
      AddCoin(99);
   }

   public void AddCoin(int amount)
   {
      currentNumberOfCoins += amount;
      currentNumberOfCoins = Mathf.Clamp(currentNumberOfCoins, 0, maxCoins);
      AnnounceCoinCount?.Invoke(currentNumberOfCoins);
   }

   public void UseCoin()
   {
      currentNumberOfCoins--;
      currentNumberOfCoins = Mathf.Clamp(currentNumberOfCoins, 0, maxCoins);
      AnnounceCoinCount?.Invoke(currentNumberOfCoins);
   }
}
