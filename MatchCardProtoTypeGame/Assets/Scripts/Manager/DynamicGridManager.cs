using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MatchCard
{
    public class DynamicGridManager : MonoBehaviour
    {
        public GridLayoutGroup gridLayout;
        public float spacing = 5f;

        void Awake()
        {
            if (gridLayout == null)
                gridLayout = GetComponent<GridLayoutGroup>();
        }


        public void SetupGrid(int cardCount)
        {
            int rows = Mathf.FloorToInt(Mathf.Sqrt(cardCount));
            int columns = Mathf.CeilToInt((float)cardCount / rows);

            RectTransform rt = gridLayout.GetComponent<RectTransform>();

            float totalWidth = rt.rect.width - gridLayout.padding.left - gridLayout.padding.right - spacing * (columns - 1);
            float totalHeight = rt.rect.height - gridLayout.padding.top - gridLayout.padding.bottom - spacing * (rows - 1);

            float cellWidth = totalWidth / columns;
            float cellHeight = totalHeight / rows;

            gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
            gridLayout.spacing = new Vector2(spacing, spacing);
        }

    }

}

