using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//using BGE;

public class Boid : MonoBehaviour {
	
	[Header("Seek")]
	public Vector3 seekTarget;
	public bool seekEnabled;
	
	[Header("Arrive")]    
	public Vector3 arriveTarget;
	
	public Vector3 velocity;
	public Vector3 acceleration;
	public Vector3 force;
	public float mass;
	public float maxSpeed;
	public float radius = 1;
	
	public GameObject pursueTarget;
	
	public Path path;
	
	public bool pursueEnabled;
	public bool arriveEnabled;
	
	public bool offsetPursueEnabled;
	public GameObject offsetPursueTarget;
	public Vector3 offset;
	
	[Header("Path Following")]
	public bool pathFollowingEnabled;
	public bool Looped;


	[Header("Obstacle Avoidance")]                
	public bool obstacleAvoidanceEnabled;
	public float minBoxLength;
	//public float obstacleAvoidanceWeight;

	[Header("Grouped")]
	public bool groupMember;
	public bool groupLeader;


	[HideInInspector]  
	public Vector3 memberPosition;

	[HideInInspector]  
	public Vector3 currentPosition;

	[HideInInspector]  
	public int memberNumber;

	private Color color = Color.red;

	public Boid()
	{
		mass = 1;
		velocity = Vector3.zero;
		force = Vector3.zero;
		acceleration = Vector3.zero;
		maxSpeed = 10.0f;
		
		path = new Path();
		Looped = false;
		
	}
	
	// Use this for initialization
	void Start () {
		if (offsetPursueEnabled)
		{
			if (offsetPursueTarget != null)
			{
				offset = offsetPursueTarget.transform.position - transform.position;
			}
		}
		path.Looped = Looped;





	}
	
	Vector3 FollowPath()
	{
		Vector3 next = path.NextWaypoint();
		float dist = (transform.position - next).magnitude;
		float waypointDistance = 5;
		if (dist < waypointDistance)
		{
			next = path.Advance();
		}
		if (! path.Looped && path.IsLast())
		{
			return Arrive(next);
		}
		else
		{
			return Seek(next);
		}
	}
	
	Vector3 OffsetPursue(GameObject offsetPursueTarget)
	{
		Vector3 targetPos = offsetPursueTarget.transform.TransformPoint(offset);
		
		Vector3 toTarget = targetPos - transform.position;
		float distance = toTarget.magnitude;
		float time = distance / maxSpeed;
		Vector3 target = targetPos
			+ offsetPursueTarget.GetComponent<Boid>().velocity * time;
		
		//LineDrawer.DrawTarget(target, Color.gray);
		
		return Arrive(target);
	}
	

	//clean
	Vector3 Seek(Vector3 targetPos)
	{
		Vector3 desiredVelocity;
		
		desiredVelocity = targetPos - transform.position;
		desiredVelocity.Normalize();
		desiredVelocity *= maxSpeed;
		return (desiredVelocity - velocity);
	}





	Vector3 Arrive(Vector3 arriveTarget)
	{
		Vector3 toTarget = arriveTarget - transform.position;
		
		float distance = toTarget.magnitude;
		
		float slowingDistance = 10;
		

		float ramped = (distance / slowingDistance) * maxSpeed;
		float clamped = Mathf.Min(ramped, maxSpeed);
		Vector3 desired = (toTarget / distance) * clamped;
		return desired - velocity;
	}






	Vector3 pursue(GameObject pursueTarget)
	{
		Vector3 toTarget = pursueTarget.transform.position - transform.position;
		float distance = toTarget.magnitude;
		
		float time = distance / maxSpeed;
		Vector3 target = pursueTarget.transform.position + pursueTarget.GetComponent<Boid>().velocity * time;
		Debug.DrawLine(target, target + Vector3.forward);
		return Seek(target);
	}


