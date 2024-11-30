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
        private SlotIcon _finalTarget;

        [SerializeField] private SlotIcon[] realIcons;
        [SerializeField] private SlotIcon ghostIconToLoop;
        [SerializeField] private float timeToStop;

        [SerializeField] private float scrollSpeed;
        [SerializeField] private Transform comparisonToReset;
        [SerializeField] private Transform middlePoint;
        Vector3 startPosition;

        private void Start()
        {
            rowStopped = true;
            startPosition = transform.position;
            //GameControl.startButtonPressed += StartRotating;
        }
        public void StartRotating()
        {
            stoppedSlot = "";
            rowStopped = false;
            //StopAllCoroutines();
           //StartCoroutine(Rotate());
        }

        private void FixedUpdate()
        {
            if(!rowStopped)
            {
                if (ghostIconToLoop.transform.position.y <= comparisonToReset.position.y)
                {
                    transform.position = startPosition;
                }
                else
                {
                    transform.position = new Vector3(transform.position.x,
                        transform.position.y + scrollSpeed * Time.deltaTime, transform.position.z);
                    if(_goingToStop)
                    {
                        if (_finalTarget.transform.position.y <= middlePoint.position.y)
                        {
                            rowStopped = true;
                            _goingToStop = false;
                        }
                    }
                }
            }
        }

        public void PrepareToStop()
        {
            _goingToStop = true;
            _finalTarget = GetClosestHigherIconFromCenter();
        }


        private SlotIcon GetClosestHigherIconFromCenter()
        {
            SlotIcon closestIcon = realIcons[0];
            float closestDistance = Vector3.Distance(closestIcon.transform.position, middlePoint.position); //  closestIcon.transform.position.y - middlePoint.position.y;
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

        }
    }