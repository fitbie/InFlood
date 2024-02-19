using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace InFlood.InputSystem
{
    /// <summary>
    /// Contains data, describing Touchscreen swipe.
    /// </summary>
    public readonly struct TouchSwipeData
    {
        public enum SwipeDirection { Left, Right, Up, Down }
        public readonly SwipeDirection swipeDirection;
        public readonly float length;
        public readonly float duration;



        public TouchSwipeData(Vector2 delta, float length, float duration)
        {
            swipeDirection = delta switch 
            {
                _ when Mathf.Abs(delta.x) > Mathf.Abs(delta.y) && delta.x >= 0 => SwipeDirection.Right,
                _ when Mathf.Abs(delta.x) > Mathf.Abs(delta.y) && delta.x < 0 => SwipeDirection.Left,
                _ when Mathf.Abs(delta.y) >= Mathf.Abs(delta.x) && delta.y >= 0 => SwipeDirection.Up,
                _ when Mathf.Abs(delta.y) >= Mathf.Abs(delta.x) && delta.y < 0 => SwipeDirection.Down,
                _ => throw new System.Exception("Invalid swipe delta!")
            };
            this.length = length; 
            this.duration = duration;
        }


        public TouchSwipeData(TouchState touch) : this
        (touch.delta,
        
        //Divide current resolution to 1920*1080 resolution to get proper length multiplier.
        (Screen.currentResolution.width * Screen.currentResolution.height / 2073600) * 
        Vector2.Distance(touch.position, touch.startPosition),

        (float)(Time.realtimeSinceStartup - touch.startTime))
        {}


        public override string ToString()
        {
            return $"Direction: {swipeDirection}, Length: {length}, Duration: {duration}";
        }

    }

}