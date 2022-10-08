using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsTable : MonoBehaviour
{
    [Header("Cells Table")]
    [SerializeField] private GameObject[] _cells;
    [Header("Cubs in Table")]
    [SerializeField] private GameObject[] _cubsInTable;
    [Header("Cubs player")]
    [SerializeField] private GameObject _cubPlayer;

    private List<int> indexPositonsInTable = new List<int>(); // Stores the position index of a cube in a table
    public List<int> IndexPositionsInTable { get { return indexPositonsInTable; } set { indexPositonsInTable = value; } }

    private Vector3 _startPositionsCubePlayer; // Stores the starting position of the main cube
    private Transform _startParentCubePlayer; // Stores the starting parent of the main cube

    private List<GameObject> _cellsCompleted; //Saves cells that are not empty
    private float _sizeConversionSpeed = 4f; // The rate of disappearance
    private Vector3 _deactiveScale = new Vector3(0.2f, 0.2f, 0.2f); //Size when disappearing

    public static CellsTable Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _startPositionsCubePlayer = _cubPlayer.transform.position;
        _startParentCubePlayer = _cubPlayer.transform.parent;

        if (indexPositonsInTable.Count > 0)
            LoadPositionsCubs();
    }

    //Sorting Cubs random in slots
    public void Sorting()
    {
        if (indexPositonsInTable.Count > 0)
            indexPositonsInTable.Clear();

        List<GameObject> Cells = new List<GameObject>();

        for (int x = 0; x < _cells.Length; x++)
            Cells.Add(_cells[x]);

        for (int x = 0; x < _cubsInTable.Length; x++)
        {
            int RandomPositon = Random.Range(0, Cells.Count);

            _cubsInTable[x].transform.position = Cells[RandomPositon].transform.position;
            _cubsInTable[x].transform.SetParent(Cells[RandomPositon].transform, true);

            indexPositonsInTable.Add(RandomPositon);
            Cells.RemoveAt(RandomPositon);
        }

        Cells.Clear();
    }

    public void MainCubeInCell()
    {
        if (CheckCell(0))
            StartCoroutine(DeActiveCellsAnim());
        else
            GameManager.Instance.ResultGame(false);
    }

    //Checking a cell by value
    private bool CheckCell(int value)
    {
        return CheckIfCellsMatch(0, 1, 2, value)
               || CheckIfCellsMatch(3, 4, 5, value)
               || CheckIfCellsMatch(6, 7, 8, value)
               || CheckIfCellsMatch(0, 3, 6, value)
               || CheckIfCellsMatch(1, 4, 7, value)
               || CheckIfCellsMatch(2, 5, 8, value);
    }

    private bool CheckIfCellsMatch(int a, int b, int c, int value)
    {
        //Checking if cells are empty vertically or horizontally
        bool check = _cells[a].transform.childCount > value && _cells[a].transform.childCount == _cells[b].transform.childCount &&
           _cells[b].transform.childCount == _cells[c].transform.childCount;

        if (check)
        {
            //Adding filled cells to a separate sheet
            _cellsCompleted = new List<GameObject> { _cells[a], _cells[b], _cells[c] };
        }

        return check;
    }

    //Loading cubs positions in a table
    public void LoadPositionsCubs()
    {
        for (int x = 0; x < indexPositonsInTable.Count; x++)
        {
            _cubsInTable[x].transform.position = _cells[indexPositonsInTable[x]].transform.position;
            _cubsInTable[x].transform.SetParent(_cells[indexPositonsInTable[x]].transform, true);
        }
    }

    //Reset the position of the main cube
    public void ResetPositonPlayer()
    {
        _cubPlayer.transform.position = _startPositionsCubePlayer;
        _cubPlayer.transform.SetParent(_startParentCubePlayer, true);
    }

    //Deaccivation of non-empty cells
    private IEnumerator DeActiveCellsAnim()
    {
        for (int x = 0; x < _cellsCompleted.Count; x++)
        {
            while (_cellsCompleted[x].transform.localScale != _deactiveScale)
            {
                _cellsCompleted[x].transform.localScale = Vector3.MoveTowards(_cellsCompleted[x].transform.localScale, _deactiveScale, _sizeConversionSpeed * Time.deltaTime);
                yield return null;
            }

            _cellsCompleted[x].SetActive(false);
        }

        _cellsCompleted.Clear();

        GameManager.Instance.ResultGame(true);
        yield break;
    }
}
