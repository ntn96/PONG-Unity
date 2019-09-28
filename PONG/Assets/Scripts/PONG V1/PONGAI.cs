using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace pong
{
    //Autor : Antonio Serrano Miralles
    public class PONGAI : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D paddleRigid;
        [SerializeField] private PaddleController controller;
        private float goalPositionX = 8f;
        private float limits = 5f;
        private bool targetReached = true;

        private float paddleYTarget;

        private void Update()
        {
            if (!targetReached && Mathf.Abs(paddleYTarget - transform.position.y) < 0.1f)
            {
                paddleRigid.velocity = Vector2.zero;
                targetReached = true;
            }
        }

        public void CalculatePosition(Vector2 position, Vector2 direction)
        {
            if (direction.y == 0) paddleYTarget = position.y;
            else
            {
                float intersectX = SideIntersect(position, direction);
                Vector2 actualDirection = direction;
                while (intersectX < goalPositionX) 
                {
                    Vector2 hitPosition;
                    if (actualDirection.y > 0) hitPosition = new Vector2(intersectX, limits);
                    else hitPosition = new Vector2(intersectX, -limits);
                    actualDirection.y = -actualDirection.y;
                    intersectX = SideIntersect(hitPosition, actualDirection);
                    direction = actualDirection;
                    position = hitPosition;
                }
                paddleYTarget = GoalIntersect(position, direction);
                if (paddleYTarget > 0 && paddleYTarget > (limits - paddleRigid.transform.localScale.y * 0.16f))
                    paddleYTarget = (limits - paddleRigid.transform.localScale.y * 0.16f);
                else if (paddleYTarget < 0 && paddleYTarget < (-limits + paddleRigid.transform.localScale.y * 0.16f))
                    paddleYTarget = (-limits + paddleRigid.transform.localScale.y * 0.16f);
                targetReached = false;
                if (paddleYTarget > transform.position.y)
                    paddleRigid.velocity = Vector2.up * controller.Velocity;
                else paddleRigid.velocity = Vector2.down * controller.Velocity;
            }
        }

        private float SideIntersect(Vector2 position, Vector2 direction)
        {
            float actualLimit;
            if (direction.y > 0) //It will intersect with upper limit
            {
                actualLimit = limits;
            } else //It will intersect with lower limit, can't be 0
            {
                actualLimit = -limits;
            }
            float intersectX = ((actualLimit - position.y) / direction.y) * direction.x + position.x;
            return intersectX;
        }

        private float GoalIntersect(Vector2 position, Vector2 direction)
        {
            float intersectY = ((goalPositionX - position.x) / direction.x) * direction.y + position.y;
            return intersectY;
        }
    }
}
