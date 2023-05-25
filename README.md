# [Catlike-Coding](https://catlikecoding.com/unity/tutorials)
 Unity and Shaders
## Basics
### Clock
_Albedo_
The color of something when illuminated by white light.

_internal_
If not defined as public, classes are defined as internal.
It means it's only accessable among those in the same assembly.

_Quaternion_
4D vector that represents rotations => (w, x, y, z)
Prevents gimbal lock.
Rotation multiplication is faster than matrix ones.
One quaternion cannot represent rotation larger than 180

_Single Precision_
Most game engines and GPUs use single precision floating-point values.
If we used double precisions, it would double the memory size of numbers.

### Moving Graphs
_Surface Shader_
does lighting calculations. Doesn't work for the URP.
* Fallback: a default shader that will be chosen if all subshader fails.
* pragma: compiler directive
#pragma surface ConfigureSurface Standard fullforwardshadows
instructs the shader to generate a surface shader with **Standard** lighting and **full support for shadows**.
* saturate : clamps all components to 0 - 1 

_Universal Render Pipeline(URP)_
scriptable render pipeline. Has a shader graph. Cannot be modified, though.

_Mesh_
* Vector3[] vertices
* int[] triangle: triangle[i] is a point of i/3th triangle.
* Vector3[] normals: per vertices.
* Color[] colors: per vertices
* mesh.RecalculateNormals

_UV_
Texture coordinates.
Ranges between(0, 0) and (1, 1)

_Tangents_
Where normal maps are defined. It's like the 3D space that flows around the surface of an object.
Uses 4 dimension to represent in/out direction.

_Shadow Cascade, Distance_
Unity render shadows as textures, these are sampled to create shadows. Because the texture has a fixed resolution, they are more blocky if they have to cover a bigger area.
Less distance, better and less shadow.
Cascades uses multiple maps based on distance so nearby shadows have a higher resolution than those far away.

#### Lecture
- Why we're changing the return value from float to vector3
    - => Because currently, we're tied to the 2D relm. To go to the 3D relm, we need a vector return value
- Game Quality
    - FPS : Frames per Second
    - batches : draw calls sent to the GPU
        - depth pass, shadows included 
        - URP doesn't use a separate depth pass for directional shadows
        - dynamic batching : Commands CPU to dynamically combine small meshes to a larger one, therefore reducing drawcall. (요즘은 GPU 성능이 좋아져서 dynamic batching이 더 걸릴 수 있다.)
    - GPU Instancing
        - renders multiple copies of a mesh with the same material in a single draw call
        - optimizes cases where same meshes come multiple times.
    - Unity's Built in render pipline renders each point for every lighting.
        - Therefore if more lights and points are added, GPU Instancing may not be your best option.
    - Use a Profiler.
    
### Fractals
_Instantiate_
instantiate(this) duplicates the gameobject in their current state, including its child objects.

_SetParent(Transform parent, bool worldPositionStays)_
`worldpositionstays`가 `true`이면 `child`는 본인의 `worldposition`을 유지하고
`false`이면 `localposition`이 있을 시 새로운 `parent`에 대해 `localposition`이 유지된다. 

_Hierarchy_
유니티는 `recursive`한 hierarchy에서 효율적으로 움직이지 못한다.
각 오브젝트 update -> object-to-world 행렬 계산 -> culling -> rendering


## Pseudorandom Noise
### Hashing
_IJob_
can be used to schedule a single job that runs in parallel to other jobs and the main thread.
_IJobFor_
Interface for scheduling items. We use it here to fill hashes.
```cs
public interface IJobFor
    {
        //
        // 요약:
        //     Implement this method to perform work against a specific iteration index.
        //
        // 매개 변수:
        //   index:
        //     The index of the for loop at which to perform work.
        void Execute(int index);
    }
```

_Burst Compiler_
빌드 된 클라이언트가 개별 플랫폼과 하드웨어에 맞춰 극한으로 최적화되어 실행되도록 컴파일함.