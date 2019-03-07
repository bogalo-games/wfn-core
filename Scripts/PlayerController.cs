using Godot;
using System;

using WfnCore.Common;
using WfnCore.Game;

namespace WfnCore
{
  public class PlayerController : Node2D
  {
    public float BackVelocity = 115;
    public float ForwardVelocity = 140;

    private GameWorld lastWorld;
    private GameWorld currentWorld;
    private AnimatedSprite currentAnimation;

    private FSM<PlayerStateInfo, GameWorld> playerFSM;

    public override void _Ready()
    {
      lastWorld = new GameWorld();
      currentWorld = new GameWorld();

      PlayerState walkBack = new PlayerState();
      walkBack.Data.Direction = Vector2.Left * BackVelocity;
      walkBack.Data.Animation = GetNode<AnimatedSprite>("WalkBack");

      PlayerState idle = new PlayerState();
      idle.Data.Direction = Vector2.Zero;
      idle.Data.Animation = GetNode<AnimatedSprite>("Idle");

      PlayerState walkForward = new PlayerState();
      walkForward.Data.Direction = Vector2.Right * ForwardVelocity;
      walkForward.Data.Animation = GetNode<AnimatedSprite>("WalkForward");

      // Manually constructing each of the node's connections.
      walkBack.AddTransition(
        delegate (GameWorld lastWorld, GameWorld currentWorld)
        {
          return !currentWorld.Player1Input.LeftPressed && !currentWorld.Player1Input.RightPressed;
        },
        idle
      );

      walkBack.AddTransition(
        delegate (GameWorld lastWorld, GameWorld currentWorld)
        {
          return currentWorld.Player1Input.RightPressed;
        },
        walkForward
      );

      idle.AddTransition(
        delegate (GameWorld lastWorld, GameWorld currentWorld)
        {
          return currentWorld.Player1Input.LeftPressed;
        },
        walkBack
      );

      idle.AddTransition(
        delegate (GameWorld lastWorld, GameWorld currentWorld)
        {
          return currentWorld.Player1Input.RightPressed;
        },
        walkForward
      );

      walkForward.AddTransition(
        delegate (GameWorld lastWorld, GameWorld currentWorld)
        {
          return currentWorld.Player1Input.LeftPressed;
        },
        walkBack
      );

      walkForward.AddTransition(
        delegate (GameWorld lastWorld, GameWorld currentWorld)
        {
          return !currentWorld.Player1Input.LeftPressed && !currentWorld.Player1Input.RightPressed;
        },
        idle
      );

      playerFSM = new FSM<PlayerStateInfo, GameWorld>(idle);
      currentAnimation = playerFSM.CurrentState.Data.Animation;
      Play(currentAnimation);
    }

    public override void _Process(float delta)
    {
      lastWorld = currentWorld;
      currentWorld = new GameWorld();

      currentWorld.Player1Input.LeftPressed = Input.IsActionPressed("p1_left");
      currentWorld.Player1Input.RightPressed = Input.IsActionPressed("p1_right");

      playerFSM.Tick(lastWorld, currentWorld);

      Translate(playerFSM.CurrentState.Data.Direction * delta);
      if (playerFSM.CurrentState.Data.Animation != currentAnimation)
      {
        Play(playerFSM.CurrentState.Data.Animation);
      }
    }

    private void Play(AnimatedSprite animation)
    {
      // Stop the last animation
      currentAnimation.Frame = 0;
      currentAnimation.Playing = false;
      currentAnimation.Visible = false;

      // Start the current animation
      currentAnimation = animation;

      currentAnimation.Frame = 0;
      currentAnimation.Playing = true;
      currentAnimation.Visible = true;
    }
  }
}
