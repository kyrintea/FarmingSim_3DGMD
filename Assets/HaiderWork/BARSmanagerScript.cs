using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BARSmanagerScript : MonoBehaviour
{
    public Image HealthBar;
    public float HealthBarAmount = 100f;
    public float OxygenBarAmount = 100f;

    public float HungerTimeToDie = 300f;
    public float OxygenTimeToDie = 60f;
    private float damagePerSecond;

    void Start()
    {
        // How much damage happens every second
        damagePerSecond = 100f / HungerTimeToDie;
    }

    void Update()
    {
        // Take damage over time
        HealthBarAmount -= damagePerSecond * Time.deltaTime;

        // Clamp health between 0 and 100
        HealthBarAmount = Mathf.Clamp(HealthBarAmount, 0, 100);

        // Update the health bar fill
        HealthBar.fillAmount = HealthBarAmount / 100f;

        // If health is 0, reload the current scene
        if (HealthBarAmount <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void TakeDamage(float damage)
    {
        HealthBarAmount -= damage;
        HealthBarAmount = Mathf.Clamp(HealthBarAmount, 0, 100);
        HealthBar.fillAmount = HealthBarAmount / 100f;
    }

    public void Heal(float healAmount)
    {
        HealthBarAmount += healAmount;
        HealthBarAmount = Mathf.Clamp(HealthBarAmount, 0, 100);
        HealthBar.fillAmount = HealthBarAmount / 100f;
    }
}
