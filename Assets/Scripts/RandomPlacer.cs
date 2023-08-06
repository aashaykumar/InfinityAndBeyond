using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlacer : PipeItemGenerator
{
    public PipeItem[] ItemPrefabs;

    public override void GenerateItems(Pipe pipe)
    {
        float angleStep = pipe.CurveAngle / pipe.CurveSegmentCount;

        for (int i = 0; i < pipe.CurveSegmentCount; i++)
        {
            PipeItem item = Instantiate<PipeItem>(ItemPrefabs[UnityEngine.Random.Range(0, ItemPrefabs.Length)]);
            float pipeRotation = (UnityEngine.Random.Range(0, pipe.PipeSegmentCount) + 0.5f) * 360f / pipe.PipeSegmentCount;
            item.Position(pipe, i * angleStep, pipeRotation);
        }
    }
}
