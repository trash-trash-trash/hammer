using System.Collections;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float spinPower = 5f;

    public Rigidbody rb;

    public float waitTime = 0.05f;

    private void OnEnable()
    {
        StartCoroutine(WaitThenSpin());
    }

    IEnumerator WaitThenSpin()
    {
        yield return new WaitForSeconds(waitTime);
        SpinCoin();
    }

    void SpinCoin()
    {
        int axis = Random.Range(0, 2);
        int direction = Random.Range(0, 2) == 0 ? -1 : 1;

        Vector3 spinAxis = axis == 0 ? Vector3.right : Vector3.forward;

        rb.AddTorque(spinAxis * spinPower * direction, ForceMode.Impulse);
    }
}