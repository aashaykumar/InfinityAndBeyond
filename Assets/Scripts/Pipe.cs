using UnityEngine;


public class Pipe : MonoBehaviour
{
   
    public float PipeRadius;
    public int PipeSegmentCount;
    public float MinCurveRadius, MaxCurveRadius;
    public int MinCurveSegmentCount, MaxCurveSegmentCount;
    public float RingDistance;

    public PipeItemGenerator[] Generators;
    private int curveSegmentCount;
    private float curveRadius;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uv;

    private float curveAngle;
    private float relativeRotation;
    #region Properties

    public int CurveSegmentCount 
    {
        get
        {
            return curveSegmentCount;
        }
    }
    
    public float RelativeRotation
    {
        get
        {
            return relativeRotation;

        }
    }
  
    public float CurveRadius 
    {
        get 
        {
            return curveRadius;
        }
    }

    public float CurveAngle 
    { 
        get
        {
            return curveAngle;
        }
    }
    #endregion

    

    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Pipe";
    }
    public void Generate(bool withItems = true)
    {
        curveRadius = Random.Range(MinCurveRadius, MaxCurveRadius);
        curveSegmentCount = Random.Range(MinCurveSegmentCount, MaxCurveSegmentCount);
        mesh.Clear();
        SetVertices();
        SetUV();
        SetTriangles();
        mesh.RecalculateNormals();
        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
        if(withItems)
        Generators[Random.Range(0, Generators.Length)].GenerateItems(this);
    }

    private void SetUV()
    {
        uv = new Vector2[vertices.Length];

        for (int i = 0; i < vertices.Length; i += 4)
        {
            uv[i] = Vector2.zero;
            uv[i + 1] = Vector2.right;
            uv[i + 2] = Vector2.up;
            uv[i + 3] = Vector2.one;
        }
        mesh.uv =uv; 
    }

    public void AlignWith(Pipe pipe)
    {
        transform.SetParent(pipe.transform, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0f, 0f, -pipe.curveAngle);
        transform.Translate(0f, pipe.curveRadius, 0f);
        transform.Rotate(0f, 0f, 0f);
        transform.Translate(0f, -curveRadius, 0f);
        transform.SetParent(pipe.transform.parent);
        transform.localScale = Vector3.one;
    }

    private void SetVertices()
    {
        vertices = new Vector3[PipeSegmentCount * curveSegmentCount * 4];
        float uStep = RingDistance / curveRadius;
        curveAngle = uStep * curveSegmentCount * (360f / (2f * Mathf.PI));
        CreateFirstQaudRing(uStep);
        int iDelta = PipeSegmentCount * 4;
        
        for (int u = 2, i = iDelta; u <= curveSegmentCount; u++, i += iDelta)
            CreateQuadRing(u * uStep, i);

        mesh.vertices = vertices;
    }

    private void CreateQuadRing(float u, int i)
    {
        float vStep = (2f * Mathf.PI) / PipeSegmentCount;
        int ringOffset = PipeSegmentCount * 4;

        Vector3 vertex = GetPointOnTorus(u, 0f);

        for (int v = 1; v <= PipeSegmentCount; v++, i += 4)
        {
            vertices[i] = vertices[i - ringOffset + 2];
            vertices[i + 1] = vertices[i - ringOffset + 3];
            vertices[i + 2] = vertex;
            vertices[i + 3] = vertex = GetPointOnTorus(u, v * vStep);
        }
    }

    private void CreateFirstQaudRing(float u)
    {
        float vStep = (2f * Mathf.PI) / PipeSegmentCount;

        Vector3 vertexA = GetPointOnTorus(0f, 0f);
        Vector3 vertexB = GetPointOnTorus(u, 0f);

        for (int v = 1,i = 0 ; v <= PipeSegmentCount; v++, i += 4)
        {
            vertices[i] = vertexA;
            vertices[i + 1] = vertexA = GetPointOnTorus(0f, v * vStep);
            vertices[i + 2] = vertexB;
            vertices[i + 3] = vertexB = GetPointOnTorus(u, v * vStep); 
        }
    }

    private void SetTriangles()
    {
        triangles = new int[PipeSegmentCount * curveSegmentCount * 6];

        for (int t = 0, i = 0; t < triangles.Length; t += 6, i += 4)
        {
            triangles[t] = i;
            triangles[t + 1] = triangles[t + 4] = i + 2;
            triangles[t + 2] = triangles[t + 3] = i + 1;
            triangles[t + 5] = i + 3;
        }

        mesh.triangles = triangles;
    }

    private Vector3 GetPointOnTorus(float u, float v)
    {
        Vector3 p;
        float r = (curveRadius + PipeRadius * Mathf.Cos(v));
        p.x = r * Mathf.Sin(u);
        p.y = r * Mathf.Cos(u);
        p.z = PipeRadius * Mathf.Sin(v);
        return p;
    }

   /* private void OnDrawGizmos()
    {
        float uStep = (2f * Mathf.PI) / CurveSegmentCount;
        float vStep = (2f * Mathf.PI) / PipeSegmentCount;

        for (int u = 0; u < CurveSegmentCount; u++)
        {
            for (int v = 0; v < PipeSegmentCount; v++)
            {
                Vector3 point = GetPointOnTorus(u * uStep, v * vStep);
                Gizmos.color = new Color(1f, (float)v / PipeSegmentCount, (float)u / CurveSegmentCount);
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }*/
}
