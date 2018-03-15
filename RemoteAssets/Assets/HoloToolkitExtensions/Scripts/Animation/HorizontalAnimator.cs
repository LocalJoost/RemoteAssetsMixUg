using UnityEngine;

namespace HoloToolkitExtensions.Animation
{
    public class HorizontalAnimator : MonoBehaviour
    {
        public float SpinTime = 2.5f;

        public bool AbsoluteAxis = false;

        private void Start()
        {
            if (AbsoluteAxis)
            {
                LeanTween.rotateAround(gameObject, Vector3.up, 360, SpinTime).setLoopClamp();
            }
            else
            {
                var rotation = Quaternion.AngleAxis(360f / 3.0f, Vector3.up).eulerAngles;
                LeanTween.rotateLocal(gameObject, rotation, SpinTime / 3.0f).setLoopClamp();
            }
        }

        private void OnEnable()
        {
            LeanTween.resume(gameObject);
        }

        private void OnDisable()
        {
            LeanTween.pause(gameObject);
        }
    }
}
