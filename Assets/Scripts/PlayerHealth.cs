using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty", 1);

        switch (difficulty)
        {
            case 0:
                maxHealth = 3;
                break;

            case 1:
                maxHealth = 2;
                break;

            case 2:
                maxHealth = 1;
                break;
        }

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        currentHealth -= 1;

        Debug.Log("HP: " + currentHealth);

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
