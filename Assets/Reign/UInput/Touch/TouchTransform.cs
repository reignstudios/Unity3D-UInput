using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reign
{
    public abstract class TouchTransform : MonoBehaviour
    {
        public bool autoScale = true;
        public float scale = 1;
        public Vector2 position;
        public TouchAlignment alignment = TouchAlignment.Center;
        
        protected Vector2 GetAnchor()
        {
            Vector2 anchor;
            switch (alignment)
            {
                case TouchAlignment.Center: anchor = new Vector2(Screen.width, Screen.height) * .5f; break;

                case TouchAlignment.Left: anchor = new Vector2(0, Screen.height * .5f); break;
                case TouchAlignment.Right: anchor = new Vector2(Screen.width, Screen.height * .5f); break;
                case TouchAlignment.Top: anchor = new Vector2(Screen.width * .5f, 0); break;
                case TouchAlignment.Bottom: anchor = new Vector2(Screen.width * .5f, Screen.height); break;

                case TouchAlignment.TopLeft: anchor = new Vector2(0, 0); break;
                case TouchAlignment.BottomLeft: anchor = new Vector2(0, Screen.height); break;
                case TouchAlignment.TopRight: anchor = new Vector2(Screen.width, 0); break;
                case TouchAlignment.BottomRight: anchor = new Vector2(Screen.width, Screen.height); break;

                default: anchor = Vector2.zero; break;
            }
            return anchor;
        }

        protected Rect GetElementTransform(float elementScale)
        {
            var anchor = GetAnchor();
            var size = 64 * scale * elementScale;
            float sizeHalf = size * .5f;
            var pos = anchor + (position * scale);
            pos.x -= sizeHalf;
            pos.y -= sizeHalf;
            return new Rect(pos.x, pos.y, size, size);
        }

        protected Rect GetElementTransform(float elementScale, Vector2 pos)
        {
            var anchor = GetAnchor();
            var size = 64 * scale * elementScale;
            float sizeHalf = size * .5f;
            pos.x -= sizeHalf;
            pos.y -= sizeHalf;
            return new Rect(pos.x, pos.y, size, size);
        }
    }
}