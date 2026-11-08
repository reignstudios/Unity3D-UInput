using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reign
{
    public class TouchJoystick : MonoBehaviour
    {
        public Texture2D stickPad;

        public bool autoScale = true;
        public float scale = 1;
        public Vector2 position;
        public TouchAlignment alignment = TouchAlignment.Center;

        private bool drag, dragIsTouch;
        private int dragIndex = -1;
        private EventType dragEventDown, dragEventUp;

        public Vector2 value { get; private set; }

        private void Start()
        {
            #if UNITY_EDITOR
            dragIsTouch = false;
            dragEventDown = EventType.MouseDown;
            dragEventUp = EventType.MouseUp;
            #else
            dragIsTouch = true;
            dragEventDown = EventType.TouchDown;
            dragEventUp = EventType.TouchUp;
            #endif
        }

        private void OnGUI()
        {
            // draw base
            GUI.color = new Color(1, 1, 1, .1f);
            var rect = GetElementTransform(2);
            GUI.DrawTexture(rect, stickPad);

            // get click
            var e = Event.current;
            if (e.type == dragEventDown)
            {
                if (rect.Contains(e.mousePosition))
                {
                    drag = true;
                    dragIndex = e.button;
                }
            }

            // get input down
            var dragPos = Vector2.zero;
            if (drag)
            {
                if (dragIsTouch)
                {
                    dragPos = UInput.GetTouch(dragIndex);
                }
                else
                {
                    dragPos = UInput.mousePosition;
                }
                dragPos.y = Screen.height - dragPos.y;

                var center = GetAnchor() + position;
                float d = Vector2.Distance(center, dragPos);
                float r = 64 * scale;
                if (d > r * 4) drag = false;
                //if (d > r) dragPos = center + (dragPos - center).normalized * r;
                var v = (dragPos - center) / r;
                value = new Vector2(v.x, -v.y);
                if (value.magnitude > 1) value = value.normalized;
            }
            else
            {
                value = Vector2.zero;
            }

            // draw stick
            GUI.color = Color.white;
            if (drag) rect = GetElementTransform(1, dragPos);
            else rect = GetElementTransform(1);
            GUI.DrawTexture(rect, stickPad);

            // get input up
            if (e.type == dragEventUp && e.button == dragIndex)
            {
                if (rect.Contains(e.mousePosition))
                {
                    drag = false;
                    dragIndex = -1;
                }
            }
        }

        private Vector2 GetAnchor()
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

        private Rect GetElementTransform(float elementScale)
        {
            var anchor = GetAnchor();
            var size = 64 * scale * elementScale;
            float sizeHalf = size * .5f;
            var pos = anchor + (position * scale);
            pos.x -= sizeHalf;
            pos.y -= sizeHalf;
            return new Rect(pos.x, pos.y, size, size);
        }

        private Rect GetElementTransform(float elementScale, Vector2 pos)
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