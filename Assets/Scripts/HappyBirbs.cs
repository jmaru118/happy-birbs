using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyBirbs : MonoBehaviour
{
    public float timer = 0f;
    public float growTime = 0.2f;
    public float maxSize = 0.69936f;
    public float startingSize = 0.25f;
    public bool isMaxSize = false;

    // Start is called before the first frame update
    void Start()
    {
        if( !isMaxSize )
            StartCoroutine(Grow());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Grow()
    {
        Vector2 startScale = transform.localScale;
        Vector2 maxScale = new Vector2(maxSize, maxSize);

        while ( timer < growTime )
        {
            transform.localScale = Vector3.Lerp(startScale, maxScale, timer / growTime);
            timer += Time.deltaTime;
            yield return null;
        }

        isMaxSize = true;
    }
}
