using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private enum TypeCells { RegularŅell, TableCell }

    [Header("Type Cell")]
    [SerializeField] private TypeCells Cell;

    //Cell received an object
    public void OnDrop(PointerEventData eventData) 
    {
        if (eventData.pointerDrag != null && transform.childCount == 0)
        {
            if (Cell == TypeCells.RegularŅell && eventData.pointerDrag.transform.CompareTag("Cube"))
                return;

            eventData.pointerDrag.transform.SetParent(transform, true);
            eventData.pointerDrag.transform.position = transform.position;

            if (Cell == TypeCells.TableCell && eventData.pointerDrag.transform.CompareTag("Player"))
                CellsTable.Instance.MainCubeInCell();
        }
    }
}
