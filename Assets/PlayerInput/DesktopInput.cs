using UnityEngine;
using System;

namespace PlayerInput
{
    public class DesktopInput: IPlayerInput
    {
        public event Action mainActionStarted;
        public event Action mainActionEnded;
        public event Action mainAction;
        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
                mainActionStarted?.Invoke();

            if (Input.GetMouseButtonUp(0))
                mainActionEnded?.Invoke();

            if (Input.GetMouseButton(0))
                mainAction?.Invoke();
        }
    }
}
