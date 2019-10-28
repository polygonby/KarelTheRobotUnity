using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Tweens
{
    public static IEnumerator AnimateMaterialColorCoroutine(List<Material> materials, Color targetColor,
        int animationsCount = 1, float animationTime = 0.2f)
    {
        if (materials.Count == 0)
            throw new ArgumentException("No materials passed");
        
        if (animationsCount <= 0)
            throw new ArgumentException($"Invalid animations count = {animationsCount}");
        
        if (animationTime <= 0.0f)
            throw new ArgumentException($"Invalid animation time = {animationTime}");
        
        List<Color> startColors = materials.Select(m => m.color).ToList();

        float timePassed;
        
        for (int i = 0; i < animationsCount; i++)
        {
            timePassed = 0.0f;
            while (timePassed < animationTime / 2.0f)
            {
                for (int j = 0; j < materials.Count; j++)
                {
                    materials[j].color = Color.Lerp(startColors[j], targetColor, timePassed / (animationTime / 2.0f));
                }
                yield return null;
                timePassed += Time.deltaTime;
            }

            if (animationsCount > 1)
            {
                timePassed = 0.0f;
                while (timePassed < animationTime / 2.0f)
                {
                    for (int j = 0; j < materials.Count; j++)
                    {
                        materials[j].color = Color.Lerp(targetColor, startColors[j], timePassed / (animationTime / 2.0f));
                    }
                    yield return null;
                    timePassed += Time.deltaTime;
                }
            }
        }
        
        // Final lerp to target color
        if (animationsCount > 1)
        {
            timePassed = 0.0f;
            while (timePassed < animationTime / 2.0f)
            {
                for (int j = 0; j < materials.Count; j++)
                {
                    materials[j].color = Color.Lerp(startColors[j], targetColor, timePassed / (animationTime / 2.0f));
                }
                yield return null;
                timePassed += Time.deltaTime;
            }
        }
        
        for (int j = 0; j < materials.Count; j++)
        {
            materials[j].color = targetColor;
        }
    }
}
