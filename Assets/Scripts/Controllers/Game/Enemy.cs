using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private int life = 75;

    [SerializeField]
    private GameObject destroyAnimation;

    [SerializeField]
    private AudioClip _soundHitClip;

    public EnemyType type;

    private Animator _animator;

    //----------------------------------------------------------------------------------
    //  MonoBehaviour
    //----------------------------------------------------------------------------------

	void Start () {

        float x = 0;
        float y = 0;

        switch (type) {

            case EnemyType.Big:
                y = Random.Range(8.5f, 6.5f);
                x = Random.Range(-4.0f, 4.0f);
                transform.localScale = new Vector3(Constants.ScaleEnemyBig, Constants.ScaleEnemyBig, 0);
                break;

            case EnemyType.Normal:
                y = Random.Range(8.1f, 6.1f);
                x = Random.Range(-4.0f, 4.0f);
                transform.localScale = new Vector3(Constants.ScaleEnemyNormal, Constants.ScaleEnemyNormal, 0);
                break;
        }

        transform.position = new Vector3(x, y, 0);

        _animator = GetComponent<Animator>();
	}

	void Update () {

        float y = 0;
        int damage = 0;

        switch (type) {

            case EnemyType.Big:
                y = -6.4f;
                damage = 15;
                break;

            case EnemyType.Normal:
                y = -6.08f;
                damage = 10;
                break;
        }

        if (transform.position.y < y) {

            GameManager.DecreaseEnemyCount();
            GameManager.DecreaseWorldLife(damage);

            TurnAnimatorBackToNormalState();

            Destroy(this.gameObject);
        }
	}

	private void OnTriggerEnter2D(Collider2D collision) {

		if (SettingsManager.EnabledSoundFX) {
            AudioSource.PlayClipAtPoint(_soundHitClip, Camera.main.transform.position, 0.3f);
        }

        _animator.SetBool("Hurt", true);

        Invoke("TurnAnimatorBackToNormalState", 0.15f);
	}

	private void TurnAnimatorBackToNormalState() {

        _animator.SetBool("Hurt", false);
    }

	//----------------------------------------------------------------------------------
	//  Damage
	//----------------------------------------------------------------------------------

	public void Damage(int value) {

        if (life > 0) {
            life -= value;
        }else{

            switch (type) {
                case EnemyType.Big:
                    GameManager.IncreaseScore(15);
                    break;
                case EnemyType.Normal:
                    GameManager.IncreaseScore(10);
                    break;
            }

            DestroyObject();
        }
    }

    public void DestroyObject() {
        
        GameManager.DecreaseEnemyCount();

        destroyAnimation.layer = gameObject.layer + 1;

        Instantiate(destroyAnimation, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
