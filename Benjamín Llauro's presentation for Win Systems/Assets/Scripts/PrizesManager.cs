using UnityEngine;
namespace Prizes
{
    [CreateAssetMenu(fileName = "PrizesManager", menuName = "ScriptableObjects/PrizesManager", order = 1)]
    public class PrizesManager : ScriptableObject
    {
        [SerializeField] private Prize[] prizes;
    }
}