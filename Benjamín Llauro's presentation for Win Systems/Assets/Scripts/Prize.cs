using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prizes
{
    [System.Serializable]
    public class Prize
    {
        public SlotIcon.Category category;
        public int amountNeeded;
        public int prize;
    }
}
