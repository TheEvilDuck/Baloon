using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInput
{
    public class MobileInput : IPlayerInput
    {
        public event Action mainActionStarted;
        public event Action mainActionEnded;
        public event Action mainAction;

        private bool _touch = false;
        private bool _blocked = false;

        public void Update()
        {
            if (_blocked)
                return;

            if (Input.touchCount>0)
            {
                mainAction?.Invoke();

                if (!_touch)
                {
                    _touch = true;
                    mainActionStarted?.Invoke();
                }
            }

            if (Input.touchCount==0&&_touch)
            {
                _touch = false;
                mainActionEnded?.Invoke();
            }
        }

        public void Block() => _blocked = true;

        public void Enable() => _blocked = false;
    }
}
