using Prizes;
using UnityEngine;

namespace SlotMachine
{
    public class ResultManager : MonoBehaviour
    {
        [SerializeField] PrizesManager prizesManager;

        private int _sameIconCount;
        private int _firstIconCategory;
        public int CheckResult(Row[] rows)
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
            return CheckPrize(icons[0].iconType, _sameIconCount);
        }

        private int CheckPrize(SlotIcon.Category category, int _sameIconCount)
        {
            Prize[] allPrizes = prizesManager.GetAllPrizes();

            foreach(Prize currentPrize in allPrizes)
            {
                if (currentPrize.category == category && currentPrize.amountNeeded == _sameIconCount)
                    return currentPrize.prize;
            }
            return 0;
        }
    }
}