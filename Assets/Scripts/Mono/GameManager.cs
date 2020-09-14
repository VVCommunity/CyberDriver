using VV.Core;
public enum EStateGame
{
    world, construction, control, menu
}

public class GameManager : VVBehaviour
{ 
    public EStateGame stateGame;
}