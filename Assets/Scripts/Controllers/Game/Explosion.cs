using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	private AudioSource _soundExplosion;

	//----------------------------------------------------------------------------------
	//  MonoBehaviour
	//----------------------------------------------------------------------------------

	void Start() {
    
        _soundExplosion = gameObject.GetComponent<AudioSource>();

		if (SettingsManager.EnabledSoundFX) {
			_soundExplosion.volume = SettingsManager.VolumeSoundFX;
            _soundExplosion.Play();
        }
        
        SpriteRenderer render = this.GetComponent<SpriteRenderer>();
        render.sortingOrder = GameManager.explosionLayerControl++;

        Destroy(this.gameObject, 3.0f);
	}

	private void OnDestroy() {
        GameManager.explosionLayerControl--;
	}
}
