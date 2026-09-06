using UnityEngine;

public class ParallaxLoop : MonoBehaviour
{
    public Transform cameraTransform;

    [Range(0f,1f)]
    public float parallax = 0.3f;

    public Transform bg1;
    public Transform bg2;

    float width;
    float lastCameraX;

    void Start()
    {
        width = bg1.GetComponent<SpriteRenderer>().bounds.size.x;
        lastCameraX = cameraTransform.position.x;
    }

    void LateUpdate()
    {
        float delta = cameraTransform.position.x - lastCameraX;

        // 背景跟随摄像机缓慢移动
        bg1.position += Vector3.right * delta * parallax;
        bg2.position += Vector3.right * delta * parallax;

        lastCameraX = cameraTransform.position.x;

        // 判断哪张在左边
        Transform left = bg1.position.x < bg2.position.x ? bg1 : bg2;
        Transform right = left == bg1 ? bg2 : bg1;

        // 左边背景完全离开屏幕后放到右边
        if (cameraTransform.position.x - left.position.x > width)
        {
            left.position = new Vector3(
                right.position.x + width,
                left.position.y,
                left.position.z
            );
        }

        // 如果玩家会向左走，再加下面这一段
        if (right.position.x - cameraTransform.position.x > width)
        {
            right.position = new Vector3(
                left.position.x - width,
                right.position.y,
                right.position.z
            );
        }
    }
}