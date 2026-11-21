using UnityEngine;

public class CoinTosser : MonoBehaviour
{
    public CoinTracker coinTracker;
    public Transform coinOriginTransform;
    public GameObject coinPrefab;

    public float tossVertPower = 5f;
    public float tossHorizPower = 5f;
    public float verticalOffset = 5f;

    public void AttemptToTossCoin(Vector2 inputDirection)
    {
        if (coinTracker.currentNumberOfCoins <= 0)
            return;

        Vector3 tossDirection = new Vector3(
            inputDirection.x,
            0f,
            inputDirection.y
        );

        if (tossDirection == Vector3.zero)
            return;

        tossDirection.Normalize();

        GameObject coin = Instantiate(
            coinPrefab,
            coinOriginTransform.position + Vector3.up * verticalOffset,
            Quaternion.identity
        );

        Rigidbody rb = coin.GetComponent<Rigidbody>();

        Vector3 tossVelocity = tossDirection * tossHorizPower;
        tossVelocity.y = tossVertPower;

        rb.linearVelocity = tossVelocity;

        coinTracker.UseCoin();
    }
}