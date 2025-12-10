using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Collectable : MonoBehaviour
{
    public AudioClip eatingAudio;

    private CollectableBehavior _collectableBehavior;

    private void Awake()
    {
      _collectableBehavior = GetComponent<CollectableBehavior>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<CharacterController>();

        if (player != null) 
        {
            _collectableBehavior.OnCollected(player.gameObject);
            Destroy(gameObject);

            AudioSource playerAudioSource = player.GetComponent<AudioSource>();
            if (playerAudioSource != null && eatingAudio != null)
            {
                playerAudioSource.PlayOneShot(eatingAudio);
            }

            // Disable or destroy the collectible after playing the sound
            gameObject.SetActive(false);
        }
    }
}
