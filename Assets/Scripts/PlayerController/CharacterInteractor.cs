using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractor : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        InteractablePart _isResource = collision.GetComponent<InteractablePart>();
        if (_isResource)
        {
            IDestructible destructible = _isResource.GetMainGameObject().GetComponent<Loot>() as IDestructible;
            if (destructible != null)
            {
                destructible.DestroyAndGetResources();
            }
        }
    }
}
