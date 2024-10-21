using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class UIRecord : MonoBehaviour
{
    private TMP_Text _text;


    private void OnEnable() => _text.text = $"Record: {PlayerPrefs.GetInt("Record")}";

    private void Awake() => _text = GetComponent<TMP_Text>();
}
