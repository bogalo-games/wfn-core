using System.Collections.Generic;

namespace WfnCore.Common
{
  /**
   * Provides the interface of a state in a finite state machine.
   */
  public interface IState<T>
  {
    /**
     * Return a state to transition out of this state. Return `this` to remain
     * in this state.
     */
    IState<T> Tick(GameWorld lastWorld, GameWorld currentWorld);

    /**
     * Get the information contained in this state, e.g. the player's current
     * move.
     */
    T Data
    {
      get;
    }
  }
}
