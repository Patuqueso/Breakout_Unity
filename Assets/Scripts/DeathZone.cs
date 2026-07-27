using UnityEngine;
using System;
public class DeathZone : MonoBehaviour
{
    public static event Action OnBallLost;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            OnBallLost?.Invoke();
        }
    }
}
