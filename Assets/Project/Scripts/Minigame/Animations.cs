using System.Collections;
using UnityEngine;

namespace Project.Scripts.Minigame
{
    public static class Animations
    {
        public static IEnumerator RotateTo(Transform transform, Quaternion end, float duration)
        {
            var start = transform.rotation;
            float timeElapsed = 0;

            while (timeElapsed < duration)
            {
                transform.rotation = Quaternion.Lerp(start, end, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.rotation = end;
        }
    }
}