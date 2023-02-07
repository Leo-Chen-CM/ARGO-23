/// <summary>
/// Our jump command which will be shared with player and AI bots
/// Worked on by: Jack Sinnott
/// </summary>

using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Controls/Commands/JumpingCommand")]
public class CommandJump : ICommand
{
    [Range(0, 20f)] [SerializeField] private float _jumpVelocity = 16f;
    [Range(0, 10f)] [SerializeField] private float _fallLongMult = 0.85f;
    [Range(0, 10f)] [SerializeField] private float _fallShortMult = 1.55f;
    [SerializeField] private LayerMask _groundLayer;

    float lastJumpY = 0;
    bool jump = false, jumpHeld = false;

    public override void Execute(InputAction action, GameObject gameObject)
    {
        if (isOnGround(gameObject) && action.WasPressedThisFrame()) 
            jump = true;
        jumpHeld = (!isOnGround(gameObject) && action.IsPressed()) ? true : false;

        if (!isOnGround(gameObject))
        {
            if (lastJumpY < gameObject.transform.position.y)
            {
                lastJumpY = gameObject.transform.position.y;
            }
        }
    }

    public override void FixedExecute(InputAction action, GameObject gameObject)
    {
        // Jumping...
        if (jump)
        {
            float jumpvel = 2f;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpvel;
            jump = false;
        }

        // Jumping High...
        if (jumpHeld && gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (_fallLongMult - 1) * Time.fixedDeltaTime;
        }
        // Jumping Low...
        else if (!jumpHeld && gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (_fallShortMult - 1) * Time.fixedDeltaTime;
        }
    }

    private bool isOnGround(GameObject gameObject)
    {
        RaycastHit2D hit = Physics2D.CircleCast(gameObject.GetComponent<CircleCollider2D>().bounds.center, gameObject.GetComponent<CircleCollider2D>().radius, Vector2.down, 0.1f, _groundLayer);
        if (hit && !lastJumpY.Equals(0)) lastJumpY = 0;
        return hit.collider != null;
    }
}
