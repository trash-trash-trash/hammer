using System.Collections;
using UnityEngine;

public class CoinTimer : MonoBehaviour
{
    public float timeToBeAlive = 5f;

    public void OnEnable()
    {
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        yield return new WaitForSeconds(timeToBeAlive);
        
        Destroy(gameObject);
    }
}
