using UnityEngine;

public interface Iinteractable
{
    //No public/private modifier is allowed here
    //Because interface members are public by definition
    void Interact(GameObject interactor);
}
