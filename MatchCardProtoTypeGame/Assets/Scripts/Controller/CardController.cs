
using UnityEngine;
namespace MatchCard
{
    public class CardController
    {
        public CardData model;
        public CardView view;
        public bool IsBusy { get; private set; }
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
            view.ShowFrontAnimated();
            view.ShowFront();
            OnSelected?.Invoke(this);
        }
       

        public void FlipUp()
        {
            if (model.isMatched || model.isFlipped || IsBusy) return;
            IsBusy = true;
            view.ShowFront(() => { IsBusy = false; }); // callback after flip anim
            model.isFlipped = true;
        }
        public void FlipDown()
        {
            model.isFlipped = false;
            view.ShowBackAnimated();
            view.ShowBack();
        }

        public void SetMatched()
        {
            model.isMatched = true;
            view.PlayMatchAnimation();
        }
    }

}
