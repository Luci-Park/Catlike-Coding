using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fractal : MonoBehaviour
{
    [SerializeField, Range(1, 8)]
    int depth;
    [SerializeField] Mesh mesh;
    [SerializeField] Material material;
    struct FractalPart
    {
        public Vector3 direction;
        public Quaternion rotation;
        public Transform transform;
    };

    static Vector3[] directions =
    { Vector3.up, Vector3.right, Vector3.left, Vector3.forward, Vector3.back };

    static Quaternion[] rotations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, -90f), Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(90f, 0f, 0f), Quaternion.Euler(-90f, 0f, 0f)
    };

    FractalPart[][] parts;

    private void Awake()
    {
        parts = new FractalPart[depth][];
        for(int i = 0, length = 1; i < parts.Length; i++, length *= 5)
        {
            parts[i] = new FractalPart[length];
        }
        float scale = 1f;
        parts[0][0] = CreatePart(0, 0, scale);
        for (int li = 1; li < parts.Length; li++)
        {
            scale *= 0.5f;
            for(int fpi = 0; fpi < parts[li].Length; fpi+= 5)
            {
                for (int ci = 0; ci < 5; ci++)
                {
                    parts[li][fpi + ci] = CreatePart(li, ci, scale);
                }
            }
        }
    }

    private void Update()
    {
        Quaternion deltaRotation = Quaternion.Euler(0f, 22.5f * Time.deltaTime, 0f);

        parts[0][0].rotation = deltaRotation;
        parts[0][0].transform.localRotation = parts[0][0].rotation;

        for(int li = 1; li < parts.Length; li++)
        {
            for(int fpi = 0; fpi < parts[li].Length; fpi++)
            {
                Transform parentTransform = parts[li - 1][fpi / 5].transform;
                FractalPart part = parts[li][fpi];
                part.rotation *= deltaRotation;
                part.transform.localRotation = parentTransform.localRotation * part.rotation;
                part.transform.localPosition =
                    parentTransform.localPosition +
                    parentTransform.localRotation *
                        (1.5f * part.transform.localScale.x * part.direction);
                parts[li][fpi] = part;
            }
        }
    }

    FractalPart CreatePart(int levelIndex, int childIndex, float scale)
    {
        var go = new GameObject("Fractal Part " + levelIndex + " C" + childIndex);
        go.transform.SetParent(transform, false);
        go.transform.localScale = scale * Vector3.one;
        go.AddComponent<MeshFilter>().mesh = mesh;
        go.AddComponent<MeshRenderer>().material = material;
        return new FractalPart
        { 
            direction = directions[childIndex],
            rotation = rotations[childIndex],
            transform = go.transform
        };

    }
}
