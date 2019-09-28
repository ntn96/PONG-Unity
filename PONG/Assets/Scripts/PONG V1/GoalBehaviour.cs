using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong
{
    //Autor : Antonio Serrano Miralles
    public class GoalBehaviour : MonoBehaviour
    {
        [SerializeField] private int owner;
        [SerializeField] private int scorer;
        
        public void Goal()
        {

            GameManager.instance.NotifyGoal(owner);
            ScoreManager.instance.Score(scorer);
        }
    }
}
