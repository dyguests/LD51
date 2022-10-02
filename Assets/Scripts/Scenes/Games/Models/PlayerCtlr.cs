using System;
using Cysharp.Threading.Tasks;
using Scenes.Games.Physicses;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Games.Models
{
    public class PlayerCtlr : PhysicsObject, IPlayerInputActions, IPlayerActions
    {
        private const float Epsilon = 0.001f;

        private static PlayerCtlr sPrefab;
        private static PlayerCtlr sInstance;
        public static PlayerCtlr Instance => sInstance;


        [Space] [SerializeField] private SpriteRenderer sr;

        #region Define Variants

        // --------------- move ---------------

        [Space] [SerializeField] private float moveSpeed = 9f;
        [SerializeField] private float moveAccelerationTime = 6 * 0.02f;
        private float moveAcceleration;
        [SerializeField] private float moveDecelerationTime = 3 * 0.02f;
        private float moveDeceleration;

        // --------------- jump ---------------

        [SerializeField] private float jumpHeight = 3.2f;
        private float jumpSpeed;
        [SerializeField] private float jumpSpoolTime = 4 * 0.02f;
        [SerializeField] private float jumpCoyoteTime = 3 * 0.02f;
        [SerializeField] private int jumpMaxTimes = 1;
        private int jumpRemainingTimes = 1;
        [SerializeField] private float jumpCost = 25f;
        [SerializeField] private AnimationCurve jumpInterpolator;

        // --------------- limit ---------------

        [SerializeField] private float dropSpeed = 20f;

        #endregion

        #region Input Variants

        private Vector2 moveInput;

        private bool jumpInput;
        private float lastJumpDownInputTime = float.MinValue;

        #endregion

        #region temp variants

        /// <summary>
        /// 当前是否在跳跃状态
        /// </summary>
        private bool jumping;
        /// <summary>
        /// 无操作时的速度
        /// 注：在有风等情况下，不一定为0
        /// </summary>
        private float idleVelocityX = 0f;

        public float LastGroundedTime { get; private set; }

        #endregion

        public static PlayerCtlr Generate(Vector3 position)
        {
            if (sPrefab == null)
            {
                sPrefab = Resources.Load<PlayerCtlr>("Prefabs/Models/Player");
            }

            var instantiate = Instantiate(sPrefab, position, Quaternion.identity);
            instantiate.name = "Player";
            return instantiate;
        }

        private void Awake()
        {
            sInstance = this;
        }

        protected override void Start()
        {
            base.Start();
            SetDirty();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            SetDirty();
        }
#endif
        private void SetDirty()
        {
            moveAcceleration = moveSpeed / moveAccelerationTime;
            moveDeceleration = moveSpeed / moveDecelerationTime;

            jumpSpeed = Mathf.Sqrt(2 * -Physics2D.gravity.y * jumpHeight);

            // climbUpAcceleration = climbUpSpeed / climbUpAccelerationTime;
            // climbUpDeceleration = climbUpSpeed / climbUpDecelerationTime;
            // climbDownAcceleration = -Physics2D.gravity.y * climbDownGravityScale;
            // climbDownDeceleration = climbDownSpeed / climbDownDecelerationTime;
            // climbJumpSpeed = Mathf.Sqrt(2 * -Physics2D.gravity.y * climbJumpHeight);
            //
            // dashSpeed = dashDistance / dashTime;
            //
            // if (afterimageTimes.Length != 3)
            // {
            //     var wrongAfterimageTimes = afterimageTimes;
            //     afterimageTimes = new float[3];
            //     afterimageTimes[0] = wrongAfterimageTimes.Length > 0 ? wrongAfterimageTimes[0] : 0f;
            //     afterimageTimes[1] = wrongAfterimageTimes.Length > 1 ? wrongAfterimageTimes[1] : 0f;
            //     afterimageTimes[2] = wrongAfterimageTimes.Length > 2 ? wrongAfterimageTimes[2] : 0f;
            // }
        }

        public override Vector2 Velocity { get; set; }

        protected override void HandleGraphics()
        {
            if (moveInput.x > Epsilon && sr.flipX)
            {
                sr.flipX = false;
            }
            else if (moveInput.x < -Epsilon && !sr.flipX)
            {
                sr.flipX = true;
            }
        }

        protected override void HandleKinematics()
        {
            if (jumping && Grounded)
            {
                jumping = false;
            }

            var velocity = Velocity;

            // accept input
            if (moveInput.x > Epsilon)
            {
                if (velocity.x < idleVelocityX - Epsilon)
                {
                    velocity.x += moveDeceleration * Time.fixedDeltaTime;
                }
                else if (velocity.x < idleVelocityX + moveSpeed)
                {
                    velocity.x = Mathf.Min(idleVelocityX + moveSpeed, velocity.x + moveAcceleration * Time.fixedDeltaTime);
                }
                else
                {
                    velocity.x = Mathf.Max(idleVelocityX + moveSpeed, velocity.x - moveDeceleration * Time.fixedDeltaTime);
                }
            }
            else if (moveInput.x < -Epsilon)
            {
                if (velocity.x > idleVelocityX + Epsilon)
                {
                    velocity.x -= moveDeceleration * Time.fixedDeltaTime;
                }
                else if (velocity.x > idleVelocityX - moveSpeed)
                {
                    velocity.x = Mathf.Max(idleVelocityX - moveSpeed, velocity.x - moveAcceleration * Time.fixedDeltaTime);
                }
                else
                {
                    velocity.x = Mathf.Min(idleVelocityX - moveSpeed, velocity.x + moveDeceleration * Time.fixedDeltaTime);
                }
            }
            else
            {
                var offsetVelocityX = velocity.x - idleVelocityX;
                velocity.x = idleVelocityX + Math.Sign(offsetVelocityX) * Mathf.Max(0, offsetVelocityX - moveDeceleration * Time.fixedDeltaTime);
            }

            // accept jump
            var groundedRecent = Grounded || Time.fixedTime - LastGroundedTime < jumpCoyoteTime + Epsilon;
            var jumpDownRecent = (jumpInput && Time.fixedTime - lastJumpDownInputTime < jumpSpoolTime + Epsilon);
            if (groundedRecent && jumpRemainingTimes > 0 && jumpDownRecent)
            {
                velocity.y = jumpSpeed;

                jumping = true;

                jumpRemainingTimes--;
                lastJumpDownInputTime = 0f;
            }
            // else if (jumping && !Grounded && jumpInput)
            // {
            //     // accept jump hold gravity
            //     velocity.y += player.JumpHoldGravityScale * Time.fixedDeltaTime * Physics2D.gravity.y;
            // }
            else
            {
                // accept gravity
                velocity.y += Time.fixedDeltaTime * Physics2D.gravity.y;
            }

            // clamp velocity(Y)
            if (velocity.y < -dropSpeed)
            {
                velocity.y = -dropSpeed;
            }

            Velocity = velocity;
            // Debug.Log("player.Velocity" + Velocity);
        }

        protected override void UpdateGrounded(bool grounded)
        {
            if (grounded)
            {
                // energy = energyMax;
                jumpRemainingTimes = jumpMaxTimes;
                // dashRemainingTimes = dashMaxTimes;
                LastGroundedTime = Time.fixedTime;
            }
        }

        public void HandleMoveInput(InputAction.CallbackContext ctx)
        {
            // Debug.Log("HandleMoveInput ctx:" + ctx);
            if (ctx.phase == InputActionPhase.Performed || ctx.phase == InputActionPhase.Canceled)
            {
                var value = ctx.ReadValue<Vector2>();
                // Debug.Log("HandleMoveInput value:" + value);
                moveInput = value;
            }
        }

        public void HandleJumpInput(InputAction.CallbackContext ctx)
        {
            // Debug.Log("HandleJumpInput ctx:" + ctx);
            if (ctx.phase == InputActionPhase.Started || ctx.phase == InputActionPhase.Canceled)
            {
                var value = ctx.ReadValue<float>();
                var jump = Math.Abs(value - 1) < 0.001f;
                jumpInput = jump;
                if (jump) lastJumpDownInputTime = Time.fixedTime;
                // Debug.Log("HandleJumpInput jump:" + jump);
            }
        }

        public async void Disable()
        {
            // Destroy(cd);
            await UniTask.Delay(250);
            Destroy(this);
        }

        protected override void Caught()
        {
            cd.enabled = false;
            GameCtlr.Instance.LoseGame();
        }

        public void Dead()
        {
            Destroy(gameObject);
        }
    }

    public interface IPlayerInputActions
    {
        void HandleMoveInput(InputAction.CallbackContext ctx);
        void HandleJumpInput(InputAction.CallbackContext ctx);
    }

    public interface IPlayerActions { }
}