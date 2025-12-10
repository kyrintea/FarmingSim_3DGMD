using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthCollectableBehavior : MonoBehaviour
{
    public AudioClip eatingAudio;
    private CollectableBehavior _collectableBehavior;

    private void Awake()
    {
        _collectableBehavior = GetComponent<CollectableBehavior>();
    }

    public void Collect(GameObject player)
    {
        _collectableBehavior.OnCollected(player);

        var audio = player.GetComponent<AudioSource>();
        if (audio != null && eatingAudio != null)
            audio.PlayOneShot(eatingAudio);

        Destroy(gameObject);
    }
}
