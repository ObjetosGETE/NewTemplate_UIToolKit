using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Tweens
{
    /// <summary>
    /// Class used to fade in or out CanvasGroup components.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupFader : MonoBehaviour
    {
        /// <summary>
        /// If this box is checked, the game object will begin play visible.
        /// </summary>
        public bool activeOnStart;

        /// <summary>
        /// Events to play after the fade in is completed.
        /// </summary>
        public UnityEvent onFadeInComplete;
        /// <summary>
        /// Events to play after the fade out is completed.
        /// </summary>
        public UnityEvent onFadeOutComplete;
    
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = activeOnStart ? 1 : 0;
            _canvasGroup.interactable = activeOnStart;
            _canvasGroup.blocksRaycasts = activeOnStart;
        }

        /// <summary>
        /// Fades in the object in fixed time (0.5 seconds).
        /// </summary>
        [ContextMenu("FadeIn")]
        public void FadeIn()
        {
            _canvasGroup.DOFade(1f,0.5f).OnComplete(OnCompleteFadeIn);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        /// <summary>
        /// Fades in the object in the time specified.
        /// </summary>
        /// <param name="seconds">The time to fade in</param>
        public void FadeIn(float seconds)
        {
            _canvasGroup.DOFade(1f,seconds).OnComplete(OnCompleteFadeIn);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    
        /// <summary>
        /// Fades out the object in fixed time (0.5 seconds).
        /// </summary>
        [ContextMenu("FadeOut")]
        public void FadeOut()
        {
            _canvasGroup.DOFade(0f,0.5f).OnComplete(OnCompleteFadeOut);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        /// <summary>
        /// Fades in the object in the time specified.
        /// </summary>
        /// <param name="seconds">The time to fade out</param>
        public void FadeOut(float seconds)
        {
            _canvasGroup.DOFade(0f,seconds).OnComplete(OnCompleteFadeOut);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void OnCompleteFadeIn()
        {
            onFadeInComplete?.Invoke();
        }
        private void OnCompleteFadeOut()
        {
            onFadeOutComplete?.Invoke();
        }
    }
}
