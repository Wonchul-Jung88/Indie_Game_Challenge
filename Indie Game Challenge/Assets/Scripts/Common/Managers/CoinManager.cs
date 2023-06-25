using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; } // Singleton instance

    public int CoinsCollected { get; private set; } // The number of coins the player has collected

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // Destroy if more than one instance exists
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the gameObject alive between scenes

            // Initialize CoinsCollected with the value from PlayerPrefs
            // If no data exists, default to 0
            CoinsCollected = PlayerPrefs.GetInt("CoinsCollected", 0);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        LoadCoinsCollected(); // Load the number of coins collected from the last session
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0) // Check if the loaded scene is the scene with index 0
        {
            SaveCoinsCollected();
        }
    }

    // Call this method to increase the coin count
    public void AddCoin()
    {
        CoinsCollected++;
        // Save the updated coin count to PlayerPrefs
        PlayerPrefs.SetInt("CoinsCollected", CoinsCollected);
        PlayerPrefs.Save();

        Debug.Log("Coin Count = " + CoinsCollected);
    }

    public void SaveCoinsCollected()
    {
        PlayerPrefs.SetInt("CoinsCollected", CoinsCollected);
    }

    public void LoadCoinsCollected()
    {
        CoinsCollected = PlayerPrefs.GetInt("CoinsCollected", 0);
    }
}
