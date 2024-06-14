using UnityEngine;

public class GameAssets : MonoBehaviour
{
    #region Singleton

    public static GameAssets instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Game Assets found!");
            return;
        }

        instance = this;
    }

    #endregion

    public GameObject itemButtonPrefab;
}
