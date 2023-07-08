using UnityEngine.SceneManagement;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; } // Singleton instance

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // Unsubscribe from the sceneLoaded event
            SceneManager.sceneLoaded -= OnSceneLoaded;

            // Destroy if more than one instance exists
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the gameObject alive between scenes

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        // "CoinsCollected" is the key we're interested in
        if (PlayerPrefs.HasKey("CoinsCollected"))
        {
            // If the key exists, delete the PlayerPrefs data for that key
            PlayerPrefs.DeleteKey("CoinsCollected");
            Debug.Log("PlayerPrefs data for 'CoinsCollected' deleted.");
        }
        //else
        //{
        //    Debug.Log("No PlayerPrefs data found for 'CoinsCollected'.");
        //}
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
        GameData.Instance.CoinCount++;
    }

    public void SaveCoinsCollected()
    {
        GameData.Instance.Save();
    }
}
