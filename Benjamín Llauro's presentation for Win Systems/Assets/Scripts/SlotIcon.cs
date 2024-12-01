using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotIcon : MonoBehaviour
{
    [SerializeField] private bool ghost;
    [SerializeField] private SpriteRenderer highlightSprite;
    [SerializeField] private SlotIcon ghostMimic;
    public enum Category { orange, bell, watermelon, cherries, blue, lemon, grapes}
    public Category iconType;

    public void Highlight(bool showSprite)
    {
        highlightSprite.enabled = showSprite;
        if(ghostMimic != null )
            ghostMimic.Highlight(showSprite);
    }
}