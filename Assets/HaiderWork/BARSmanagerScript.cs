using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class BARSmanagerScript : MonoBehaviour
{
    public Image HealthBar;
    public Image OxygenBar;
    public float HealthBarAmount = 100f;
    public float OxygenBarAmount = 100f;

    public float HungerTimeToDie = 300f;
    public float OxygenTimeToDie = 60f;
    private float damagePerSecondHunger; 
    private float damagePerSecondOxygen;

    void Start()
    {
        // How much damage happens every second
        damagePerSecondHunger = 100f / HungerTimeToDie;
        damagePerSecondOxygen = 100f / OxygenTimeToDie;
    }

    void Update()
    {
        // Take damage over time
        HealthBarAmount -= damagePerSecondHunger * Time.deltaTime;
        OxygenBarAmount -= damagePerSecondOxygen * Time.deltaTime;

        // Clamp health between 0 and 100
        HealthBarAmount = Mathf.Clamp(HealthBarAmount, 0, 100);
        OxygenBarAmount = Mathf.Clamp(OxygenBarAmount, 0, 100);

        // Update the bar fill
        HealthBar.fillAmount = HealthBarAmount / 100f;
        OxygenBar.fillAmount = OxygenBarAmount / 100f;

        // If health is 0, reload the current scene
        if (HealthBarAmount <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (OxygenBarAmount <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float damage)
    {
        HealthBarAmount -= damage;
        HealthBarAmount = Mathf.Clamp(HealthBarAmount, 0, 100);
        HealthBar.fillAmount = HealthBarAmount / 100f;

        OxygenBarAmount -= damage;
        OxygenBarAmount = Mathf.Clamp(OxygenBarAmount, 0, 100);
        OxygenBar.fillAmount = OxygenBarAmount / 100f;
    }

    public void Heal(float healAmount)
    {
        HealthBarAmount += healAmount;
        HealthBarAmount = Mathf.Clamp(HealthBarAmount, 0, 100);
        HealthBar.fillAmount = HealthBarAmount / 100f;

        OxygenBarAmount += healAmount;
        OxygenBarAmount = Mathf.Clamp(OxygenBarAmount, 0, 100);
        OxygenBar.fillAmount = OxygenBarAmount / 100f;
    }
}
