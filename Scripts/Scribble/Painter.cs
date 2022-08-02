using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SplineMesh;

using TMPro;

public class Painter : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public Camera cam;

    public TextMeshProUGUI temptext;

    public float pointStep;
    public float pointMinAmount;
    public List<Vector3> points = new List<Vector3>();

    // public PathCreator pathCreator;
    // BezierPath bezierPath;
    public Spline[] splines;

    public float rotSpeed;
    public Transform[] wheelTransforms;

    public Rigidbody rb;

    public int score;
    Vector3 origin;

    private void Awake()
    {
        score = 0;
        origin = rb.transform.position;
    }
    private void Update()
    {
        Paint();
        float distance = Vector3.Distance(rb.transform.position, origin);
        CanvasManager.instance.TrySetText("Score", distance.ToString("0000"));
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.forward, ForceMode.Impulse);
        RotateWheels();

    }

    private void Paint()
    {
        Vector2 worldpoint = cam.ScreenToWorldPoint(new Vector3(Inputs.mousePos.x, Inputs.mousePos.y, 10));

        temptext.text = worldpoint.ToString();
        trailRenderer.transform.position = worldpoint;

        trailRenderer.emitting = Inputs.triggerRight;

        if (!Inputs.triggerRight || worldpoint.x < -2.5f || worldpoint.x > 2.5f || worldpoint.y < -5.15f || worldpoint.y > -2.35f)
        {
            if (points.Count >= pointMinAmount) CreatePath();
            points.Clear();
            trailRenderer.Clear();
            return;
        }
        if (points.Count == 0) points.Add(worldpoint);
        else if (Vector3.Distance(worldpoint, points.Last()) > pointStep) points.Add(worldpoint);

    }

    private void CreatePath()
    {

        //Clean up
        splines[0].nodes.Clear();
        splines[0].curves.Clear();
        Transform meshSegmentsHolder = splines[0].transform.GetChild(0);
        for (int i = 0; i < meshSegmentsHolder.childCount; i++)
        {
            Destroy(meshSegmentsHolder.GetChild(i).gameObject);
        }

        foreach (Vector3 point in points)
        {
            Vector3 adjustedPoint = point + Vector3.up * 4;
            SplineNode newnode = new SplineNode(adjustedPoint, adjustedPoint);
            splines[0].AddNode(newnode);
        }

        splines[0].RefreshCurves();
        GameObject newWheel = splines[0].transform.GetChild(0).gameObject;

        for (int i = 1; i < splines.Length; i++)
        {
            splines[i].nodes = splines[0].nodes;
            splines[i].RefreshCurves();
        }

    }

    private void RotateWheels()
    {
        foreach (Transform t in wheelTransforms)
        {
            t.Rotate(Vector3.forward * -rotSpeed, Space.Self);
        }
    }
}
