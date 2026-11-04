
using System;
using UnityEngine;

public class PaintableTask : Interactuable
{
    public event Action<PaintableTask> OnCompleted;

    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private Material unpaintedMaterial;

    private Material[] originalMaterials;
    private bool isSelected;
    private bool isCompleted;

    public bool IsCompleted => isCompleted;
    public override bool CanInteract => isSelected && !isCompleted;

    private void Awake()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponentInChildren<Renderer>();

        if (targetRenderer != null)
            originalMaterials = targetRenderer.sharedMaterials;
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        isCompleted = false;

        if (targetRenderer == null || originalMaterials == null)
            return;

        if (selected)
        {
            if (unpaintedMaterial == null)
            {
                Debug.LogError("Falta material sin color en." + name);
                return;
            }

            Material[] greyMaterials = new Material[originalMaterials.Length];

            for (int i = 0; i < greyMaterials.Length; i++)
            {
                greyMaterials[i] = unpaintedMaterial;

                targetRenderer.sharedMaterials = greyMaterials;
            }
        }

        else
        {
            targetRenderer.sharedMaterials = originalMaterials;
        }
    }

    public override void Interact()
    {
        if (!CanInteract)
            return;

        isCompleted = true;
        targetRenderer.sharedMaterials = originalMaterials;

        OnCompleted?.Invoke(this);
        AudioManager.Instance.Play("PaintingEnd");
    }
}
