﻿using System;
using Microsoft.Xna.Framework.Input;

namespace Pixel3D.Network.Rollback.Input // Put it in with the InputState struct
{
    public static class NetworkInput
    {
        public static InputState MapInputs(this KeyboardState keyboardState, Keys[] keyboardMap)
        {
            if(keyboardMap.Length > 32)
                throw new ArgumentException("Too many inputs specified");

            InputState output = 0;
            for(int i = 0; i < keyboardMap.Length; i++)
            {
                if(keyboardState.IsKeyDown(keyboardMap[i]))
                    output |= (InputState)(1u << i);
            }

            return output;
        }

        public static InputState MapInputs(this GamePadState gamePadState, Buttons[] gamePadMap)
        {
            if(gamePadMap.Length > 32)
                throw new ArgumentException("Too many inputs specified");

            InputState output = 0;
            for(int i = 0; i < gamePadMap.Length; i++)
            {
                if(gamePadState.IsButtonDown(gamePadMap[i]))
                    output |= (InputState)(1u << i);
            }

            return output;
        }

    }
}
