
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace MatchCard
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private  Image _frontImage;
        [SerializeField] private GameObject _front;
        [SerializeField] private GameObject _back;
        [SerializeField] private Button _button;
        [SerializeField] private float _flipDuration = 0.4f;
        [SerializeField] private float _matchScale = 1.2f;
        private bool _isAnimating = false;
        public void Initialize(Sprite image, System.Action onClick)
        {
            _frontImage.sprite = image;
            _button.onClick.AddListener(() => onClick.Invoke());
            ShowBack();
        }
        public void SetButtonInteractability(bool value)
        {
            _button.interactable = value;
        }
        public void ShowFrontAnimated()
        {
            AnimateFlip(true);
        }

        public void ShowBackAnimated()
        {
            AnimateFlip(false);
        }

        private void AnimateFlip(bool showFront)
        {
            if (_isAnimating) return;
            _isAnimating = true;

            // Shrink halfway
            transform.DOScaleX(0, _flipDuration / 2f)
                .SetEase(Ease.InQuad)
                .OnComplete(() =>
                {
                    if (showFront) ShowFront(); else ShowBack();

                    // Expand back
                    transform.DOScaleX(1, _flipDuration / 2f)
                        .SetEase(Ease.OutQuad)
                        .OnComplete(() => _isAnimating = false);
                });
        }
        public void PlayMatchAnimation()
        {
            if (_isAnimating) return;

            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale(_matchScale, 0.15f).SetEase(Ease.OutBack))
               .Append(transform.DOScale(1f, 0.15f).SetEase(Ease.InOutSine));
        }
        public void ShowFront() { _front.SetActive(true); _back.SetActive(false); }
        public void ShowBack() { _front.SetActive(false); _back.SetActive(true); }
    }
}

