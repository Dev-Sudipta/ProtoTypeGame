using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MatchCard
{
    public class DynamicGridManager : MonoBehaviour
    {
        [SerializeField] private  GridLayoutGroup _gridLayout;
        [SerializeField] private float _spacing;

        void Awake()
        {
            if (_gridLayout == null)
                _gridLayout = GetComponent<GridLayoutGroup>();
        }


        public void SetupGrid(int cardCount)
        {
            int rows = Mathf.FloorToInt(Mathf.Sqrt(cardCount));
            int columns = Mathf.CeilToInt((float)cardCount / rows);

            RectTransform rt = _gridLayout.GetComponent<RectTransform>();

            float totalWidth = rt.rect.width - _gridLayout.padding.left - _gridLayout.padding.right - _spacing * (columns - 1);
            float totalHeight = rt.rect.height - _gridLayout.padding.top - _gridLayout.padding.bottom - _spacing * (rows - 1);

            float cellWidth = totalWidth / columns;
            float cellHeight = totalHeight / rows;

            _gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
            _gridLayout.spacing = new Vector2(_spacing, _spacing);
        }

    }

}

