using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RankingManager : SceneBase {

    [SerializeField]
    private RectTransform _scrollScores;

    [SerializeField]
    private GameObject _reusableRankingView;

    private APIRequestManager _request;

    [HideInInspector]
    public static RankingScore gamerScore;

    //----------------------------------------------------------------------------------
    //  MonoBehavior
    //----------------------------------------------------------------------------------

	private void Start() {

        _request = new APIRequestManager();
        _request.rankingListDelegate = Ranking;

        StartCoroutine(_request.GetRanking());
	}

	private void OnDestroy() {
        
        gamerScore = null;
	}

	//----------------------------------------------------------------------------------
	//  Load ranking list
	//----------------------------------------------------------------------------------

	private void Ranking(RankingScore[] ranking) {

        int position = 1;
        foreach (RankingScore item in ranking) {
            
            GameObject reusableView = Instantiate(_reusableRankingView);
            reusableView.GetComponent<ReusableRankingView>().loadUI(position, item, gamerScore);
            reusableView.transform.SetParent(_scrollScores);

            position++;
        }

        _scrollScores.sizeDelta = new Vector2(_scrollScores.sizeDelta.x, ranking.Length * 70 /* item height */);
    }
}
