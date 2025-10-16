using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace MatchCard
{
   
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform _gridParent;
        [SerializeField] private CardView _cardPrefab;
        [SerializeField] private Sprite[] _cardSprites;
        [SerializeField] private DynamicGridManager _dynamicGridManager;

        private List<CardController> _cards = new List<CardController>();
        private CardController _firstCard, _secondCard;
        public event System.Action OnGameWonEvent;
        private bool _isPreviewing = true;
        private int score;
        private int turns;
        public int Score => score;
        public int Turns => turns;  
        // Start is called before the first frame update

        public void StartGame()
        {
            LoadGameData();
            _cards.Clear();
            CreateCards();
            StartCoroutine(PreviewAndShuffleRoutine(0.5f));
            UIManager.OncardFlip?.Invoke(score);
        }

        public void ClearCards()
        {
            foreach (Transform child in _gridParent)
                Destroy(child.gameObject);
            _cards.Clear();
        }

        private void LoadGameData()
        {
            GameData data = GameDataSaveManager.Load();
            score = data.score;
            Debug.Log($"Loaded Score: {score}, Turns: {turns}");
        }

        private void SaveGameData()
        {
            GameData data = new GameData { score = score };
            GameDataSaveManager.Save(data);
        }
        void CreateCards()
        {
            int[] possibleCounts = { 4, 6, 12 };
            int cardCount = possibleCounts[Random.Range(0, possibleCounts.Length)];

            List<CardData> dataList = new List<CardData>();
            int pairsNeeded = cardCount / 2;

            List<Sprite> shuffledSprites = new List<Sprite>(_cardSprites);
            ShuffleUtility.Shuffle(shuffledSprites);

            for (int i = 0; i < pairsNeeded; i++)
            {
                Sprite sprite = shuffledSprites[i];
                dataList.Add(new CardData(i, sprite));
                dataList.Add(new CardData(i, sprite));
            }
            // Setup grid based on card count
            _dynamicGridManager.SetupGrid(dataList.Count);
            ShuffleUtility.Shuffle(dataList);
            // Spawn cards
            foreach (var data in dataList)
            {
                CardView view = Instantiate(_cardPrefab, _dynamicGridManager.transform);
                CardController controller = new CardController(data, view);
                controller.OnSelected += OnCardSelected;
                _cards.Add(controller);
            }
        }
        private IEnumerator PreviewAndShuffleRoutine(float previewTime)
        {
            
            foreach (var card in _cards)
            {
                Debug.Log("Show front");
                card.view.ShowFront();
                card.view.SetButtonInteractability(false);
            }

            yield return new WaitForSeconds(previewTime);

            foreach (var card in _cards)
            {
                card.FlipDown();
                card.view.SetButtonInteractability(true);
            }

            ShuffleCards();

            _isPreviewing = false;
        }

        private void ShuffleCards()
        {
            ShuffleUtility.Shuffle(_cards);
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].view.transform.SetSiblingIndex(i);
            }
        }

       

        private void OnCardSelected(CardController selected)
        {
            if (_isPreviewing) return;
            if (_firstCard == null)
            {
                _firstCard = selected;
            }
            else if (_secondCard == null)
            {
                _secondCard = selected;
                StartCoroutine(CheckMatch());
            }
        }

        private IEnumerator CheckMatch()
        {
            yield return new WaitForSeconds(0.2f);

            if (_firstCard.model.id == _secondCard.model.id)
            {
                _firstCard.SetMatched();
                _secondCard.SetMatched();
                score += 10;
                UIManager.OncardFlip?.Invoke(score);
                SaveGameData();
            }
            else
            {
                _firstCard.FlipDown();
                _secondCard.FlipDown();
            }

            _firstCard = null;
            _secondCard = null;
           
            if (_cards.TrueForAll(c => c.model.isMatched))
            {
                Debug.Log("You Won!");
                OnGameWonEvent?.Invoke();
            }

        }
    }
}
