using System.Collections.Generic;
using System;

namespace WfnCore.Common
{
  /**
   * A delegate representing the condition that one should move to another
   * state, given the state of the world last frame and this frame.
   */
  public delegate bool Condition<World>(World lastWorld, World currentWorld);

  /**
   * A partial implementation of a state that transitions to another state only
   * when some Condition delegate is satisfied.
   */
  public abstract class ConditionState<T, World> : IState<T, World>
  {
    private List<Tuple<Condition<World>, IState<T, World>>> transitions;
    protected T data;

    /**
     * Abstract implementation of the data type, so that it must be implemented
     * in the deriving class.
     */
    public T Data { get { return data; } }

    /**
     * Construct a ConditionState with default data and an existing list of
     * transitions.
     */
    public ConditionState(T data, List<Tuple<Condition<World>, IState<T, World>>> transitions)
    {
      this.data = data;
      this.transitions = transitions;
    }

    /**
     * Construct a ConditionState with default data without any transitions.
     * Used in tandem with `addTransition` to add cyclic dependencies.
     */
    public ConditionState(T data)
    {
      this.data = data;
      this.transitions = new List<Tuple<Condition<World>, IState<T, World>>>();
    }

    /**
     * Adds a transition to the existing list of transitions.
     */
    public void AddTransition(Condition<World> condition, IState<T, World> state)
    {
      transitions.Add(new Tuple<Condition<World>, IState<T, World>>(condition, state));
    }

    /**
     * An implementation of Tick that transitions to the first state that has
     * as satisfied Condition.
     */
    public IState<T, World> Tick(World lastWorld, World currentWorld)
    {
      foreach (var tuple in transitions)
      {
        if (tuple.Item1(lastWorld, currentWorld))
        {
          return tuple.Item2;
        }
      }

      return this;
    }
  }
}
