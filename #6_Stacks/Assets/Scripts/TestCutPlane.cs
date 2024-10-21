using UnityEngine;

public class TestCutPlane : MonoBehaviour
{
    [SerializeField] private Transform _currentBlockTransform;
    [SerializeField] private Transform _previousBlockTransform;

    private Vector3 _currentblockScale;
    private Vector3 _currentBlockPosition;
    private float _currentLeftX;
    private float _currentRightX;

    private Vector3 _previousBlockScale;
    private Vector3 _previousBlockPosition;
    private float _previousLeftX;
    private float _previousRightX;

    Material _blockMaterial;

    private void Start()
    {
        InitializeFields();
        CutBlock();
    }

    private void InitializeFields()
    {
        _currentblockScale = _currentBlockTransform.localScale;
        _currentBlockPosition = _currentBlockTransform.position;
        _currentLeftX = _currentBlockPosition.x - _currentblockScale.x / 2;
        _currentRightX = _currentBlockPosition.x + _currentblockScale.x / 2;

        _previousBlockScale = _previousBlockTransform.localScale;
        _previousBlockPosition = _previousBlockTransform.position;
        _previousLeftX = _previousBlockPosition.x - _previousBlockScale.x / 2;
        _previousRightX = _previousBlockPosition.x + _previousBlockScale.x / 2;

        _blockMaterial = _currentBlockTransform.GetComponent<MeshRenderer>().material;
    }

    public void CutBlock()
    {
        //Если текущий куб не попал в пределы предыдущего куба
        if (_currentLeftX > _previousRightX || _currentRightX < _previousLeftX)
            Debug.Log("GG");
        else if (_currentBlockPosition.x < _previousBlockPosition.x)
        {
            Destroy(_currentBlockTransform.gameObject);

            // Правый куб остается
            GameObject newRightSideBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //Позиция правого куба (меняется только Х)
            float newRightPositionX = (_currentRightX + _previousLeftX) / 2;
            newRightSideBlock.transform.position = new Vector3(newRightPositionX, _currentBlockPosition.y, _currentBlockPosition.z);

            //Толщина правого куба
            float rightWidth = _currentRightX - _previousLeftX;
            newRightSideBlock.transform.localScale = new Vector3(rightWidth, _currentblockScale.y, _currentblockScale.z);
            newRightSideBlock.GetComponent<MeshRenderer>().material = _blockMaterial;

            //Левый куб падает
            GameObject newLeftSideBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //Позиция левого куба (меняется только Х)
            float newLeftPositionX = (_currentLeftX + _previousLeftX) / 2;
            newLeftSideBlock.transform.position = new Vector3(newLeftPositionX, _currentBlockPosition.y, _currentBlockPosition.z);

            ////Толщина левого куба
            float leftWidth = _previousLeftX - _currentLeftX;
            newLeftSideBlock.transform.localScale = new Vector3(leftWidth, _currentblockScale.y, _currentblockScale.z);
            newLeftSideBlock.GetComponent<MeshRenderer>().material = _blockMaterial;
            newLeftSideBlock.AddComponent<Rigidbody>().mass = 100f;
            newLeftSideBlock.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            Destroy(_currentBlockTransform.gameObject);

            //Левый куб остается
            GameObject newLeftSideBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //Позиция левого куба (меняется только Х)
            float newLeftPositionX = (_previousRightX + _currentLeftX) / 2;
            newLeftSideBlock.transform.position = new Vector3(newLeftPositionX, _currentBlockPosition.y, _currentBlockPosition.z);

            //Толщина левого куба
            float leftWidth = _previousRightX - _currentLeftX;
            newLeftSideBlock.transform.localScale = new Vector3(leftWidth, _currentblockScale.y, _currentblockScale.z);
            newLeftSideBlock.GetComponent<MeshRenderer>().material = _blockMaterial;

            //Правый куб падает
            GameObject newRightSideBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //Позиция правого куба (меняется только Х)
            float newRightPositionX = (_currentRightX + _previousRightX) / 2;
            newRightSideBlock.transform.position = new Vector3(newRightPositionX, _currentBlockPosition.y, _currentBlockPosition.z);

            //Толщина правого куба
            float rightWidth = _currentRightX - _previousRightX;
            newRightSideBlock.transform.localScale = new Vector3(rightWidth, _currentblockScale.y, _currentblockScale.z);
            newRightSideBlock.GetComponent<MeshRenderer>().material = _blockMaterial;
            newRightSideBlock.AddComponent<Rigidbody>().mass = 100f;
            newRightSideBlock.GetComponent<BoxCollider>().enabled = false;
        }
    }
}