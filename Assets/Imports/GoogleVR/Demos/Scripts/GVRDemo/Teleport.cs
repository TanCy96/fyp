using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Material inactiveMaterial;
    public Material gazedAtMaterial;


    void Start()
    {
        SetGazedAt(false);
    }

    public void SetGazedAt(bool gazedAt)
    {
        if (gazedAt)
        {
            GetComponent<Renderer>().material = gazedAtMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = inactiveMaterial;
        }
    }

    public void TeleportRandomly()
    {
        Vector3 direction = Random.onUnitSphere;
        direction.y = Mathf.Clamp(direction.y, 0.5f, 1f);
        float distance = 2 * Random.value + 1.5f;
        transform.localPosition = direction * distance;
    }
}
