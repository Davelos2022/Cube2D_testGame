using System.Collections;
using UnityEngine;

public class SlotsActivator : MonoBehaviour
{
    [Header("Game slots")]
    [SerializeField] private GameObject[] Slots;
    [Header("Cell spawn rate at start")]
    [SerializeField] private float _sizeConversionSpeed;

    private Vector3 _defaultSize = new Vector3(1, 1, 1);

    private void Start()
    {
        Active();
    }

    public void Active()
    {
        StartCoroutine(SlotsAnim());
    }

    //Anim scale slots to startGame
    private IEnumerator SlotsAnim()
    {
        //Disable slots
        for (int x = 0; x < Slots.Length; x++)
            Slots[x].SetActive(false);

        for (int x = 0; x < Slots.Length; x++)
        {
            Slots[x].transform.localScale /= 2;
            Slots[x].SetActive(true);

            while (Slots[x].transform.localScale != _defaultSize)
            {
                Slots[x].transform.localScale = Vector3.MoveTowards(Slots[x].transform.localScale, _defaultSize, _sizeConversionSpeed * Time.deltaTime);
                yield return null;
            }
        }

        yield break;
    }
}
