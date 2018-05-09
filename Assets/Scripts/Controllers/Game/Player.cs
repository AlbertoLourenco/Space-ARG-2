using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private LifeManager _lifeManager;

    [SerializeField]
    private GameObject _shields;

    [SerializeField]
    private GameObject _speedFire;

    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private GameObject _hurtLeft;

    [SerializeField]
    private GameObject _hurtLeftExplosion;

    [SerializeField]
    private GameObject _hurtRight;

    [SerializeField]
    private GameObject _hurtRightExplosion;

    [SerializeField]
    private GameObject _shotSingleLaser;

    [SerializeField]
    private GameObject _shotTripleLaser;

    [SerializeField]
    private GameObject _backgroundNebula;

    [SerializeField]
    private GameObject _backgroundStars;

    [SerializeField]
    private Joystick _joystickMove;

    [SerializeField]
    private JoystickFire _joystickFire;

    [SerializeField]
    private float _powerUpDuration = 10.0f;

    [SerializeField]
    private AudioSource _soundEffectSpeed;

    [SerializeField]
    private AudioSource _soundEffectShield;

    [SerializeField]
    private AudioSource _soundEffectLaser;

    [SerializeField]
    private AudioSource _soundEffectTurbine;

    [HideInInspector]
    public int damageCount = 0;

    private bool _enableShield = false;
    private bool _enableTripleShot = false;

    private float _speedDefault = 5.0f;
    private float _nextFireTime = 0.0f;

    private float _fireRate = 0.25f;

	//----------------------------------------------------------------------------------
    //  MonoBehaviour
	//----------------------------------------------------------------------------------

	private void Start() {
        
        transform.localScale = new Vector3(Constants.ScalePlayer, Constants.ScalePlayer, 0);

		if (!SettingsManager.EnabledSoundFX) {
			_soundEffectTurbine.Stop();
		}
	}

	void Update() {
        
        Movement();
        ShootLaser();
    }

	//----------------------------------------------------------------------------------
	//  Collision detect
	//----------------------------------------------------------------------------------

	private void OnCollisionEnter2D(Collision2D collision) {

        Enemy enemy = collision.collider.GetComponent<Enemy>();

        if (_enableShield) { // remove shield if active

            enemy.DestroyObject();

            _enableShield = false;
            _shields.SetActive(false);
            _soundEffectShield.Stop();
            return;
        }

        if (enemy.type == EnemyType.Big) {

            enemy.DestroyObject();

            PlayerDead();

        }else{
            
            ContactPoint2D contact = collision.contacts[0];
            Vector3 position = contact.point;
            position.y -= 0.45f;

            enemy.DestroyObject();

            switch (damageCount) {

                case 0:
                    _hurtLeft.SetActive(true);
                    _hurtLeft.transform.position = position;
                    break;

                case 1:
                    _hurtRight.SetActive(true);
                    _hurtRight.transform.position = position;
                    break;

                default:
                    PlayerDead();
                    break;
            }

            damageCount++;
        }
	}

    public void PlayerDead() {
        
        _lifeManager.Damage();

        Instantiate(_explosion, transform.position, Quaternion.identity);

        gameObject.SetActive(false);
    }
    
	//----------------------------------------------------------------------------------
	//  Power up
	//----------------------------------------------------------------------------------

	public void TurnOnPower(PowerUpType type) {

        switch (type) {

            case PowerUpType.Speed:
                _speed = 10.0f;
                _speedFire.SetActive(true);

				if (SettingsManager.EnabledSoundFX) {
                    _soundEffectSpeed.Play();
                }
                break;

            case PowerUpType.Shield:
                _enableShield = true;
                _shields.SetActive(true);

				if (SettingsManager.EnabledSoundFX) {
                    _soundEffectShield.Play();
				}
                return;

            case PowerUpType.TripleShot:
                _enableTripleShot = true;
                break;
        }

        StartCoroutine(TurnOffPower(type));
    }

    private IEnumerator TurnOffPower(PowerUpType type) {

        yield return new WaitForSeconds(_powerUpDuration);

        switch (type) {

            case PowerUpType.Speed:
                _speed = _speedDefault;
                _speedFire.SetActive(false);
                _soundEffectSpeed.Stop();
                break;

            case PowerUpType.TripleShot:
                _enableTripleShot = false;
                break;
        }
    }
    
	public void TurbineTurnOn() {
		_soundEffectTurbine.Play();
	}

	public void TurbineTurnOff() {
		_soundEffectTurbine.Pause();
	}

    //----------------------------------------------------------------------------------
    //  Shoot laser
    //----------------------------------------------------------------------------------

    private void ShootLaser() {

        if (!GameManager.Playing) {
            return;
        }
      
        if (_joystickFire.Pressed) {
           
           if (Time.time > _nextFireTime) {

                _nextFireTime = Time.time + _fireRate;

                if (_enableTripleShot) {

                    // Triple Shot

                    Vector3 laserPosition = transform.position + new Vector3(0.553f, 0.042f, 0);
                    Instantiate(_shotTripleLaser, laserPosition, Quaternion.identity);

                }else{

                    // Single Shot

                    Vector3 laserPosition = transform.position + new Vector3(0, 0.88f, 0);
                    Instantiate(_shotSingleLaser, laserPosition, Quaternion.identity);
                }

				if (SettingsManager.EnabledSoundFX) {
					_soundEffectLaser.volume = SettingsManager.VolumeSoundFX;
                    _soundEffectLaser.Play();
                }
            }
        }
    }

    //----------------------------------------------------------------------------------
    //  Move spaceship
    //----------------------------------------------------------------------------------

    private void Movement() {

        float verticalAxis = Input.GetAxis("Vertical") + _joystickMove.Vertical;
        float horizontalAxis = Input.GetAxis("Horizontal") + _joystickMove.Horizontal;

        transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalAxis);
        transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalAxis);

        _backgroundNebula.transform.Translate(Vector3.up * Time.deltaTime * 0.2f * verticalAxis);
        _backgroundNebula.transform.Translate(Vector3.right * Time.deltaTime * 0.2f * horizontalAxis);

        _backgroundStars.transform.Translate(Vector3.down * Time.deltaTime * 0.4f * verticalAxis);
        _backgroundStars.transform.Translate(Vector3.left * Time.deltaTime * 0.4f * horizontalAxis);

        LimitSceneAxis();
    }

    private void LimitSceneAxis() {

        //-------------------------------------
        //  Player
        //-------------------------------------

        //  Limit axis X

        if (transform.position.x > 8.88f) {
            transform.position = new Vector3(-8.88f, transform.position.y, 0);
        }else
            if (transform.position.x < -8.88f) {
                transform.position = new Vector3(8.88f, transform.position.y, 0);
            }

        //  Limit axis Y

        if (transform.position.y > 4.2f) {
            transform.position = new Vector3(transform.position.x, 4.2f, 0);
        }else
            if (transform.position.y < -4.2f) {
                transform.position = new Vector3(transform.position.x, -4.2f, 0);
            }

        //-------------------------------------
        //  Nebula
        //-------------------------------------

        //  Limit axis X

        if (_backgroundNebula.transform.position.x < -4.26f) {
            _backgroundNebula.transform.position = new Vector3(-4.26f, _backgroundNebula.transform.position.y, 0);
        }else
            if (_backgroundNebula.transform.position.x > 4.26f) {
                _backgroundNebula.transform.position = new Vector3(4.26f, _backgroundNebula.transform.position.y, 0);
            }

        //  Limit axis Y

        if (_backgroundNebula.transform.position.y < -7.2f) {
            _backgroundNebula.transform.position = new Vector3(_backgroundNebula.transform.position.x, -7.2f, 0);
        }else
            if (_backgroundNebula.transform.position.y > 7.2f) {
                _backgroundNebula.transform.position = new Vector3(_backgroundNebula.transform.position.x, 7.2f, 0);
            }

        //-------------------------------------
        //  Stars
        //-------------------------------------

        //  Limit axis X

        if (_backgroundStars.transform.position.x < -11.7f) {
            _backgroundStars.transform.position = new Vector3(-11.7f, _backgroundStars.transform.position.y, 0);
        }else
            if (_backgroundStars.transform.position.x > 11.7f) {
                _backgroundStars.transform.position = new Vector3(11.7f, _backgroundStars.transform.position.y, 0);
            }

        //  Limit axis Y

        if (_backgroundStars.transform.position.y < -14.9f) {
            _backgroundStars.transform.position = new Vector3(_backgroundStars.transform.position.x, -14.9f, 0);
        }else
            if (_backgroundStars.transform.position.y > 14.9f) {
                _backgroundStars.transform.position = new Vector3(_backgroundStars.transform.position.x, 14.9f, 0);
            }
    }
}