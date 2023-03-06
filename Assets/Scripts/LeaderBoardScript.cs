using TMPro;
using UnityEngine;
using Firebase.Database;
using System.Linq;
using System.Collections;

public class LeaderBoardScript : MonoBehaviour
{
    [SerializeField] GameObject _leaderBoard;
    [SerializeField] Transform _leaderBoardContent;
    private TMP_Text _leader;
    public void CrateLeaders(DataSnapshot snapshot)
    {

        foreach (DataSnapshot childSnap in snapshot.Children.Reverse<DataSnapshot>())
        {
            string name = childSnap.Child("Name").Value.ToString();
            int score = int.Parse(childSnap.Child("Score").Value.ToString());
            GameObject boardElement = Instantiate(_leaderBoard, _leaderBoardContent);
            boardElement.GetComponent<Element>().NewElement(name, score);
        }

    }
}
public class Element
{
    public TMP_Text name;
    public TMP_Text score;
    public void NewElement(string name, int score)
    {
        this.name.text = name;
        this.score.text = score.ToString();
    }
}
