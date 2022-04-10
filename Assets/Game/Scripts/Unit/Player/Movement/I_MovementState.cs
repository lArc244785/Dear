public interface I_MovementState
{
    void Enter(PlayerMovementManager manager);
    void UpdateExcute(PlayerMovementManager manager);
    void FixedExcute(PlayerMovementManager manager);
    void Exit(PlayerMovementManager manager);
}
