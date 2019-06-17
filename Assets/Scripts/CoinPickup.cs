using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;
    [SerializeField] int pointforCoins = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position);
        Destroy(gameObject);
        GameManager.instance.AddScore(pointforCoins);
    }
}
