using UnityEngine;
namespace MatchCard
{
    [System.Serializable]
    public class CardData
    {
        public int id;        // unique for matching
        public Sprite image;  // front face image
        public bool isMatched = false;
        public bool isFlipped = false;

        public CardData(int id, Sprite image)
        {
            this.id = id;
            this.image = image;
        }

    }
}

