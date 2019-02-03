using Godot;
using System;

using WfnCore.Common;

namespace WfnCore
{
  public class PlayerController : Node2D
  {
    public struct GameWorld
    {
      public bool LeftPressed;
      public bool RightPressed;
    }

    private struct Direction
    {
      public float dx;
      public float dy;
    }

    private class DirectionState : ConditionState<Direction, GameWorld>
    {
      private Direction data;

      public override Direction Data { get { return data; } }

      public DirectionState(float dx, float dy)
      {
        data = new Direction();
        data.dx = dx;
        data.dy = dy;
      }
    }

    private FSM<Direction, GameWorld> directionFSM;
    private GameWorld lastWorld;

    public override void _Ready()
    {
      lastWorld = new GameWorld();

      // Creating all of the possible states
      DirectionState walkBack = new DirectionState(-100.0f, 0.0f);
      DirectionState idle = new DirectionState(0.0f, 0.0f);
      DirectionState walkForward = new DirectionState(100.0f, 0.0f);

      // Manually constructing each of the node's connections.
      walkBack.AddTransition(
        delegate (GameWorld lastWorld, GameWorld currentWorld)
        {
          return !currentWorld.LeftPressed && !currentWorld.RightPressed;
        },
        idle
      );

      walkBack.AddTransition(
        delegate (GameWorld lastWorld, GameWorld currentWorld)
        {
          return currentWorld.RightPressed;
        },
        walkForward
      );

      idle.AddTransition(
        delegate (GameWorld lastWorld, GameWorld currentWorld)
        {
          return currentWorld.LeftPressed;
        },
        walkBack
      );

      idle.AddTransition(
        delegate (GameWorld lastWorld, GameWorld currentWorld)
        {
          return currentWorld.RightPressed;
        },
        walkForward
      );

      walkForward.AddTransition(
        delegate (GameWorld lastWorld, GameWorld currentWorld)
        {
          return currentWorld.LeftPressed;
        },
        walkBack
      );

      walkForward.AddTransition(
        delegate (GameWorld lastWorld, GameWorld currentWorld)
        {
          return !currentWorld.LeftPressed && !currentWorld.RightPressed;
        },
        idle
      );

      directionFSM = new FSM<Direction, GameWorld>(idle);
    }

    public override void _Process(float delta)
    {
      GameWorld currentWorld = new GameWorld();

      currentWorld.LeftPressed = Input.IsActionPressed("p1_left");
      currentWorld.RightPressed = Input.IsActionPressed("p1_right");

      directionFSM.Tick(lastWorld, currentWorld);
      lastWorld = currentWorld;

      directionFSM.Tick(lastWorld, currentWorld);
      Translate(new Vector2(directionFSM.CurrentState.Data.dx, directionFSM.CurrentState.Data.dy) * delta);
    }
  }
}
