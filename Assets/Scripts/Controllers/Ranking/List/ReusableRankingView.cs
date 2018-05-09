using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReusableRankingView : MonoBehaviour {

    [SerializeField]
    private Text _textRanking;

    [SerializeField]
    private Text _textName;

    [SerializeField]
    private Text _textScore;

    public void loadUI(int position, RankingScore score, RankingScore gamerScore) {
        
        _textName.text = score.name;
        _textRanking.text = position.ToString();
        _textScore.text = score.score.ToString();

        if (score != null &&
            gamerScore != null &&
            score.id == gamerScore.id) {
            _textName.color = Color.red;   
        }
	}
}
