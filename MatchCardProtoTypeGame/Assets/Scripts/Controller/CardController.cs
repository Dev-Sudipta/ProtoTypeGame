
using UnityEngine;
namespace MatchCard
{
    public class CardController
    {
        public CardData model;
        public CardView view;
        public CardController(CardData data, CardView view)
        {
            this.model = data;
            this.view = view;

            view.Initialize(model.image, OnCardClicked);
        }

        public System.Action<CardController> OnSelected;

        private void OnCardClicked()
        {
            if (model.isFlipped || model.isMatched) return;

            model.isFlipped = true;
            view.ShowFront();
            OnSelected?.Invoke(this);
        }

        public void FlipDown()
        {
            model.isFlipped = false;
            view.ShowBack();
        }

        public void SetMatched()
        {
            model.isMatched = true;
        }
    }

}
