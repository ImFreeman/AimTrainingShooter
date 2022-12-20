using System;
namespace Features.Input
{
    public interface IFireButtonInput : IInputElement
    {
        public event EventHandler FireButtonPressedEvent;
    }
}