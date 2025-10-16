
using UnityEngine;
using UnityEngine.UI;
namespace MatchCard
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private  GameObject _menuPage;
        [SerializeField] private GameObject _gameplayPage;
        [SerializeField] private GameObject _gameWonPage;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _reloadButton;
        [SerializeField] private GameManager _gameManager;

        void Start()
        {
            ShowMenu();

            _playButton.onClick.AddListener(StartGame);
            _reloadButton.onClick.AddListener(ReloadGame);
            //restartButton.onClick.AddListener(RestartGame);

            // Subscribe to GameWon event
            _gameManager.OnGameWonEvent += ShowGameWon;
        }

        void ShowMenu()
        {
            _menuPage.SetActive(true);
            _gameplayPage.SetActive(false);
            _gameWonPage.SetActive(false);
        }

        void ShowGameplay()
        {
            _menuPage.SetActive(false);
            _gameplayPage.SetActive(true);
            _gameWonPage.SetActive(false);
        }

        void ShowGameWon()
        {
            _menuPage.SetActive(false);
            _gameplayPage.SetActive(false);
            _gameWonPage.SetActive(true);
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
        }


    }

}
