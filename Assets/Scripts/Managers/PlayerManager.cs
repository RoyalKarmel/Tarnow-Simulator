using UnityEngine;

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

    [Header("Player Resource Bars")]
    public ResourceBar playerHealthBar;
    public ResourceBar playerStaminaBar;
}
