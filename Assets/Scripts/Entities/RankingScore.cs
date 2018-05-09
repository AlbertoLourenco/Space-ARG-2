using UnityEngine;
using System.Collections;

[System.Serializable]
public class RankingScore {

    public int id;
    public int score;
    public string name;
    public string date;

    public static RankingScore CreateFromJSON(string jsonString) {
        return JsonUtility.FromJson<RankingScore>(jsonString);
    }
}
