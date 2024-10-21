using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubesHolder : MonoBehaviour
{
    [SerializeField] private Transform _hero;
    private List<Cube> _allCubes = new List<Cube>();

    public Vector3 GetPositionForNewCube()
    {
        var heroPosition = _hero.position;
        return new Vector3(heroPosition.x, _allCubes.Count, heroPosition.z);
    }

    public void AddNewCube(Cube cube)
    {
        _allCubes.Add(cube);
        _allCubes[0].ActivateTrail();
        cube.transform.parent = transform;
    }

    public void RemoveCube(Cube cube)
    {
        _allCubes.Remove(cube);

        if (!_allCubes.Any())
        {
            UISingleton.Instance.ShowRestartLevelButton();
            Debug.Log("ТЫ ПРОИГРАЛ, КУБОВ НЕ ОСТАЛОСЬ!");
            return;
        }

        _allCubes[0].ActivateTrail();
        cube.transform.parent = null;
    }

    public void MoveHeroUp()
    {
        _hero.Translate(0, 2, 0);
    }
}
