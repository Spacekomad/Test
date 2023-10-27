using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Globals
{
    public static class RubyAnimation
    {
        public static int Speed = Animator.StringToHash("Speed");
        public static int LookX = Animator.StringToHash("Look X");
        public static int LookY = Animator.StringToHash("Look Y");
        public static int Hit = Animator.StringToHash("Hit");
        public static int Launch = Animator.StringToHash("Launch");
    }

    public static class EnemyAnimation
    {
        public static int MoveX = Animator.StringToHash("MoveX");
        public static int MoveY = Animator.StringToHash("MoveY");
        public static int Fixed = Animator.StringToHash("Fixed");
    }
}
