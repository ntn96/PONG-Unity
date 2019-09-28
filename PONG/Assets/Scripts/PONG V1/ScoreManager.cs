using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace pong
{
    //Autor : Antonio Serrano Miralles
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private Text scoreboard1;
        [SerializeField] private Text scoreboard2;
        [SerializeField] private int score1 = 0;
        [SerializeField] private int score2 = 0;

        public static ScoreManager instance = null;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        public void Score(int player)
        {
            if (player < 1 || player > 2) return;
            if (player == 1)
            {
                score1++;
                scoreboard1.text = score1.ToString();
            }
            else
            {
                score2++;
                scoreboard2.text = score2.ToString();
            }
        }
    }
}
