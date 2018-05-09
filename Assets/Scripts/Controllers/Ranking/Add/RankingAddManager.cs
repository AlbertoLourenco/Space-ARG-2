using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RankingAddManager : SceneBase {

    [SerializeField]
    private Text _textScore;

    [SerializeField]
    private InputField _textUserName;

    private APIRequestManager _request;

    //----------------------------------------------------------------------------------
    //  MonoBehaviour
    //----------------------------------------------------------------------------------

	void Start() {

        _request = new APIRequestManager();
        _request.rankingAddDelegate = ScoreAdded;

        _textScore.text = "SCORE: " + GameManager.gameScore;

        _textUserName.ActivateInputField();
    }

	void Update() {}

    //----------------------------------------------------------------------------------
    //  Send score to API
    //----------------------------------------------------------------------------------

    public void AddRankingScore() {
        
        if (_textUserName.text.Length <= 3) {
            return;
        }

        StartCoroutine(_request.AddScore(_textUserName.text, GameManager.gameScore));
    }

    private void ScoreAdded(RankingScore score) {

        RankingManager.gamerScore = score;

        LoadScene(SceneType.RankingList);
    }
}
