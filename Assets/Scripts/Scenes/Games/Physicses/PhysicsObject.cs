using UnityEngine;

namespace Scenes.Games.Physicses
{
    public abstract class PhysicsObject : MonoBehaviour
    {
        #region Constants

        private const float MinMoveDistance = 0.001f;
        private const float ShellRadius = 0.01f;
        private const float MinGroundNormalY = .65f;

        #endregion

        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected Collider2D cd;

        #region Define Variants

        private ContactFilter2D contactFilter;

        #endregion

        #region Calculate Variants

        private readonly RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

        public abstract Vector2 Velocity { get; set; }
        public bool Grounded { get; private set; }

        private Vector2 groundNormal;

        #endregion

        protected virtual void Start()
        {
            contactFilter.useTriggers = false;
            contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(rb.gameObject.layer));
            contactFilter.useLayerMask = true;
        }

        protected virtual void Update()
        {
            HandleGraphics();
        }

        protected abstract void HandleGraphics();

        private void FixedUpdate()
        {
            HandleKinematics();
            ApplyMovement();
        }

        protected abstract void HandleKinematics();

        private void ApplyMovement()
        {
            var deltaPosition = Velocity * Time.fixedDeltaTime;
            var moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

            var grounded = false;
            var movement = moveAlongGround * deltaPosition.x;
            Move(movement, false, ref grounded);
            movement = Vector2.up * deltaPosition.y;
            Move(movement, true, ref grounded);

            this.Grounded = grounded;
            UpdateGrounded(grounded);
        }

        protected abstract void UpdateGrounded(bool grounded);

        private void Move(Vector2 movement, bool yMovement, ref bool grounded)
        {
            var distance = movement.magnitude;
            if (distance < MinMoveDistance) return;
            int count = cd.Cast(movement, contactFilter, hitBuffer, distance + ShellRadius);
            for (int i = 0; i < count; i++)
            {
                Vector2 currentNormal = hitBuffer[i].normal;
                if (currentNormal.y > MinGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(Velocity, currentNormal);
                if (projection < 0)
                {
                    Velocity = Velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBuffer[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }

            rb.position += movement.normalized * distance;
        }
    }
}