using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public enum MainState{
Tutorial,
Free,
DroneFree,
DroneRace
}

//Floppy Po Game States.
public enum GameState
{
    Intro,
    Playing,
    Dead
}

public static class GameStateManager
{
    public static GameState GameState { get; set; }
    public static MainState MainState {get; set;}

    static GameStateManager ()
    {
        GameState = GameState.Intro;
    }



}

