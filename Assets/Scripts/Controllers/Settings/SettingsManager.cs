using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SettingsManager : SceneBase {

    [SerializeField]
    private Toggle _toggleEnableMusic;

    [SerializeField]
    private Slider _sliderVolumeMusic;

    [SerializeField]
    private Toggle _toggleEnableSoundFX;

    [SerializeField]
    private Slider _sliderVolumeSoundFX;

    bool _sliderIsDraging = false;

    //  Music enabled

	private static bool _enableMusic = true;

	public static bool EnabledMusic {
        get {
			return _enableMusic;
        }
    }

    //  Music volume
    
    private static float _volumeMusic = 0.75f;

	public static float VolumeMusic {
        get {
            return _volumeMusic;
        }
    }

    //  SoundFX enabled

	private static bool _enableSoundFX = true;

	public static bool EnabledSoundFX {
        get {
			return _enableSoundFX;
        }
    }

    //  Music volume

	private static float _volumeSoundFX = 0.75f;

    public static float VolumeSoundFX {
        get {
			return _volumeSoundFX;
        }
    }

	//----------------------------------------------------------------------------------
	//  MonoBehavior
	//----------------------------------------------------------------------------------
       
	private void Start() {

        _toggleEnableMusic.isOn = _enableMusic;
        _sliderVolumeMusic.value = (_enableMusic) ? _volumeMusic : 0;

        _toggleEnableSoundFX.isOn = _enableSoundFX;
        _sliderVolumeSoundFX.value = (_enableSoundFX) ? _volumeSoundFX : 0;
	}

	//----------------------------------------------------------------------------------
	//  Change Sound Settings
	//----------------------------------------------------------------------------------

	public void EnableMusicChanged() {

		_enableMusic = !_enableMusic;

		if (!_sliderIsDraging) {
            _sliderVolumeMusic.value = (!_enableMusic) ? 0 : 0.75f;
        }
    }

	public void EnableSoundFXChanged() {
		
		_enableSoundFX = !_enableSoundFX;

		if (!_sliderIsDraging) {
            _sliderVolumeSoundFX.value = (!_enableSoundFX) ? 0 : 0.75f;	
		}
    }

    public void VolumeMusicChanged() {

        if (_sliderIsDraging) {
			_volumeMusic = _sliderVolumeMusic.value;
            _enableMusic = !_volumeMusic.Equals(0);         
            _toggleEnableMusic.isOn = _sliderVolumeMusic.value > 0;
		}
    }

    public void VolumeSoundFXChanged() {

        if (_sliderIsDraging) {
			_volumeSoundFX = _sliderVolumeSoundFX.value;
			_enableSoundFX = !_volumeSoundFX.Equals(0);         
            _toggleEnableSoundFX.isOn = _sliderVolumeSoundFX.value > 0;
        }
    }

    //----------------------------------------------------------------------------------
    //  Slider is dragging
    //----------------------------------------------------------------------------------

	public void SliderDragBegin() {
		_sliderIsDraging = true;
	}

    public void SliderDragEnd() {
		_sliderIsDraging = false;
    }
}
