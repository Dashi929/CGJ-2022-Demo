using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameMain
{
    public class BlockDetection : MonoBehaviour
    {
        bool isFirst = true;
    
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (isFirst)
            {

                if (collision.collider.tag == "Ground")
                {
                    GameEntry.Utlis.Combo = 0;

                }
                else if(collision.collider.tag == "Block" || collision.collider.tag == "Floor")
                {
                    GameEntry.Utlis.upGradeCount++;
                    GameEntry.Utlis.Score++;
                    isFirst = false;
                }
            }
           else
            {
                if (collision.collider.tag == "Ground")
                {
                    GameEntry.Utlis.upGradeCount--;
                    GameEntry.Utlis.Score--;

                }
            }
            Debug.Log(GameEntry.Utlis.Combo);

        }
    }
}