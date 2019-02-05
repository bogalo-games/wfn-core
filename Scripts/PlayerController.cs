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

    private class DirectionState : ConditionState<Vector2, GameWorld>
    {
      public DirectionState(float dx, float dy) : base(new Vector2(dx, dy)) { }
    }

    private FSM<Vector2, GameWorld> directionFSM;
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

      directionFSM = new FSM<Vector2, GameWorld>(idle);
    }

    public override void _Process(float delta)
    {
      GameWorld currentWorld = new GameWorld();

      currentWorld.LeftPressed = Input.IsActionPressed("p1_left");
      currentWorld.RightPressed = Input.IsActionPressed("p1_right");

      directionFSM.Tick(lastWorld, currentWorld);
      lastWorld = currentWorld;

      directionFSM.Tick(lastWorld, currentWorld);
      Translate(directionFSM.CurrentState.Data * delta);
    }
  }
}
