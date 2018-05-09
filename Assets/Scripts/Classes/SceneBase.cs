using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneBase : MonoBehaviour {

    [SerializeField]
    private Animator _animatorTransition;

    private bool _transitioning;
   
    //----------------------------------------------------------------------------------
    //  MonoBehaviour
    //----------------------------------------------------------------------------------

	void Start() {}

	void Update() {}

    //----------------------------------------------------------------------------------
    //  Load scene
    //----------------------------------------------------------------------------------

    protected void LoadScene(SceneType scene) {

        StartCoroutine(FadeIn(scene.ToString()));
    }

    private IEnumerator FadeIn(string sceneName) {

        _animatorTransition.SetBool("Fade", true);

        yield return new WaitUntil(() => _animatorTransition.GetComponent<Image>().color.a.Equals(1.0f));

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    //----------------------------------------------------------------------------------
    //  Load scene
    //----------------------------------------------------------------------------------

    public void ShowMenu() {

		if (!_transitioning) {

			_transitioning = true;

            LoadScene(SceneType.Menu);
		}
    }
}
