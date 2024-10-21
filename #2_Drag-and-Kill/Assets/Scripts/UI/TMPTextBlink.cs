using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TMPTextBlink : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _period;

    private TMP_Text _text;
    private Color _color;


    private void Awake() => _text = GetComponent<TMP_Text>();

    private void Start() => _color = _text.color;

    private void Update()
    {
        _color.a = Mathf.PingPong(Time.time * _speed, _period);
        _text.color = _color;
    }
}
