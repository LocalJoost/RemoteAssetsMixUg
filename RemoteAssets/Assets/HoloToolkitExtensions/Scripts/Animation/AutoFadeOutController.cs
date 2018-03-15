using UnityEngine;

namespace HoloToolkitExtensions.Animation
{
    public class AutoFadeOutController : MonoBehaviour
    {
        public float FadeOutTime = 0.5f;

        public float FadeOutStartDelay = 3;

        public bool DeactivateAfterFadeout = false;

        private bool _hasFadeOutStarted;

        public virtual void Start()
        {
        }

        private void OnEnable()
        {
            if (gameObject.activeSelf && !_hasFadeOutStarted)
            {
                Hide();
            }
        }

        private void FadeOut()
        {
            _hasFadeOutStarted = true;
            LeanTween.alpha(gameObject, 0, FadeOutTime).setOnComplete(() => Hide()).delay = FadeOutStartDelay; ;
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            LeanTween.alpha(gameObject, 1, 0);
            FadeOut();
        }

        public void Hide()
        {
            LeanTween.alpha(gameObject, 0, 0);
            if(DeactivateAfterFadeout && _hasFadeOutStarted)
            {
                gameObject.SetActive(false);
            }
            _hasFadeOutStarted = false;
        }
    }
}
