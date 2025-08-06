using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public TextMeshProUGUI healthText;

    void Update()
    {
        healthText.text = "Health: " + playerHealth.currentHealth;
    }
}
