using UnityEngine;
using UnityEngine.Rendering;

namespace Graphics
{
    /// <summary>
    /// This class manages the game's graphic quality and color blind profiles.
    /// </summary>
    public class VolumeManager : MonoBehaviour
    {
        public static VolumeManager Instance { get; private set; }
        
        private Volume _volume;

        public VolumeProfile[] colorblindProfiles;

        private void Start()
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
            
            _volume = GetComponent<Volume>();
        }

        /// <summary>
        /// Changes colorblind profile.
        /// </summary>
        /// <param name="id">The id of the profile to change to.</param>
        public void SetProfile(int id)
        {
            _volume.profile = colorblindProfiles[id];
        }
        
        /// <summary>
        /// Changes quality of graphics.
        /// </summary>
        /// <param name="id">The id of the profile to change to.</param>
        public static void SetQuality(int id)
        {
            QualitySettings.SetQualityLevel(id);
        }

        public void RestoreDefaultSettings()
        {
            SetProfile(0);
            SetQuality(3);
        }
    }
}
