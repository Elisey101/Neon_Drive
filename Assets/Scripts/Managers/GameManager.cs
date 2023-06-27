using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("Текст счёта")] private Text _scoreTxt;
    [SerializeField, Tooltip("Текст окончания игры")] private Text _gameOverTxt;
    [SerializeField, Tooltip("Меню игры")] private GameObject _menu;
    [SerializeField, Tooltip("Кнопка продолжения игры")] private GameObject _resumeBtn;
    private PlayerController _playerController;
    private MoveLeft _moveLeft;
    private int _score;
    private int _scoreDelay = 10;
    private float _btnInput;
    private bool _pauseActive;
    public float BtnInput{get => _btnInput;}

    private void Awake() => _playerController = GameObject.Find("Player").GetComponentInChildren<PlayerController>();
    void Start()
    {
        _score = 0;
        _moveLeft = FindObjectOfType<MoveLeft>();
        InvokeRepeating("GetAddScore", 0, (Time.deltaTime * _scoreDelay));
    }

    void Update() => MenuCtrl();


    private void GetAddScore() 
    {
        // _scoreTxt.text = (!_playerController.GameOver && _playerController.Move > 0) ? $"Score: {++_score}" : $"Score: {_score}";
        _scoreTxt.text = (!_playerController.GameOver && (_moveLeft.CommonSpeed >= _moveLeft.DefaultSpeed + 0.02f)) ? $"Score: {++_score}" : $"Score: {_score}";
    }

    public void GameOver()
    {
        _gameOverTxt.gameObject.SetActive(true);
        _menu.gameObject.SetActive(true);
        _resumeBtn.gameObject.SetActive(false);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    private void MenuCtrl()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !_pauseActive) Pause();
        else if(Input.GetKeyDown(KeyCode.Escape) && _pauseActive) Resume();

        if(Input.GetKeyDown(KeyCode.R)) Restart();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        _menu.gameObject.SetActive(true);
        _pauseActive = true;
    }

    private void Resume()
    {
        if(!_playerController.GameOver)
        {
            Time.timeScale = 1;
            _menu.gameObject.SetActive(false);
            _pauseActive = false;
        }
    }

    private void Exit() => Application.Quit();
    private void OnCtrLeftDown() => _btnInput = -0.95f;
    private void OnCtrRightDown() => _btnInput = 0.95f;
    private void OnCrtlUp() => _btnInput = 0.0f;
}
