using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;  
using TMPro;

[Serializable]
public class Scores{
    public string _id;
    public string Name;
    public int Score;
    public int __v;

    public Scores(string _id, string name, int score, int __v){
        this._id = _id;
        this.Name = name;
        this.Score = score;
        this.__v = __v;

    }
}

[Serializable]
public class ScoreList{
    public List<Scores> score;
    public ScoreList() => this.score = new List<Scores>();
}
public class ScoresTable : MonoBehaviour
{   
    // string base_url = "https://morbin-backend.onrender.com/";
    // string highscores = "highScores";
    ScoreList scoreList;
    int listSize;
    Transform entryContainer;
    Transform entryTemplate;

    // Start is called before the first frame update
    void Awake()
    {
        scoreList = new ScoreList();
        StartCoroutine(Highscores());

        entryContainer = transform.Find("Information");
        entryTemplate = transform.Find("Placeholder");

        entryTemplate.gameObject.SetActive(false);

    
    }

    IEnumerator Highscores(){
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:8000/highScores");
        yield  return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success){
        }else{
            Debug.Log(www.downloadHandler.text);
            scoreList = JsonUtility.FromJson<ScoreList>(www.downloadHandler.text);
            
        }

        listSize = scoreList.score.Count;

        Debug.Log(scoreList.score[1].Name);

        for(int i = 0; i < listSize; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -80f * i);
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            string rankstring;
            switch (rank) {
                default:

                rankstring = rank + "th"; break;

                case 1: rankstring = "1st"; break;
                case 2: rankstring = "2nd"; break;
                case 3: rankstring = "3rd"; break;
            }

            entryTransform.Find("Position").GetComponent<TMP_Text>().text = rankstring;
            entryTransform.Find("Name").GetComponent<TMP_Text>().text = scoreList.score[i].Name;
            entryTransform.Find("Score").GetComponent<TMP_Text>().text = scoreList.score[i].Score.ToString();
        }   
    }
}
