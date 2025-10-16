
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using System.Collections;
namespace MatchCard
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private  GameObject _menuPage;
        [SerializeField] private GameObject _gameplayPage;
        [SerializeField] private GameObject _gameWonPage;
        [SerializeField] private GameObject _gameOverPage;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _reloadButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private TMP_Text _scoreTxt;
        [SerializeField] private TMP_Text _timerText;

        public static Action<int> OncardFlip;
        void Start()
        {
            ShowMenu();

            _playButton.onClick.AddListener(StartGame);
            _reloadButton.onClick.AddListener(ReloadGame);
            _restartButton.onClick.AddListener(ReloadGame);
            OncardFlip += UpdateScore;
            GameManager.OnGameOver += ShowGameOver;
            GameManager.OnTimeChanged += UpdateTimer;
            // Subscribe to GameWon event
            _gameManager.OnGameWonEvent += ShowGameWon;
        }
        public void UpdateTimer(float remainingTime)
        {
            if (_timerText != null)
                _timerText.text = $"Time: {remainingTime:F1}s";
        }

        private void UpdateScore(int s)
        {

            _scoreTxt.text = "Score: " + s.ToString();
        }
        void ShowMenu()
        {
            _menuPage.SetActive(true);
            _gameplayPage.SetActive(false);
            _gameWonPage.SetActive(false);
            _gameOverPage.SetActive(false);
        }

        void ShowGameplay()
        {
            _menuPage.SetActive(false);
            _gameplayPage.SetActive(true);
            _gameWonPage.SetActive(false);
            _gameOverPage.SetActive(false);
        }
        void ShowGameOver()
        {
            StartCoroutine(GameOver());
            _menuPage.SetActive(false);
            _gameWonPage.SetActive(false);
           
        }
        void ShowGameWon()
        {
            StartCoroutine(GameWin());
            _menuPage.SetActive(false);
            _gameOverPage.SetActive(false);
          
           
        }

        private IEnumerator GameWin()
        {
            yield return new WaitForSeconds(1.5f);
            Debug.Log("Coroutine called");
            SoundManager.Instance.PlayGameWinSound();
            _gameplayPage.SetActive(false);
            _gameWonPage.SetActive(true);
        }
        private IEnumerator GameOver()
        {
            yield return new WaitForSeconds(1.5f);
            Debug.Log("Coroutine called");
            SoundManager.Instance.PlayGameOverSound();
            _gameplayPage.SetActive(false);
            _gameOverPage.SetActive(true);
        }
        public void StartGame()
        {
            ShowGameplay();
            _gameManager.StartGame();
        }

        public void ReloadGame()
        {
            _gameManager.ClearCards();
            _gameManager.StartGame();
            _gameplayPage.SetActive(true);
            _gameWonPage.SetActive(false);
            _gameOverPage.SetActive(false);
        }

        private void OnDestroy()
        {
            OncardFlip -= UpdateScore;
            GameManager.OnGameOver -= ShowGameOver;
            GameManager.OnTimeChanged -= UpdateTimer;
            _gameManager.OnGameWonEvent -= ShowGameWon;
            _playButton.onClick.RemoveListener(StartGame);
            _reloadButton.onClick.RemoveListener(ReloadGame);
            _restartButton.onClick.RemoveListener(ReloadGame);
        }
    }

}
