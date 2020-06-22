using UnityEngine;
using UnityEditor;
public class ColliderGenerator : Editor
{
    [MenuItem("MyTools/AddBoxCollider")]
    static void AddBoxCollider()
    {
        if (Selection.activeGameObject == null)
        {
#if UNITY_EDITOR
            Debug.Log("<color=red>MyTools/AddBoxCollider：未选中物体</color>");
#endif
            return;
        }
        Transform target = Selection.activeGameObject.transform;
        Vector3 center = Vector3.zero;
        //注意GetComponentsInChildren获取的组件中包括父物体挂载的组件
        var renders = target.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renders.Length; i++)
        {
            //bounds.center 为渲染边界中心点的世界坐标
            center += renders[i].bounds.center;
        }
        //求平均中心点，类似于(点A+点B)/2 为两点连线的中心点。
        center /= renders.Length;
        //创建一个包围盒，中心为计算出的平均中心点。
        Bounds bounds = new Bounds(center, Vector3.zero);
        for (int i = 0; i < renders.Length; i++)
        {
            //使包围盒包裹子物体的RendererBounds(扩大)
            bounds.Encapsulate(renders[i].bounds);
        }
        BoxCollider boxCollider = target.gameObject.AddComponent<BoxCollider>();
        //bounds.center 为世界坐标，需要将其转换为父物体的本地坐标
        boxCollider.center = target.InverseTransformPoint(bounds.center);
        //父物体缩放之后，boxCollider的size 1 代表的长度也会跟着缩放，1m—>0.1m
        //而Bounds就不同了，它一直是标准的1m，所以需除以父物体缩放。
        boxCollider.size = bounds.size / target.localScale.z;
    }
}