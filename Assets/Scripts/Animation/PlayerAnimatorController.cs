using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int UseHash   = Animator.StringToHash("Use");
    private static readonly int JumpHash = Animator.StringToHash("Jumping");
    private Animator defaultAnimator;
    
    private void Awake()
    {
        if (!animator)
            animator = GetComponent<Animator>();

        defaultAnimator = animator;
    }
        
    public void PlayUse()
    {
        animator.SetTrigger(UseHash);
    }

    public void SetMovementSpeed(float speed)
    {
        animator.SetFloat(SpeedHash, speed);
    }

    public void PlayJump()
    {
        animator.SetTrigger(JumpHash);
    }

    public void ApplyOverride(RuntimeAnimatorController controller)
    {
        if (controller == null) return;
        animator.runtimeAnimatorController = controller;
    }

    public void ClearOverride()
    {
        animator = defaultAnimator;
    }
}

