namespace Infrastructure.States{
    public interface IState : IExitableState{
        void Enter();
    }

    public interface IExitableState{
        void Exit();
    }
    
    public interface IPayloadedState<TPayload> : IExitableState{
        void Enter(TPayload _payload);
    }
}