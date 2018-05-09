using UnityEngine;
using System.Collections;

[System.Serializable]
public class RankingList {

    public RankingScore[] ranking;

    public static RankingList CreateFromJSON(string jsonString) {
        return JsonUtility.FromJson<RankingList>(jsonString);
    }
}

