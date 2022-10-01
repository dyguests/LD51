using System;
using Scenes.Games.Physicses;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Games.Models
{
    public class PlayerCtlr : PhysicsObject, IPlayerInputActions
    {
        private const float Epsilon = 0.001f;

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
        [SerializeField] private float jumpSpoolTime = 3 * 0.02f;
        [SerializeField] private float jumpCoyoteTime = 3 * 0.02f;
        [SerializeField] private int jumpMaxTimes = 1;
        private int jumpRemainingTimes = 1;
        [SerializeField] private float jumpCost = 25f;
        [SerializeField] private AnimationCurve jumpInterpolator;

        #endregion

        #region Input Variants

        private Vector2 moveInput;

        private bool jumpInput;
        private float lastJumpDownInputTime = float.MinValue;

        #endregion

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

        protected override void HandleKinematics() { }

        protected override void UpdateGrounded(bool grounded) { }

        public void HandleMoveInput(InputAction.CallbackContext ctx)
        {
            Debug.Log("HandleMoveInput ctx:" + ctx);
            if (ctx.phase == InputActionPhase.Performed || ctx.phase == InputActionPhase.Canceled)
            {
                var value = ctx.ReadValue<Vector2>();
                Debug.Log("HandleMoveInput value:" + value);
                moveInput = value;
            }
        }

        public void HandleJumpInput(InputAction.CallbackContext ctx)
        {
            Debug.Log("HandleJumpInput ctx:" + ctx);
            if (ctx.phase == InputActionPhase.Started || ctx.phase == InputActionPhase.Canceled)
            {
                var value = ctx.ReadValue<float>();
                var jump = Math.Abs(value - 1) < 0.001f;
                jumpInput = jump;
                if (jump) lastJumpDownInputTime = Time.fixedTime;
                Debug.Log("HandleJumpInput jump:" + jump);
            }
        }
    }

    public interface IPlayerInputActions { }
}