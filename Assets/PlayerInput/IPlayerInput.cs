using System;

namespace PlayerInput
{
    public interface IPlayerInput
    {
        public event Action mainActionStarted;
        public event Action mainActionEnded;
        public event Action mainAction;
        public void Update();
    }
}
