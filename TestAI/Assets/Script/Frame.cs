using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class Frame : MonoBehaviour
{
    // Start is called before the first frame update
    struct FrameSize {
        public float Left;
        public float Top;
        public FrameSize(float Left, float Top) {
            this.Left = Left;
            this.Top = Top;
        }
    }
    FrameSize[] gameFrame;
    void Start()
    {
        gameFrame = new FrameSize[2];
        gameFrame[0] = new FrameSize(-47,42);
        gameFrame[1] = new FrameSize(22, 31);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeTarget(int num) {
        const float posz = 189f;
        RectTransform targetrect = this.gameObject.GetComponent<RectTransform>();
        targetrect.position = new Vector3((float)gameFrame[num].Left, (float)gameFrame[num].Top,posz);
    }
}
