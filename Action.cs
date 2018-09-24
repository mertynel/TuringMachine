using System;

namespace Turing_Machiene
{
    public class Action
    {
        public string currentSymbol { get; set; }
        public string nextSymbol { get; set; }
        public string moveDirection { get; set; }
        public int nextHeadState { get; set; }
    }
}