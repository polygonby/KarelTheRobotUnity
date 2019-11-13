using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public static class Tweens
{
    public static IEnumerator MoveTransformCoroutine(Transform target, Vector3 targetPosition, 
        float moveSpeed, Action onCompleted = null)
    {
        float remainingDistance;
        float maxDistance;
        
        do
        {
            maxDistance = moveSpeed * Time.deltaTime;
            remainingDistance = (targetPosition - target.position).magnitude;

            target.position = Vector3.MoveTowards(target.position, targetPosition, maxDistance);
            yield return null;
        } 
        while (remainingDistance > maxDistance);
        
        target.position = targetPosition;
        onCompleted?.Invoke();
    }
    
    public static IEnumerator RotateTransformCoroutine(Transform target, Quaternion targetRotation, 
        float rotationSpeed, Action onCompleted = null)
    {
        float remainingRotationAngle;
        float maxRotationAngle;
        
        do
        {
            maxRotationAngle = rotationSpeed * Time.deltaTime;
            remainingRotationAngle = Quaternion.Angle(target.rotation, targetRotation);

            target.rotation = Quaternion.RotateTowards(target.rotation, targetRotation, maxRotationAngle);
            yield return null;
        } 
        while (Mathf.Abs(remainingRotationAngle) > Mathf.Abs(maxRotationAngle));
        
        target.rotation = targetRotation;
        onCompleted?.Invoke();
    }

    public static IEnumerator AnimateMaterialsColorCoroutine(List<Material> materials, Color targetColor,
        int animationsCount = 1, float animationTime = 0.2f, Action onCompleted = null)
    {
        Assert.IsFalse(materials.Count == 0, "No materials passed");
        Assert.IsFalse(animationsCount <= 0, $"Invalid animations count = {animationsCount}");
        Assert.IsFalse(animationTime <= 0.0f, $"Invalid animation time = {animationTime}");

        Color startColor = materials.Select(m => m.color).ToList()[0];

        IEnumerator enumerator;
        
        for (int i = 0; i < animationsCount; i++)
        {
            enumerator = LerpMaterialsColorsCoroutine(materials, targetColor, animationTime / 2.0f);
            while (enumerator.MoveNext())
            {
                yield return enumerator;
            }

            enumerator = LerpMaterialsColorsCoroutine(materials, startColor, animationTime / 2.0f);
            while (enumerator.MoveNext())
            {
                yield return enumerator;
            }
        }
        
        // Final lerp to target color
        enumerator = LerpMaterialsColorsCoroutine(materials, targetColor, animationTime / 2.0f);
        while (enumerator.MoveNext())
        {
            yield return enumerator;
        }
        
        for (int j = 0; j < materials.Count; j++)
        {
            materials[j].color = targetColor;
        }
        
        onCompleted?.Invoke();
    }

    private static IEnumerator LerpMaterialsColorsCoroutine(List<Material> materials, Color targetColor,
        float animationTime)
    {
        List<Color> startColors = materials.Select(m => m.color).ToList();
        
        var timePassed = 0.0f;
        while (timePassed < animationTime)
        {
            for (int j = 0; j < materials.Count; j++)
            {
                materials[j].color = Color.Lerp(startColors[j], targetColor, timePassed / (animationTime / 2.0f));
            }
            yield return null;
            timePassed += Time.deltaTime;
        }
    }
}
