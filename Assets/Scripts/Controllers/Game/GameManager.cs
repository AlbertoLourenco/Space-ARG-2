using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : SceneBase {

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private GameObject _enemyBig;

    [SerializeField]
    private GameObject _enemyNormal;

    [SerializeField]
    private GameObject[] _powerUps;

    [SerializeField]
    private GameObject _healthBar;

    [SerializeField]
    private GameObject _worldBar;

    [SerializeField]
    private Button _buttonPause;

    [SerializeField]
    private Sprite _imagePlay;

    [SerializeField]
    private Sprite _imagePause;
   
    [SerializeField]
    private Player _player;

    [SerializeField]
	private GameObject _quitDialog;

    [HideInInspector]
    public static int gameScore = 0;

    [HideInInspector]
    public static int explosionLayerControl = 10;

    private AudioSource _musicBackground;

    private SimpleHealthBar _barLife;

    private SimpleHealthBar _barWorldLife;

    private static int _enemyCount = 0;

    private static int _worldLife = 100;

    private static bool _isPlaying = true;

    public static bool Playing {
        get {
            return _isPlaying;
        }
    }

    private float _enemyBigTime = 15.0f;
    private float _enemyNormalTime = 2.5f;

    //----------------------------------------------------------------------------------
    //  MonoBehaviour
    //----------------------------------------------------------------------------------

	void Start () {
        
        _isPlaying = true;

        _musicBackground = gameObject.GetComponent<AudioSource>();

        if (!SettingsManager.EnabledMusic) {
            _musicBackground.Stop();
        } else {
            _musicBackground.volume = SettingsManager.VolumeMusic;
        }

        // Health & World bars

        _barLife = _healthBar.GetComponent<SimpleHealthBar>();

        _barWorldLife = _worldBar.GetComponent<SimpleHealthBar>();

        // Power up

        StartCoroutine(RafflePowerUp());

        // Big Enemy

        StartCoroutine(CreateBigEnemy());

        // Normal Enemy

        StartCoroutine(CreateNormalEnemy());
	}

	private void FixedUpdate() {

        _scoreText.text = "SCORE: " + gameScore;

        _barLife.UpdateBar(3-_player.damageCount, 3);
        _barWorldLife.UpdateBar(_worldLife, 100);

        if (_worldLife <= 0) {

            _player.PlayerDead();

            Destroy(gameObject);
        }
	}

	private void OnDestroy() {

        _enemyCount = 0;
        _worldLife = 100;
        _isPlaying = false;

        explosionLayerControl = 0;
	}

    //----------------------------------------------------------------------------------
    //  Pause game
    //----------------------------------------------------------------------------------

    public void PauseGame() {
        
        if (_isPlaying) {
			Time.timeScale = 0;
            _player.TurbineTurnOff();
			_musicBackground.Pause();
            _buttonPause.image.sprite = _imagePlay;
        }else{
			Time.timeScale = 1;
            _buttonPause.image.sprite = _imagePause;

			if (SettingsManager.EnabledMusic) {
                _musicBackground.Play();
			}

			if (SettingsManager.EnabledSoundFX) {
				_player.TurbineTurnOn();
			}
        }

        _isPlaying = !_isPlaying;
    }

	public void ConfirmQuitGame() {

		PauseGame();
		_quitDialog.SetActive(true);
	}

    public void QuitGame() {
        
		ShowMenu();

        Time.timeScale = 1;
    }

    public void CloseDialog() {

        PauseGame();
        _quitDialog.SetActive(false);
    }

	//----------------------------------------------------------------------------------
	//  Game Score - increase
	//----------------------------------------------------------------------------------

	public static void IncreaseScore(int score) {

        gameScore += score;
    }

    //----------------------------------------------------------------------------------
    //  Enemy count - decrease
    //----------------------------------------------------------------------------------

    public static void DecreaseEnemyCount() {

        _enemyCount--;
    }

    //----------------------------------------------------------------------------------
    //  World life - decrease
    //----------------------------------------------------------------------------------

    public static void DecreaseWorldLife(int value) {
        
        _worldLife -= value;
    }

	//----------------------------------------------------------------------------------
	//  Enemy
	//----------------------------------------------------------------------------------

	private IEnumerator CreateBigEnemy() {

        while (true) {

            if (_enemyCount < 8) {

                _enemyCount++;
                explosionLayerControl++;

                Instantiate(_enemyBig, _enemyBig.transform.position, Quaternion.identity);

                _enemyBigTime -= 0.5f; // increase difficult
            }

            yield return new WaitForSeconds(Random.Range(_enemyBigTime, 25.0f));
        }
    }

    private IEnumerator CreateNormalEnemy() {

        while (true) {

            if (_enemyCount < 8) {

                _enemyCount++;
                explosionLayerControl++;

                Instantiate(_enemyNormal, _enemyNormal.transform.position, Quaternion.identity);

                _enemyNormalTime -= 0.2f; // increase difficult
            }

            yield return new WaitForSeconds(Random.Range(_enemyNormalTime, 5.0f));
        }
    }

    //----------------------------------------------------------------------------------
    //  Power up
    //----------------------------------------------------------------------------------

    private IEnumerator RafflePowerUp() {

        while (true) {

            int index = Random.Range(0, 3);

            Instantiate(_powerUps[index], _powerUps[index].transform.position, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(10.0f, 15.0f));
        }
    }
}
