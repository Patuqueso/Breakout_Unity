using UnityEngine;
using System;
public class Brick : MonoBehaviour
{
    public static event Action<int> OnBrickDestroyed;
    [SerializeField] private int points = 100;
    public void DestroyBrick()
    {
        OnBrickDestroyed?.Invoke(points);
        Destroy(gameObject);
    }


}