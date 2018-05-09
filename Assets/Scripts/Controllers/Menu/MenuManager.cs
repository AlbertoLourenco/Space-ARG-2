using UnityEngine;
using System.Collections;

public class MenuManager : SceneBase {

    [SerializeField]
    private GameObject _stars;

    [SerializeField]
    private GameObject _nebula;

    [SerializeField]
    private AudioClip _audioButtonClick;

    private bool _transitioning;

	private AudioSource _musicBackground;

    private Vector3 _starsInitialPostion = new Vector3(11.0f, -15.0f, 0);
    private Vector3 _starsFinalPostion = new Vector3(-9.0f, 12.0f, 0);

    private Vector3 _nebulaInitialPostion = new Vector3(-10.34f, -14.04f, 0);
    private Vector3 _nebulaFinalPostion = new Vector3(10.82f, 14.75f, 0);

    //----------------------------------------------------------------------------------
    //  MonoBehaviour
    //----------------------------------------------------------------------------------

    void Start() {
        
        _stars.transform.position = _starsInitialPostion;
        _nebula.transform.position = _nebulaInitialPostion;

		_musicBackground = gameObject.GetComponent<AudioSource>();

		if (!SettingsManager.EnabledMusic) {
			_musicBackground.Stop();
		}else{
            _musicBackground.Play();
			_musicBackground.volume = SettingsManager.VolumeMusic;
		}
    }

    void Update() {

        _stars.transform.Translate(_starsFinalPostion * 0.01f * Time.deltaTime);
        if (_stars.transform.position.x < -11.5f) {
            _stars.transform.position = _starsInitialPostion;
        }

        _nebula.transform.Translate(_nebulaFinalPostion * 0.01f * Time.deltaTime);
        if (_nebula.transform.position.x > 10.5f) {
            _nebula.transform.position = _nebulaInitialPostion;
        }
    }

    //----------------------------------------------------------------------------------
    //  Menu options
    //----------------------------------------------------------------------------------

    public void StartGame() {

        if (!_transitioning) {

            _transitioning = true;

            LifeManager.lifes = 3;
            GameManager.gameScore = 0;

            PlayButtonClickSound();

            LoadScene(SceneType.Game);
        }
    }

    public void ShowRanking() {

        if (!_transitioning) {

            _transitioning = true;

            PlayButtonClickSound();

            LoadScene(SceneType.RankingList);
        }
    }

    public void ShowSettings() {
              
        if (!_transitioning) {

            _transitioning = true;

			PlayButtonClickSound();

			LoadScene(SceneType.Settings);
        }
    }

	private void PlayButtonClickSound() {

        if (SettingsManager.EnabledSoundFX) {
            AudioSource.PlayClipAtPoint(_audioButtonClick, Camera.main.transform.position, 1.0f);
        }
	}
}
