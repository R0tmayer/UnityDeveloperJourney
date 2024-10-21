using UnityEngine.Events;

public interface IFloatEventable
{
    event UnityAction<float> ValueSetted;
}
