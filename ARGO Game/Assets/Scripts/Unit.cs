/// <summary>
/// Our player script which outlines all the logic the player follows
/// Worked on by: Jack Sinnott
/// </summary>

using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private bool _isActing;

    public void Move(Vector3 t_dir)
    {
        if (_isActing) return;
        StartCoroutine(MoveCoroutine(t_dir));
    }

    private IEnumerator MoveCoroutine(Vector3 t_dir)
    {
        _isActing = true;

        var targetPosition = transform.position + t_dir;

        yield return new WaitForSeconds(0.4f);
        yield return new WaitUntil(() =>
        {
            var currentPosition = transform.position;
            currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, Time.deltaTime * _moveSpeed);
            transform.position = currentPosition;

            return currentPosition == targetPosition;
        });

        _isActing = false;
    }
}
