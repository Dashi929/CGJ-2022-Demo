using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameMain
{
    public class CameraController : MonoBehaviour
    {
        public float scaleRatio;
        public Transform playerTrans;
        public Collider2D leftEdge, RightEdge;
        Camera cam;
        LevelController levelController;
        float curSpeed;
        float originSize;
        // Start is called before the first frame update
        void Start()
        {
            cam = gameObject.GetComponent<Camera>();
            originSize = cam.orthographicSize;
            levelController = playerTrans.GetComponent<LevelController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            UpdateCam();
        }

        public void UpdateCam()
        {

            ScaleCam();
            MoveCam();
            RoteCam();
            UpdateEdge();
        }
        public void MoveCam()
        {
            Vector3 buttomPos = new Vector3(0, 0, 10);

            buttomPos = Camera.main.ScreenToWorldPoint(buttomPos);
            cam.transform.position = new Vector3(cam.transform.position.x, 
                cam.transform.position.y + playerTrans.position.y - buttomPos.y - 2,
                cam.transform.position.z);
        }
        public void RoteCam()
        {

        }

        public void ScaleCam()
        {
            float targetScale = originSize + (GameEntry.Utlis.curLevel) * scaleRatio;
            float scale = Mathf.SmoothDamp(cam.orthographicSize, targetScale, ref curSpeed, 0.12f);
            cam.orthographicSize = scale;
        }

        public void UpdateEdge()
        {
            leftEdge.offset = new Vector2(-cam.orthographicSize * 2, 0);
            RightEdge.offset = new Vector2(cam.orthographicSize * 2, 0);
        }

        public void FocuOn(Vector2 Pos)
        {

        }
    }
}
