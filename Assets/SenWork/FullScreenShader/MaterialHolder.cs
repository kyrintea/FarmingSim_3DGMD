using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class MaterialHolder : MonoBehaviour
{
    [Header("Fullscreen Damage")]
    public ScriptableRendererFeature fullscreenDamage;
    public Material takeDamageMAT;

    public float hitDisplayTime = 0.75f;
    public float hurtFadeOutTime = 0.25f;

    private int damageIntensity = Shader.PropertyToID("_VignetteIntensity");
    public float damageIntensity_StartAmount = 0.17f;

    private Coroutine damageHolder;

    [Header("Health Pickup")]
    public ScriptableRendererFeature healthPickup;
    public Material healthPickupMAT;

    public float healthDisplayTime = 0.75f;
    public float healthFadeOutTime = 0.25f;

    private int healthIntensity = Shader.PropertyToID("_VignetteIntensity");
    public float healthIntensity_StartAmount = 0.25f;

    private Coroutine healthHolder;

    //[Header("Ammo Pickup")]
    //[Header("Dash")]

    private void Start()
    {
        fullscreenDamage.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) 
        {
          StartCoroutine(HealthPickup());
        }
    }
    private IEnumerator TakeDamage()
    {
        fullscreenDamage.SetActive(true);
        takeDamageMAT.SetFloat(damageIntensity, damageIntensity_StartAmount);

        yield return new WaitForSeconds(hitDisplayTime);

        float elapsedTime = 0f;
        while(elapsedTime < hurtFadeOutTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedVignetteIntensity = Mathf.Lerp(damageIntensity_StartAmount, 0f, (elapsedTime / hurtFadeOutTime));

            takeDamageMAT.SetFloat(damageIntensity, lerpedVignetteIntensity);

            yield return null;
        }
        fullscreenDamage.SetActive(false);
    }

    public void StartTakeDamageCoroutine()
    {
        //this buffer basically just makes it so if there are two calls to the coroutine that end up trying to adjust the values of the vignette at the same time
        //it will instead stop the current coroutine and start a new one
        if (damageHolder != null)
        {
            StopCoroutine(damageHolder);
        }

        damageHolder = StartCoroutine(TakeDamage());
    }

    private IEnumerator HealthPickup()
    {
        //print("should be working");
        healthPickup.SetActive(true);
        healthPickupMAT.SetFloat(healthIntensity, healthIntensity_StartAmount);

        yield return new WaitForSeconds(healthDisplayTime);

        float elapsedTime = 0f;
        while (elapsedTime < healthFadeOutTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedVignetteIntensity = Mathf.Lerp(healthIntensity_StartAmount, 0f, (elapsedTime / healthFadeOutTime));

            healthPickupMAT.SetFloat(healthIntensity, lerpedVignetteIntensity);
       
            yield return null;
        }
        healthPickup.SetActive(false);
    }

    public void StartHealthPickupCoroutine()
    {
        if (healthHolder != null)
        {
            StopCoroutine(healthHolder);
        }

        healthHolder = StartCoroutine(HealthPickup());
    }
}
