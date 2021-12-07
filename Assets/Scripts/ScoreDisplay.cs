using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text text = GetComponent<Text>();
        text.text = ScoreKeeper.scorePoints.ToString();
        ScoreKeeper.ResetScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