	Vector3 ObstacleAvoidance()
        {
            Vector3 LocalForce = Vector3.zero;
            //makeFeelers();
            List<Obstacle> tagged = new List<Obstacle>();
            float boxLength = minBoxLength + ((velocity.magnitude / maxSpeed) * minBoxLength * 2.0f);

            if (float.IsNaN(boxLength))
            {
                System.Console.WriteLine("NAN");
            }


            Obstacle[] obstacles = BoidManager.Instance.obstacles;
            // Matt Bucklands Obstacle avoidance
            // First tag obstacles in range
            if (obstacles.Length == 0)
            {
                return Vector3.zero;
            }
            foreach (Obstacle obstacle in obstacles)
            {
                //
                if (obstacle == null || obstacle.gameObject == gameObject)
                {
                    continue;
                }

                Vector3 toCentre = transform.position - obstacle.transform.position;
                float dist = toCentre.magnitude;
                if (dist < boxLength)
                {
                    tagged.Add(obstacle);
                }
            }

            float distToClosestIP = float.MaxValue;
            Obstacle closestIntersectingObstacle = null;
            Vector3 localPosOfClosestObstacle = Vector3.zero;
            Vector3 intersection = Vector3.zero;

            foreach (Obstacle o in tagged)
            {
                Vector3 localPos = transform.InverseTransformPoint(o.transform.position);

                // If the local position has a positive Z value then it must lay
                // behind the agent. (in which case it can be ignored)
                if (localPos.z >= 0)
                {
                    // If the distance from the x axis to the object's position is less
                    // than its radius + half the width of the detection box then there
                    // is a potential intersection.

                    //float obstacleRadius = o.transform.localScale.x / 2;
                    float obstacleRadius = o.radius;
                    float expandedRadius = radius + obstacleRadius;
                    if ((Math.Abs(localPos.y) < expandedRadius) && (Math.Abs(localPos.x) < expandedRadius))
                    {
                        // Now to do a ray/sphere intersection test. The center of the				
                        // Create a temp Entity to hold the sphere in local space
                        Sphere tempSphere = new Sphere(expandedRadius, localPos);

                        // Create a ray
                        //BGE.Geom.Ray ray = new BGE.Geom.Ray();
                        //ray.pos = new Vector3(0, 0, 0);
                        //ray.look = Vector3.forward;
						Vector3 RayPos	 = new Vector3(0, 0, 0);
						Vector3 RayLook = Vector3.forward;

                        // Find the point of intersection
                        if (tempSphere.closestRayIntersects(RayPos, RayLook, Vector3.zero, ref intersection) == false)
                        {
                            continue;
                        }

                        // Now see if its the closest, there may be other intersecting spheres
                        float dist = intersection.magnitude;
                        if (dist < distToClosestIP)
                        {
                            dist = distToClosestIP;
                            closestIntersectingObstacle = o;
                            localPosOfClosestObstacle = localPos;
                        }
                    }
                }
            }

            if (closestIntersectingObstacle != null)
            {
                // Now calculate the force
                float multiplier = 1.0f + (boxLength - localPosOfClosestObstacle.z) / boxLength;

                //calculate the lateral force
                float obstacleRadius = closestIntersectingObstacle.radius; // closestIntersectingObstacle.GetComponent<Renderer>().bounds.extents.magnitude;
                float expandedRadius = radius + obstacleRadius;
                LocalForce.x = (expandedRadius - Math.Abs(localPosOfClosestObstacle.x)) * multiplier;
				LocalForce.y = (expandedRadius - Math.Abs(localPosOfClosestObstacle.y)) * multiplier;

                // Generate positive or negative direction so we steer around!
                // Not always in the same direction as in Matt Bucklands book
                if (localPosOfClosestObstacle.x > 0)
                {
				LocalForce.x = -LocalForce.x;
                }

                // If the obstacle is above, steer down
                if (localPosOfClosestObstacle.y > 0)
                {
				LocalForce.y = -LocalForce.y;
                }

                
                //apply a braking force proportional to the obstacle's distance from
                //the vehicle.
                const float brakingWeight = 0.01f;
				LocalForce.z = (expandedRadius -
                                   localPosOfClosestObstacle.z) *
                                   brakingWeight;

                //finally, convert the steering vector from local to world space
                // Dont include position!                    
			LocalForce = transform.TransformDirection(LocalForce);
            }


		return LocalForce;
        }



	// Update is called once per frame
	void Update () {

	
		currentPosition = transform.position;


		if (obstacleAvoidanceEnabled) {
			force += ObstacleAvoidance();
		}


		if (pursueEnabled)
		{
			if (pursueTarget ==null)
			{
				pursueEnabled = false;
			}
			force += pursue(pursueTarget);
		}
		if (seekEnabled)
		{
			force += Seek(seekTarget);
		}        
		if (arriveEnabled)
		{
			force += Arrive(arriveTarget);
		}




		if (offsetPursueEnabled)
		{
			force += OffsetPursue(offsetPursueTarget);
		}
		if (pathFollowingEnabled)
		{
			path.Draw();
			force += FollowPath();
		}
		acceleration =  force / mass;
		velocity += acceleration * Time.deltaTime;
		Vector3.ClampMagnitude(velocity, maxSpeed);
		
		
		
		transform.position += velocity * Time.deltaTime;
		
		if (velocity.magnitude > float.Epsilon)
		{
			transform.forward = velocity.normalized;
			velocity *= 0.99f;
		}
		
		force = Vector3.zero;
	}



	void OnDrawGizmos()
	{
		Gizmos.color = color;
		Gizmos.DrawWireSphere(transform.position, radius);
	}


	public bool LeaderInGroup()
	{
		return groupLeader;
	}

	public bool GroupMember()
	{
		return groupMember;
	}

}