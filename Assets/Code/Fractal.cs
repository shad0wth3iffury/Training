using System.Collections;
using UnityEngine;

public class Fractal : MonoBehaviour {

    public Mesh mesh;
    public Material material;
    public int maxDepth;
    public float childScale;

    public bool rotate = false;
    public float rotationSpeed = 10f;

    private int depth;

    private void Start()
    {
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        if (rotate)
        {
            gameObject.AddComponent<Rotator>().RotationSpeed = rotationSpeed;
        }

        if (depth < maxDepth)
        {
            CreateChildren();
        }
    }

   
    public void Initialize(Fractal parent, Vector3 direction, Quaternion orientation)
    {
        rotate = parent.rotate;
        rotationSpeed = parent.rotationSpeed;
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = direction * (0.5f + 0.5f * childScale);
        transform.localRotation = orientation;
    }

    private void CreateChildren()
    {
        new GameObject("Fractal Child").
                AddComponent<Fractal>().Initialize(this, Vector3.up, Quaternion.identity);
        new GameObject("Fractal Child").
                AddComponent<Fractal>().Initialize(this, Vector3.right, Quaternion.Euler(0f, 0f, -90f));
        new GameObject("Fractal Child").
                AddComponent<Fractal>().Initialize(this, Vector3.left, Quaternion.Euler(0f, 0f, 90f));
        new GameObject("Fractal Child").
                AddComponent<Fractal>().Initialize(this, Vector3.forward, Quaternion.Euler(90f, 0f, 0f));
        new GameObject("Fractal Child").
                AddComponent<Fractal>().Initialize(this, Vector3.back, Quaternion.Euler(-90f, 0f, 0f));
        if(depth == 0)
        {
            new GameObject("Fractal Child").
                AddComponent<Fractal>().Initialize(this, Vector3.down, Quaternion.Euler(0f, 0f, 180f));
        }
        
    }

}
