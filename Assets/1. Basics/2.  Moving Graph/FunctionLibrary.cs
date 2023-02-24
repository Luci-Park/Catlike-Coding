using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public delegate Vector3 Function(float u, float v, float t);

    public enum FunctionName { Wave, MultiWave, Ripple, Sphere, ScalingSphere, VerticalBandsSphere, HorizontalBandSphere, TwistingSphere, Torus, Size }

    static Function[] functions = { Wave, MultiWave, Ripple, Sphere, ScalingSphere, VerticalBandsSphere, HorizontalBandSphere, TwistingSphere, Torus };

    public static Function GetFunction(int index)
    {
        return functions[index];
    }
    public static FunctionName GetRandomFunctionNameExcept(FunctionName name)
    {
        int choice;
        do
        {
            choice = Random.Range(0, (int)FunctionName.Size);
        } while (choice == (int)name);
        return (FunctionName)choice;
    }
    public static Function GetFunction(FunctionName function)
    {
        return functions[(int) function];
    }

    public static FunctionName GetNextFunctionName(FunctionName name)
    {
        int idx = (int)name;
        idx = (idx + 1) % (int)FunctionName.Size;
        return (FunctionName)idx;
    }

    public static Vector3 Morph(float u, float v, float t, Function from, Function to, float progress)
    {
        return Vector3.LerpUnclamped(from(u,v,t), to(u, v, t), SmoothStep(0f, 1f, progress));
    }

    public static Vector3 Wave(float u, float v, float t)
    {
        Vector3 p = new Vector3(u, Sin(PI * (u + v + t)), v);
        return p;
    }

    public static Vector3 MultiWave(float u, float v, float t)
    {
        Vector3 p = new Vector3(u, 0, v);
        p.y = Sin(PI * (u + 0.5f * t));
        p.y += 0.5f * Sin(2f * PI * (u + v + t));
        p.y += Sin(PI * (u + v + 0.25f * t));
        p.y *= (2f / 3f);
        return p;
    }
    public static Vector3 Ripple(float u, float v, float t)
    {
        float d = Sqrt(u * u + v * v);
        Vector3 p = new Vector3(u, 0, v);
        p.y = Sin(PI * (4f * d - t));
        p.y /= (1f + 10f * d);
        return p;
    }

    public static Vector3 Sphere(float u, float v, float t)
    {
        float r = Cos(0.5f * PI * v);

        Vector3 p;
        p.x = r * Sin(PI * u);
        p.y = Sin(PI * 0.5f * v);
        p.z = r * Cos(PI * u);
        return p;
    }

    public static Vector3 ScalingSphere(float u, float v, float t)
    {
        float r = 0.5f + 0.5f * Sin(PI * t);
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    public static Vector3 VerticalBandsSphere(float u, float v, float t)
    {
        float r = 0.9f + 0.1f * Sin(8f * PI * u);
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    public static Vector3 HorizontalBandSphere(float u, float v, float t)
    {
        float r = 0.9f + 0.1f * Sin(8f * PI * v);
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    public static Vector3 TwistingSphere(float u, float v, float t)
    {
        float r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(0.5f * PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    public static Vector3 Torus(float u, float v, float t)
    {
        float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
        float r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t));
        float s = r1 + r2 * Cos(PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r2 * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

}
