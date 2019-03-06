using Godot;
using System;

using WfnCore.Common;
using WfnCore.Game;

namespace WfnCore
{
  public class PlayerController : Node2D
  {
    private GameWorld lastWorld;
    private GameWorld currentWorld;

    private FSM<PlayerStateInfo, GameWorld> playerFSM;

    public override void _Ready()
    {
      lastWorld = new GameWorld();

      // // Creating all of the possible states
      // DirectionState walkBack = new DirectionState(-100.0f, 0.0f);
      // DirectionState idle = new DirectionState(0.0f, 0.0f);
      // DirectionState walkForward = new DirectionState(100.0f, 0.0f);

      // // Manually constructing each of the node's connections.
      // walkBack.AddTransition(
      //   delegate (GameWorld lastWorld, GameWorld currentWorld)
      //   {
      //     return !currentWorld.LeftPressed && !currentWorld.RightPressed;
      //   },
      //   idle
      // );

      // walkBack.AddTransition(
      //   delegate (GameWorld lastWorld, GameWorld currentWorld)
      //   {
      //     return currentWorld.RightPressed;
      //   },
      //   walkForward
      // );

      // idle.AddTransition(
      //   delegate (GameWorld lastWorld, GameWorld currentWorld)
      //   {
      //     return currentWorld.LeftPressed;
      //   },
      //   walkBack
      // );

      // idle.AddTransition(
      //   delegate (GameWorld lastWorld, GameWorld currentWorld)
      //   {
      //     return currentWorld.RightPressed;
      //   },
      //   walkForward
      // );

      // walkForward.AddTransition(
      //   delegate (GameWorld lastWorld, GameWorld currentWorld)
      //   {
      //     return currentWorld.LeftPressed;
      //   },
      //   walkBack
      // );

      // walkForward.AddTransition(
      //   delegate (GameWorld lastWorld, GameWorld currentWorld)
      //   {
      //     return !currentWorld.LeftPressed && !currentWorld.RightPressed;
      //   },
      //   idle
      // );

      // directionFSM = new FSM<PlayerState, GameWorld>(idle);
    }

    public override void _Process(float delta)
    {
      // GameWorld currentWorld = new GameWorld();

      // currentWorld.LeftPressed = Input.IsActionPressed("p1_left");
      // currentWorld.RightPressed = Input.IsActionPressed("p1_right");

      // directionFSM.Tick(lastWorld, currentWorld);
      // lastWorld = currentWorld;

      // directionFSM.Tick(lastWorld, currentWorld);
      // Translate(directionFSM.CurrentState.Data * delta);
    }
  }
}
