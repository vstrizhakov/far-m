using UnityEngine;

public class ContextManager : MonoBehaviour
{
    public Context context;

    private void Awake()
    {
        context = new DefaultContext();
    }
}
