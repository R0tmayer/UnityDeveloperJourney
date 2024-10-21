using System.Collections;
using UnityEngine;

public class EnemyDissolvingPart : MonoBehaviour
{
    [SerializeField] private float _dissolveDelay;
    [SerializeField] private float _dissolveSpeed;

    private Vector3 _scale;


    private void OnEnable() => StartCoroutine(Dissolve());

    private void Awake() => _scale = transform.localScale;

    private IEnumerator Dissolve()
    {
        transform.localScale = _scale;

        yield return new WaitForSeconds(_dissolveDelay);
        while (transform.localScale != Vector3.zero)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime * _dissolveSpeed);

            yield return null;
        }
    }
}
