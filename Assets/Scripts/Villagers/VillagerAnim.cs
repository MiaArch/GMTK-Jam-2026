using System.Collections;
using UnityEngine;

namespace Villagers
{
    enum MoveState
    {
        Wander,
        Return, 
        Idle
    }
    
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
        MoveState state;
        private Camera _camera;

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
            _camera = Camera.main;
            StartCoroutine(WanderRoutine());
        }

        private void FixedUpdate()
        {
            switch (state)
            {
                case MoveState.Wander:
                    rb.linearVelocity = moveDirection * moveSpeed;
                    break;

                case MoveState.Return:

                    Vector2 target = GetNearestPointOnScreen();
                    Vector2 dir = (target - rb.position).normalized;

                    rb.linearVelocity = dir * moveSpeed;

                    if (Vector2.Distance(rb.position, target) < 0.2f)
                    {
                        state = MoveState.Idle;
                    }

                    break;
                case MoveState.Idle:
                    rb.linearVelocity = Vector2.zero;
                    break;
            }
            villageAnimator.SetFloat(Speed, rb.linearVelocity.magnitude);
        }
        
        Vector2 GetNearestPointOnScreen()
        {
            Vector3 vp = _camera.WorldToViewportPoint(transform.position);

            vp.x = Mathf.Clamp(vp.x, 0.05f, 0.95f);
            vp.y = Mathf.Clamp(vp.y, 0.05f, 0.95f);

            return _camera.ViewportToWorldPoint(vp);
        }

        private IEnumerator WanderRoutine()
        {
            while (true)
            {
                moveDirection = Random.insideUnitCircle.normalized;
                state = MoveState.Wander;
                if (moveDirection.x < 0)
                {
                    SpriteRenderer.flipX = true;
                }
                

                yield return new WaitForSeconds(Random.Range(moveTimeRange.x, moveTimeRange.y));

                
                SpriteRenderer.flipX = false;
                
                if (!IsVisible())
                {
                    state = MoveState.Return;

                    yield return new WaitUntil(() => state != MoveState.Return);
                }
                else
                {
                    state = MoveState.Idle;
                    yield return new WaitForSeconds(Random.Range(idleTimeRange.x, idleTimeRange.y));
                }
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private bool IsVisible()
        {
            Vector3 viewport = _camera!.WorldToViewportPoint(transform.position);

            return viewport.x is > 0.15f and < 0.85f &&
                   viewport.y is > 0.15f and < 0.85f;
        }
    }
}