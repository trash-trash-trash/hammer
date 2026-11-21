using UnityEngine;

public class PlayerCoinToss : MonoBehaviour
{
    public CoinTracker coinTracker;
    public PlayerInputs playerInputs;
    public Transform coinOriginTransform;
    public GameObject coinPrefab;

    public float tossVertPower = 5f;
    public float tossHorizPower = 5f;

    public float verticalOffset = 5f;

    public Vector2 tossDirection;

    public Vector2 lastHeldDirection;

    public bool canToss = true;
    
    void OnEnable()
    {
        playerInputs.AnnounceLeftClick += AttemptToTossCoin;
    }

    void Update()
    {
        tossDirection = playerInputs.moveInput;
        
        if(tossDirection!=Vector2.zero)
            lastHeldDirection = playerInputs.moveInput;
    }

    public void FlipCanToss(bool input)
    {
        canToss = input;
    }
    
    private void AttemptToTossCoin(bool input)
    {
        if (!input || coinTracker.currentNumberOfCoins <= 0 || !canToss)
            return;

        GameObject coin = Instantiate(coinPrefab,
            new Vector3(coinOriginTransform.position.x, coinOriginTransform.position.y + verticalOffset,
                coinOriginTransform.position.z),
            Quaternion.identity);
        
        Rigidbody rb = coin.GetComponent<Rigidbody>();

        Vector2 inputDirection = playerInputs.moveInput;

        if (inputDirection != Vector2.zero)
            lastHeldDirection = inputDirection;
        else
            inputDirection = lastHeldDirection;

        Vector3 tossDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);

        if (tossDirection.sqrMagnitude == 0f)
            tossDirection = coinOriginTransform.forward;

        tossDirection.Normalize();

        Vector3 tossVelocity = tossDirection * tossHorizPower;
        tossVelocity.y = tossVertPower;

        Debug.DrawRay(
            coinOriginTransform.transform.position,
            tossDirection * 5f,
            Color.red,
            5f
        );

        rb.linearVelocity = tossVelocity;
        
        coinTracker.UseCoin();
    }

    void OnDisable()
    {
        playerInputs.AnnounceLeftClick -= AttemptToTossCoin;
    }
}