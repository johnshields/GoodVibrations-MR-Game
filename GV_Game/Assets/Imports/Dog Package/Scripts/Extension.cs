using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
    public class AnimationNames
    {
        public const string Bark = "Base Layer.Bark";
        public const string Bite = "Base Layer.Bite";
        public const string Die = "Base Layer.Die";
        public const string GetUpFromSit = "Base Layer.Get Up From Sit";
        public const string GetUpFromLyingDown = "Base Layer.Get Up From Lying Down";
        public const string Idle = "Base Layer.Idle";
        public const string Jump = "Base Layer.Jump";
        public const string LayDown = "Base Layer.Lay Down";
        public const string LyingDownIdle = "Base Layer.Lying Down Idle";
        public const string Run = "Base Layer.Run";
        public const string SitDown = "Base Layer.Sit Down";
        public const string SitIdle = "Base Layer.Sit Idle";
        public const string Surprised = "Base Layer.Surprise";
        public const string Walk = "Base Layer.Walk";
    }

    public static class AnimationParameters
    {
        public const string Bark = "Bark";
        public const string Bite = "Bite";
        public const string Die = "Die";
        public const string GetUp = "GetUp";
        public const string IsRunning = "IsRunning";
        public const string IsWalking = "IsWalking";
        public const string Jump = "Jump";
        public const string LayDown = "LayDown";
        public const string Sit = "Sit";
        public const string Surprise = "Surprise";
    }

    public static class AxisNames
    {
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
        public const string MouseX = "Mouse X";
        public const string MouseY = "Mouse Y";
    }

    public static class MouseButtonId
    {
        public const int LeftClick = 0;
        public const int RightClick = 1;
    }


    public static class Methods
    {
        /// <summary>
        /// Check if animation is being played
        /// </summary>
        /// <param name="animationName"></param>
        /// <returns></returns>
        public static bool AnimationBeingPlayed(Animator animator, string animationName)
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
        }
    }
}
