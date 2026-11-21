using UnityEngine;
using System.Collections;

public class HammerSwing : MonoBehaviour
{
    public Transform hammerTransform;

    public float pullBackAngle = -60f;
    public float slamAngle = 90f;
    public float pullBackDuration = 0.3f;
    public float slamDuration = 0.1f;
    public float returnDuration = 0.2f;

    private float idleAngle;
    private Coroutine rotationCoroutine;

    private void Awake()
    {
        idleAngle = hammerTransform.localEulerAngles.z;
    }

    public void PullBack()
    {
        RotateToAngle(idleAngle + pullBackAngle, pullBackDuration);
    }

    public void SlamDown()
    {
        RotateToAngle(idleAngle + slamAngle, slamDuration);
    }

    public void ReturnToIdle()
    {
        RotateToAngle(idleAngle, returnDuration);
    }

    private void RotateToAngle(float targetAngle, float duration)
    {
        if (rotationCoroutine != null)
            StopCoroutine(rotationCoroutine);

        rotationCoroutine = StartCoroutine(RotateRoutine(targetAngle, duration));
    }

    private IEnumerator RotateRoutine(float targetAngle, float duration)
    {
        float startAngle = hammerTransform.localEulerAngles.z;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);
            float angle = Mathf.LerpAngle(startAngle, targetAngle, t);

            hammerTransform.localRotation = Quaternion.Euler(0f, 0f, angle);

            yield return null;
        }

        hammerTransform.localRotation = Quaternion.Euler(0f, 0f, targetAngle);
        rotationCoroutine = null;
    }
}