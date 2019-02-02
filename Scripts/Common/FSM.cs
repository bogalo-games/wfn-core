using System.Collections.Generic;
using System;

namespace WfnCore::Common
{
  /**
   * Provides the basic implementation of a finite state machine.
   */
  class FSM<T>
  {
    private IState<T> currentState;

    /**
     * Retrieve the current state.
     */
    public IState<T> CurrentState { get { return currentState; } }

    /**
     * Constructs a finite state machine with an initial state. All selection
     * of state space is defined by the states.
     */
    public FSM(IState<T> initialStates)
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
