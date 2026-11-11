using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reign
{
    [DefaultExecutionOrder(-(32000 - 1))]
    public sealed class TouchButton : TouchTransform
    {
        public Texture2D buttonPad;
        
        private int touchIndex = -1;
        private EventType touchEventDown, touchEventUp;
        
        public bool on { get; private set; }
        public bool down { get; private set; }
        public bool up { get; private set; }
        
        private void Start()
        {
            #if UNITY_EDITOR
            touchEventDown = EventType.MouseDown;
            touchEventUp = EventType.MouseUp;
            #else
            touchEventDown = EventType.TouchDown;
            touchEventUp = EventType.TouchUp;
            #endif
        }
        
        private void OnGUI()
        {
            down = false;
            up = false;
            var e = Event.current;
            if (e.type == touchEventDown)
            {
                on = true;
                down = true;
                touchIndex = e.button;
            }
            else if (e.type == touchEventUp)
            {
                on = false;
                up = true;
                touchIndex = e.button;
            }
        }
    }
}