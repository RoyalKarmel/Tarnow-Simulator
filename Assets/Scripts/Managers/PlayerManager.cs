using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Player Manager found!");
            return;
        }

        instance = this;
    }
    #endregion

    public GameObject player;
    public PlayerStats playerStats;

    [Header("Player Resource Bars")]
    public ResourceBar playerHealthBar;
    public ResourceBar playerStaminaBar;

    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
