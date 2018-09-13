using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Game : PersistableObject {

    public PersistantStorage storage;
    public ShapeFactory shapeFactory;

    public KeyCode createKey = KeyCode.C;
    public KeyCode newGameKey = KeyCode.N;
    public KeyCode saveGameKey = KeyCode.S;
    public KeyCode loadGameKey = KeyCode.L;

    List<Shape> shapes;

    const int saveVersion = 1;

    private void Awake()
    {
        shapes = new List<Shape>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(createKey))
        {
            CreateShape();
        }
        else if (Input.GetKeyDown(newGameKey))
        {
            BeginNewGame();
        }
        else if (Input.GetKeyDown(saveGameKey))
        {
            storage.Save(this);
        }
        else if (Input.GetKeyDown(loadGameKey))
        {
            BeginNewGame();
            storage.Load(this);
        }
    }

    void CreateShape()
    {
        Shape instance = shapeFactory.GetRandom();
        Transform t = instance.transform;
        t.localPosition = Random.insideUnitSphere * 5f;
        t.localRotation = Random.rotation;
        t.localScale = Vector3.one * Random.Range(0.1f, 1f);
        shapes.Add(instance);
    }

    void BeginNewGame()
    {
        for (int i = 0; i < shapes.Count; i++)
        {
            Destroy(shapes[i].gameObject);
        }
        shapes.Clear();
    }

    public override void Save (GameDataWriter writer)
    {
        writer.Write(-saveVersion);
        writer.Write(shapes.Count);
        for(int i = 0; i < shapes.Count; i++)
        {
            shapes[i].Save(writer);
        }
    }

    public override void Load (GameDataReader reader)
    {
        int version = -reader.ReadInt();
        if (version > saveVersion)
        {
            Debug.LogError("Unsupported future save version " + version);
            return;
        }
        int count = version <= 0 ? -version : reader.ReadInt();
        for(int i = 0; i < count; i++)
        {
            Shape instance = shapeFactory.Get(0);
            instance.Load(reader);
            shapes.Add(instance);
        }
    }

}
