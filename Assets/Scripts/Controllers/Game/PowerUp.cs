using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public PowerUpType powerUpType;

    [SerializeField]
    private AudioClip _soundClip;

    private float _maxY = Constants.ScreenMaxY + 1.5f; // 1.5 = scene out stage

    //----------------------------------------------------------------------------------
    //  MonoBehaviour
    //----------------------------------------------------------------------------------

	void Start () {

        float maxX = Constants.ScreenMaxX - 0.9f; // 0.9 = sprite width

        transform.position = new Vector3(Random.Range(-maxX, maxX), _maxY, 0);
	}
	
	void Update () {

        if (transform.position.y < -_maxY) {
            
            Destroy(this.gameObject);
        }
	}

    //----------------------------------------------------------------------------------
    //  Collision detect
    //----------------------------------------------------------------------------------

	private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Player") {

            if (SettingsManager.EnabledSoundFX) {
				
				AudioSource.PlayClipAtPoint(_soundClip, Camera.main.transform.position, (SettingsManager.VolumeSoundFX - 0.1f));
            }

            Player player = collision.GetComponent<Player>();

            player.TurnOnPower(this.powerUpType);

            Destroy(this.gameObject);
        }
	}
}
