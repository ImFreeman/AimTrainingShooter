using System;
using Zenject;

namespace Features.Input
{
    public class MouseButtonInput : InputElement, IFireButtonInput
    {
        public event EventHandler FireButtonPressedEvent;

        public MouseButtonInput(TickableManager tickableManager) : base(tickableManager)
        {
        }

        public override void Tick()
        {
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Mouse0))
            {
                FireButtonPressedEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}