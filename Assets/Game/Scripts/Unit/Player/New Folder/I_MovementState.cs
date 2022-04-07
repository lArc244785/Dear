public interface I_MovementState
{
    void Enter(A_MovementManager manager);
    void UpdateExcute(A_MovementManager manager);
    void FixedExcute(A_MovementManager manager);
    void Exit(A_MovementManager manager);
}
