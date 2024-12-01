using System.Collections;
using UnityEngine;

namespace SlotMachine
{
    public class Row : MonoBehaviour
    {
        private float _timeInterval;
        private bool _goingToStop = false;


        public bool rowStopped;
        public string stoppedSlot;
        private SlotIcon _finalTargetMiddle;

        private SlotIcon _finalTargetUp;
        private SlotIcon _finalTargetDown;

        [SerializeField] private SlotIcon[] realIcons;
        [SerializeField] private SlotIcon ghostIconToLoop;
        [SerializeField] private float timeToStop;

        [SerializeField] private float scrollSpeed;
        [SerializeField] private Transform comparisonToReset;
        [SerializeField] private Transform middlePoint;
        Vector3 startPosition;

        #region Unity Methods
        private void Start()
        {
            rowStopped = true;
            startPosition = transform.position;
            //GameControl.startButtonPressed += StartRotating;
        }

        private void FixedUpdate()
        {
            if (!rowStopped)
            {
                if (ghostIconToLoop.transform.position.y <= comparisonToReset.position.y)
                {
                    transform.position = startPosition;
                }
                else
                {
                    transform.position = new Vector3(transform.position.x,
                        transform.position.y + scrollSpeed * Time.deltaTime, transform.position.z);
                    if (_goingToStop)
                    {
                        if (_finalTargetMiddle.transform.position.y <= middlePoint.position.y)
                        {
                            rowStopped = true;
                            _goingToStop = false;
                        }
                    }
                }
            }
        }
        #endregion

        #region Public Methods
        public void StartRotating()
        {
            stoppedSlot = "";
            rowStopped = false;
        }
        public void PrepareToStop()
        {
            _goingToStop = true;
            _finalTargetMiddle = GetClosestHigherIconFromCenter();
            _finalTargetUp = GetUpSlot(_finalTargetMiddle);
            _finalTargetDown = GetDownSlot(_finalTargetMiddle);

        }
        public SlotIcon GetUpIconResult() {  return _finalTargetUp; }
        public SlotIcon GetMiddleIconResult() { return _finalTargetMiddle; }
        public SlotIcon GetDownIconResult() { return _finalTargetDown; }

        #endregion

        #region Private Methods
        private SlotIcon GetClosestHigherIconFromCenter()
        {
            SlotIcon closestIcon = realIcons[0];
            float closestDistance = Vector3.Distance(closestIcon.transform.position, middlePoint.position);
            float currentDistance;
            for(int i = 0; i < realIcons.Length; i++)
            {
                currentDistance = Vector3.Distance(realIcons[i].transform.position, middlePoint.position);
                if (currentDistance < closestDistance && realIcons[i].transform.position.y > middlePoint.position.y)
                {
                    Debug.Log("ICONO MAS CERCANO: " + realIcons[i].gameObject.name + ". Posicion en Y:" + realIcons[i].transform.position.y);
                    Debug.Log("Middlepoint Y: " + middlePoint.position.y);
                    closestDistance = currentDistance;
                    closestIcon = realIcons[i];
                }
            }
            return closestIcon;
        }
        private SlotIcon GetUpSlot(SlotIcon middleSlot)
        {
            if(middleSlot == realIcons[0])
                return realIcons[realIcons.Length - 1];
            for (int i = 0; i < realIcons.Length; i++)
            {
                if (realIcons[i] == middleSlot)
                {
                    return realIcons[i - 1];
                }

            }
            return null;
        }
        private SlotIcon GetDownSlot(SlotIcon middleSlot)
        {
            if (middleSlot == realIcons[realIcons.Length - 1])
                return realIcons[0];
            for (int i = 0; i < realIcons.Length; i++)
            {
                if (realIcons[i] == middleSlot)
                {
                    return realIcons[i + 1];
                }
            }
            return null;
        }
        #endregion
    }
}