using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reign
{
    [DefaultExecutionOrder(-(32000 - 1))]
    public sealed class TouchJoystick : TouchTransform
    {
        public Texture2D stickPad;

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
    }
}