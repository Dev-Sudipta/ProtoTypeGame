
using UnityEngine;
using UnityEngine.UI;
namespace MatchCard
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private  Image _frontImage;
        [SerializeField] private GameObject _front;
        [SerializeField] private GameObject _back;
        [SerializeField] private Button _button;

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
        public void ShowFront() { _front.SetActive(true); _back.SetActive(false); }
        public void ShowBack() { _front.SetActive(false); _back.SetActive(true); }
    }
}

