using Godot;
using System;

using WfnCore.Common;

namespace WfnCore
{
  public class PlayerController : Node2D
  {
    /**
     * A data model of the game's state.
     */
    public struct GameWorld
    {
      public bool UpPressed;
      public bool DownPressed;
      public bool LeftPressed;
      public bool RightPressed;
    }

    private struct Direction
    {
      public float dx;
      public float dy;
    }

    private class DirectionState : IState<Direction, GameWorld>
    {
      private Direction data;

      public Direction Data { get { return data; } }

      public DirectionState(float dx, float dy)
      {
        data = new Direction();
        data.dx = dx;
        data.dy = dy;
      }

      public IState<Direction, GameWorld> Tick(GameWorld lastWorld, GameWorld currentWorld)
      {
        float ndx = 0;
        float ndy = 0;

        if (currentWorld.UpPressed)
        {
          ndy -= 100;
        }

        if (currentWorld.DownPressed)
        {
          ndy += 100;
        }

        if (currentWorld.LeftPressed)
        {
          ndx -= 100;
        }

        if (currentWorld.RightPressed)
        {
          ndx += 100;
        }

        return new DirectionState(ndx, ndy);
      }
    }

    private FSM<Direction, GameWorld> directionFSM;
    private GameWorld lastWorld;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      lastWorld = new GameWorld();
      directionFSM = new FSM<Direction, GameWorld>(new DirectionState(0.0f, 0.0f));
    }

    public override void _Process(float delta)
    {
      GameWorld currentWorld = new GameWorld();

      currentWorld.UpPressed = Input.IsActionPressed("up");
      currentWorld.DownPressed = Input.IsActionPressed("down");
      currentWorld.LeftPressed = Input.IsActionPressed("left");
      currentWorld.RightPressed = Input.IsActionPressed("right");

      directionFSM.Tick(lastWorld, currentWorld);
      lastWorld = currentWorld;

      Translate(new Vector2(directionFSM.CurrentState.Data.dx, directionFSM.CurrentState.Data.dy) * delta);
    }
  }
}
