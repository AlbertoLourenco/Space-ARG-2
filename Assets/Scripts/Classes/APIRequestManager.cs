using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APIRequestManager {
    
    public delegate void RankingListDelegate(RankingScore[] ranking);
    public RankingListDelegate rankingListDelegate;

    public delegate void RankingAddDelegate(RankingScore score);
    public RankingAddDelegate rankingAddDelegate;

	//----------------------------------------------------------------------------------
	//  Make API requests
	//----------------------------------------------------------------------------------

	public IEnumerator GetRanking() {
        
        using (UnityWebRequest www = UnityWebRequest.Get(Constants.APIURL + "ranking")) {
            
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            }else{
                Debug.Log(www.downloadHandler.text);

                RankingScore[] ranking = RankingList.CreateFromJSON(www.downloadHandler.text).ranking;

                rankingListDelegate(ranking);
            }
        }
    }

    public IEnumerator AddScore(string name, int score) {

        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(Constants.APIURL + "score", form)) {

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            } else {
                Debug.Log(www.downloadHandler.text);

                RankingScore rankingScore = RankingScore.CreateFromJSON(www.downloadHandler.text);

                rankingAddDelegate(rankingScore);
            }
        }
    }
}
