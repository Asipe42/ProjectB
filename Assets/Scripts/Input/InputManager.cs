using System;
using UnityEngine;

namespace Modin
{
    public class InputManager : MonoSingleton<InputManager>
    {
        private IInputHandler currentHandler;
        
        public void RegisterHandler(IInputHandler inputHandler)
        {
            currentHandler = inputHandler;
        }

        public void Clear()
        {
            currentHandler = null;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentHandler?.OnSpacebar();
            }
        }
    }
}