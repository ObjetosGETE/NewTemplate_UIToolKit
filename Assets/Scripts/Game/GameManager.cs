using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
        
        public void LoadScene(int sceneId)
        {
            SceneManager.LoadScene(sceneId);
        }
        
        public void Pause()
        {
            Time.timeScale = 0f;
        }
        
        public void Unpause()
        {
            Time.timeScale = 1f;
        }
    }
    
}
