using Godot;
using System;
using System.Collections.Generic;
using WfnCore.Common;

namespace WfnCore.Game
{
  /**
   * Information that defines the current state of the player. Note that this
   * should only contain information that changes when the player changes
   * state, not global player information (e.g. position or health).
   */
  public class PlayerStateInfo
  {
    /**
     * The direction to translate the player every frame.
     */
    public Vector2 Direction;

    /**
     * The animation used to paint the player.
     */
    public AnimatedSprite Animation;

    /**
     * Default constructor of PlayerInformation that sets everything to its
     * default. One is supposed to construct this programmatically.
     */
    public PlayerStateInfo()
    {
      Direction = new Vector2();
      Animation = new AnimatedSprite();
    }
  }

  /**
   * The ConditionState that represents changes for a player. Provides the
   * developer the ability to define default info for a player state.
   */
  public class PlayerState : ConditionState<PlayerStateInfo, GameWorld>
  {
    public PlayerState(PlayerStateInfo info) : base(info) { }
  }
}
