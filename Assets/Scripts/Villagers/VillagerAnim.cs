using System.Collections;
using UnityEngine;

namespace Villagers
{
    public class VillagerAnim : MonoBehaviour
    {
        public Animator villageAnimator;
        public Rigidbody2D rb;
        public SpriteRenderer SpriteRenderer;
        
        [Header("Movement")]
        public float moveSpeed = 2f;
        public Vector2 moveTimeRange = new Vector2(1f, 3f);
        public Vector2 idleTimeRange = new Vector2(1f, 2.5f);
        
        private Vector2 moveDirection;
        private bool isMoving;
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            if (rb == null)
                rb = GetComponent<Rigidbody2D>();

            if (villageAnimator == null)
                villageAnimator = GetComponent<Animator>();

            if (SpriteRenderer == null)
            {
                SpriteRenderer = GetComponent<SpriteRenderer>();
            }
        }

        private void Start()
        {
            StartCoroutine(WanderRoutine());
        }

        private void FixedUpdate()
        {
            if (isMoving)
            {
                rb.linearVelocity = moveDirection * moveSpeed;
                
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
            
            villageAnimator.SetFloat(Speed, rb.linearVelocity.magnitude);
        }

        private IEnumerator WanderRoutine()
        {
            while (true)
            {
                moveDirection = Random.insideUnitCircle.normalized;
                isMoving = true;
                if (moveDirection.x < 0)
                {
                    SpriteRenderer.flipX = true;
                }
                

                yield return new WaitForSeconds(Random.Range(moveTimeRange.x, moveTimeRange.y));

                isMoving = false;
                SpriteRenderer.flipX = false;
                
                yield return new WaitForSeconds(Random.Range(idleTimeRange.x, idleTimeRange.y));
            }
        }
    }
}