using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : Transformer
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private float naturalScale;
    
    public void Init(Sprite sprite, bool flipX, float naturalScale)
    {
        _spriteRenderer.sprite = sprite;
        _spriteRenderer.flipX = flipX;
        this.naturalScale = naturalScale;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        const float EDGE = 1.5f;
        float percent = 1;
        while (true)
        {
            percent = Mathf.Lerp(percent, 2.5f, Time.deltaTime * 5);
            if (percent > EDGE)
                Destroy(gameObject);
            transform.localScale = naturalScale * (percent * 1.5f) * Vector3.one;
            _spriteRenderer.color = VectorHelper.SetA(Color.white, EDGE - percent);
            yield return null;
        }
    }

    public static void SpawnCorpse(GameObject prefab, Vector3 position, Sprite sprite, bool flipX, float naturalScale)
    {
        Corpse newCorpse = Instantiate(prefab, position, Quaternion.identity).GetComponent<Corpse>();
        newCorpse.Init(sprite, flipX, naturalScale);
    }
}
