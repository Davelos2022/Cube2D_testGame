using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private enum TypeCells { Regular—ell, TableCell }

    [Header("Type Cell")]
    [SerializeField] private TypeCells Cell;

    //Cell received an object
    public void OnDrop(PointerEventData eventData) 
    {
        if (eventData.pointerDrag != null && transform.childCount == 0)
        {
            eventData.pointerDrag.transform.SetParent(transform, true);
            eventData.pointerDrag.transform.position = transform.position;

            if (Cell == TypeCells.TableCell && eventData.pointerDrag.transform.CompareTag("Player"))
                CellsTable.Instance.MainCubeInCell();
        }
    }
}
