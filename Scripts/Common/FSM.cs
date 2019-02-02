using System.Collections.Generic;
using System;

namespace WfnCore.Common
{
  /**
   * Provides the basic implementation of a finite state machine.
   */
  public class FSM<T, World>
  {
    private IState<T, World> currentState;

    /**
     * Retrieve the current state.
     */
    public IState<T, World> CurrentState { get { return currentState; } }

    /**
     * Constructs a finite state machine with an initial state. All selection
     * of state space is defined by the states.
     */
    public FSM(IState<T, World> initialState)
    {
      this.currentState = initialState;
    }

    /**
     * Ticks the FSM by checking if the currentState should transition.
     */
    public void Tick(World lastWorld, World currentWorld)
    {
      currentState = currentState.Tick(lastWorld, currentWorld);
    }
  }
}
