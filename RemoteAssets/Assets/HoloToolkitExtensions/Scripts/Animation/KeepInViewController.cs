using HoloToolkit.Unity.InputModule;
using UnityEngine;
using HoloToolkitExtensions.Utilities;

namespace HoloToolkitExtensions.Animation
{
    public class KeepInViewController : MonoBehaviour
    {

        public float MaxDistance = 2f;

        public float MoveTime = 0.8f;

        private BaseRayStabilizer _stabilizer;


        void Start()
        {
            _stabilizer = GazeManager.Instance.Stabilizer;
        }

        // Update is called once per frame
        void Update()
        {
            if ( _isBusy)
            {
                return;
            }

            _isBusy = true;
            LeanTween.moveLocal(transform.gameObject,
                   LookingDirectionHelpers.GetPostionInLookingDirection(MaxDistance, _stabilizer), MoveTime).
                setEase(LeanTweenType.easeInOutSine).
                setOnComplete(MovingDone);
        }

        private void MovingDone()
        {
            _isBusy = false;
        }

        private bool _isBusy;

    }
}