using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _color1;
    [SerializeField] private Color _color2;
    [SerializeField] private float _changeSpeed;
    [SerializeField] private float _changePeriod;

    private Material _material;



    private void Awake() => _material = GetComponent<MeshRenderer>().material;

    private void Update() => _material.color = Color.Lerp(_color1, _color2, Mathf.PingPong(Time.time * _changeSpeed, _changePeriod));
}
