using System.Collections;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using UnityEngine;
namespace GameMain
{
    public class LevelController : MonoBehaviour
    {

        private int totalLevel;
        public int upgradeConditions;
        public int createRatioPerSecond;
        private int m_combo = 0;
       
        private int curRatio;
        public float[] createProportion;
        private float[] randomTime;
        private float totleProportion;
        private float curTime;
        public GameObject[] prefabs;
        public GameObject floor;
        public Transform playerTrans;
        private float curCount;
        List<GameObject> blockPool;
        List<GameObject> floorPool;

         private Transform curFloor;
        public float floorOffset;


        public void Start()
        {
            Init();
            GameEntry.Event.Subscribe(GameFailEventArgs.EventId, GameFail);
            GameEntry.Event.Subscribe(TimeOutEventArgs.EventId, TimeOut);
        }

        public void Update()
        {
           Check();
        }

        public void Init()
        {
            

            randomTime = new float[createRatioPerSecond];
            blockPool = new List<GameObject>();
            floorPool = new List<GameObject>();
            totleProportion = 0;
            curFloor = playerTrans;
            floorPool.Add(playerTrans.gameObject);
            GameEntry.Utlis.curLevel = 1;
            GameEntry.Utlis.Score = 0;
            curTime = 0;
            curCount = upgradeConditions;
            foreach (var proportion in createProportion)
            {
                totleProportion += proportion;
            }
            for (int i = 0; i < randomTime.Length; i++)
            {
                randomTime[i] = RandomFloat();
            }
        }

        public void Check()
        {
            //Ïú»Ù
            for (int i = 0; i < blockPool.Count; i++)
            {
                var e = blockPool[i];
                if (e.transform.position.y < playerTrans.position.y - 5.0f)
                {
                    blockPool.Remove(e);
                    Destroy(e);
                    if (Random.Range(1, 4) > 2)
                    {
                        int x = Random.Range(1, 4);
                        GameEntry.Sound.PlaySound(30000 + x);

                    }
                }
            }

            for (int i = 0; i < floorPool.Count; i++)
            {
                var e = floorPool[i];
                if (e.transform.position.y < playerTrans.position.y - 10.0f)
                {
                    floorPool.Remove(e);
                    Destroy(e);
                }
                GameEntry.Utlis.curLevel = floorPool.Count;

            }


            //Éú³É
            if (IsUpGradeAble())
            {
                UpGrade();
            }
            else
            {
                curTime += Time.deltaTime;
                for (int i = 0; i < randomTime.Length; i++)
                {
                    if (randomTime[i] < curTime && curRatio < createRatioPerSecond)
                    {
                        Create(RandomIndex());
                        curRatio++;
                        randomTime[i] = 10;
                    }
                }
                if (curTime > 1.0f)
                {
                    curTime = 0;
                    curRatio = 0;
                    for (int i = 0; i < randomTime.Length; i++)
                    {
                        randomTime[i] = RandomFloat();
                    }
                }
            }

          
        }

        public void Create(int index)
        {
            GameObject go = GameObject.Instantiate(prefabs[index], RandomPos(), RandomQuaternion());
            if (Random.Range(1, 5) > 2)
            {
                GameEntry.Sound.PlaySound(10002);

            }
            blockPool.Add(go);
        }

        public float RandomFloat()
        {
            return Random.Range(0.0f, 1.0f);
        }


        public int RandomIndex()
        {
            float value = Random.Range(0.0f, 1.0f) * totleProportion;
            for (int i = 0; i < createProportion.Length; i++)
            {
                if (createProportion[i] > value)
                    return i;
                else
                    value -= createProportion[i];
            }
            return createProportion.Length - 1;

        }

        public void ChangeRatio(int ratio)
        {
            createRatioPerSecond = ratio;

        }

        public Vector3 RandomPos()
        {
            float x, y, z;

            x = Random.Range(0, Screen.width);
            y = Screen.height;
            z = 10;

            return Camera.main.ScreenToWorldPoint(new Vector3(x, y, z));
        }

        public Quaternion RandomQuaternion()
        {
            float z = Random.Range(0.0f, 360.0f);
            return Quaternion.Euler(0, 0, z);
        }

        public void UpGrade()
        {
            Vector3 pos = new Vector3(playerTrans.position.x, playerTrans.position.y + GameEntry.Utlis.curLevel * floorOffset, 0);
            var go = GameObject.Instantiate(floor, pos, Quaternion.identity);
            //go.transform.SetParent(playerTrans.transform);
            //go.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //go.transform.localScale = Vector3.one;
            //go.transform.localPosition = new Vector3(0, go.transform.localPosition.y, 0);
            curFloor = go.transform;
            Debug.Log(GameEntry.Utlis.upGradeCount);
            floorPool.Add(go);
            GameEntry.Utlis.upGradeCount = 0;

        }

        public void OnDisable()
        {
            GameEntry.Event.Unsubscribe(GameFailEventArgs.EventId, GameFail);
            GameEntry.Event.Unsubscribe(TimeOutEventArgs.EventId, TimeOut);

        }
        public bool IsUpGradeAble()
        {
            if(GameEntry.Utlis.upGradeCount > curCount)
                return true;
            else
                return false;
        }

        public float GetHigestBlockHeight()
        {
            float height = 0;
            foreach (var go in blockPool)
            {
                if (height < go.transform.position.y)
                    height = go.transform.position.y;
            }
            return height;
        }

        public void GameClear()
        {
            GameEntry.Event.FireNow(this, GameClearEventArgs.Create(GameEntry.Utlis.curLevel));

        }

        public void GameFail(object sender, GameFramework.Event.GameEventArgs e)
        {
            //GameEntry.Event.FireNow(this, GameClearEventArgs.Create(curLevel));

        }

        private void TimeOut(object sender, GameFramework.Event.GameEventArgs e)
        {
            GameEntry.Event.FireNow(this, GameClearEventArgs.Create(GameEntry.Utlis.Score));
        }

    }

}