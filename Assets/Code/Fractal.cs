using System.Collections;
using UnityEngine;

public class Fractal : MonoBehaviour {

    public Mesh[] meshs;
    public Material material;
    public int maxDepth;
    public float childScale;
    public float spawnProability;
    public float maxTwist;

    public bool rotate = false;
    public float rotationSpeed = 10f;

    private int depth;

    private void Start()
    {
        transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f);
        if(materials == null)
        {
            InitializeMaterials();
        }
        gameObject.AddComponent<MeshFilter>().mesh = meshs[Random.Range(0, meshs.Length)];
        gameObject.AddComponent<MeshRenderer>().material = materials[depth, Random.Range(0, 2)];
        if (rotate)
        {
            gameObject.AddComponent<Rotator>().RotationSpeed = rotationSpeed;
        }

        if (depth < maxDepth)
        {
            CreateChildren();
        }
    }

    private Material[,] materials;

    private void InitializeMaterials()
    {
        materials = new Material[maxDepth + 1, 2];
        for (int i = 0; i <= maxDepth; i++)
        {
            float t = i / (maxDepth);
            //t *= t;
            materials[i, 0] = new Material(material);
            materials[i, 0].color =
                Color.Lerp(Color.black, Color.grey, t);
            materials[i, 1] = new Material(material);
            materials[i, 1].color = Color.Lerp(Color.white, Color.grey, t);
        }
        /*
        materials[maxDepth, 0].color = Color.cyan;
        materials[maxDepth, 1].color = Color.red;
        */
    }
   
    public void Initialize(Fractal parent, int childIndex)
    {
        maxTwist = parent.maxTwist;
        spawnProability = parent.spawnProability;
        rotate = parent.rotate;
        rotationSpeed = parent.rotationSpeed;
        meshs = parent.meshs;
        materials = parent.materials;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = childDirection[childIndex] * (0.5f + 0.5f * childScale);
        transform.localRotation = childOrientations[childIndex];
    }

    private static Vector3[] childDirection =
    {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back,
        Vector3.down
    };

    private static Quaternion[] childOrientations =
    {
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(90f, 0f, 0f),
        Quaternion.Euler(-90f, 0f, 0f),
        Quaternion.Euler(0f, 0f, 180f)
    };

    private void CreateChildren()
    {
        for(int i = 0; i < childDirection.Length; i++)
        {
            if (Random.value > spawnProability)
                continue;
            if(i < childDirection.Length - 1)
            {
                new GameObject("Fractal Child").
                AddComponent<Fractal>().Initialize(this, i);
            }
            else if ( depth == 0)
            {
                new GameObject("Fractal Child").
                AddComponent<Fractal>().Initialize(this, i);
            }
            
        }
    }

}
