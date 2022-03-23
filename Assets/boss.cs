using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BulletHell;
public class boss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void pattern_example()
    {
        GetComponent<customProjectileEmitter>().FireProjectile(this.transform.position, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

namespace BulletHell{
    public class customProjectileEmitter :  ProjectileEmitterBase
    {

        public new void Awake()
        {
            base.Awake();
        }

        void Start()
        {
            // To allow for the enable / disable checkbox in Inspector
        }

        //총알 발사 함수
        public override Pool<ProjectileData>.Node FireProjectile(Vector2 direction, float leakedTime)
        {
            Pool<ProjectileData>.Node node = new Pool<ProjectileData>.Node();
            node = Projectiles.Get();

            //발사체 속성 부여
            node.Item.Position = transform.position;
            node.Item.Speed = Speed;
            node.Item.Scale = Scale;
            node.Item.TimeToLive = TimeToLive;
            node.Item.Gravity = Gravity;
            //Deprecated
            //node.Item.Color = Color.Evaluate(0);
            node.Item.Acceleration = Acceleration;

            if (ProjectilePrefab.Outline != null && DrawOutlines)
            {
                Pool<ProjectileData>.Node outlineNode = ProjectileOutlines.Get();

                //아웃라인 속성 부여
                
                //발사체에 아웃라인 첨부
                node.Item.Outline = outlineNode;

                // Keep track of active projectiles                       
                PreviousActiveProjectileIndexes[ActiveProjectileIndexesPosition] = node.NodeIndex;
                ActiveProjectileIndexesPosition++;
                if (ActiveProjectileIndexesPosition < ActiveProjectileIndexes.Length)
                {
                    PreviousActiveProjectileIndexes[ActiveProjectileIndexesPosition] = -1;
                }
                else
                {
                    Debug.Log("Error: Projectile was fired before list of active projectiles was refreshed.");
                }
            }

            return node;
        }

        //총알 업데이트 중앙 처리
        protected override void UpdateProjectiles(float tick)
        {
            if (CullProjectilesOutsideCameraBounds)
            {
                GeometryUtility.CalculateFrustumPlanes(Camera, Planes);
            }
        }

        protected override void UpdateProjectile(ref Pool<ProjectileData>.Node node, float tick)
        {
            if (node.Active)
            {
                node.Item.TimeToLive -= tick;

                // Projectile is active
                if (node.Item.TimeToLive > 0)
                {
                    // 가속도 적용
                    node.Item.Velocity *= (1 + node.Item.Acceleration * tick);

                    // 중력 적용 (deprecated)
                    //node.Item.Velocity += node.Item.Gravity * tick;

                    // 총알 위치 계산
                    Vector2 deltaPosition = node.Item.Velocity * tick;
                    float distance = deltaPosition.magnitude;

                    // 총알이 카메라 뷰안에 존재하지 않을시 컬링
                    if (CullProjectilesOutsideCameraBounds)
                    {
                        Bounds bounds = new Bounds(node.Item.Position, new Vector3(node.Item.Scale, node.Item.Scale, node.Item.Scale));
                        if (!GeometryUtility.TestPlanesAABB(Planes, bounds))
                        {
                            ReturnNode(node);
                            return;
                        }
                    }

                    float radius = 0;
                    if (node.Item.Outline.Item != null)
                    {
                        radius = node.Item.Outline.Item.Scale / 2f;
                    }
                    else
                    {
                        radius = node.Item.Scale / 2f;
                    }
                    
                    //컬러 업데이트 (deprecated)
                    // node.Item.Color = Color.Evaluate(1 - node.Item.TimeToLive / TimeToLive);
                    // if (node.Item.Outline.Item != null)
                    // {
                    //     node.Item.Outline.Item.Color = OutlineColor.Evaluate(1 - node.Item.TimeToLive / TimeToLive);
                    // }

                    //충돌 감지
                    int result = -1;
                    if (CollisionDetection == CollisionDetectionType.Raycast)
                    {
                        result = Physics2D.Raycast(node.Item.Position, deltaPosition, ContactFilter, RaycastHitBuffer, distance);
                    }
                    else if (CollisionDetection == CollisionDetectionType.CircleCast)
                    {
                        result = Physics2D.CircleCast(node.Item.Position, radius, deltaPosition, ContactFilter, RaycastHitBuffer, distance);
                    }

                    //충돌 처리
                    if (result > 0)
                    {
                        // 총알의 충돌 이벤트 발생시 수행할 동작
                        //ProjectileCollideEvent(ref node);
                        ReturnNode(node);
                    }
                    else
                    {
                        //총알 위치 업데이트
                        node.Item.Position += deltaPosition;
                        if (node.Item.Outline.Item != null)
                        {
                            node.Item.Outline.Item.Position = node.Item.Position;
                        }                   
                    }
                }
                else
                {
                    // End of life - return to pool
                    ReturnNode(node);
                }             
            }
        }
        //protected abstract void ProjectileCollideEvent(ref Pool<ProjectileData>.Node node);
    }
}
