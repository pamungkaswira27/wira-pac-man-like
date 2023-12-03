using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private List<Pickable> _pickableList = new List<Pickable>();

    private void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        Pickable[] pickables = FindObjectsOfType<Pickable>();

        for (int i = 0; i < pickables.Length; i++)
        {
            _pickableList.Add(pickables[i]);
            pickables[i].OnPicked += OnPickablePicked;
        }
    }

    private void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);

        if (pickable.PickableType == PickableType.PowerUp)
        {
            _player.PickPowerUp();
        }

        if (_pickableList.Count <= 0 )
        {
            Debug.Log($"[{nameof(PickableManager)}]: Win!");
        }
    }
}
