using UnityEngine;

public abstract class Item : MonoBehaviour, ITriggerObject
{
    public virtual void Include(bool isInclude)
    {
        gameObject.SetActive(isInclude);
    }

    protected abstract void OnTriggerEnter(Collider other);

    protected virtual void OnTriggerExit(Collider other) { }
}
