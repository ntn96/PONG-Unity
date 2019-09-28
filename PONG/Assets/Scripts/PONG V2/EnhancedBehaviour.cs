using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pong;

namespace pongV2
{
    //Autor : Antonio Serrano Miralles
    public class EnhancedBehaviour : MonoBehaviour
    {
        private int actualHits = 0;
        [SerializeField] private float originalPaddleSize;
        [SerializeField] private float minPaddleSize = 1f;
        [SerializeField] private float speedIncrease;
        [SerializeField] private float maxBallSpeed;
        private BallBehaviour ball;


        public void PaddleHit()
        {
            if (ball == null) ball = GameManager.instance.Ball;
            actualHits++;
            if (actualHits == 10)
            {
                ball.Velocity = Mathf.Min(maxBallSpeed, ball.Velocity + speedIncrease);
                actualHits = 0;
            }
        }

        public void ReducePaddle(GameObject paddle)
        {
            if (paddle.transform.localScale.y == minPaddleSize) return;
            paddle.transform.localScale = new Vector3(1, Mathf.Max(paddle.transform.localScale.y - 1, minPaddleSize), 1);
        }

        public void SetPaddleOriginalSize(GameObject paddle)
        {
            paddle.transform.localScale = new Vector3(1, originalPaddleSize, 1);
        }
    }
}
