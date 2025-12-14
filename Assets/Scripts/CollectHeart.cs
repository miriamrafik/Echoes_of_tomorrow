using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectHeart : MonoBehaviour
{
    public AudioClip heartSound;

    // Start is called before life's first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerStats.lives++;
            AudioManager.Instance.PlayMusicSFX(heartSound);
            Destroy(gameObject);
        }
    }
}