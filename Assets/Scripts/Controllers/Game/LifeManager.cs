using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : SceneBase {

    public static int lifes = 3;

    private Image image;

    [SerializeField]
    private Sprite[] _lifes;

    //----------------------------------------------------------------------------------
    //  MonoBehaviour
    //----------------------------------------------------------------------------------

	void Start () {

        image = this.GetComponent<Image>();

        UpdateSprite();
	}

	//----------------------------------------------------------------------------------
	//  Collision detect
	//----------------------------------------------------------------------------------

	public void Damage() {

        if (lifes > 0) {
            lifes--;    
        }

        UpdateSprite();

        Invoke("LoadNextScene", 2.7f);
    }

    private void UpdateSprite() {

        image.sprite = _lifes[lifes]; 
    }

    //----------------------------------------------------------------------------------
    //  Load next scene
    //----------------------------------------------------------------------------------

    private void LoadNextScene() {

        if (lifes == 0) {

            if (GameManager.gameScore > 0) {
                LoadScene(SceneType.RankingAdd);
            }else{
                LoadScene(SceneType.Menu);
            }
        }else{
            LoadScene(SceneType.Game);
        }
    }
}
