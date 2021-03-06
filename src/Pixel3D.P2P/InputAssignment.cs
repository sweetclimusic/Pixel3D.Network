﻿using System;
using Lidgren.Network;

namespace Pixel3D.P2P
{
    [Flags]
    public enum InputAssignment
    {
        None = 0x0,

        Player1 = 0x1,
        Player2 = 0x2,
        Player3 = 0x4,
        Player4 = 0x8,

        All = 0xF,
        Full = All,
    }


    public static class InputAssignmentExtensions
    {
        public const int MaxPlayerInputAssignments = 4;

        // There a bunch of bit-twiddling that could be done here, for some fun over-optimisation.

        public static InputAssignment GetNextAssignment(this InputAssignment ia)
        {
            for(int i = 0; i < MaxPlayerInputAssignments; i++)
            {
                if(((int)ia & (1 << i)) == 0)
                    return (InputAssignment)(1 << i);
            }

            return 0;
        }


        // TODO: To add support for multiple players per network peer, remove this (find-all-references to fix up the places it gets used)
        /// <returns>The first player assigned in a given assignment, or -1 if no players are assigned</returns>
        public static int GetFirstAssignedPlayerIndex(this InputAssignment ia)
        {
            for(int i = 0; i < MaxPlayerInputAssignments; i++)
            {
                if(((int)ia & (1 << i)) != 0)
                    return i;
            }

            return -1;
        }
        
        #region Network Read/Write

        public static void Write(this NetOutgoingMessage message, InputAssignment ia)
        {
            message.Write((uint)ia, MaxPlayerInputAssignments);
        }

        public static InputAssignment ReadInputAssignment(this NetIncomingMessage message)
        {
            return (InputAssignment)message.ReadUInt32(MaxPlayerInputAssignments);
        }

        #endregion

    }
}
