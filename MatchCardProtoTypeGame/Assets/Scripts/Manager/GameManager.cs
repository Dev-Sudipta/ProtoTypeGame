using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchCard
{
   
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private DynamicGridManager _dynamicGridManager;        // Start is called before the first frame update
        void Start()
        {
            int[] possibleCounts = { 4, 6, 30 };
            int cardCount = possibleCounts[Random.Range(0, possibleCounts.Length)];
            Debug.Log(cardCount);
            _dynamicGridManager.SetupGrid(cardCount);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
