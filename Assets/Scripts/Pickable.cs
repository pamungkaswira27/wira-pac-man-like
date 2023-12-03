using System;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField]
    private PickableType _pickableType;

    public Action<Pickable> OnPicked;

    public PickableType PickableType => _pickableType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPicked(this);
            Destroy(gameObject);
        }
    }
}
