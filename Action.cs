using System;

namespace Turing_Machiene
{
public class Action {
    public string currentState { get; set; }
    public string nextState { get; set; }
    public string moveDirection { get; set; }
    public int nextPosition { get; set; }
}
}