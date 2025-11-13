using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MetalDetection : MonoBehaviour
{
    public float detectionRange = 10f;
    public GameObject indicatorIcon; 
    public float flashInterval = 0.2f;

    private Coroutine flashCoroutine;
    private bool isFlashing = false;
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Metal");
        bool enemyInRange = false;

        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < detectionRange)
            {
                enemyInRange = true;
                break; // Found an enemy in range, no need to check others
            }
        }

        if (enemyInRange && !isFlashing)
        {
            flashCoroutine = StartCoroutine(FlashIcon());
            isFlashing = true;
        }
        else if (!enemyInRange && isFlashing)
        {
            StopCoroutine(flashCoroutine);
            indicatorIcon.SetActive(true); 
            isFlashing = false;
        }
    }

    IEnumerator FlashIcon()
    {
        while (true)
        {
            indicatorIcon.SetActive(true);
            yield return new WaitForSeconds(flashInterval);
            indicatorIcon.SetActive(false);
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
