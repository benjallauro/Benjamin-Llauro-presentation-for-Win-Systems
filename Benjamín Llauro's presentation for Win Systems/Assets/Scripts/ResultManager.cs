using UnityEngine;

namespace SlotMachine
{
    public class ResultManager : MonoBehaviour
    {
        private int _sameIconCount;
        private int _firstIconCategory;
        public void CheckResult(Row[] rows)
        {

            SlotIcon[] icons = new SlotIcon[rows.Length];

            for (int i = 0; i < icons.Length; i++)
            {
                icons[i] = rows[i].GetIconResult();
            }


            _sameIconCount = 0;
            _firstIconCategory = (int)icons[0].iconType;

            bool stopCombo = false;

            for(int i  = 0; i < icons.Length; i++)
            {
                if (_firstIconCategory == (int)icons[i].iconType && !stopCombo)
                    _sameIconCount++;
                else
                    stopCombo = true;
            }
            Debug.Log("COMBO: " + _sameIconCount);
        }
    }
}